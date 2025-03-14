using BusinessDirectory.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessDirectory.Models;

[Table("Site")]
public class Site
{
    public int Id { get; set; }

    [Display(Name = "Nom du site")]
    public string SiteName { get; set; }
   
    public SiteType SiteType { get; set; }

}
