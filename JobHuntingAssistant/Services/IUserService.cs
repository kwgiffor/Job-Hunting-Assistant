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
        User GetUser(int id);

        /// <summary>
        /// Asynchronously adds a user to the database.
        /// </summary>
        Task AddUser(User user);

        /// <summary>
        /// Updates a user in the database asynchronously.
        /// </summary>
        Task<bool> UpdateUser(User user);

        /// <summary>
        /// Validates user credentials.
        /// </summary>
        bool ValidateUserCredentials(string username, string password);
    }
}