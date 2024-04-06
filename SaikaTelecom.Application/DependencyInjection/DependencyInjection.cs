namespace SaikaTelecom.Application.DependencyInjection;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(LeadMapping));

        InitService(services);
    }

    private static void InitService(this IServiceCollection services)
    {
        services.AddScoped<LeadService>();
    }
}
