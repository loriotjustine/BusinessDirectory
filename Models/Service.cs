using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessDirectory.Models;

[Table("Service")]
public class Service
{
    public int Id { get; set; }

    [Display(Name = "Nom du service")]
    public string ServiceName { get; set; }
}
