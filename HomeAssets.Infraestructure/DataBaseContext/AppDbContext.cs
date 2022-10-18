using HomeAssets.Domain.Models;
using HomeAssets.Infraestructure.ExtendedIdentity;
using HomeAssets.Infraestructure.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HomeAssets.Infraestructure.DataBaseContext
{
    public class AppDbContext : IdentityDbContext<App_IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<HomeService> HomeServices { get; set; }
        public DbSet<AuthorizedEmail> AuthorizedEmails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Seed();

            foreach (var foreignKey in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}