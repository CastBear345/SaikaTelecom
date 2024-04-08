namespace SaikaTelecom.Application.DependencyInjection;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ContactMapping));
        services.AddAutoMapper(typeof(SaleMapping));
        services.AddAutoMapper(typeof(LeadMapping));

        InitService(services);
    }

    private static void InitService(this IServiceCollection services)
    {
        services.AddScoped<ContactService>();
        services.AddScoped<SaleService>();
        services.AddScoped<LeadService>();
    }
}
