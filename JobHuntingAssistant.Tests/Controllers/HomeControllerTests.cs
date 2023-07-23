using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using JobHuntingAssistant.Controllers;
using JobHuntingAssistant.Services;
using JobHuntingAssistant.Models;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

public class HomeControllerTests
{
    [Fact]
    public void Index_ReturnsViewResult_WithUserModel()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<HomeController>>();
        var mockJobListingService = new Mock<IJobListingService>();
        var controller = new HomeController(mockLogger.Object, mockJobListingService.Object);

        // Act
        var result = controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.IsType<User>(viewResult.Model);
    }

    [Fact]
    public void Privacy_ReturnsViewResult()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<HomeController>>();
        var mockJobListingService = new Mock<IJobListingService>();
        var controller = new HomeController(mockLogger.Object, mockJobListingService.Object);

        // Act
        var result = controller.Privacy();

        // Assert
        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void ResumeGeneration_ReturnsViewResult_WithResumeGenerationViewModel()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<HomeController>>();
        var mockJobListingService = new Mock<IJobListingService>();
        var controller = new HomeController(mockLogger.Object, mockJobListingService.Object);

        // Act
        var result = controller.ResumeGeneration();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.IsType<ResumeGenerationViewModel>(viewResult.Model);
    }

    [Fact]
    public void Error_ReturnsViewResult_WithErrorViewModel()
    {
        // Arrange
        var mockLogger = new Mock<ILogger<HomeController>>();
        var mockJobListingService = new Mock<IJobListingService>();
        var controller = new HomeController(mockLogger.Object, mockJobListingService.Object);

        // Act
        var result = controller.Error();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.IsType<ErrorViewModel>(viewResult.Model);
    }
}
