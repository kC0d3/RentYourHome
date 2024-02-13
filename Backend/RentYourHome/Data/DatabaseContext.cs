using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using RentYourHome.Models.Addresses;
using RentYourHome.Models.Ads;
using RentYourHome.Models.Images;
using RentYourHome.Models.UserAdApplications;
using RentYourHome.Models.Users;

namespace RentYourHome.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Ad> Ads { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<UserAdApplication> UserAdApplications { get; set; }
    public DbSet<Image> Images { get; set; }

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
        builder.Entity<Image>(entity =>
        {
            entity.HasOne(i => i.Ad)
                .WithMany(a => a.Images)
                .HasForeignKey(i => i.AdId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Ad>(entity =>
        {
            entity.HasOne(a => a.Address)
                .WithOne(a => a.Ad)
                .HasForeignKey<Address>(a => a.AdId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(a => a.User)
                .WithMany(u => u.PublishedAds)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<User>(entity =>
        {
            entity.HasMany(u => u.PublishedAds)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<UserAdApplication>(entity =>
        {
            entity.HasKey(ua => new { ua.UserId, ua.AdId });

            entity.HasOne(ua => ua.User)
                .WithMany(u => u.UserAdApplications)
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.ClientCascade);

            entity.HasOne(ua => ua.Ad)
                .WithMany(a => a.UserAdApplications)
                .HasForeignKey(ua => ua.AdId)
                .OnDelete(DeleteBehavior.ClientCascade);
        });
    }
}