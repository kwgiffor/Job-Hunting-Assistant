using Xunit;
using JobHuntingAssistant.Models;
using System.Collections.Generic;

namespace JobHuntingAssistant.Tests;

public class JobListingTests
{
    [Fact]
    public void Constructor_SetsTitleAndDescription()
    {
        // Arrange
        var title = "Software Engineer";
        var description = "Develop and maintain software applications.";

        // Act
        var jobListing = new JobListing(title, description);

        // Assert
        Assert.Equal(title, jobListing.Title);
        Assert.Equal(description, jobListing.Description);
    }

    [Fact]
    public void Constructor_InitializesResumesList()
    {
        // Arrange
        var title = "Software Engineer";
        var description = "Develop and maintain software applications.";

        // Act
        var jobListing = new JobListing(title, description);

        // Assert
        Assert.NotNull(jobListing.Resumes);
        Assert.Empty(jobListing.Resumes);
    }

    [Fact]
    public void CanAddToResumesList()
    {
        // Arrange
        var title = "Software Engineer";
        var description = "Develop and maintain software applications.";
        var jobListing = new JobListing(title, description);
        var resume = new Resume(1, 1, "OldResume");

        // Act
        jobListing.Resumes.Add(resume);

        // Assert
        Assert.Contains(resume, jobListing.Resumes);
    }
}
