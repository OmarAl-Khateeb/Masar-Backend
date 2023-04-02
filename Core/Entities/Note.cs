
using static API.Entities._Enums;
namespace API.Entities
{
    public class Note
    {
        public string Title { get; set; }
        public string Subject { get; set; }
        public string Id { get; set; }
        public Student Student { get; set; }
        public string StudentId { get; set; }
        public DateTime CreatedDate { get; set; }
        public UserType AssignedTo { get; set; }
    }
}