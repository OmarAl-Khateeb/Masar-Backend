using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Installment
    {
        public string Id { get; set; }
        public string StudentId { get; set; }
        public Student Student { get; set; }
        public double Amount { get; set; }
        public double Remaining { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DeadlineDate { get; set; }
    }
}