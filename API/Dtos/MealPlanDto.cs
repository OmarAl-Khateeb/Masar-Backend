using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class MealPlanDto
    {
        public int Day { get; set; }
        public int CaloriesTotal { get; set; }
        public int AppUserId { get; set; }
        public List<MealDto> MealList { get; set; }
    }

}