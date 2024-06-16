using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManagementSystem.Repositories.Users
{
    public interface IUsersRepository
    {
        Task<User> GetUserById(long id);
        Task<List<User>> GetAllUsers();
    }
}
