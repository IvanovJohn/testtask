namespace PipelinesApp.Api.Tasks
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Authorization.Infrastructure;

    using PipelinesApp.Api.Auth;
    using PipelinesApp.Api.Tasks.ViewModels;

    internal class TaskAuthorizationHandler : AuthorizationHandler<NameAuthorizationRequirement, TaskViewModel>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            NameAuthorizationRequirement requirement,
            TaskViewModel resource)
        {
            if (context.User == null || !context.User.Identity.IsAuthenticated)
            {
                return Task.CompletedTask;
            }

            if (requirement.RequiredName == TaskActions.Create)
            {
                context.Succeed(requirement);
            }
            else if (context.User.GetUserIdFromClaims() == resource.CreatorUserId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}