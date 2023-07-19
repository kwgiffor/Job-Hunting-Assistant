using Xunit;
using JobHuntingAssistant.Models;

namespace JobHuntingAssistant.Tests;

public class UserTests
{
    [Fact]
    public void Constructor_SetsIdAndOldResume()
    {
        // Arrange
        var id = 1;
        var oldResume = "Old Resume Content";

        // Act
        var user = new User { Id = id, OldResume = oldResume };

        // Assert
        Assert.Equal(id, user.Id);
        Assert.Equal(oldResume, user.OldResume);
    }

    [Fact]
    public void Constructor_SetsOldResumeToEmptyString_WhenNullIsPassed()
    {
        // Arrange
        var id = 1;
        string oldResume = null;

        // Act
        var user = new User { Id = id, OldResume = oldResume };

        // Assert
        Assert.Equal(string.Empty, user.OldResume);
    }
}
