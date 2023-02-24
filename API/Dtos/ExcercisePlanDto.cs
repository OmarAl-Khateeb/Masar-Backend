using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Excercise;

namespace API.Dtos
{
    public class ExcercisePlanDto
    {
        public List<ExcerciseSetDto> Excerciselist { get; set; }
        public int Day { get; set; }
        public string AppUserId { get; set; }
        
    }
}