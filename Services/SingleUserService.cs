using JobHuntingAssistant.Models;

namespace JobHuntingAssistant.Services
{
    /// <summary>
    /// A simple implementation of <see cref="IUserService"/> that only allows one user to be active at a time.
    /// </summary>
    public class SingleUserService : IUserService
    {
        private static User _user;

        public SingleUserService()
        {
            _user = null;
        }

        public User GetUser(int id)
        {
            if (_user != null && _user.Id == id)
            {
                return _user;
            }

            return null;
        }

        public User GetActiveUser()
        {
            return _user;
        }

        public void AddUser(User user)
        {
            if (_user == null)
            {
                _user = user;
            }
            else
            {
                throw new ArgumentException("A user already exists");
            }
        }

        public void UpdateUser(User user)
        {
            if (_user != null && _user.Id == user.Id)
            {
                _user = user;
            }
            else
            {
                throw new ArgumentException($"User with ID {user.Id} not found or ID mismatch");
            }
        }
    }
}