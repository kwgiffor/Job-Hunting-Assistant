using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using JobHuntingAssistant.Controllers;
using JobHuntingAssistant.Services;
using JobHuntingAssistant.Models;
using System;
using Microsoft.Extensions.Logging;

public class UserControllerTests
{
    [Fact]
    public void AddUserInfo_ReturnsBadRequest_WhenUserIsNull()
    {
        // Arrange
        var mockUserService = new Mock<IUserService>();
        var mockLogger = new Mock<ILogger<UserController>>();
        var controller = new UserController(mockUserService.Object, mockLogger.Object);

        // Act
        var result = controller.AddUserInfo(null);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public void AddUserInfo_ReturnsRedirectToActionResult_WhenUserIsValid()
    {
        // Arrange
        var mockUserService = new Mock<IUserService>();
        var mockLogger = new Mock<ILogger<UserController>>();
        var controller = new UserController(mockUserService.Object, mockLogger.Object);
        var user = new User();

        // Act
        var result = controller.AddUserInfo(user);

        // Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("ResumeGeneration", redirectToActionResult.ActionName);
    }

    [Fact]
    public void ViewUserInfo_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        var mockUserService = new Mock<IUserService>();
        mockUserService.Setup(service => service.GetUser(It.IsAny<int>())).Returns((User)null);
        var mockLogger = new Mock<ILogger<UserController>>();
        var controller = new UserController(mockUserService.Object, mockLogger.Object);

        // Act
        var result = controller.ViewUserInfo(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void ViewUserInfo_ReturnsViewResult_WhenUserExists()
    {
        // Arrange
        var mockUserService = new Mock<IUserService>();
        var user = new User();
        mockUserService.Setup(service => service.GetUser(It.IsAny<int>())).Returns(user);
        var mockLogger = new Mock<ILogger<UserController>>();
        var controller = new UserController(mockUserService.Object, mockLogger.Object);

        // Act
        var result = controller.ViewUserInfo(1);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal(user, viewResult.Model);
    }

    [Fact]
    public void UpdateUserInfo_ReturnsBadRequest_WhenUserIsNull()
    {
        // Arrange
        var mockUserService = new Mock<IUserService>();
        var mockLogger = new Mock<ILogger<UserController>>();
        var controller = new UserController(mockUserService.Object, mockLogger.Object);

        // Act
        var result = controller.UpdateUserInfo(null);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public void UpdateUserInfo_ReturnsRedirectToActionResult_WhenUserIsValid()
    {
        // Arrange
        var mockUserService = new Mock<IUserService>();
        var mockLogger = new Mock<ILogger<UserController>>();
        var controller = new UserController(mockUserService.Object, mockLogger.Object);
        var user = new User();

        // Act
        var result = controller.UpdateUserInfo(user);

        // Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("ViewUserInfo", redirectToActionResult.ActionName);
    }
}
