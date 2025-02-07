using BusinessDirectory.Validators;
using System.ComponentModel.DataAnnotations;

namespace BusinessDirectory.DTOs;

public class LoginDTO
{
    [StringLength(100)]
    public string Email { get; set; }

    [PasswordValidator]
    public string Password { get; set; }
}
