using System.Security.Claims;
using Core.Entities.Identity;
using Core.Specifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<AppUser> FindUserByClaimsPrincipleWithSubscription(this UserManager<AppUser> userManager, 
            ClaimsPrincipal user)
        {
            var id = int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            return await userManager.Users
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public static async Task<AppUser> FindByEmailFromClaimsPrincipal(this UserManager<AppUser> userManager, 
            ClaimsPrincipal user)
        {
            var id = int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            return await userManager.Users
                .SingleOrDefaultAsync(x => x.Id ==id);
        }
    }
}