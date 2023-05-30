using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.Identity;
using static Core.Entities._Enums;

namespace Core.Entities
{
    public class Transaction : BaseEntity
    {
        public Student Student { get; set; }
        public int StudentId { get; set; }
        public AppUser Roller { get; set; }
        public int RollerId { get; set; }
        public Document Document { get; set; }
        public TransactionType Type { get; set; }
        public int TypeId { get; set; }
        public Statuses Status { get; set; }
        public int Index { get; set; } = 0;
    }
}