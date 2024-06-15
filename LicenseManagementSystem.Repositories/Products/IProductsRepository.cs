using LicenseManagementSystem.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManagementSystem.Repositories.Products
{
    public interface IProductsRepository
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(long id);
        Task CreateProduct(Product product);
        void UpdateProduct(Product product);
        Task DeleteProductById(long id);
    }
}
