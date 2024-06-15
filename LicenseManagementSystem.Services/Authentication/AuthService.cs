using LicenseManagementSystem.Models.User;
using LicenseManagementSystem.Repositories.Authentication;
using LicenseManagementSystem.Repositories.UnitofWork;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LicenseManagementSystem.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepo;
        private readonly IUnitOfWork _unitOfWork;
        public AuthService(IAuthRepository authRepo, IUnitOfWork unitOfWork)
        {
            _authRepo = authRepo;
            _unitOfWork = unitOfWork;
        }
        public async Task<string> AuthenticateUserService(AuthRequestModel user)
        {
            var userData = await _authRepo.CheckUserExists(user);
            if (userData != null)
            {
                return await GenerateToken(userData);
            }
            return "user doesnt exists";
        }
        public async Task<bool> CreateNewUserService(CreateUserRequestModel user)
        {
            await _authRepo.CreateNewUser(new User()
            {
                Email = user.Email,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                CreatedDate = DateTime.Now
            });
            var updated = _unitOfWork.SaveWithCount();
            return updated == 1;
        }
        private async Task<string> GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("ascsfdfeadfcxfsdgasdfrerwerfesyfgsdfaerdsfvdasd12ddeqwasadxxsASDF");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim("Username", user.UserName ?? ""),
                        new Claim("Email", user.Email),
                        new Claim("Id", user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenGenerated = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(tokenGenerated);
        }
    }
}
