using BusinessDirectory.Data;
using BusinessDirectory.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessDirectory.Repositories
{
    public class SitesRepository : BaseRepository<Site>, ISitesRepository
    {
        private readonly ApplicationDbContext _context;
        public SitesRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Site> AddAsync(Site site)
        {
            await _context.Sites.AddAsync(site);
            await _context.SaveChangesAsync();
            return site;
        }

        public async Task DeleteAsync(Site site)
        {
            _context.Sites.Remove(site);
            await _context.SaveChangesAsync();
        }

        public async Task<Site?> FindAsync(int id)
        {
            return await _context.Sites
                .Include(s => s.SiteType)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<Site>> ListAsync()
        {
            return await _context.Sites
                .Include(s => s.SiteType)
                .ToListAsync();
        }

        public async Task UpdateAsync(Site site)
        {
            _context.Sites.Update(site);
            await _context.SaveChangesAsync();
        }
    }
}
