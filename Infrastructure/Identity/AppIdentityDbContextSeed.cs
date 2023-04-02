using Core.Entities;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser
                {
                    FullName = "Bob",
                    Email = "bob@test.com",
                    UserName = "bob@test.com",
                    Address = "basmayah",
                    PhoneNumber = "77493829839",
                    DateOfBirth = DateTime.UtcNow.AddYears(-32),

                };
                await userManager.CreateAsync(user, "Pa$$w0rd");
                var user2 = new AppUser
                {
                    FullName = "Bob2",
                    Email = "bob2@test.com",
                    UserName = "bob2@test.com",
                    Address = "bob2@test.com",
                    PhoneNumber = "77493324432",
                    DateOfBirth = DateTime.UtcNow.AddYears(-32),
                };

                await userManager.CreateAsync(user2, "Pa$$w0rd");
            }
        }
    }
}