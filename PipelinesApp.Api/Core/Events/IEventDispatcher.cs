namespace PipelinesApp.Api.Core.Events
{
    using System;

    using Microsoft.Extensions.DependencyInjection;

    using PipelinesApp.Api.BackgroundTasks;

    public interface IEventDispatcher
    {
        void PublishEvent<TEvent>(TEvent domainEvent)
            where TEvent : IEvent;
    }

    internal class EventDispatcher : IEventDispatcher
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IBackgroundTaskQueue backgroundTaskQueue;

        public EventDispatcher(IServiceProvider serviceProvider, IBackgroundTaskQueue backgroundTaskQueue)
        {
            this.serviceProvider = serviceProvider;
            this.backgroundTaskQueue = backgroundTaskQueue;
        }

        public void PublishEvent<TEvent>(TEvent domainEvent)
            where TEvent : IEvent
        {
            if (domainEvent == null)
            {
                throw new ArgumentNullException(nameof(domainEvent));
            }

            var handlers = this.serviceProvider.GetServices(typeof(IEventHandler<TEvent>));

            foreach (var handler in handlers)
            {
                this.backgroundTaskQueue.QueueBackgroundWorkItem(async token =>
                    {
                        await ((IEventHandler<TEvent>)handler).Handle(domainEvent);
                    });
            }
        }
    }
}