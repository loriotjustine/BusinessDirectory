using BusinessDirectory.Enums;
using System.ComponentModel.DataAnnotations;

namespace BusinessDirectory.DTOs
{
    public class CreateSiteDTO
    {
        [Required(ErrorMessage = "Le nom du site est requis.")]
        public string SiteName { get; set; }

        [Required(ErrorMessage = "Le type du site est requis.")]
        public SiteType SiteType { get; set; }

    }
}
