using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Notification : BaseEntity
    {
        public string Name { get; set; }
        public string Details { get; set; }
        public string AppUserId { get; set; }
    }
}