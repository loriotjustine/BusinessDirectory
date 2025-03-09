using BusinessDirectory.DTOs;
using BusinessDirectory.Exceptions;
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
}
