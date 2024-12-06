using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using pfaproject.Models;
using System.Reflection.Emit;

namespace pfaproject.Data
{
    public class MyContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Cooperative> Cooperatives { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Magasin> Magasins { get; set; }
        public DbSet<ClientFidele> clientFideles { get; set; }
        public DbSet<SMSSetting> SMSSettings { get; set; }
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure the relationship between Cooperative and ApplicationUser
            builder.Entity<Cooperative>()
                .HasKey(c => c.UserId);
            builder.Entity<ClientFidele>()
                .HasKey(c => c.UserId);

            builder.Entity<Cooperative>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Client>()
                .HasKey(c => c.UserId);
            builder.Entity<Client>()
                .HasOne(c => c.User)
                .WithOne()
                .HasForeignKey<Client>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ClientFidele>()
                .HasKey(cf => cf.UserId);

            builder.Entity<ClientFidele>()
                .HasOne(cf => cf.ApplicationUser)
                .WithOne()
                .HasForeignKey<ClientFidele>(cf => cf.UserId);

            SeedRoles(builder);
        }
        private static void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData
            (
                new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
                new IdentityRole() { Name = "Client", ConcurrencyStamp = "2", NormalizedName = "Client" },
                new IdentityRole() { Name = "Cooperative", ConcurrencyStamp = "3", NormalizedName = "Cooperative" }
            );
        }
    }
}
