using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using static API.Entities._Enums;

namespace Core.Entities
{
    public class Transaction : BaseEntity
    {
        public Student Student { get; set; }
        public string RollerId { get; set; }
        public TransactionType Type { get; set; }
    }
}