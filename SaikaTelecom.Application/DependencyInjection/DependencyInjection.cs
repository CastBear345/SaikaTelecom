using Microsoft.Extensions.DependencyInjection;
using SaikaTelecom.Application.Mapping;
using SaikaTelecom.Application.Services;

namespace SaikaTelecom.Application.DependencyInjection;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(SaleMapping));

        InitService(services);
    }

    private static void InitService(this IServiceCollection services)
    {
        services.AddScoped<SaleService>();
    }
}
