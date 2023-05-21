using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.Identity;
using static Core.Entities._Enums;

namespace Core.Entities
{
    public class Notification : BaseEntity
    {
        public Document Document { get; set; }
        public Student Student { get; set; }
        public int StudentId { get; set; } //0 for ignoring it?
        // public ChannelTypes ChannelType { get; set; }// need to specify how channels lead to Stage/department combinations
        public AppUser User { get; set; }
        public int UserId { get; set; }
        public Departments Department { get; set; }
        public int Stage { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
    }
}