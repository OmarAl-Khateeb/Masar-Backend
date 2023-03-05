using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities.Meal
{
    public class Meal : BaseEntity
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Calories { get; set; }
        public int Weight { get; set; }
        public int MealPlanId { get; set; }
    }
}