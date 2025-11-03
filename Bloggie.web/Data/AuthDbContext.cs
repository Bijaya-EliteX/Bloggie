using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace Bloggie.web.Data
{
    public class AuthDbContext : IdentityDbContext

    {
        public AuthDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //Define unique GUIDs for roles(Important for consistency)
            var adminRoleId = "831b3f78-6eba-46fd-8977-03c6bb5a5ddc";
            var superAdminRoleId = "7b4f187c-f4fa-43d2-a2f5-c1ac1d0d4326";
            var userRoleId = "d306e792-f2f5-4bac-beca-d6c0ecb80d6c";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpperInvariant(),
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin".ToUpperInvariant(),
                    Id = superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "User".ToUpperInvariant(),
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId
                }
            };
            // Insert roles into the IdentityRole table
            builder.Entity<IdentityRole>().HasData(roles);


            // Seed SuperAdmin User
            var superAdminId = "7afb1aa2-df21-4729-973d-6949f7ffec6e";
            var superAdminUser = new IdentityUser
            {
                UserName = "superadmin@bloggie.com",
                Email = "superadmin@bloggie.com",
                NormalizedEmail = "superadmin@bloggie.com".ToUpper(),
                NormalizedUserName = "superadmin@bloggie.com".ToUpper(),
                Id = superAdminId
            };
            // Hash the default password for the SuperAdmin user
            var passwordHasher = new PasswordHasher<IdentityUser>();
            superAdminUser.PasswordHash = passwordHasher.HashPassword(superAdminUser, "SuperAdminPassword123!");
            //This seeds  SuperAdmin account into the database when migrations are run.
            builder.Entity<IdentityUser>().HasData(superAdminUser);

            // Insert the SuperAdmin user into the IdentityUser table
            builder.Entity<IdentityUser>().HasData(superAdminUser);

            // Add all roles to SuperAdmin User (mapping table entries)
            var superAdminRoles = new List<IdentityUserRole<string>> // This model represents the join table(AspNetUserRoles) that links users to their roles.
            {
                // Map Admin Role
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = superAdminId
                },
                // Map SuperAdmin Role
                new IdentityUserRole<string>
                {
                    RoleId = superAdminRoleId,
                    UserId = superAdminId
                },
                // Map User Role
                new IdentityUserRole<string>
                {
                    RoleId = userRoleId,
                    UserId = superAdminId
                }
            };

            // Insert the role mappings into the IdentityUserRole table
            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);




        }
    }
}
