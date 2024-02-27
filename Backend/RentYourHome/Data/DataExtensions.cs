using Microsoft.EntityFrameworkCore;

namespace RentYourHome.Data;

public static class DataExtensions
{
    public static async Task InitializeTestDbAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        var usersContext = scope.ServiceProvider.GetRequiredService<UsersContext>();
        await databaseContext.Database.MigrateAsync();
        await usersContext.Database.MigrateAsync();
    }
}