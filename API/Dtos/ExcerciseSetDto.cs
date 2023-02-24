using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class ExcerciseSetDto
    {
        public int Reps { get; set; }
        public bool IsDone { get; set; } = false;
        public int ExcerciseId { get; set; }
    }
}