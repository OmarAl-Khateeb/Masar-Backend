using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities.Excercise
{
    public class ExcerciseSet : BaseEntity
    {
        public int Index { get; set; }
        public int Reps { get; set; }
        public int Count { get; set; }
        public int Duration { get; set; }
        public bool IsDone { get; set; } = false;
        public int ExcerciseId { get; set; }
        public int ExcercisePlanId { get; set; }
    }
}