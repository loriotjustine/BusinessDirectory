using BusinessDirectory.DTOs;
using BusinessDirectory.Extensions;
using BusinessDirectory.Models;
using BusinessDirectory.Repositories;

namespace BusinessDirectory.Services;

public class ServicesService
{

    private readonly IServicesRepository _servicesRepository;
    private readonly IUsersRepository _usersRepository;

    public ServicesService(IServicesRepository servicesRepository, IUsersRepository usersRepository)
    {
        _servicesRepository = servicesRepository;
        _usersRepository = usersRepository;
    }

    public async Task<List<Service>> GetAllServices()
    {
        return await _servicesRepository.ListAsync();
    }

    public async Task<Service?> GetServiceById(int id)
    {
        return await _servicesRepository.FindAsync(id);
    }

    public async Task<Service> AddService(CreateServiceDTO serviceDTO)
    {
        var service = serviceDTO.MapToModel();

        if (await _servicesRepository.AnyAsync(s => s.ServiceName == service.ServiceName))
            throw new Exception("Le service existe déjà");

        return await _servicesRepository.AddAsync(service);
    }

    public async Task<Service?> UpdateService(int id, UpdateServiceDTO serviceDTO)
    {
        var existingService = await _servicesRepository.FindAsync(id);
        if (existingService == null) return null;

        if (await _servicesRepository.AnyAsync(s => s.ServiceName == serviceDTO.ServiceName && s.Id != id))
            throw new Exception("Un autre service avec ce nom existe déjà.");

        existingService.ServiceName = serviceDTO.ServiceName;

        await _servicesRepository.UpdateAsync(existingService);
        return existingService;
    }

    public async Task<bool> DeleteService(int id)
    {
        var usersWithService = await _usersRepository.ListAsync(u => u.ServiceId == id);
        if (usersWithService.Any())
        {
            throw new Exception("Suppression impossible : au moins un utilisateur est affecté à ce service.");
        }

        var service = await _servicesRepository.FindAsync(id);
        if (service == null) return false;

        await _servicesRepository.DeleteAsync(service);
        return true;
    }
}
