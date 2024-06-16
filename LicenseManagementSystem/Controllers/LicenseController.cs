using LicenseManagementSystem.Helper.Validators;
using LicenseManagementSystem.Models.Product;
using LicenseManagementSystem.Services.License;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LicenseManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LicenseController : ControllerBase
    {
        private readonly ILicenseService _licenseService;
        public LicenseController(ILicenseService licenseService)
        {
            _licenseService = licenseService;
        }
        [HttpPost]
        [Route("CreateLicenseKey")]
        [Authorize(Roles = "Admin")]
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
        [HttpGet]
        [Route("GetAllLicenses")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllLicenses()
        {
            BaseResponse response = new();
            try
            {
                response.Result = await _licenseService.GetAllLicensesService();
                return Ok(response);
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Failed to get all licenses!";
                return BadRequest(response);
            }
        }
        [HttpGet]
        [Route("GetUserLicensesById")]
        [Authorize]
        public async Task<IActionResult> GetUserLicensesById(long id)
        {
            BaseResponse response = new();
            try
            {
                if (id > 0)
                {
                    response.Result = await _licenseService.GetUserLicensesByIdService(id);
                    return Ok(response);
                }
                throw new Exception();
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Failed to get all licenses!";
                return BadRequest(response);
            }
        }
        [HttpGet]
        [Route("ActiveteLicense")]
        [Authorize]
        public async Task<IActionResult> ActiveteLicense(long userId, string key)
        {
            BaseResponse response = new();
            try
            {
                if (!string.IsNullOrEmpty(key) && userId > 0)
                {
                    response.Result = await _licenseService.ActiveteLicenseService(userId, key);
                    if (response.Result != null)
                    {
                        return Ok(response);
                    }
                }
                response.Success = false;
                response.Message = "Failed to get all licenses!";
                return BadRequest(response);
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Failed to get all licenses!";
                return BadRequest(response);
            }
        }
    }
}
