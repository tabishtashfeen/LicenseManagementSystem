namespace LicenseManagementSystem.Repositories.Authentication
{
    public interface IAuthRepository
    {
        Task<User> CheckUserExists(AuthRequestModel user);
        Task CreateNewUser(User user);
        Task<User> AuthenticateUser(AuthRequestModel user);
    }
}
