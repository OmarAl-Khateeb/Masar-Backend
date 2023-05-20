using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //should use usertype? or approle
    }
}