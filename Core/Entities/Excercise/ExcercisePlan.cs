using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities.Excercise
{
    public class ExcercisePlan : BaseEntity
    {
        public List<ExcerciseSession> Excerciselist { get; set; }
        public int AppUserId { get; set; }
    }
}