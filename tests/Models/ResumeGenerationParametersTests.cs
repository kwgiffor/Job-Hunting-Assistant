using Xunit;
using JobHuntingAssistant.Models;
using System;

namespace JobHuntingAssistant.Tests;

public class ResumeGenerationParametersTests
{
    [Fact]
    public void Constructor_SetsJobListingAndUser()
    {
        // Arrange
        var jobListing = new JobListing("Software Engineer", "Develop and maintain software applications.");
        var user = new User { Id = 1, OldResume = "Old Resume Content" };

        // Act
        var parameters = new ResumeGenerationParameters(jobListing, user);

        // Assert
        Assert.Equal(jobListing, parameters.JobListing);
        Assert.Equal(user, parameters.User);
    }

    [Fact]
    public void ConstructPrompt_ReturnsCorrectPrompt()
    {
        // Arrange
        var jobListing = new JobListing("Software Engineer", "Develop and maintain software applications.");
        var user = new User { Id = 1, OldResume = "Old Resume Content" };
        var parameters = new ResumeGenerationParameters(jobListing, user);

        // Act
        var prompt = parameters.ConstructPrompt();

        // Assert
        Assert.Contains("Old Resume:", prompt);
        Assert.Contains(user.OldResume, prompt);
        Assert.Contains("New Job Listing:", prompt);
        Assert.Contains(jobListing.Title, prompt);
        Assert.Contains(jobListing.Description, prompt);
        Assert.Contains("As a professional resume writer", prompt);
    }
}
