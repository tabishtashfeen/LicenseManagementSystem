using LicenseManagementSystem.Helper.Validators;
using LicenseManagementSystem.Models.Product;
using LicenseManagementSystem.Services.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LicenseManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;
        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }
        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            BaseResponse response = new();
            try
            {
                response.Result = await _productsService.GetAllProducts();
                return Ok(response);
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Failed to get all products!";
                return BadRequest(response);
            }
        }
        [HttpGet]
        [Route("GetProductById")]
        public async Task<IActionResult> GetProductById(long id)
        {
            BaseResponse response = new();
            try
            {
                if (id != null && id != 0)
                {
                    response.Result = await _productsService.GetProductById(id);
                    return Ok(response);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Id cannot be null/empty or 0!";
                    return BadRequest(response);
                }
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Failed to get product by id!";
                return BadRequest(response);
            }
        }
        [HttpPost]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct(CreateProductRequestModel product)
        {
            BaseResponse response = new();
            try
            {
                var validator = new CreateProductValidator();
                var validatorRes = validator.Validate(product);
                if (validatorRes.IsValid)
                {
                    response.Result = await _productsService.CreateProduct(product);
                    response.Message = "Product Successfully Created!";
                    return Ok(response);
                }
                else
                {
                    response.Success = false;
                    response.Errors = validatorRes.Errors;
                    response.Message = "Validation error!";
                    return BadRequest(response);
                }
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Failed to Create the Product!";
                return BadRequest(response);
            }
        }
        [HttpPost]
        [Route("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(UpdateProductRequestModel product)
        {
            BaseResponse response = new();
            try
            {
                var validator = new UpdateProductValidator();
                var validatorRes = validator.Validate(product);
                if (validatorRes.IsValid)
                {
                    response.Result = await _productsService.UpdateProduct(product);
                    response.Message = "Product Successfully Updated!";
                    return Ok(response);
                }
                else
                {
                    response.Success = false;
                    response.Errors = validatorRes.Errors;
                    response.Message = "Validation error!";
                    return BadRequest(response);
                }
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Failed to Update the Product!";
                return BadRequest(response);
            }
        }
        [HttpDelete]
        [Route("CreateProduct")]
        public async Task<IActionResult> DeleteProductById(long id)
        {
            BaseResponse response = new();
            try
            {
                if (id != null && id != 0)
                {
                    response.Result = await _productsService.DeleteProductById(id);
                    response.Message = "Product Successfully Deleted!";
                    return Ok(response);
                }
                else
                {
                    response.Success = false;
                    response.Message = "Id cannot be null/empty or 0!";
                    return BadRequest(response);
                }
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Failed to Delete the Product!";
                return BadRequest(response);
            }
        }
    }
}
