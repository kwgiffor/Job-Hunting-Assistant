using JobHuntingAssistant.Models;

namespace JobHuntingAssistant.Services
{
    /// <summary>
    /// Interface for the user service.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Gets the active user.
        /// </summary>
        User GetActiveUser();

        /// <summary>
        /// Gets a user by id.
        /// </summary>
        User GetUserById(int id);

        /// <summary>
        /// Asynchronously adds a user to the database.
        /// </summary>
        /// <returns>The id of the user that was added.</returns>
        Task<int> AddUser(User user);

        /// <summary>
        /// Updates a user in the database asynchronously.
        /// </summary>
        /// <returns>True if the user was updated, false otherwise.</returns>
        Task<bool> UpdateUser(User user);

        /// <summary>
        /// Validates user credentials.
        /// </summary>
        bool ValidateUserCredentials(string username, string password);
    }
}