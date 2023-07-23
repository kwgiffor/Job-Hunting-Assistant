using Xunit;
using Moq;
using JobHuntingAssistant.Services;
using JobHuntingAssistant.Models;
using JobHuntingAssistant.AI;

public class AIResumeGenerationServiceTests
{
    [Fact]
    public void GenerateResume_ReturnsResume_WhenParametersAreValid()
    {
        // Arrange
        var mockAIModel = new Mock<IAIModel>();
        mockAIModel.Setup(model => model.Prompt(It.IsAny<string>())).Returns("Generated resume content");
        var service = new AIResumeGenerationService(mockAIModel.Object);
        var parameters = new ResumeGenerationParameters(new JobListing("",""), new User());

        // Act
        var resume = service.GenerateResume(parameters);

        // Assert
        Assert.NotNull(resume);
        Assert.Equal("Generated resume content", resume.Content);
    }

    [Fact]
    public void GenerateResume_ThrowsException_WhenParametersAreNull()
    {
        // Arrange
        var mockAIModel = new Mock<IAIModel>();
        var service = new AIResumeGenerationService(mockAIModel.Object);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => service.GenerateResume(null));
    }

    [Fact]
    public void GenerateResume_ThrowsException_WhenAIModelReturnsEmptyString()
    {
        // Arrange
        var mockAIModel = new Mock<IAIModel>();
        mockAIModel.Setup(model => model.Prompt(It.IsAny<string>())).Returns(string.Empty);
        var service = new AIResumeGenerationService(mockAIModel.Object);
        var parameters = new ResumeGenerationParameters(new JobListing("",""), new User());

        // Act & Assert
        Assert.Throws<Exception>(() => service.GenerateResume(parameters));
    }
}
