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


}
