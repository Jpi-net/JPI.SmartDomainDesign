using JPI.SmartDomainDesign.Api.Helpers;
using JPI.SmartDomainDesign.Application;
using JPI.SmartDomainDesign.Infrastructure;

namespace JPI.SmartDomainDesign.Api;

internal static class ApiServiceCollectionExtension
{
    public static IServiceCollection ConfigureApi(this IServiceCollection services)
        => services
            .ConfigureSwagger()
            .ConfigureApplication()
            .ConfigureInfrastructure();

    private static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = $"{ApplicationConstants.AppName} API",
                Version = VersionHelper.GetVersion(),
                Description = "This project serves as a template for implementing Smart Domain Design principles in a scalable and maintainable API architecture.",
                Contact = new Microsoft.OpenApi.Models.OpenApiContact
                {
                    Name = "Julien PIERLOT",
                    Email = "julien.pierlot@icloud.com",
                },
            });
        });

        return services;
    }
}
