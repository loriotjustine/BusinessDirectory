using BusinessDirectory.DTOs;
using BusinessDirectory.Extensions;
using BusinessDirectory.Models;
using BusinessDirectory.Repositories;

namespace BusinessDirectory.Services;

public class ServicesService
{

    private readonly IServicesRepository _servicesRepository;

    public ServicesService(IServicesRepository servicesRepository)
    {
        _servicesRepository = servicesRepository;
    }

    public async Task<List<Service>> GetAllServices()
    {
        return await _servicesRepository.ListAsync();
    }

    public async Task<Service?> GetServiceById(int id)
    {
        return await _servicesRepository.FindAsync(id);
    }

    // Lors de l'ajout on doit vérifier que le livre n'est pas déjà présent
    public async Task<Service> AddService(CreateServiceDTO serviceDTO)
    {
        var service = serviceDTO.MapToModel();

        if (await _servicesRepository.AnyAsync(s => s.ServiceName == service.ServiceName))
            throw new Exception("Le service existe déjà");

        return await _servicesRepository.AddAsync(service);
    }
}
