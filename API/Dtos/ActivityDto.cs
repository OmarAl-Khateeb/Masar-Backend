using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using static Core.Entities._Enums;

namespace API.Dtos
{
    public class ActivityDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Name { get; set; }
        public string ActivityType { get; set; }
        public int StudentId { get; set; }
        public string Note { get; set; }
        public string Tags { get; set; }
        public DateTime DeadLine { get; set; }
        public List<DocumentDto> Documents { get; set; }
        public Statuses Status { get; set; }
        
    }
    
    public class ActivityCDto
    {
        public string Name { get; set; }
        public string ActivityType { get; set; }
        public int StudentId { get; set; }
        public string Note { get; set; }
        public string Tags { get; set; }
        public DateTime DeadLine { get; set; }
        public Statuses Status { get; set; }
        
        [SwaggerFileUpload]
        public List<IFormFile> Files { get; set; }
        
    }
}