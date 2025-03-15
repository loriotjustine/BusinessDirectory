using BusinessDirectory.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessDirectory.Models
{
    [Table("User")]
    public class User
    {
        public int Id { get; set; }

        [Display(Name = "Nom")]
        public string LastName { get; set; }

        [Display(Name = "Prénom")]
        public string FirstName { get; set; }

        [Display(Name = "Adresse de courriel")]
        public string Email { get; set; }

        [Display(Name = "N° de téléphone fixe")]
        public string LandlinePhone { get; set; }

        [Display(Name = "N° de téléphone mobile")]
        public string MobilePhone { get; set; }

        [Required(ErrorMessage = "Veuillez sélectionner un rôle.")]
        [Display(Name = "Rôle")]
        public Role Role { get; set; }


        [Required(ErrorMessage = "Veuillez sélectionner un service.")]
        [Display(Name = "Service")]
        public int ServiceId { get; set; }
        public Service Service { get; set; }


        [Required(ErrorMessage = "Veuillez sélectionner un site.")]
        [Display(Name = "Site")]
        public int SiteId { get; set; }
        public Site Site { get; set; }

        public string Password { get; set; }
        public string Salt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
