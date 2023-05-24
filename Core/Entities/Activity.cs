using Core.Entities;
using static Core.Entities._Enums;

namespace Core.Entities
{
    public class Activity : BaseEntity
    {
        public string Name { get; set; }
        public string ActivityType { get; set; }
        public Student Student { get; set; }
        public int StudentId { get; set; }
        public string Note { get; set; }
        public string Tags { get; set; }
        
        // public DateTime DeadLine { get; set; }
        public List<Document> Documents { get; set; }
        // public Statuses Status { get; set; }
    }
}