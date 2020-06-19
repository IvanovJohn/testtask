namespace PipelinesApp.Api.Core.Commands
{
    using System.Security.Claims;

    public abstract class AuthenticatedCommandContext : ICommandContext
    {
        public ClaimsPrincipal CurrentUser { get; set; }
    }
}