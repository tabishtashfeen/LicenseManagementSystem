using LicenseManagementSystem.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManagementSystem.Repositories.Products
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly DatabaseContext _databaseContext;
        public ProductsRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public async Task<List<Product>> GetAllProducts() => await _databaseContext.Products.ToListAsync();
        public async Task<Product> GetProductById(long id) => await _databaseContext.Products.FirstOrDefaultAsync(x => x.Id == id);
        public async Task CreateProduct(Product product) => await _databaseContext.Products.AddAsync(product);
        public void UpdateProduct(Product product)
        {
            _databaseContext.Products.Update(product);
        }
        public async Task DeleteProductById(long id)
        {
            var product = await _databaseContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            product.IsDeleted = true;
        }
    }
}
