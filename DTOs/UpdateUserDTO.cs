using BusinessDirectory.Enums;

namespace BusinessDirectory.DTOs
{
    public class UpdateUserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public int ServiceId { get; set; }
        public string LandlinePhone { get; set; }
        public string MobilePhone { get; set; }
        public int SiteId { get; set; }
        public string Password { get; set; }
    }
}
