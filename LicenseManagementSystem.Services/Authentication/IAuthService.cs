namespace LicenseManagementSystem.Services.Authentication
{
    public interface IAuthService
    {
        Task<string> AuthenticateUserService(AuthRequestModel user);
        Task<bool> CreateNewUserService(CreateUserRequestModel user);
    }
}
