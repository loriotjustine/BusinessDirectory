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

    public async Task<User?> AuthenticateUser(string email, string password)
    {
        // Utilise FirstOrDefaultAsync pour rechercher un utilisateur par email
        var user = await _usersRepository.FirstOrDefaultAsync(u => u.Email == email);

        if (user == null)
        {
            Console.WriteLine($"Aucun utilisateur trouvé pour l'email {email}");
            return null; // Aucun utilisateur trouvé
        }

        Console.WriteLine($"Utilisateur trouvé : {user.Email}");

        // Afficher le salt et le mot de passe haché en base
        Console.WriteLine($"Salt en base : {user.Salt}");
        Console.WriteLine($"Mot de passe haché en base : {user.Password}");

        // Convertir correctement le Salt depuis Hex
        byte[] salt;
        try
        {
            salt = Convert.FromHexString(user.Salt);
            Console.WriteLine("Salt converti avec succès.");
        }
        catch (FormatException e)
        {
            Console.WriteLine($"Erreur lors de la conversion du Salt : {e.Message}");
            return null;
        }

        // Vérifier le mot de passe avec la méthode existante
        var passwordMatch = PasswordCrypter.VerifyPassword(password, user.Password, salt);

        if (passwordMatch)
        {
            Console.WriteLine("Authentification réussie.");
            return user; // Authentification réussie
        }

        Console.WriteLine("Échec de l'authentification : les mots de passe ne correspondent pas.");
        return null;
    }

    public async Task<List<User>> GetUsersBySiteId(int siteId)
    {
        return await _usersRepository.GetUsersBySiteIdAsync(siteId);
    }

    public async Task<List<User>> GetUsersByServiceId(int serviceId)
    {
        return await _usersRepository.GetUsersByServiceIdAsync(serviceId);
    }
}
