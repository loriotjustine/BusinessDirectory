using BusinessDirectory.Data;
using BusinessDirectory.Models;

namespace BusinessDirectory.Repositories
{
    public class SitesRepository : BaseRepository<Site>, ISitesRepository
    {
        public SitesRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
