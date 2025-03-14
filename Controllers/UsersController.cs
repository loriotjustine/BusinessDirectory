using BusinessDirectory.DTOs;
using BusinessDirectory.Enums;
using BusinessDirectory.Exceptions;
using BusinessDirectory.Models;
using BusinessDirectory.Services;
using Microsoft.AspNetCore.Mvc;

namespace BusinessDirectory.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
   private readonly UsersService _usersService;

    public UsersController(UsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _usersService.GetAllUsers());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var user = await _usersService.GetUserById(id);

        if (user == null) return NotFound();

        return Ok(user);

    }

    [HttpPost]
    //[Authorize(Roles = "Admin")] // Pour ajouter une vérification du rôle au niveau de la route, on procède en ajoutant une nouvelle annotation possédant le paramètre Roles ainsi qu'une liste des rôles possible pour la route
    public async Task<IActionResult> Create([FromBody] CreateUserDTO userDTO)
    {
        try
        {
            var user = await _usersService.AddUser(userDTO);
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }
        catch (UserAlreadyExistsException ex)
        {
            return Conflict(new { ex.Message });
        }

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDTO updateUserDTO)
    {
        var updatedUser = await _usersService.UpdateUser(
            id,
            updateUserDTO.FirstName,
            updateUserDTO.LastName,
            updateUserDTO.Email,
            updateUserDTO.Role,
            updateUserDTO.ServiceId,
            updateUserDTO.LandlinePhone,
            updateUserDTO.MobilePhone,
            updateUserDTO.SiteId,
            updateUserDTO.Password
        );

        if (updatedUser == null) return NotFound();

        return Ok(updatedUser);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _usersService.DeleteUser(id);
        if (!deleted) return NotFound();

        return NoContent();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var user = await _usersService.AuthenticateUser(loginRequest.Email, loginRequest.Password);

        if (user == null)
        {
            return Unauthorized(new { message = "Email ou mot de passe incorrect." });
        }

        // Si l'utilisateur existe, on retourne les informations de l'utilisateur, incluant le rôle
        return Ok(new { role = user.Role });
    }

    [HttpGet("BySite/{siteId}")]
    public async Task<IActionResult> GetUsersBySiteId(int siteId)
    {
        var users = await _usersService.GetUsersBySiteId(siteId);
        if (users == null || !users.Any())
        {
            return Ok(new List<User>());
        }

        return Ok(users);
    }

    [HttpGet("ByService/{serviceId}")]
    public async Task<IActionResult> GetUsersByServiceId(int serviceId)
    {
        var users = await _usersService.GetUsersByServiceId(serviceId);
        if (users == null || !users.Any())
        {
            return Ok(new List<User>());
        }

        return Ok(users);
    }

    [HttpGet("roles")]
    public IActionResult GetRoles()
    {
        var roles = Enum.GetValues(typeof(Role))
                        .Cast<Role>()
                        .Select(r => new { id = (int)r, name = r.ToString() })
                        .ToList();

        return Ok(roles);
    }
}
