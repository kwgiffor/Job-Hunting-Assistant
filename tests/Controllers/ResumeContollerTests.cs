using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using JobHuntingAssistant.Controllers;
using JobHuntingAssistant.Services;
using JobHuntingAssistant.Models;

public class ResumeControllerTests
{
    [Fact]
    public void GenerateResume_ReturnsBadRequest_WhenJobListingIsNull()
    {
        // Arrange
        var mockResumeGenerationService = new Mock<IResumeGenerationService>();
        var mockJobListingService = new Mock<IJobListingService>();
        var mockUserService = new Mock<IUserService>();
        mockJobListingService.Setup(service => service.GetJobListingById(It.IsAny<int>())).Returns((JobListing)null);
        var controller = new ResumeController(mockResumeGenerationService.Object, mockJobListingService.Object, mockUserService.Object);

        // Act
        var result = controller.GenerateResume(1);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public void GenerateResume_ReturnsBadRequest_WhenUserIsNull()
    {
        // Arrange
        var mockResumeGenerationService = new Mock<IResumeGenerationService>();
        var mockJobListingService = new Mock<IJobListingService>();
        var mockUserService = new Mock<IUserService>();
        mockUserService.Setup(service => service.GetActiveUser()).Returns((User)null);
        var controller = new ResumeController(mockResumeGenerationService.Object, mockJobListingService.Object, mockUserService.Object);

        // Act
        var result = controller.GenerateResume(1);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public void GenerateResume_ReturnsJsonResult_WithGeneratedResume_WhenJobListingAndUserAreNotNull()
    {
        // Arrange
        var mockResumeGenerationService = new Mock<IResumeGenerationService>();
        var mockJobListingService = new Mock<IJobListingService>();
        var mockUserService = new Mock<IUserService>();
        var jobListing = new JobListing("title", "description");
        var user = new User(){ Id = 1 };
        var generatedResume = new Resume(1,0,"");
        mockJobListingService.Setup(service => service.GetJobListingById(It.IsAny<int>())).Returns(jobListing);
        mockUserService.Setup(service => service.GetActiveUser()).Returns(user);
        mockResumeGenerationService.Setup(service => service.GenerateResume(It.IsAny<ResumeGenerationParameters>())).Returns(generatedResume);
        var controller = new ResumeController(mockResumeGenerationService.Object, mockJobListingService.Object, mockUserService.Object);

        // Act
        var result = controller.GenerateResume(1);

        // Assert
        var jsonResult = Assert.IsType<JsonResult>(result);
        Assert.Equal(generatedResume, jsonResult.Value);
    }
}
