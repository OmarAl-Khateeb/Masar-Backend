namespace API.Dtos
{
    public class UserDto
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Token { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }
        public int Hieght { get; set; }
        public int Wieght { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime SubscriptionExpDate { get; set; }
        public int SubscriptionTypeId { get; set; }
        public int GymId { get; set; }
    }
}