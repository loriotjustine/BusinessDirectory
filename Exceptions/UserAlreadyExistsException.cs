using BusinessDirectory.Models;

namespace BusinessDirectory.Exceptions
{
    public class UserAlreadyExistsException : Exception
    {

        public UserAlreadyExistsException(string message) : base(message) { }

        public UserAlreadyExistsException(User user) : base($"User already exist{(user.Id == 0 ? "" : $" with id:{user.Id}")}") { }

    }
}
