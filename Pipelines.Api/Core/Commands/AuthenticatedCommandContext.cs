namespace Pipelines.Api.Core.Commands
{
    public abstract class AuthenticatedCommandContext : ICommandContext
    {
        public string CurrentUserId { get; set; }
    }
}