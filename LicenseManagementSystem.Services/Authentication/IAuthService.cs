namespace LicenseManagementSystem.Services.Authentication
{
    public interface IAuthService
    {
        Task<TokenResponse> AuthenticateUserService(AuthRequestModel user);
        Task<bool> CreateNewUserService(CreateUserRequestModel user);
        Task<bool> CreateNewAdminUserService(CreateUserRequestModel user);
    }
}
