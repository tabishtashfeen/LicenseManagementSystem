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
        public async Task<BaseResponse> AuthenticateUser(AuthRequestModel user)
        {
            BaseResponse response = new BaseResponse();
            try
            {
                var tokenResponse = await _authService.AuthenticateUserService(user);
                response.Result = tokenResponse;
                if (tokenResponse != null && tokenResponse.Contains("user doesnt exists"))
                {
                    response.Success = false;
                    response.Message = tokenResponse;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return response;
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("CreateNewUser")]
        public async Task<BaseResponse> CreateNewUserService(CreateUserRequestModel user)
        {
            BaseResponse response = new BaseResponse();
            var newUser = await _authService.CreateNewUserService(user);
            if (newUser == true)
            {
                response.Message = "User successfully created.";
                response.Result = "User successfully created.";
            }
            else
            {
                response.Success = false;
                response.Message = "New user not Created";
            }
            return response;
        }
    }

}
