using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class MealDto
    {
        
        public string Name { get; set; }
        public string Description { get; set; }
        public int Calories { get; set; }
        public int Wieght { get; set; }
    }
}