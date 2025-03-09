using BusinessDirectory.DTOs;
using BusinessDirectory.Exceptions;
using BusinessDirectory.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusinessDirectory.Controllers;

[ApiController]
[Route("[controller]")]
public class ServicesController : ControllerBase
{

    public readonly ServicesService _servicesService;

    public ServicesController(ServicesService servicesService)
    {
        _servicesService = servicesService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _servicesService.GetAllServices());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var service = await _servicesService.GetServiceById(id);

        if (service == null) return NotFound();

        return Ok(service);

    }

    [HttpPost]
    //[Authorize(Roles = "Admin")] // Pour ajouter une vérification du rôle au niveau de la route, on procède en ajoutant une nouvelle annotation possédant le paramètre Roles ainsi qu'une liste des rôles possible pour la route
    public async Task<IActionResult> Create([FromBody] CreateServiceDTO serviceDTO)
    {
        try
        {
            var service = await _servicesService.AddService(serviceDTO);
            return CreatedAtAction(nameof(Get), new { id = service.Id }, service);
        }
        catch (ServiceAlreadyExistsException ex)
        {
            return Conflict(new { ex.Message });
        }
        
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateServiceDTO serviceDTO)
    {
        try
        {
            var updatedService = await _servicesService.UpdateService(id, serviceDTO);
            if (updatedService == null) return NotFound();

            return Ok(updatedService);
        }
        catch (ServiceAlreadyExistsException ex)
        {
            return Conflict(new { ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deletedService = await _servicesService.DeleteService(id);
        if (!deletedService) return NotFound();

        return NoContent();
    }

}
