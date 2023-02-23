using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities.Excercise
{
    public class ExcerciseSession : BaseEntity
    {
        public int Day { get; set; }
        public int SetNum { get; set; }
        public bool IsDone { get; set; }
        public int ExcerciseId { get; set; }
        public int ExcercisePlanId { get; set; }
    }
}