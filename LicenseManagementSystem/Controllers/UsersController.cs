using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LicenseManagementSystem.Services.Users;

namespace LicenseManagementSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }
        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            BaseResponse response = new();
            try
            {
                response.Result = await _usersService.GetAllUsersService();
                return Ok(response);
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Failed to get all users!";
                return BadRequest(response);
            }
        }
        [HttpGet]
        [Route("GetUserById")]
        public async Task<IActionResult> GetUserById(long id)
        {
            BaseResponse response = new();
            try
            {
                response.Result = await _usersService.GetUserByIdService(id);
                return Ok(response);
            }
            catch (Exception)
            {
                response.Success = false;
                response.Message = "Failed to get user!";
                return BadRequest(response);
            }
        }
    }
}
