using LicenseManagementSystem.Helper.Validators;
using LicenseManagementSystem.Models.Product;
using LicenseManagementSystem.Services.License;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LicenseManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class LicenseController : ControllerBase
    {
        private readonly ILicenseService _licenseService;
        public LicenseController(ILicenseService licenseService)
        {
            _licenseService = licenseService;
        }
        [HttpPost]
        [Route("CreateLicenseKey")]
        public async Task<IActionResult> CreateLicenseKey(CreateLicenseRequestModel model)
        {
            BaseResponse response = new();
            try
            {
                var validator = new CreateLicenseRequestModelValidator();
                var validatorRes = validator.Validate(model);
                if (validatorRes.IsValid)
                {
                    response.Result = await _licenseService.CreateLicenseKey(model);
                    response.Message = "License Successfully Created!";
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
            catch (Exception ex)
            {

                response.Success = false;
                response.Message = "Failed to Create the License key!";
                return BadRequest(response);
            }
        }
    }
}
