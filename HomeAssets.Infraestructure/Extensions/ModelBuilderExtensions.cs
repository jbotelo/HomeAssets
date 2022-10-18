using HomeAssets.Infraestructure.ExtendedIdentity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HomeAssets.Infraestructure.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder builder)
        {
            var passwordHasher = new PasswordHasher<App_IdentityUser>();

            const string USERNAME = "superuser";
            const string EMAIL = "superuser@homeassets.test";
            const string PASSWORD = "$$$uperuser";

            string userId = new Guid().ToString();

            var user = new App_IdentityUser()
            {
                Id = userId,
                UserName = USERNAME,
                NormalizedUserName = USERNAME.ToUpper(),
                Email = EMAIL,
                NormalizedEmail = EMAIL.ToUpper(),
                EmailConfirmed = true,
                LockoutEnabled = true,
            };

            user.PasswordHash = passwordHasher.HashPassword(user, PASSWORD);

            builder.Entity<App_IdentityUser>().HasData(user);

            var claim = new IdentityUserClaim<string>()
            {
                Id = int.MaxValue,
                UserId = userId,
                ClaimType = "Role",
                ClaimValue = "admin1"
            };

            builder.Entity<IdentityUserClaim<string>>().HasData(claim);
        }
    }
}