using LicenseManagementSystem.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManagementSystem.Services.Users
{
    public interface IUsersService
    {
        Task<List<UserResponseModel>> GetAllUsersService();
        Task<UserResponseModel> GetUserByIdService(long id);
    }
}
