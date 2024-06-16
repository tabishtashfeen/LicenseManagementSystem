using LicenseManagementSystem.Services.Users;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManagementSystem.Tests.Tests
{
    [TestFixture]
    public class UsersControllerTests
    {
        private Mock<IUsersService> _mockUsersService;
        private UsersController _controller;

        [SetUp]
        public void Setup()
        {
            _mockUsersService = new Mock<IUsersService>();
            _controller = new UsersController(_mockUsersService.Object);
        }

        [Test]
        public async Task GetAllUsers_ShouldReturnSuccess_WithUsers()
        {
            // Arrange
            var expectedUsers = new List<UserResponseModel> { new UserResponseModel { Id = 1, FirstName = "Test" } };
            _mockUsersService.Setup(s => s.GetAllUsersService()).ReturnsAsync(expectedUsers);

            // Act
            var response = await _controller.GetAllUsers();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(response);
            var okResult = response as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);

            var baseResponse = okResult.Value as BaseResponse;
            Assert.IsTrue(baseResponse.Success);
            Assert.AreEqual(expectedUsers, baseResponse.Result);
        }

        [Test]
        public async Task GetAllUsers_ShouldReturnBadRequest_WhenExceptionIsThrown()
        {
            // Arrange
            _mockUsersService.Setup(s => s.GetAllUsersService()).ThrowsAsync(new Exception("Service error"));

            // Act
            var response = await _controller.GetAllUsers();

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(response);
            var badRequestResult = response as BadRequestObjectResult;
            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);

            var baseResponse = badRequestResult.Value as BaseResponse;
            Assert.IsFalse(baseResponse.Success);
            Assert.AreEqual("Failed to get all users!", baseResponse.Message);
        }

        [Test]
        public async Task GetUserById_ShouldReturnSuccess_WithUser()
        {
            // Arrange
            var expectedUser = new UserResponseModel { Id = 1, FirstName = "Test" };
            _mockUsersService.Setup(s => s.GetUserByIdService(It.IsAny<long>())).ReturnsAsync(expectedUser);

            // Act
            var response = await _controller.GetUserById(1);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(response);
            var okResult = response as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);

            var baseResponse = okResult.Value as BaseResponse;
            Assert.IsTrue(baseResponse.Success);
            Assert.AreEqual(expectedUser, baseResponse.Result);
        }

        [Test]
        public async Task GetUserById_ShouldReturnBadRequest_WhenExceptionIsThrown()
        {
            // Arrange
            _mockUsersService.Setup(s => s.GetUserByIdService(It.IsAny<long>())).ThrowsAsync(new Exception("Service error"));

            // Act
            var response = await _controller.GetUserById(1);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(response);
            var badRequestResult = response as BadRequestObjectResult;
            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);

            var baseResponse = badRequestResult.Value as BaseResponse;
            Assert.IsFalse(baseResponse.Success);
            Assert.AreEqual("Failed to get user!", baseResponse.Message);
        }
    }
}
