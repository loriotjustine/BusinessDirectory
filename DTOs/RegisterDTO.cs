using BusinessDirectory.Enums;
using BusinessDirectory.Validators;
using System.ComponentModel.DataAnnotations;

namespace BusinessDirectory.DTOs;

public class RegisterDTO
{

    [StringLength(100)]
    public string Email { get; set; }

    [PasswordValidator]
    public string Password { get; set; }

    [Required(ErrorMessage = "Le prénom est obligatoire.")]
    [StringLength(100, ErrorMessage = "Le prénom ne doit pas dépasser 100 caractères.")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Le nom est obligatoire.")]
    [StringLength(100, ErrorMessage = "Le nom ne doit pas dépasser 100 caractères.")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Le numéro de téléphone fixe est obligatoire.")]
    [Phone(ErrorMessage = "Numéro de téléphone non valide.")]
    public string LandlinePhone { get; set; }

    [Required(ErrorMessage = "Le numéro de téléphone mobile est obligatoire.")]
    [Phone(ErrorMessage = "Numéro de téléphone non valide.")]
    public string MobilePhone { get; set; }

    [Required(ErrorMessage = "Veuillez sélectionner un rôle.")]
    public Role Role { get; set; }

    [Required(ErrorMessage = "Veuillez sélectionner un service.")]
    public int ServiceId { get; set; }

    [Required(ErrorMessage = "Veuillez sélectionner un site.")]
    public int SiteId { get; set; }

}
