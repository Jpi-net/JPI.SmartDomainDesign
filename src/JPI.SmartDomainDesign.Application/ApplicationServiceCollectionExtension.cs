using Microsoft.Extensions.DependencyInjection;

namespace JPI.SmartDomainDesign.Application;

public static class ApplicationServiceCollectionExtension
{
    public static IServiceCollection ConfigureApplication(this IServiceCollection services)
    {
        return services;
    }
}
