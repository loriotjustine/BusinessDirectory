using BusinessDirectory.Models;

namespace BusinessDirectory.Repositories;

public interface IUsersRepository : IBaseRepository<User>
{
    Task<List<User>> ListAsync();
    Task<User?> FindAsync(int id);
    Task<User> AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
    Task<List<User>> GetUsersBySiteIdAsync(int siteId);
    Task<List<User>> GetUsersByServiceIdAsync(int serviceId);
}
