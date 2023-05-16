using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using static Core.Entities._Enums;

namespace Core.Entities
{
    public class Notification : BaseEntity
    {
        public Student Student { get; set; }
        public ChannelTypes ChannelType { get; set; }// need to specify how channels lead to Stage/department combinations
        public string Title { get; set; }
        public string Subject { get; set; }
    }
}