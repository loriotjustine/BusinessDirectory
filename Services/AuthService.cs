using BusinessDirectory.DTOs;
using BusinessDirectory.Extensions;
using BusinessDirectory.Repositories;
using BusinessDirectory.Utilities;

namespace BusinessDirectory.Services;

public class AuthService(IUsersRepository usersRepository)
{
    public async Task<bool> RegisterUser(RegisterDTO registerDTO)
    {
        if (await usersRepository.AnyAsync(u => u.Email == registerDTO.Email))
            return false;

        var user = registerDTO.MapToModel();

        var hashedPassword = PasswordCrypter.HashPassword(registerDTO.Password, out var salt);
        user.Password = hashedPassword;
        user.Salt = Convert.ToBase64String(salt);

        return await usersRepository.AddAsync(user) is not null;
        ;
    }
}
