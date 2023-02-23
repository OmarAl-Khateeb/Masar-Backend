using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities.Meal
{
    public class MealPlan : BaseEntity
    {
        public List<Meal> MealList { get; set; }
        public int Day { get; set; }
        public int CaloriesTotal { get; set; }
        public int AppUserId { get; set; }
    }
}