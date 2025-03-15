using BusinessDirectory.Data;
using BusinessDirectory.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessDirectory.Repositories;

public class UsersRepository : BaseRepository<User>, IUsersRepository
{
    private readonly ApplicationDbContext _context;
    public UsersRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<User> AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task DeleteAsync(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> FindAsync(int id)
    {
        return await _context.Users
            .Include(u => u.Site) 
            .Include(u => u.Service)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<List<User>> ListAsync()
    {
        return await _context.Users
            .Include(u => u.Site)
            .Include(u => u.Service)
            .ToListAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task<List<User>> GetUsersBySiteIdAsync(int siteId)
    {
        return await _context.Users
                             .Where(u => u.SiteId == siteId)
                             .ToListAsync();
    }

    public async Task<List<User>> GetUsersByServiceIdAsync(int serviceId)
    {
        return await _context.Users
                             .Where(u => u.ServiceId == serviceId)
                             .ToListAsync();
    }

}

