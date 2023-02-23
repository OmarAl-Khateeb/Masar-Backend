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
                var gym = new Gym
                {
                    Name = "GoGym",
                    ImageUrl = "some/some"
                };
                var user = new AppUser
                {
                    FullName = "Bob",
                    Email = "bob@test.com",
                    UserName = "bob@test.com",
                    DateOfBirth = DateTime.Today.AddYears(-32),
                    Hieght = 180,
                    Wieght = 100,
                    PhotoUrl = "some/some",
                    GymId = 1,
                    SubscriptionExpDate = DateTime.UtcNow.AddMonths(1),

                    SubscriptionType = new SubscriptionType
                    {
                        Name = "Standard",
                        Details = "the basic",
                        Duration = 30,
                        Price = 30000,
                        GymId = 1
                    }
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
            }
        }
    }
}