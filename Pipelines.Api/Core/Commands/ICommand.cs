namespace Pipelines.Api.Core.Commands
{
    using System.Threading.Tasks;

    public interface ICommand<in TCommandContext> where TCommandContext : ICommandContext
    {
        Task Execute(TCommandContext commandContext);
    }
}