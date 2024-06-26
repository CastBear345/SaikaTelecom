﻿namespace SaikaTelecom.DAL.DependencyInjection;

public static class DependencyInjection
{
    public static void AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options => {
            options.UseSqlServer(configuration.GetConnectionString("AppDbContext"));
        });
    }
}
