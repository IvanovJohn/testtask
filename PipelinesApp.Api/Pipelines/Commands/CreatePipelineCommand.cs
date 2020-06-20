namespace PipelinesApp.Api.Pipelines.Commands
{
    using System.Threading.Tasks;

    using PipelinesApp.Api.Auth;
    using PipelinesApp.Api.ExceptionHandling;
    using PipelinesApp.Api.Settings;

    internal class CreatePipelineCommand : AbstractPipelineCommand<CreatePipelineCommandContext>
    {
        public CreatePipelineCommand(IMongoDbSettings mongoDbSettings)
            : base(mongoDbSettings)
        {
        }

        public override async Task Execute(CreatePipelineCommandContext commandContext)
        {
            if (commandContext.CurrentUser == null || !commandContext.CurrentUser.Identity.IsAuthenticated)
            {
                throw ApiException.ErrorAuthorization("You should login before create Pipeline");
            }

            await this.PipelinesCollection.InsertOneAsync(
                new PipelineDbEntity
                    {
                        Name = commandContext.Form.Name,
                        CreatorUserId = commandContext.CurrentUser.GetUserIdFromClaims(),
                    });
        }
    }
}