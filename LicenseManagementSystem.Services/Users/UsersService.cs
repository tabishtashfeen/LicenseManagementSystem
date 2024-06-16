using LicenseManagementSystem.Models.User;
using LicenseManagementSystem.Repositories.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManagementSystem.Services.Users
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;
        public UsersService(IUsersRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;

        }
        public async Task<List<UserResponseModel>> GetAllUsersService() => _mapper.Map<List<UserResponseModel>>(await _usersRepository.GetAllUsers());
        public async Task<UserResponseModel> GetUserByIdService(long id) => _mapper.Map<UserResponseModel>(await _usersRepository.GetUserById(id));
        
    }
}
