namespace SaikaTelecom.Application.DependencyInjection;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<UserService>();
        services.AddTransient<PasswordHasher>();

        services.AddHttpContextAccessor();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddAuthentication().AddCookie("Cookies", authenticationOptions =>
        {
            authenticationOptions.ExpireTimeSpan = TimeSpan.FromMinutes(10);

            authenticationOptions.Events.OnRedirectToLogin = (redirectContext) =>
            {
                redirectContext.Response.StatusCode = 401;
                return Task.CompletedTask;
            };

            authenticationOptions.Events.OnRedirectToAccessDenied = (redirectContext) =>
            {
                redirectContext.Response.StatusCode = 403;
                return Task.CompletedTask;
            };
        });
    }
}
