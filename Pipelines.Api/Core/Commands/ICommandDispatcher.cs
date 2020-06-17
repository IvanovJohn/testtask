namespace Pipelines.Api.Core.Commands
{
    using System;
    using System.Threading.Tasks;

    public interface ICommandDispatcher
    {
        Task Execute<TCommandContext>(TCommandContext commandContext) where TCommandContext : ICommandContext;
    }

    internal class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task Execute<TCommandContext>(TCommandContext commandContext)
            where TCommandContext : ICommandContext
        {
            var command = (ICommand<TCommandContext>)this.serviceProvider.GetService(typeof(ICommand<TCommandContext>));

            if (command == null)
            {
                throw new CommandNotFoundException(typeof(ICommand<TCommandContext>));
            }

            await command.Execute(commandContext);
        }
    }
}