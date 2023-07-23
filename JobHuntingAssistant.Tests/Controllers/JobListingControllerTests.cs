using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using JobHuntingAssistant.Controllers;
using JobHuntingAssistant.Services;
using JobHuntingAssistant.Models;
using System;
using Microsoft.Extensions.Logging;

public class JobListingControllerTests
{
    [Fact]
    public void AddResumeToJobListing_ReturnsBadRequest_WhenResumeIsNull()
    {
        // Arrange
        var mockJobListingService = new Mock<IJobListingService>();
        var mockLogger = new Mock<ILogger<JobListingController>>();
        var controller = new JobListingController(mockJobListingService.Object, mockLogger.Object);

        // Act
        var result = controller.AddResumeToJobListing(1, null);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public void AddResumeToJobListing_ReturnsNotFound_WhenExceptionIsThrown()
    {
        // Arrange
        var mockJobListingService = new Mock<IJobListingService>();
        mockJobListingService.Setup(service => service.AddResumeToJobListing(It.IsAny<int>(), It.IsAny<Resume>())).Throws(new Exception());
        var mockLogger = new Mock<ILogger<JobListingController>>();
        var controller = new JobListingController(mockJobListingService.Object, mockLogger.Object);
        var resume = new Resume(0,0,"");

        // Act
        var result = controller.AddResumeToJobListing(1, resume);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void AddResumeToJobListing_ReturnsRedirectToActionResult_WhenResumeIsValid()
    {
        // Arrange
        var mockJobListingService = new Mock<IJobListingService>();
        var mockLogger = new Mock<ILogger<JobListingController>>();
        var controller = new JobListingController(mockJobListingService.Object, mockLogger.Object);
        var resume = new Resume(0,0,"");

        // Act
        var result = controller.AddResumeToJobListing(1, resume);

        // Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Details", redirectToActionResult.ActionName);
    }

    [Fact]
    public void AddJobListing_ReturnsJsonResult_WithSuccessFalse_WhenModelStateIsInvalid()
    {
        // Arrange
        var mockJobListingService = new Mock<IJobListingService>();
        var mockLogger = new Mock<ILogger<JobListingController>>();
        var controller = new JobListingController(mockJobListingService.Object, mockLogger.Object);
        controller.ModelState.AddModelError("error", "some error");
        var jobListing = new JobListing("", "");

        // Act
        var result = controller.AddJobListing(jobListing);

        // Assert
        var jsonResult = Assert.IsType<JsonResult>(result);
        var value = Assert.IsType<Dictionary<string, object>>(jsonResult.Value);
        Assert.False((bool)value["success"]);
    }

    [Fact]
    public void AddJobListing_ReturnsJsonResult_WithSuccessTrue_WhenModelStateIsValid()
    {
        // Arrange
        var mockJobListingService = new Mock<IJobListingService>();
        var mockLogger = new Mock<ILogger<JobListingController>>();
        var controller = new JobListingController(mockJobListingService.Object, mockLogger.Object);
        var jobListing = new JobListing("", "");

        // Act
        var result = controller.AddJobListing(jobListing);

        // Assert
        var jsonResult = Assert.IsType<JsonResult>(result);
        var value = Assert.IsType<Dictionary<string, object>>(jsonResult.Value);
        Assert.True((bool)value["success"]);
    }

    [Fact]
    public void GetJobListing_ReturnsNotFound_WhenJobListingDoesNotExist()
    {
        // Arrange
        var mockJobListingService = new Mock<IJobListingService>();
        mockJobListingService.Setup(service => service.GetJobListingById(It.IsAny<int>())).Returns((JobListing)null);
        var mockLogger = new Mock<ILogger<JobListingController>>();
        var controller = new JobListingController(mockJobListingService.Object, mockLogger.Object);

        // Act
        var result = controller.GetJobListing(1);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void GetJobListing_ReturnsPartialViewResult_WhenJobListingExists()
    {
        // Arrange
        var mockJobListingService = new Mock<IJobListingService>();
        var jobListing = new JobListing("", "");
        mockJobListingService.Setup(service => service.GetJobListingById(It.IsAny<int>())).Returns(jobListing);
        var mockLogger = new Mock<ILogger<JobListingController>>();
        var controller = new JobListingController(mockJobListingService.Object, mockLogger.Object);

        // Act
        var result = controller.GetJobListing(1);

        // Assert
        var partialViewResult = Assert.IsType<PartialViewResult>(result);
        Assert.Equal("_JobListingDetails", partialViewResult.ViewName);
        Assert.Equal(jobListing, partialViewResult.Model);
    }
}
