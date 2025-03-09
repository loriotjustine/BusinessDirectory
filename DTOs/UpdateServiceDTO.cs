using System.ComponentModel.DataAnnotations;

namespace BusinessDirectory.DTOs
{
    public class UpdateServiceDTO
    {
        [Required(ErrorMessage = "Le nom du service est requis.")]
        public string ServiceName { get; set; }
    }
}
