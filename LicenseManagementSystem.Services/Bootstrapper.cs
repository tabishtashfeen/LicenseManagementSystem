using LicenseManagementSystem.Services.Authentication;

namespace LicenseManagementSystem.Services
{
    public static class Bootstrapper
    {
        public static void Initialize(IServiceCollection services)
        {
            LicenseManagementSystem.Repositories.Bootstrapper.Initialize(services);
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}
