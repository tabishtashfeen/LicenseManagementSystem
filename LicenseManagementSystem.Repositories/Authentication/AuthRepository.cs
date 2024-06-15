namespace LicenseManagementSystem.Repositories.Authentication
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DatabaseContext _dbContext;
        public AuthRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<User> CheckUserExists(AuthRequestModel user) => await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
        public async Task CreateNewUser(User user) => await _dbContext.Users.AddAsync(user);
        public async Task<User> AuthenticateUser(AuthRequestModel user) => await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == user.Email && x.Password == user.Password && x.IsActive);
    }
}
