using LicenseManagementSystem.Repositories.Authentication;
using LicenseManagementSystem.Repositories.Products;
using LicenseManagementSystem.Repositories.UnitofWork;

namespace LicenseManagementSystem.Repositories
{
    public static class Bootstrapper
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IProductsRepository, ProductsRepository>();
        }
    }
}
