
using Core.Entities;
using static Core.Entities._Enums;
namespace Core.Entities
{
    public class Note : BaseEntity
    {
        public string Title { get; set; }
        public string Subject { get; set; }
        public Student Student { get; set; }
        public int StudentId { get; set; }
        
        // public UserType AssignedTo { get; set; }//should this even be a thing 
    }
}