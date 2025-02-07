using BusinessDirectory.Data;
using BusinessDirectory.Models;

namespace BusinessDirectory.Repositories;

public class UsersRepository : BaseRepository<User>, IUsersRepository
{

    public UsersRepository(ApplicationDbContext context) : base(context)
    {

    }

}

