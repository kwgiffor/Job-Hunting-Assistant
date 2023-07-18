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
        /// Adds a user to the database.
        /// </summary>
        void AddUser(User user);

        /// <summary>
        /// Updates a user in the database.
        /// </summary>
        void UpdateUser(User user);
    }
}