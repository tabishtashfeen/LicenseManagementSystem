using LicenseManagementSystem.Helper.Validators;

namespace LicenseManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("AuthenticateUser")]
        public async Task<IActionResult> AuthenticateUser(AuthRequestModel user)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var validator = new AuthRequestModelValidator();
                var validatorRes = validator.Validate(user);
                if (!validatorRes.IsValid)
                {
                    response.Success = false;
                    response.Message = "Validation error!";
                    response.Errors = validatorRes.Errors;
                    return BadRequest(response);
                }
                var tokenResponse = await _authService.AuthenticateUserService(user);
                response.Result = tokenResponse;
                if (tokenResponse != null && tokenResponse.Contains("user doesnt exists"))
                {
                    response.Success = false;
                    response.Message = tokenResponse;
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Failed to Authenticate User!";
                return BadRequest(response);
            }
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("CreateNewUser")]
        public async Task<IActionResult> CreateNewUser(CreateUserRequestModel user)
        {
            BaseResponse response = new BaseResponse();
            var validator = new CreateUserRequestModelValidator();
            var validatorRes = validator.Validate(user);
            if (!validatorRes.IsValid)
            {
                response.Success = false;
                response.Message = "Validation error!";
                response.Errors = validatorRes.Errors;
                return BadRequest(response);
            }
            var newUser = await _authService.CreateNewUserService(user);
            if (newUser == true)
            {
                response.Message = "User successfully created.";
                response.Result = "User successfully created.";
                return Ok(response);
            }
            else
            {
                response.Success = false;
                response.Message = "New user not Created";
                return BadRequest(response);
            }
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("CreateNewAdminUser")]
        public async Task<IActionResult> CreateNewAdminUser(CreateUserRequestModel user)
        {
            BaseResponse response = new BaseResponse();
            var validator = new CreateUserRequestModelValidator();
            var validatorRes = validator.Validate(user);
            if (!validatorRes.IsValid)
            {
                response.Success = false;
                response.Message = "Validation error!";
                response.Errors = validatorRes.Errors;
                return BadRequest(response);
            }
            var newUser = await _authService.CreateNewAdminUserService(user);
            if (newUser == true)
            {
                response.Message = "Admin User successfully created.";
                response.Result = "Admin User successfully created.";
                return Ok(response);
            }
            else
            {
                response.Success = false;
                response.Message = "New admin user not Created";
                return BadRequest(response);
            }
        }
    }

}
