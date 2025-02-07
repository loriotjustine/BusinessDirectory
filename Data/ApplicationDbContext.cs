using BusinessDirectory.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessDirectory.Data;


public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Site> Site { get; set; }
    public DbSet<Service> Services { get; set; }

}
