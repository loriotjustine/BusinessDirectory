using BusinessDirectory.Enums;
using BusinessDirectory.Validators;
using System.ComponentModel.DataAnnotations;

namespace BusinessDirectory.DTOs;

public class CreateUserDTO
{
    [Required]
    public string LastName { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

    [Required]
    public string LandlinePhone { get; set; }

    [Required]
    public string MobilePhone { get; set; }

    [Required]
    public int ServiceId { get; set; }

    [Required]
    public int SiteId { get; set; }

    [Required]
    public Role Role { get; set; }

    [Required]
    [PasswordValidator]
    public string Password { get; set; }
}
