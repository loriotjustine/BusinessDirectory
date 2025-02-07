using BusinessDirectory.DTOs;
using BusinessDirectory.Models;

namespace BusinessDirectory.Extensions;

public static class ServiceExtensions
{
    public static Service MapToModel(this CreateServiceDTO serviceDTO)
    {
        return new Service()
        {
            Id = 0,
            ServiceName = serviceDTO.ServiceName,

        };
    }
}
