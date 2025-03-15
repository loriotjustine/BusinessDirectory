using Microsoft.EntityFrameworkCore;
using BusinessDirectory.Data;
using BusinessDirectory.Repositories;
using BusinessDirectory.Services;

namespace BusinessDirectory.Extensions;

public static class DependencyInjectionExtensions
{

    public static void InjectDependencies(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.AddSwagger();
        builder.AddEFCoreConfiguration();
        builder.AddRepositories();
        builder.AddServices();
    }

    private static void AddSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }

    private static void AddEFCoreConfiguration(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
    }

    private static void AddRepositories(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUsersRepository, UsersRepository>();
        builder.Services.AddScoped<IServicesRepository, ServicesRepository>();
        builder.Services.AddScoped<ISitesRepository, SitesRepository>();
        builder.Services.AddScoped<UsersService>();
    }

    private static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<UsersService>();
        builder.Services.AddScoped<AuthService>();
        builder.Services.AddScoped<ServicesService>();
        builder.Services.AddScoped<SitesService>();

    }

}
