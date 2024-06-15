using LicenseManagementSystem.Common.ResponseModels;
using LicenseManagementSystem.Models.User;
using LicenseManagementSystem.Services.Authentication;
using LicenseManagementSystem.Services.License;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LicenseManagementSystem.Tests.Tests
{
    public class LicenseControllerTests
    {
        private Mock<ILicenseService> _mockLicenseService;
        private LicenseController _controller;
        [SetUp]
        public void Setup()
        {
            _mockLicenseService = new Mock<ILicenseService>();
            _controller = new LicenseController(_mockLicenseService.Object);
        }
        [Test]
        public async Task CreateLicenseKey_InvalidRequest_ReturnsBadRequest()
        {
            // Arrange
            var mockLicenseService = new Mock<ILicenseService>();
            var controller = new LicenseController(mockLicenseService.Object);
            var model = new CreateLicenseRequestModel { ProductId = 0, UserId = 0 };

            var response = await controller.CreateLicenseKey(model);

            Assert.IsInstanceOf<BadRequestObjectResult>(response);

            var badRequestResult = response as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        }

        [Test]
        public async Task CreateLicenseKey_Exception_ReturnsBadRequest()
        {
            // Arrange
            var mockLicenseService = new Mock<ILicenseService>();
            mockLicenseService.Setup(s => s.CreateLicenseKey(It.IsAny<CreateLicenseRequestModel>()))
                .Throws(new Exception("License creation failed"));

            var controller = new LicenseController(mockLicenseService.Object);
            var model = new CreateLicenseRequestModel { ProductId = 1, UserId = 2 };

            var response = await controller.CreateLicenseKey(model);

            Assert.IsInstanceOf<BadRequestObjectResult>(response);

            var badRequestResult = response as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        }

        [Test]
        public async Task CreateLicenseKey_ValidRequest_ReturnsOkWithLicenseKey()
        {
            // Arrange
            const string expectedLicenseKey = "ABC-123-DEF";
            var mockLicenseService = new Mock<ILicenseService>();
            mockLicenseService.Setup(s => s.CreateLicenseKey(It.IsAny<CreateLicenseRequestModel>()))
                .ReturnsAsync(expectedLicenseKey);

            var controller = new LicenseController(mockLicenseService.Object);
            var model = new CreateLicenseRequestModel { ProductId = 1, UserId = 2 }; // Set valid values

            // Act
            var response = await controller.CreateLicenseKey(model);

            Assert.IsInstanceOf<OkObjectResult>(response);

            var okResult = response as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);

            var baseResponse = okResult.Value as BaseResponse;
            Assert.NotNull(baseResponse);
            Assert.True(baseResponse.Success);
            Assert.AreEqual(expectedLicenseKey, baseResponse.Result);
            Assert.That(baseResponse.Message, Is.EqualTo("License Successfully Created!"));
        }

        [Test]
        public async Task CreateLicenseKey_ValidRequest_ReturnsOkWithEmptyMessage()
        {
            const string expectedLicenseKey = "ABC-123-DEF";
            var mockLicenseService = new Mock<ILicenseService>();
            mockLicenseService.Setup(s => s.CreateLicenseKey(It.IsAny<CreateLicenseRequestModel>()))
                .ReturnsAsync(expectedLicenseKey);

            var controller = new LicenseController(mockLicenseService.Object);
            var model = new CreateLicenseRequestModel { ProductId = 1, UserId = 2 };
            var response = await controller.CreateLicenseKey(model);
            Assert.IsInstanceOf<OkObjectResult>(response);

            var okResult = response as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);

            var baseResponse = okResult.Value as BaseResponse;
            Assert.NotNull(baseResponse);
            Assert.True(baseResponse.Success);
            Assert.AreEqual(expectedLicenseKey, baseResponse.Result);
            Assert.That(baseResponse.Message, Is.EqualTo("License Successfully Created!"));
        }

    }
}
