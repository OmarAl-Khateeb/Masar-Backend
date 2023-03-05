namespace API.Dtos
{
    public class UserDto
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }
        public int Hieght { get; set; }
        public int Wieght { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime SubscriptionExpDate { get; set; }
        public string SubscriptionType { get; set; }
        public int GymId { get; set; }
    }

    
    public class UserTokenDto
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Token { get; set; }
        public int GymId { get; set; }
    }
}