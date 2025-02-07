using BusinessDirectory.Data;
using BusinessDirectory.Models;

namespace BusinessDirectory.Repositories;

public class ServicesRepository : BaseRepository<Service>, IServicesRepository
{
    public ServicesRepository(ApplicationDbContext context) : base(context)
    {
    }
}
