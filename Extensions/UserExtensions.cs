using BusinessDirectory.DTOs;
using BusinessDirectory.Models;

namespace BusinessDirectory.Extensions;

public static class UserExtensions
{

    public static User MapToModel(this RegisterDTO register)
    {
        return new User()
        {
            Id = 0,
            Email = register.Email,
            Password = register.Password,
            FirstName = register.FirstName,
            LastName = register.LastName,
            LandlinePhone = register.LandlinePhone,
            MobilePhone = register.MobilePhone,
            Role = register.Role,
            ServiceId = register.ServiceId,
            SiteId = register.SiteId,
            CreatedAt = DateTime.Now,
            Salt = ""
        };
    }

}
