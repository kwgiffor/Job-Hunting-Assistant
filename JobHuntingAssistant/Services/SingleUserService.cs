using JobHuntingAssistant.Models;

namespace JobHuntingAssistant.Services
{
    /// <summary>
    /// A simple implementation of <see cref="IUserService"/> that only allows one user to be active at a time.
    /// </summary>
    public class SingleUserService : IUserService
    {
        private static User _user = null;

        public SingleUserService()
        {
        }
        
        public bool ValidateUserCredentials(string email, string password)
        {
            // Check the email and password against the database.
            // If the email and password are valid, return true. Otherwise, return false.
            // If the user is not found, return false.

            // Check the email
            if (_user == null || _user.Email != email)
            {
                return false;
            }

            // Check the password
            return BCrypt.Net.BCrypt.Verify(password, _user.PasswordHash);
        }

        public User GetUserById(int id)
        {
            // Check the ID against the database.
            // If the ID is valid, return the user. Otherwise, return null.

            if (_user != null && _user.Id == id)
            {
                return _user;
            }

            return null;
        }

        public User GetActiveUser()
        {
            // Return the active user.
            // If there is no active user, return null.

            if (_user == null)
            {
                return null;
            }
            
            Console.WriteLine($" SingleUserService User: {_user}");
            return _user;
        }

        public Task<int> AddUser(User user)
        {
            // Check if there is already an active user.
            // If there is, throw an exception.
            // Otherwise, set the active user to the new user.

            // Check if there is already an active user.
            if (_user != null)
            {
                throw new ArgumentException("A user already exists");
            }

            // Set the active user to the new user.
            _user = user;

            // Return a completed task
            return Task.FromResult(0);
        }

        public Task<bool> UpdateUser(User user)
        {
            // Check if the user exists.
            // If the user exists, update the user.
            // Otherwise, throw an exception.

            // Check if the user exists.
            if (_user == null)
            {
                throw new ArgumentException($"User with ID {user.Id} not found");
            }

            // Update the user.
            _user = user;

            return Task.FromResult(true);
        }
    }
}