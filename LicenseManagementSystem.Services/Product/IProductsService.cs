namespace LicenseManagementSystem.Services.Product
{
    public interface IProductsService
    {
        Task<List<ProductResponseModel>> GetAllProducts();
        Task<ProductResponseModel> GetProductById(long id);
        Task<string> CreateProduct(CreateProductRequestModel product);
        Task<string> UpdateProduct(UpdateProductRequestModel product);
        Task<string> DeleteProductById(long id);
    }
}
