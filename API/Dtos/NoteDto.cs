using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class NoteDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public int StudentId { get; set; }
        
    }
    public class NoteCDto
    {
        public string Title { get; set; }
        public string Subject { get; set; }
        public int StudentId { get; set; }
        
    }
}