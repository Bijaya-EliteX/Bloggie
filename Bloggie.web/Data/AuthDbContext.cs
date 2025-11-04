using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.web.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Define unique GUIDs for roles (Important for consistency)
            var adminRoleId = "831b3f78-6eba-46fd-8977-03c6bb5a5ddc";
            var superAdminRoleId = "7b4f187c-f4fa-43d2-a2f5-c1ac1d0d4326";
            var userRoleId = "d306e792-f2f5-4bac-beca-d6c0ecb80d6c";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SUPERADMIN",
                    Id = superAdminRoleId,
                    ConcurrencyStamp = superAdminRoleId
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER",
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
                Id = superAdminId,
                UserName = "superadmin@bloggie.com",
                Email = "superadmin@bloggie.com",
                NormalizedEmail = "SUPERADMIN@BLOGGIE.COM",
                NormalizedUserName = "SUPERADMIN@BLOGGIE.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAIAAYagAAAAEHeDU7Sg/HcIT6h7hutcdtbwMgIzfTIWuMwOFlOODE44pXHPy1zz122IHu7dTvroUYw==", // This is hashed "SuperAdmin@123"
                SecurityStamp = "JVOECVYX4SKHTLZ7YJ3VQZPVKI35XOSN",
                ConcurrencyStamp = "c8554266-b401-4519-8274-8001b8b8b937",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0
            };

            // Seed the user
            builder.Entity<IdentityUser>().HasData(superAdminUser);

            // Add all roles to SuperAdmin User (mapping table entries)
            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = superAdminRoleId,
                    UserId = superAdminId
                },
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