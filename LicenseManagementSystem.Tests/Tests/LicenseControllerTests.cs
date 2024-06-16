using FluentValidation;
using FluentValidation.Results;
using LicenseManagementSystem.Common.ResponseModels;
using LicenseManagementSystem.Models.User;
using LicenseManagementSystem.Services.Authentication;
using LicenseManagementSystem.Services.License;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
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
        public async Task GetUserLicensesById_Exception_ReturnsBadRequest()
        {

            long userId = 0;
            var mockLicenseService = new Mock<ILicenseService>();
            mockLicenseService.Setup(service => service.GetUserLicensesByIdService(userId)).ThrowsAsync(new Exception());

            var response = await _controller.GetUserLicensesById(userId);

            Assert.IsInstanceOf<BadRequestObjectResult>(response);
            var badRequestResult = response as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.IsInstanceOf<BaseResponse>(badRequestResult.Value);
            var baseResponse = badRequestResult.Value as BaseResponse;
            Assert.False(baseResponse.Success);
            Assert.NotNull(baseResponse.Message);
        }
        [Test]
        public async Task ActivateLicense_Exception_ReturnsBadRequest()
        {

            long userId = 1;
            string key = "valid_key";
            var mockLicenseService = new Mock<ILicenseService>();
            mockLicenseService.Setup(service => service.ActiveteLicenseService(userId, key)).ThrowsAsync(new Exception());

            var response = await _controller.ActiveteLicense(userId, key);

            Assert.IsInstanceOf<BadRequestObjectResult>(response);
            var badRequestResult = response as BadRequestObjectResult;
            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
            Assert.IsInstanceOf<BaseResponse>(badRequestResult.Value);
            var baseResponse = badRequestResult.Value as BaseResponse;
            Assert.False(baseResponse.Success);
            Assert.NotNull(baseResponse.Message);
        }

        [Test]
        public async Task ActivateLicense_InvalidUserId_ReturnsBadRequest()
        {
            long userId = 0;
            string key = "valid_key";
            var mockLicenseService = new Mock<ILicenseService>();
            var response = await _controller.ActiveteLicense(userId, key);
            Assert.IsInstanceOf<BadRequestObjectResult>(response);
            var badRequestResult = response as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);

        }

        [Test]
        public async Task ActivateLicense_InvalidKey_ReturnsBadRequest()
        {
            long userId = 1;
            string key = "";
            var mockLicenseService = new Mock<ILicenseService>();
            var response = await _controller.ActiveteLicense(userId, key);
            Assert.IsInstanceOf<BadRequestObjectResult>(response);
            var badRequestResult = response as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
        }
        [Test]
        public async Task CreateLicenseKey_ShouldReturnSuccess_WhenLicenseIsCreated()
        {
            var requestModel = new CreateLicenseRequestModel { ProductId = 2, UserId = 1 };
            var expectedResponse = "License Successfully Created!";

            _mockLicenseService.Setup(s => s.CreateLicenseKey(requestModel)).ReturnsAsync(expectedResponse);

            var response = await _controller.CreateLicenseKey(requestModel);
            Assert.IsInstanceOf<OkObjectResult>(response);
            var okResult = response as OkObjectResult;
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.IsInstanceOf<BaseResponse>(okResult.Value);
            var baseResponse = okResult.Value as BaseResponse;

            Assert.That(baseResponse.Success, Is.True);
            Assert.That(baseResponse.Message, Is.EqualTo("License Successfully Created!"));
            Assert.That(baseResponse.Result, Is.EqualTo(expectedResponse));
        }
        [Test]
        public async Task CreateLicenseKey_ShouldReturnBadRequest_WhenValidationFails()
        {
            // Arrange
            var createLicenseRequest = new CreateLicenseRequestModel
            {
            };

            var validationErrors = new List<ValidationFailure>
            {
                new ValidationFailure("ProductId", "Product Id is required.")
            };
            var validationResult = new ValidationResult(validationErrors);

            var validator = new Mock<IValidator<CreateLicenseRequestModel>>();
            validator.Setup(v => v.Validate(It.IsAny<CreateLicenseRequestModel>())).Returns(validationResult);

            // Act
            var response = await _controller.CreateLicenseKey(createLicenseRequest);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(response);
            var badRequestResult = response as BadRequestObjectResult;
            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);

            var baseResponse = badRequestResult.Value as BaseResponse;
            Assert.IsNotNull(baseResponse);
            Assert.IsFalse(baseResponse.Success);
            Assert.AreEqual("Validation error!", baseResponse.Message);
        }
        [Test]
        public async Task CreateLicenseKey_ShouldReturnBadRequest_WhenExceptionIsThrown()
        {
            var createLicenseRequest = new CreateLicenseRequestModel
            {
                ProductId = 1,
                UserId = 2
            };

            _mockLicenseService.Setup(s => s.CreateLicenseKey(It.IsAny<CreateLicenseRequestModel>()))
                .ThrowsAsync(new Exception("Service error"));

            var validator = new Mock<IValidator<CreateLicenseRequestModel>>();
            validator.Setup(v => v.Validate(It.IsAny<CreateLicenseRequestModel>())).Returns(new ValidationResult());

            var response = await _controller.CreateLicenseKey(createLicenseRequest);

            Assert.IsInstanceOf<BadRequestObjectResult>(response);
            var badRequestResult = response as BadRequestObjectResult;
            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);

            var baseResponse = badRequestResult.Value as BaseResponse;
            Assert.IsNotNull(baseResponse);
            Assert.IsFalse(baseResponse.Success);
            Assert.AreEqual("Failed to Create the License key!", baseResponse.Message);
        }

    }
}
