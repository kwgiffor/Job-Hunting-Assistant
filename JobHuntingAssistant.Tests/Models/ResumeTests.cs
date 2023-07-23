using Xunit;
using JobHuntingAssistant.Models;
using System;

namespace JobHuntingAssistant.Tests;

public class ResumeTests
{
    [Fact]
    public void Constructor_SetsUserIdJobListingIdAndContent()
    {
        // Arrange
        var userId = 1;
        var jobListingId = 2;
        var content = "Resume Content";

        // Act
        var resume = new Resume(userId, jobListingId, content);

        // Assert
        Assert.Equal(userId, resume.UserId);
        Assert.Equal(jobListingId, resume.JobListingId);
        Assert.Equal(content, resume.Content);
    }

    [Fact]
    public void Constructor_SetsContentToEmptyString_WhenNullIsPassed()
    {
        // Arrange
        var userId = 1;
        var jobListingId = 2;
        string? content = null;

        // Act
        var resume = new Resume(userId, jobListingId, content);

        // Assert
        Assert.Equal(string.Empty, resume.Content);
    }

    [Fact]
    public void Constructor_SetsGeneratedAtToCurrentTime()
    {
        // Arrange
        var userId = 1;
        var jobListingId = 2;
        var content = "Resume Content";

        // Act
        var resume = new Resume(userId, jobListingId, content);

        // Assert
        Assert.True((DateTime.Now - resume.GeneratedAt).TotalSeconds < 1);
    }
    
    [Fact]
    public void Constructor_SetsContentToEmptyString_WhenEmptyStringIsPassed()
    {
        // Arrange
        var userId = 1;
        var jobListingId = 2;
        var content = string.Empty;

        // Act
        var resume = new Resume(userId, jobListingId, content);

        // Assert
        Assert.Equal(string.Empty, resume.Content);
    }


}
