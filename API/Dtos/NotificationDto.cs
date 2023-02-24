using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public string AppUserId { get; set; }
        
    }
    public class NotificationCDto
    {
        public string Name { get; set; }
        public string Details { get; set; }
        public string AppUserId { get; set; }
        
    }
}