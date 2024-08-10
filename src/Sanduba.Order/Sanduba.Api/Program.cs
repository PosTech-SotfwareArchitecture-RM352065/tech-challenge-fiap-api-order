using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Sanduba.Core.Application;
using Sanduba.Adapter.Mvc;
using Sanduba.Infrastructure.Persistence.SqlServer.Configurations;
using Sanduba.Infrastructure.Persistence.Redis.Configurations;
using Sanduba.Infrastructure.API.Payment.Configurations;
using Sanduba.Infrastructure.Broker.ServiceBus.Configurations;



namespace Sanduba.API
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            builder.Services.AddAuthConfiguration(builder.Configuration);
            builder.Services.AddSqlServerInfrastructure(builder.Configuration);
            builder.Services.AddRedisInfrastructure(builder.Configuration);
            builder.Services.AddPaymentInfrastructure(builder.Configuration);
            builder.Services.AddServiceBusInfrastructure(builder.Configuration);
            builder.Services.AddApplication(builder.Configuration);
            builder.Services.AddMvcAdapter(builder.Configuration);

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddHealthChecks()
                .AddDatabaseHealthChecks(builder.Configuration)
                .AddBrokerHealthChecks(builder.Configuration);

            builder.Services.AddHealthChecksUI(options =>
            {
                options.SetEvaluationTimeInSeconds(15);
                options.MaximumHistoryEntriesPerEndpoint(60);
                options.SetApiMaxActiveRequests(1);

            }).AddInMemoryStorage();

            builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressMapClientErrors = true;
                });

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = $"Documentação Swagger da API Restaurante Sanduba - {environment}",
                        Version = "v1",
                        Contact = new OpenApiContact
                        {
                            Name = "Victor Cangelosi de Lima - RM352065",
                            Email = "mktcangel@gmail.com"
                        },
                    });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Adicione o JWT",
                    Name = "Authorization",
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{ }
                    }
                });

                options.EnableAnnotations();
            });

            builder.Host.UseSerilog(
                (context, configuration) =>
                {
                    configuration.ReadFrom.Configuration(context.Configuration);
                    configuration.Enrich.WithCorrelationId(headerName: "x-correlation-id", addValueIfHeaderAbsence: true);
                });

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseReDoc(doc =>
            {
                doc.DocumentTitle = "Documentação da API Restaurante Sanduba";
                doc.SpecUrl = "/swagger/v1/swagger.json";

            });

            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                Predicate = _ => true
            })
            .UseHealthChecks("/healthz", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            })
            .UseHealthChecksUI(options => options.UIPath = "/healthz-ui");

            app.UseSerilogRequestLogging();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        private static IHealthChecksBuilder AddDatabaseHealthChecks(this IHealthChecksBuilder builder, IConfiguration configuration)
        {
            foreach (var databaseConfig in configuration.GetSection("ConnectionStrings").GetChildren())
            {
                switch (databaseConfig.GetValue<string>("Type"))
                {
                    case "MSSQL":
                        builder.AddSqlServer(connectionString: databaseConfig.GetValue<string>("Value"), name: databaseConfig.Key);
                        break;
                    case "REDIS":
                        builder.AddRedis(redisConnectionString: databaseConfig.GetValue<string>("Value"), name: databaseConfig.Key);
                        break;
                }
            }

            return builder;
        }

        private static IHealthChecksBuilder AddBrokerHealthChecks(this IHealthChecksBuilder builder, IConfiguration configuration)
        {
            string connectionString = configuration.GetValue<string>("BrokerSettings:ConnectionString") ?? string.Empty;
            builder.AddAzureServiceBusQueue(connectionString, "fiap-tech-challenge-order-queue");

            return builder;
        }

        private static IServiceCollection AddAuthConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSecretKey = configuration.GetValue<string>("JwtSettings:SecretKey") ?? string.Empty;
            var jwtIssuer = configuration.GetValue<string>("JwtSettings:Issuer") ?? string.Empty;
            var jwtAudience = configuration.GetValue<string>("JwtSettings:Audience") ?? string.Empty;

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtIssuer,
                    ValidateAudience = true,
                    ValidAudience = jwtAudience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey)),
                });

            services.AddAuthorization();

            return services;
        }
    }
}