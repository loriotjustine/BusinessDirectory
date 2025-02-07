using BusinessDirectory.DTOs;
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
        var service = await _servicesService.AddService(serviceDTO);
        return CreatedAtAction(nameof(Get), new { id = service.Id }, service);
    }

}
