using LicenseManagementSystem.Services.Authentication;

namespace LicenseMS.Tests
{
    public class AuthControllerTests
    {
        private Mock<IAuthService> _mockAuthService;
        private AuthController _controller;

        [SetUp]
        public void Setup()
        {
            _mockAuthService = new Mock<IAuthService>();
            _controller = new AuthController(_mockAuthService.Object);
        }

        [Test]
        public async Task AuthenticateUser_ReturnsTokenResponse()
        {
            var authRequest = new AuthRequestModel { };
            var tokenResponse = "token";
            _mockAuthService.Setup(service => service.AuthenticateUserService(authRequest))
                .ReturnsAsync(tokenResponse);

            var result = await _controller.AuthenticateUser(authRequest);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(tokenResponse, result.Result);
        }

        [Test]
        public async Task AuthenticateUser_UserDoesNotExist()
        {
            var authRequest = new AuthRequestModel {  };
            var tokenResponse = "user doesnt exists";
            _mockAuthService.Setup(service => service.AuthenticateUserService(authRequest))
                .ReturnsAsync(tokenResponse);

            var result = await _controller.AuthenticateUser(authRequest);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(tokenResponse, result.Message);
        }

        [Test]
        public async Task CreateNewUserService_UserCreatedSuccessfully()
        {
            var createUserRequest = new CreateUserRequestModel {  };
            _mockAuthService.Setup(service => service.CreateNewUserService(createUserRequest))
                .ReturnsAsync(true);

            var result = await _controller.CreateNewUserService(createUserRequest);

            Assert.IsTrue(result.Success);
            Assert.AreEqual("User successfully created.", result.Result);
        }

        [Test]
        public async Task CreateNewUserService_UserCreationFailed()
        {
            var createUserRequest = new CreateUserRequestModel {  };
            _mockAuthService.Setup(service => service.CreateNewUserService(createUserRequest))
                .ReturnsAsync(false);

            var result = await _controller.CreateNewUserService(createUserRequest);

            Assert.IsFalse(result.Success);
            Assert.AreEqual("New user not Created", result.Message);
        }
    }
}
