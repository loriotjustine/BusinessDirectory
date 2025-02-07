using BusinessDirectory.Models;
using BusinessDirectory.Repositories;

namespace BusinessDirectory.Services;

public class UsersService(IUsersRepository usersRepository)
{
    public async Task<List<User>> GetAllUsers()
    {
        return await usersRepository.ListAsync();
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        return await usersRepository.FirstOrDefaultAsync(e => e.Email == email);
    }
}
