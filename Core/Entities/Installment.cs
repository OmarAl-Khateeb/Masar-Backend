using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace API.Entities
{
    public class Installment : BaseEntity
    {
        public Student Student { get; set; }
        public double Amount { get; set; }
        public double Remaining { get; set; }
        public DateTime DeadlineDate { get; set; }
    }
}