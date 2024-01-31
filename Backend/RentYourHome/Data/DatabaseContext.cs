using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using RentYourHome.Models.Addresses;
using RentYourHome.Models.Ads;
using RentYourHome.Models.UserAdApplications;
using RentYourHome.Models.Users;

namespace RentYourHome.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Ad> Ads { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<UserAdApplication> UserAdApplications { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var root = Directory.GetCurrentDirectory();
        var dotenv = Path.Combine(root, "..", "..", ".env");
        Env.Load(dotenv);
        optionsBuilder.UseSqlServer(
            $"Server={Environment.GetEnvironmentVariable("DBHOST")},{Environment.GetEnvironmentVariable("DBPORT")};Database={Environment.GetEnvironmentVariable("DBNAME")};User Id={Environment.GetEnvironmentVariable("DBUSER")};Password={Environment.GetEnvironmentVariable("DBPASSWORD")};Encrypt=false;");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<UserAdApplication>(entity =>
        {
            entity.HasKey(ua => new { ua.UserId, ua.AdId });

            entity.HasOne(ua => ua.User)
                .WithMany(u => u.UserAdApplications)
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(ua => ua.Ad)
                .WithMany(a => a.UserAdApplications)
                .HasForeignKey(ua => ua.AdId)
                .OnDelete(DeleteBehavior.NoAction);
        });
    }
}