using Xunit;
using JobHuntingAssistant.Models;
using System.Collections.Generic;

public class ResumeGenerationViewModelTests
{
    [Fact]
    public void Constructor_InitializesJobListingsAndSelectedJobListingIndex()
    {
        // Arrange & Act
        var viewModel = new ResumeGenerationViewModel();

        // Assert
        Assert.NotNull(viewModel.JobListings);
        Assert.Empty(viewModel.JobListings);
        Assert.Equal(0, viewModel.SelectedJobListingIndex);
    }

    [Fact]
    public void CanAddToJobListingsList()
    {
        // Arrange
        var viewModel = new ResumeGenerationViewModel();
        var jobListing = new JobListing("Software Engineer", "Develop and maintain software applications.");

        // Act
        viewModel.JobListings.Add(jobListing);

        // Assert
        Assert.Contains(jobListing, viewModel.JobListings);
    }
}
