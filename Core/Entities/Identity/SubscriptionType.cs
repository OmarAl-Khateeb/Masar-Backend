namespace Core.Entities.Identity
{
    public class SubscriptionType : BaseEntity
    {
        public string Name { get; set; }
        public string Details { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }
        public int GymId { get; set; }
    }
}