using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities.Excercise
{
    public class ExcercisePlan : BaseEntity
    {
        public string AppUserId { get; set; }
        public int Day { get; set; }
        public List<ExcerciseSet> Excerciselist { get; set; }
    }
}