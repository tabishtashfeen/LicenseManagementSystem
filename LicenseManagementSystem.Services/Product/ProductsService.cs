using LicenseManagementSystem.Repositories.Products;
using LicenseManagementSystem.Repositories.UnitofWork;

namespace LicenseManagementSystem.Services.Product
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productsRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ProductsService(IProductsRepository productsRepo, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _productsRepo = productsRepo;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<List<ProductResponseModel>> GetAllProducts() => _mapper.Map<List<ProductResponseModel>>(await _productsRepo.GetAllProducts());
        public async Task<ProductResponseModel> GetProductById(long id) => _mapper.Map<ProductResponseModel>(await _productsRepo.GetProductById(id));
        public async Task<string> CreateProduct(CreateProductRequestModel product)
        {
            var newProduct = _mapper.Map<LicenseManagementSystem.Models.Product.Product>(product);
            newProduct.CreatedDate = DateTime.Now;
            await _productsRepo.CreateProduct(newProduct);
            if (_unitOfWork.SaveWithCount() > 0)
            {
                return "Product Successfully Created!";
            }
            return "Failed to Create the Product!";
        }
        public async Task<string> UpdateProduct(UpdateProductRequestModel product)
        {
            var u2Product = await _productsRepo.GetProductById(product.Id);
            u2Product.Version = product.Version;
            u2Product.ModifiedDate = DateTime.Now;
            u2Product.Name = product.Name;
            u2Product.Description = product.Description;
            if (_unitOfWork.SaveWithCount() > 0)
            {
                return "Updated Successfully!";
            }
            return "Failed to Update!";
        }
        public async Task<string> DeleteProductById(long id)
        {
            await _productsRepo.DeleteProductById(id);
            if (_unitOfWork.SaveWithCount() > 0)
            {
                return "Deleted Successfully!";
            }
            return "Failed to Delete!";
        }
    }
}
