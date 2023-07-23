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
        
        public bool ValidateUserCredentials(string username, string password)
        {
            // Check the username and password against the database.
            // If the username and password are valid, return true. Otherwise, return false.
            // If the user is not found, return false.

            // Check the username
            if (_user == null || _user.Username != username)
            {
                return false;
            }

            // Check the password
            return BCrypt.Net.BCrypt.Verify(password, _user.PasswordHash);
        }

        public User GetUser(int id)
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

        public void AddUser(User user)
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
        }

        public void UpdateUser(User user)
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
        }
    }
}