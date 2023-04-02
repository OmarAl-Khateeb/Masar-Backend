using Core.Entities;
using static API.Entities._Enums;

namespace API.Entities
{
    public class Activity : BaseEntity
    {
        public string Name { get; set; }
        public string ActivityType { get; set; }
        public Student Student { get; set; }
        public string Note { get; set; }
        public string Tags { get; set; }
        public DateTime DeadLine { get; set; }
        public List<Document> Documents { get; set; }
        public ActivityStatuses ActivityStatus { get; set; }
    }
}