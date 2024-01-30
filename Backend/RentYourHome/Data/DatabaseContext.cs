using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using RentYourHome.Models;

namespace RentYourHome.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Ad> Ads { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<UserAppliedAd> UserAppliedAds { get; set; }

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
        builder.Entity<UserAppliedAd>(entity =>
        {
            entity.HasKey(ua => new { ua.UserId, ua.AdId });
            
            entity.HasOne(ua => ua.User)
                .WithMany(u => u.AppliedAds)
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            
            entity.HasOne(ua => ua.Ad)
                .WithMany(a => a.AppliedAds)
                .HasForeignKey(ua => ua.AdId)
                .OnDelete(DeleteBehavior.NoAction);
        });
    }
}