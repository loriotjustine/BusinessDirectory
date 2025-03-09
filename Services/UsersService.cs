using BusinessDirectory.DTOs;
using BusinessDirectory.Enums;
using BusinessDirectory.Models;
using BusinessDirectory.Repositories;
using BusinessDirectory.Utilities;

namespace BusinessDirectory.Services;

public class UsersService
{
    private readonly IUsersRepository _usersRepository;

    public UsersService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<List<User>> GetAllUsers()
    {
        return await _usersRepository.ListAsync();
    }

    public async Task<User?> GetUserById(int id)
    {
        return await _usersRepository.FindAsync(id);
    }

    public async Task<User> AddUser(CreateUserDTO userDTO)
    {
        if (await _usersRepository.AnyAsync(u => u.Email == userDTO.Email))
            throw new Exception("L'utilisateur avec cet email existe déjà");

        byte[] salt;
        string hashedPassword = PasswordCrypter.HashPassword(userDTO.Password, out salt);

        var user = new User
        {
            LastName = userDTO.LastName,
            FirstName = userDTO.FirstName,
            Email = userDTO.Email,
            LandlinePhone = userDTO.LandlinePhone,
            MobilePhone = userDTO.MobilePhone,
            ServiceId = userDTO.ServiceId,
            SiteId = userDTO.SiteId,
            Role = userDTO.Role,
            Password = hashedPassword,
            Salt = Convert.ToHexString(salt),
            CreatedAt = DateTime.UtcNow
        };

        return await _usersRepository.AddAsync(user);
    }

    public async Task<User?> UpdateUser(int id, string firstName, string lastName, string email, Role role, int serviceId,
                                             string landlinePhone, string mobilePhone, int siteId, string password)
    {
        var user = await _usersRepository.FindAsync(id);
        if (user == null) return null;

        user.FirstName = firstName;
        user.LastName = lastName;
        user.Email = email;
        user.Role = role;
        user.ServiceId = serviceId;
        user.LandlinePhone = landlinePhone;
        user.MobilePhone = mobilePhone;
        user.SiteId = siteId;

        if (!string.IsNullOrEmpty(password))
        {
            byte[] salt;
            user.Password = PasswordCrypter.HashPassword(password, out salt);
            user.Salt = Convert.ToBase64String(salt);
        }

        await _usersRepository.UpdateAsync(user);
        return user;
    }

    public async Task<bool> DeleteUser(int id)
    {
        var user = await _usersRepository.FindAsync(id);
        if (user == null) return false;

        await _usersRepository.DeleteAsync(user);
        return true;
    }
}
