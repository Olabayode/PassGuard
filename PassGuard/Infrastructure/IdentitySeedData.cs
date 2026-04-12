using Microsoft.AspNetCore.Identity;
using PassGuard.DAL;

namespace PassGuard.Infrastructure
{
    public static class IdentitySeedData
    {
        private static readonly (string Role, string Email, string FullName, string Password)[] DefaultUsers =
        {
            ("Admin", "admin@passguard.local", "PassGuard Admin", "Admin123!"),
            ("HomeOwner", "homeowner@passguard.local", "Demo HomeOwner", "HomeOwner123!"),
            ("Security", "security@passguard.local", "Demo Security", "Security123!")
        };

        public static async Task SeedAsync(IServiceProvider services)
        {
            using IServiceScope scope = services.CreateScope();

            RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            UserManager<ApplicationUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            foreach (string role in new[] { "Admin", "HomeOwner", "Security" })
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            foreach ((string role, string email, string fullName, string password) in DefaultUsers)
            {
                ApplicationUser? user = await userManager.FindByEmailAsync(email);

                if (user == null)
                {
                    user = new ApplicationUser
                    {
                        UserName = email,
                        Email = email,
                        FullName = fullName,
                        EmailConfirmed = true,
                        MustChangePassword = role != "Admin"
                    };

                    IdentityResult createResult = await userManager.CreateAsync(user, password);

                    if (!createResult.Succeeded)
                    {
                        string errors = string.Join("; ", createResult.Errors.Select(e => e.Description));
                        throw new InvalidOperationException($"Failed to seed user '{email}': {errors}");
                    }
                }

                if (!await userManager.IsInRoleAsync(user, role))
                {
                    await userManager.AddToRoleAsync(user, role);
                }

                bool shouldForcePasswordChange = role != "Admin";

                if (user.MustChangePassword != shouldForcePasswordChange)
                {
                    user.MustChangePassword = shouldForcePasswordChange;
                    await userManager.UpdateAsync(user);
                }
            }
        }
    }
}
