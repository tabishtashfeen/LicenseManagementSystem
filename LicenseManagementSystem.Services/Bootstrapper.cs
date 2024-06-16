using LicenseManagementSystem.Services.Authentication;
using LicenseManagementSystem.Services.License;
using LicenseManagementSystem.Services.Product;
using LicenseManagementSystem.Services.Users;

namespace LicenseManagementSystem.Services
{
    public static class Bootstrapper
    {
        public static void Initialize(IServiceCollection services)
        {
            LicenseManagementSystem.Repositories.Bootstrapper.Initialize(services);
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ILicenseService, LicenseService>();
            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<IUsersService, UsersService>();
        }
    }
}
