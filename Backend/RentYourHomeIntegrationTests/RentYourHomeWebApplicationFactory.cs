using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RentYourHome.Data;

namespace RentYourHomeIntegrationTests;

internal class RentYourHomeWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly string _connString = ConnectionString.GetTestConnectionStringForFactory();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<DatabaseContext>));
            services.RemoveAll(typeof(DbContextOptions<UsersContext>));

            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer(_connString));
            services.AddDbContext<UsersContext>(options =>
                options.UseSqlServer(_connString));

            var databaseContext = CreateDatabaseContext(services);
            databaseContext.Database.EnsureDeleted();
            var usersContext = CreateUsersContext(services);
            usersContext.Database.EnsureDeleted();
        });
    }

    private static DatabaseContext CreateDatabaseContext(IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        return dbContext;
    }

    private static UsersContext CreateUsersContext(IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<UsersContext>();
        return dbContext;
    }
}