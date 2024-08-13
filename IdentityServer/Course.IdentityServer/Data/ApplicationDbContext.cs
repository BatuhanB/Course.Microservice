using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Course.IdentityServer.Models;

namespace Course.IdentityServer.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<CardInformation> CardInformations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>()
                .HasMany(x => x.Addresses)
                .WithOne(y => y.User)
                .HasForeignKey(f => f.UserId)
                .HasPrincipalKey(a => a.Id);

            builder.Entity<ApplicationUser>()
                .HasMany(x => x.CardInformations)
                .WithOne(y => y.User)
                .HasForeignKey(c => c.UserId)
                .HasPrincipalKey(a => a.Id);

            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}