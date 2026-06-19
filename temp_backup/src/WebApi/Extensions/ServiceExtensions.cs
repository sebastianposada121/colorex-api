namespace ColorexApp.WebApi.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddWebApi(this IServiceCollection services)
    {
        services.AddControllers();

        return services;
    }
}
