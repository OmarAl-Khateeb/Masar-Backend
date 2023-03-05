using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Hieght { get; set; }
        public int Wieght { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime SubscriptionExpDate { get; set; }
        public SubscriptionType SubscriptionType { get; set; }
        public int SubscriptionTypeId { get; set; }
        public int GymId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}