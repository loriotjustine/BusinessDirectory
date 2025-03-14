using BusinessDirectory.Enums;

namespace BusinessDirectory.DTOs
{
    public class SiteDTO
    {
        public int Id { get; set; }
        public string SiteName { get; set; }
        public int SiteType { get; set; }  // Valeur numérique
        public string SiteTypeName { get; set; }
    }

}
