namespace PipelinesApp.Api
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;

    using PipelinesApp.Api.Auth;
    using PipelinesApp.Api.BackgroundTasks;
    using PipelinesApp.Api.Core.Commands;
    using PipelinesApp.Api.Core.Events;
    using PipelinesApp.Api.Core.Queries;
    using PipelinesApp.Api.ExceptionHandling;
    using PipelinesApp.Api.Notification;
    using PipelinesApp.Api.Pipelines.Commands;
    using PipelinesApp.Api.Pipelines.Events;
    using PipelinesApp.Api.Pipelines.Queries;
    using PipelinesApp.Api.Pipelines.ViewModels;
    using PipelinesApp.Api.Settings;
    using PipelinesApp.Api.Tasks;
    using PipelinesApp.Api.Tasks.Commands;
    using PipelinesApp.Api.Tasks.Events;
    using PipelinesApp.Api.Tasks.Queries;
    using PipelinesApp.Api.Tasks.ViewModels;
    using PipelinesApp.Api.Users;
    using PipelinesApp.Api.Users.Queries;
    using PipelinesApp.Api.Users.ViewModels;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
                {
                    options.AddPolicy(
                        "AllowAll",
                        builder =>
                            {
                                builder
                                    .AllowAnyHeader()
                                    .AllowAnyMethod()
                                    .SetIsOriginAllowed((host) => true)
                                    .AllowCredentials();
                            });
                });
            services.AddControllers(p => { p.Filters.Add(typeof(ExceptionFilter)); });

            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pipelines API", Version = "v1" });
                });

            services.AddSignalR();

            services.Configure<MongoDbSettings>(this.Configuration.GetSection(nameof(MongoDbSettings)));

            services.AddSingleton<IMongoDbSettings>(sp =>
                sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);

            services.AddScoped<IQueryDispatcher, QueryDispatcher>();
            services.AddScoped<ICommandDispatcher, CommandDispatcher>();
            services.AddScoped<IEventDispatcher, EventDispatcher>();

            services.AddAuthentication(x =>
                    {
                        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                .AddJwtBearer(x =>
                    {
                        x.RequireHttpsMetadata = false;
                        x.SaveToken = true;
                        x.TokenValidationParameters = new TokenValidationParameters
                                                          {
                                                              ValidateIssuerSigningKey = true,
                                                              IssuerSigningKey = new SymmetricSecurityKey(UserAuthenticationService.TokenSecret),
                                                              ValidateIssuer = false,
                                                              ValidateAudience = false,
                                                          };
                    });

            services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
            services.AddSingleton<IAuthorizationHandler, TaskAuthorizationHandler>();

            services.AddHostedService<QueuedHostedService>();
            services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
            services.AddScoped<INotificationService, NotificationService>();

            // TODO: register by convention
            services.AddScoped<IQuery<TasksCriterion, IEnumerable<TaskViewModel>>, TasksQuery>();
            services.AddScoped<IQuery<TaskByIdsCriterion, IEnumerable<TaskViewModel>>, TaskByIdsQuery>();
            services.AddScoped<IQuery<TaskByIdCriterion, TaskViewModel>, TaskByIdQuery>();
            services.AddScoped<ICommand<CreateTaskCommandContext>, CreateTaskCommand>();
            services.AddScoped<ICommand<DeleteTaskCommandContext>, DeleteTaskCommand>();
            services.AddScoped<ICommand<UpdateTaskAverageTimeCommandContext>, UpdateTaskAverageTimeCommand>();
            services.AddScoped<IEventHandler<TaskCompletedEvent>, RecalcAverageTimeTaskCompletedEventHandler>();

            services.AddScoped<IQuery<UsersCriterion, IEnumerable<UserViewModel>>, UsersQuery>();
            services.AddScoped<IQuery<UserByNameCriterion, UserDbEntity>, UserByNameQuery>();

            services.AddScoped<IQuery<PipelinesCriterion, IEnumerable<PipelineViewModel>>, PipelinesQuery>();
            services.AddScoped<IQuery<PipelineByIdCriterion, PipelineDetailsViewModel>, PipelineByIdQuery>();
            services.AddScoped<ICommand<CreatePipelineCommandContext>, CreatePipelineCommand>();
            services.AddScoped<ICommand<DeletePipelineCommandContext>, DeletePipelineCommand>();
            services.AddScoped<ICommand<RunPipelineCommandContext>, RunPipelineCommand>();
            services.AddScoped<IEventHandler<PipelineCompletedEvent>, PipelineCompletedNotificationEventHandler>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowAll");
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<NotificationsHub>("/api/notifications");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
        }
    }
}
