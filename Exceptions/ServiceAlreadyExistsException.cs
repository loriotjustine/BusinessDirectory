using BusinessDirectory.Models;

namespace BusinessDirectory.Exceptions
{
    public class ServiceAlreadyExistsException : Exception
    {

        public ServiceAlreadyExistsException(string message) : base(message) { }

        public ServiceAlreadyExistsException(Service service) : base($"Service already exist{(service.Id == 0 ? "" : $" with id:{service.Id}")}") { }

    }
}
