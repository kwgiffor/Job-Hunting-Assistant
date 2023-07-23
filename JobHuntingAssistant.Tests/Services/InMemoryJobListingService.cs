using Xunit;
using JobHuntingAssistant.Services;
using JobHuntingAssistant.Models;

public class InMemoryJobListingServiceTests
{
    [Fact]
    public void GetAllJobListings_ReturnsJobListings()
    {
        // Arrange
        var service = new InMemoryJobListingService();

        // Act
        var jobListings = service.GetAllJobListings();

        // Assert
        Assert.NotNull(jobListings);
    }

    [Fact]
    public void GetJobListingById_ReturnsJobListing_WhenIdExists()
    {
        // Arrange
        var service = new InMemoryJobListingService();
        var jobListing = new JobListing("Title", "Description") { Id = 1 };
        service.AddJobListing(jobListing);

        // Act
        var retrievedJobListing = service.GetJobListingById(1);

        // Assert
        Assert.Equal(jobListing, retrievedJobListing);
    }

    [Fact]
    public void GetJobListingById_ThrowsException_WhenIdDoesNotExist()
    {
        // Arrange
        var service = new InMemoryJobListingService();

        // Act & Assert
        Assert.Throws<Exception>(() => service.GetJobListingById(1));
    }

    [Fact]
    public void AddJobListing_AddsJobListing_WhenIdDoesNotExist()
    {
        // Arrange
        var service = new InMemoryJobListingService();
        var jobListing = new JobListing("Title", "Description") { Id = 1 };

        // Act
        service.AddJobListing(jobListing);

        // Assert
        var retrievedJobListing = service.GetJobListingById(1);
        Assert.Equal(jobListing, retrievedJobListing);
    }

    [Fact]
    public void AddJobListing_ThrowsException_WhenIdExists()
    {
        // Arrange
        var service = new InMemoryJobListingService();
        var jobListing = new JobListing("Title", "Description") { Id = 1 };
        service.AddJobListing(jobListing);

        // Act & Assert
        Assert.Throws<Exception>(() => service.AddJobListing(jobListing));
    }

    [Fact]
    public void AddResumeToJobListing_AddsResume_WhenResumeIdDoesNotExist()
    {
        // Arrange
        var service = new InMemoryJobListingService();
        var jobListing = new JobListing("Title", "Description") { Id = 1 };
        service.AddJobListing(jobListing);
        var resume = new Resume(0, 0, "resume") { Id = 1 };

        // Act
        service.AddResumeToJobListing(1, resume);

        // Assert
        var retrievedJobListing = service.GetJobListingById(1);
        Assert.Contains(resume, retrievedJobListing.Resumes);
    }

    [Fact]
    public void AddResumeToJobListing_ThrowsException_WhenJobListingIdDoesNotExist()
    {
        // Arrange
        var service = new InMemoryJobListingService();
        var resume = new Resume(0, 0, "resume") { Id = 1 };

        // Act & Assert
        Assert.Throws<Exception>(() => service.AddResumeToJobListing(1, resume));
    }

    [Fact]
    public void AddResumeToJobListing_ThrowsException_WhenResumeIdExists()
    {
        // Arrange
        var service = new InMemoryJobListingService();
        var jobListing = new JobListing ("Title","Description"){ Id = 1 };
        service.AddJobListing(jobListing);
        var resume = new Resume (0, 0, "resume"){ Id = 1 };
        service.AddResumeToJobListing(1, resume);

        // Act & Assert
        Assert.Throws<Exception>(() => service.AddResumeToJobListing(1, resume));
    }
}
