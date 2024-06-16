using Azure;
using LicenseManagementSystem.Services.Authentication;
using Microsoft.AspNetCore.Http;

namespace LicenseManagementSystem.Tests.Tests
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
            var authRequest = new AuthRequestModel { Email = "demo@email.co", Password = "password" };
            var tokenResponse = new TokenResponse() { Token = "Token" };
            _mockAuthService.Setup(service => service.AuthenticateUserService(authRequest))
                .ReturnsAsync(tokenResponse);

            var result = await _controller.AuthenticateUser(authRequest);

            Assert.IsInstanceOf<OkObjectResult>(result);
            var okRequestResult = result as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, okRequestResult.StatusCode);
            var baseResponse = okRequestResult.Value as BaseResponse;
            Assert.IsInstanceOf<BaseResponse>(baseResponse);
            Assert.IsTrue(baseResponse.Success);
            Assert.AreEqual(tokenResponse, baseResponse.Result);
        }

        [Test]
        public async Task AuthenticateUser_UserDoesNotExist()
        {
            var authRequest = new AuthRequestModel { Email = "demo@email.co", Password = "password" };
            var tokenResponse = new TokenResponse() { };
            var message = "User doesnt exists";
            _mockAuthService.Setup(service => service.AuthenticateUserService(authRequest))
                .ReturnsAsync(tokenResponse);

            var result = await _controller.AuthenticateUser(authRequest);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.IsInstanceOf<BaseResponse>(badRequestResult.Value);
            var baseResponse = badRequestResult.Value as BaseResponse;

            Assert.IsFalse(baseResponse.Success);
            Assert.AreEqual(message, baseResponse.Message);
        }

        [Test]
        public async Task CreateNewUserService_UserCreatedSuccessfully()
        {
            var createUserRequest = new CreateUserRequestModel { FirstName = "John", LastName = "Doe", UserName = "johnDoe", Email = "John@doe.co", Password = "00001111" };
            _mockAuthService.Setup(service => service.CreateNewUserService(createUserRequest))
                .ReturnsAsync(true);

            var result = await _controller.CreateNewUser(createUserRequest);

            Assert.IsInstanceOf<OkObjectResult>(result);
            var okRequestResult = result as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, okRequestResult.StatusCode);
            var baseResponse = okRequestResult.Value as BaseResponse;

            Assert.IsTrue(baseResponse.Success);
            Assert.AreEqual("User successfully created.", baseResponse.Result);
        }

        [Test]
        public async Task CreateNewUserService_UserCreationFailed()
        {
            var createUserRequest = new CreateUserRequestModel { FirstName = "John", LastName = "Doe", UserName = "johnDoe", Email = "John@doe.co", Password = "00001111" };
            _mockAuthService.Setup(service => service.CreateNewUserService(createUserRequest))
                .ReturnsAsync(false);

            var result = await _controller.CreateNewUser(createUserRequest);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.IsInstanceOf<BaseResponse>(badRequestResult.Value);
            var baseResponse = badRequestResult.Value as BaseResponse;

            Assert.IsFalse(baseResponse.Success);
            Assert.AreEqual("New user not Created", baseResponse.Message);
        }

        [Test]
        public async Task CreateNewAdminUserService_UserCreatedSuccessfully()
        {
            var createUserRequest = new CreateUserRequestModel { FirstName = "John", LastName = "Doe", UserName = "johnDoe", Email = "John@doe.co", Password = "00001111" };
            _mockAuthService.Setup(service => service.CreateNewAdminUserService(createUserRequest))
                .ReturnsAsync(true);

            var result = await _controller.CreateNewAdminUser(createUserRequest);

            Assert.IsInstanceOf<OkObjectResult>(result);
            var okRequestResult = result as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, okRequestResult.StatusCode);
            var baseResponse = okRequestResult.Value as BaseResponse;

            Assert.IsTrue(baseResponse.Success);
            Assert.AreEqual("Admin User successfully created.", baseResponse.Result);
        }

        [Test]
        public async Task CreateNewAdminUserService_UserCreationFailed()
        {
            var createUserRequest = new CreateUserRequestModel { FirstName = "John", LastName = "Doe", UserName = "johnDoe", Email = "John@doe.co", Password = "00001111" };
            _mockAuthService.Setup(service => service.CreateNewAdminUserService(createUserRequest))
                .ReturnsAsync(false);

            var result = await _controller.CreateNewAdminUser(createUserRequest);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.IsInstanceOf<BaseResponse>(badRequestResult.Value);
            var baseResponse = badRequestResult.Value as BaseResponse;

            Assert.IsFalse(baseResponse.Success);
            Assert.AreEqual("New admin user not Created", baseResponse.Message);
        }
    }
}
