using static API.Entities._Enums;

namespace API.Entities
{
    public class Activity
    {
        public string Name { get; set; }
        public string ActivityType { get; set; }
        public string StudentId { get; set; }
        public Student Student { get; set; }
        public string Id { get; set; }
        public string Note { get; set; }
        public string Tags { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DeadLine { get; set; }
        public ICollection<Document> Documents { get; set; }
        public ActivityStatuses ActivityStatus { get; set; }
    }
}