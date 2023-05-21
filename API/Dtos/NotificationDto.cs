using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using static Core.Entities._Enums;

namespace API.Dtos
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        
        // public Student Student { get; set; }
        public int StudentId { get; set; }
        public string DocumentUrl { get; set; }
        public string RollerName { get; set; }
        public Departments Department { get; set; }
        public int Stage { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        
    }
    public class NotificationCDto
    {
        public int StudentId { get; set; }
        public Departments Department { get; set; }
        public int Stage { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        
        [SwaggerFileUpload]
        public IFormFile File { get; set; }
    }
}