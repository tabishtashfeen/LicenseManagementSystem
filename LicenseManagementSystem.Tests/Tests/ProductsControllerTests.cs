using Azure;
using LicenseManagementSystem.Common.ResponseModels;
using LicenseManagementSystem.Helper.Validators;
using LicenseManagementSystem.Services.Product;
using Microsoft.AspNetCore.Http;
using Moq;

namespace LicenseManagementSystem.Tests.Tests
{
    public class ProductsControllerTests
    {
        private readonly Mock<IProductsService> _mockProductsService;
        private readonly ProductsController _controller;
        private readonly UpdateProductValidator _updateProductValidator;
        private readonly CreateProductValidator _createProductValidator;

        public ProductsControllerTests()
        {
            _mockProductsService = new Mock<IProductsService>();
            _controller = new ProductsController(_mockProductsService.Object);
            _updateProductValidator = new UpdateProductValidator();
            _createProductValidator = new CreateProductValidator();
        }
        [Test]
        public async Task GetAllProducts_ShouldReturnSuccess_WithProducts()
        {
            var expectedProducts = new List<ProductResponseModel>() { new ProductResponseModel { Id = 1, Name = "Test Product" } };
            _mockProductsService.Setup(s => s.GetAllProducts()).ReturnsAsync(expectedProducts);

            var response = await _controller.GetAllProducts();

            Assert.IsInstanceOf<OkObjectResult>(response);
            var okRequestResult = response as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, okRequestResult.StatusCode);
            Assert.IsInstanceOf<BaseResponse>(okRequestResult.Value);
            var baseResponse = okRequestResult.Value as BaseResponse;

            Assert.That(baseResponse, Is.InstanceOf<BaseResponse>());
            var okResult = baseResponse as BaseResponse;
            Assert.That(okResult.Success, Is.EqualTo(true));
            Assert.That(okResult.Result, Is.EquivalentTo(expectedProducts));
        }
        [Test]
        public async Task GetAllProducts_ShouldReturnBadRequest_OnException()
        {
            _mockProductsService.Setup(s => s.GetAllProducts()).Throws(new Exception());

            var response = await _controller.GetAllProducts();

            Assert.IsInstanceOf<BadRequestObjectResult>(response);
            var badRequestResult = response as BadRequestObjectResult;
            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.IsInstanceOf<BaseResponse>(badRequestResult.Value);
            var baseResponse = badRequestResult.Value as BaseResponse;

            Assert.That(baseResponse, Is.InstanceOf<BaseResponse>());
            var badRequestRes = baseResponse as BaseResponse;
            Assert.That(badRequestRes.Success, Is.EqualTo(false));
            Assert.That(badRequestRes.Message, Is.EqualTo("Failed to get all products!"));
        }
        [Test]
        public async Task GetProductById_ShouldReturnSuccess_WithProduct()
        {
            long productId = 1;
            var expectedProduct = new ProductResponseModel { Id = productId, Name = "Test Product" };
            _mockProductsService.Setup(s => s.GetProductById(productId)).ReturnsAsync(expectedProduct);
            var response = await _controller.GetProductById(productId);

            Assert.IsInstanceOf<OkObjectResult>(response);
            var okRequestResult = response as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, okRequestResult.StatusCode);
            Assert.IsInstanceOf<BaseResponse>(okRequestResult.Value);
            var baseResponse = okRequestResult.Value as BaseResponse;

            Assert.That(baseResponse, Is.InstanceOf<BaseResponse>());
            var okResult = baseResponse as BaseResponse;
            Assert.That(baseResponse.Success, Is.EqualTo(true));
            Assert.That((ProductResponseModel)baseResponse.Result, Is.EqualTo(expectedProduct));
        }
        [Test]
        public async Task GetProductById_ShouldReturnBadRequest_ForNullOrEmptyId()
        {
            var response = await _controller.GetProductById(0);

            Assert.IsInstanceOf<BadRequestObjectResult>(response);
            var badRequestResult = response as BadRequestObjectResult;
            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.IsInstanceOf<BaseResponse>(badRequestResult.Value);
            var baseResponse = badRequestResult.Value as BaseResponse;

            Assert.AreEqual(false, baseResponse.Success);
            Assert.IsInstanceOf<BaseResponse>(baseResponse);
            Assert.IsFalse(baseResponse.Success);
            Assert.AreEqual("Id cannot be null/empty or 0!", baseResponse.Message);
        }
        [Test]
        public async Task GetProductById_ShouldReturnBadRequest_OnServiceException()
        {
            long productId = 1;
            _mockProductsService.Setup(s => s.GetProductById(productId)).Throws(new Exception());
            var response = await _controller.GetProductById(productId);

            Assert.IsInstanceOf<BadRequestObjectResult>(response);
            var badRequestResult = response as BadRequestObjectResult;
            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.IsInstanceOf<BaseResponse>(badRequestResult.Value);
            var baseResponse = badRequestResult.Value as BaseResponse;

            Assert.IsInstanceOf<BaseResponse>(baseResponse);
            var badRequestRes = baseResponse as BaseResponse;
            Assert.AreEqual(false, badRequestRes.Success);
            Assert.IsFalse(badRequestRes.Success);
            Assert.That(badRequestRes.Message, Is.EqualTo("Failed to get product by id!"));
        }
        [Test]
        public async Task CreateProduct_ShouldReturnSuccess_WithCreatedProduct()
        {
            var validProduct = new CreateProductRequestModel { Name = "Test Product", Description = "This is a test product", Version = "0.0.1" };
            _mockProductsService.Setup(s => s.CreateProduct(validProduct)).ReturnsAsync("Product Successfully Created!");
            var response = await _controller.CreateProduct(validProduct);

            Assert.IsInstanceOf<OkObjectResult>(response);
            var okRequestResult = response as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, okRequestResult.StatusCode);
            Assert.IsInstanceOf<BaseResponse>(okRequestResult.Value);
            var baseResponse = okRequestResult.Value as BaseResponse;

            Assert.IsInstanceOf<BaseResponse>(baseResponse);
            Assert.IsTrue(baseResponse.Success);
            Assert.AreEqual("Product Successfully Created!", baseResponse.Message);
        }
        [Test]
        public async Task CreateProduct_ShouldReturnBadRequest_OnValidationError()
        {
            var invalidProduct = new CreateProductRequestModel { Description = "This is a test product" };
            var response = await _controller.CreateProduct(invalidProduct);

            Assert.IsInstanceOf<BadRequestObjectResult>(response);
            var badRequestResult = response as BadRequestObjectResult;
            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.IsInstanceOf<BaseResponse>(badRequestResult.Value);
            var baseResponse = badRequestResult.Value as BaseResponse;

            Assert.IsInstanceOf<BaseResponse>(baseResponse);
            Assert.IsFalse(baseResponse.Success);
            Assert.AreEqual("Validation error!", baseResponse.Message);                                                                         // Assert.That(...); // Optional: Verify specific validation errors using FluentAssertions or similar
        }
        [Test]
        public async Task CreateProduct_ShouldReturnBadRequest_OnServiceException()
        {
            var validProduct = new CreateProductRequestModel { Name = "Test Product", Description = "This is a test product", Version = "0.0.1" };
            _mockProductsService.Setup(s => s.CreateProduct(validProduct)).Throws(new Exception());
            var response = await _controller.CreateProduct(validProduct);

            Assert.IsInstanceOf<BadRequestObjectResult>(response);
            var badRequestResult = response as BadRequestObjectResult;
            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.IsInstanceOf<BaseResponse>(badRequestResult.Value);
            var baseResponse = badRequestResult.Value as BaseResponse;

            Assert.IsInstanceOf<BaseResponse>(baseResponse);
            Assert.IsFalse(baseResponse.Success);
            Assert.AreEqual("Failed to Create the Product!", baseResponse.Message);
        }
        [Test]
        public async Task UpdateProduct_ShouldReturnSuccess_WithUpdatedProduct()
        {
            long productId = 1;
            var updateProduct = new UpdateProductRequestModel { Id = productId, Name = "Updated Product Name", Version = "0.0.1" };
            _mockProductsService.Setup(s => s.UpdateProduct(updateProduct)).ReturnsAsync("Updated Successfully!");
            var response = await _controller.UpdateProduct(updateProduct);

            Assert.IsInstanceOf<OkObjectResult>(response);
            var okRequestResult = response as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, okRequestResult.StatusCode);
            Assert.IsInstanceOf<BaseResponse>(okRequestResult.Value);
            var baseResponse = okRequestResult.Value as BaseResponse;

            Assert.IsInstanceOf<BaseResponse>(baseResponse);
            Assert.IsTrue(baseResponse.Success);
            Assert.AreEqual("Product Successfully Updated!", baseResponse.Message);
        }
        [Test]
        public async Task UpdateProduct_ShouldReturnBadRequest_OnValidationError()
        {
            var invalidProduct = new UpdateProductRequestModel { Description = "This is an update" };
            var response = await _controller.UpdateProduct(invalidProduct);

            Assert.IsInstanceOf<BadRequestObjectResult>(response);
            var okRequestResult = response as BadRequestObjectResult;
            Assert.AreEqual(StatusCodes.Status400BadRequest, okRequestResult.StatusCode);
            Assert.IsInstanceOf<BaseResponse>(okRequestResult.Value);
            var baseResponse = okRequestResult.Value as BaseResponse;

            Assert.IsInstanceOf<BaseResponse>(baseResponse);
            Assert.IsFalse(baseResponse.Success);
            Assert.AreEqual("Validation error!", baseResponse.Message);
        }
        [Test]
        public async Task UpdateProduct_ShouldReturnBadRequest_OnServiceException()
        {
            long productId = 1;
            var validProduct = new UpdateProductRequestModel { Id = productId, Name = "Updated Product Name" };
            _mockProductsService.Setup(s => s.UpdateProduct(validProduct)).Throws(new Exception());
            var baseResponse = await _controller.UpdateProduct(validProduct);

            Assert.IsInstanceOf<BadRequestObjectResult>(baseResponse);
            var okRequestResult = baseResponse as BadRequestObjectResult;
            Assert.AreEqual(StatusCodes.Status400BadRequest, okRequestResult.StatusCode);
            Assert.IsInstanceOf<BaseResponse>(okRequestResult.Value);
            var response = okRequestResult.Value as BaseResponse;

            Assert.IsInstanceOf<BaseResponse>(response);
            Assert.IsFalse(response.Success);
            Assert.AreEqual("Validation error!", response.Message);
        }
        [Test]
        public async Task DeleteProductById_ShouldReturnSuccess_OnSuccessfulDelete()
        {
            long productId = 1;
            _mockProductsService.Setup(s => s.DeleteProductById(productId)).ReturnsAsync("Deleted Successfully!");
            var response = await _controller.DeleteProductById(productId);

            Assert.IsInstanceOf<OkObjectResult>(response);
            var okRequestResult = response as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, okRequestResult.StatusCode);
            Assert.IsInstanceOf<BaseResponse>(okRequestResult.Value);
            var baseResponse = okRequestResult.Value as BaseResponse;

            Assert.IsInstanceOf<BaseResponse>(baseResponse);
            Assert.IsTrue(baseResponse.Success);
            Assert.AreEqual("Product Successfully Deleted!", baseResponse.Message);
        }
        [Test]
        public async Task DeleteProductById_ShouldReturnBadRequest_ForNullOrEmptyOrZeroId()
        {
            var response = await _controller.DeleteProductById(0);
            Assert.IsInstanceOf<BadRequestObjectResult>(response);
            var badRequestResult = response as BadRequestObjectResult;
            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.IsInstanceOf<BaseResponse>(badRequestResult.Value);
            var baseResponse = badRequestResult.Value as BaseResponse;
            Assert.IsFalse(baseResponse.Success);
            Assert.AreEqual("Id cannot be null/empty or 0!", baseResponse.Message);
        }
    }

}
