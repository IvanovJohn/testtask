namespace Pipelines.Api
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;

    using Pipelines.Api.Auth;
    using Pipelines.Api.Core.Commands;
    using Pipelines.Api.Core.Queries;
    using Pipelines.Api.ExceptionHandling;
    using Pipelines.Api.Settings;
    using Pipelines.Api.Tasks.Commands;
    using Pipelines.Api.Tasks.Queries;
    using Pipelines.Api.Tasks.ViewModels;
    using Pipelines.Api.Users;
    using Pipelines.Api.Users.Queries;
    using Pipelines.Api.Users.ViewModels;

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
                                    .AllowAnyOrigin()
                                    .AllowAnyMethod()
                                    .AllowAnyHeader();
                            });
                });
            services.AddControllers(p => { p.Filters.Add(typeof(ExceptionFilter)); });

            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pipelines API", Version = "v1" });
                });

            services.Configure<MongoDbSettings>(this.Configuration.GetSection(nameof(MongoDbSettings)));

            services.AddSingleton<IMongoDbSettings>(sp =>
                sp.GetRequiredService<IOptions<MongoDbSettings>>().Value);

            services.AddScoped<IQueryDispatcher, QueryDispatcher>();
            services.AddScoped<ICommandDispatcher, CommandDispatcher>();

            // TODO: register by convention
            services.AddScoped<IQuery<TasksCriterion, IEnumerable<TaskViewModel>>, TasksQuery>();
            services.AddScoped<ICommand<CreateTaskCommandContext>, CreateTaskCommand>();
            services.AddScoped<ICommand<DeleteTaskCommandContext>, DeleteTaskCommand>();

            services.AddScoped<IQuery<UsersCriterion, IEnumerable<UserViewModel>>, UsersQuery>();
            services.AddScoped<IQuery<UserByNameCriterion, UserDbEntity>, UserByNameQuery>();

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
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHsts();

            app.UseCors("AllowAll");
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
        }
    }
}
