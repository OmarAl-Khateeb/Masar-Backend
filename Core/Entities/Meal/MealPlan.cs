using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities.Meal
{
    public class MealPlan : BaseEntity
    {
        public List<Meal> MealList { get; set; } = new();
        public int Day { get; set; }
        public string AppUserId { get; set; }
        public decimal GetTotal()
        {
            return MealList.Sum(m => m.Calories);
        }
    }
}