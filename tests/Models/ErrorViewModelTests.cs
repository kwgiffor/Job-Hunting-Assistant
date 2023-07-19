using Xunit;
using JobHuntingAssistant.Models;

public class ErrorViewModelTests
{
    [Fact]
    public void ShowRequestId_ReturnsFalse_WhenRequestIdIsNull()
    {
        // Arrange
        var viewModel = new ErrorViewModel { RequestId = null };

        // Act
        var showRequestId = viewModel.ShowRequestId;

        // Assert
        Assert.False(showRequestId);
    }

    [Fact]
    public void ShowRequestId_ReturnsFalse_WhenRequestIdIsEmpty()
    {
        // Arrange
        var viewModel = new ErrorViewModel { RequestId = string.Empty };

        // Act
        var showRequestId = viewModel.ShowRequestId;

        // Assert
        Assert.False(showRequestId);
    }

    [Fact]
    public void ShowRequestId_ReturnsTrue_WhenRequestIdIsNotNullOrEmpty()
    {
        // Arrange
        var viewModel = new ErrorViewModel { RequestId = "1234" };

        // Act
        var showRequestId = viewModel.ShowRequestId;

        // Assert
        Assert.True(showRequestId);
    }
}
