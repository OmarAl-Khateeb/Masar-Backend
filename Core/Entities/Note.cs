
using Core.Entities;
using static API.Entities._Enums;
namespace API.Entities
{
    public class Note : BaseEntity
    {
        public string Title { get; set; }
        public string Subject { get; set; }
        public Student Student { get; set; }
        public UserType AssignedTo { get; set; }
    }
}