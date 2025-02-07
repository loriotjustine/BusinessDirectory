using BusinessDirectory.Enums;
using System.ComponentModel.DataAnnotations;

namespace BusinessDirectory.Models;

public class Site
{
    public int Id { get; set; }

    [Display(Name = "Nom du site")]
    public string SiteName { get; set; }
   
    public SiteType SiteType { get; set; }
}
