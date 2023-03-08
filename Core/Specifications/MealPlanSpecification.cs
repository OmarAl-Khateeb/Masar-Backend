using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Meal;

namespace Core.Specifications
{
    public class MealPlanSpecification : BaseSpecification<MealPlan>
    {
        
        public MealPlanSpecification(string appUserId, int? day)
            : base(x =>
                (string.IsNullOrEmpty(appUserId) || x.AppUserId == appUserId) &&
                (!day.HasValue || x.Day == day)
            )
        {
            AddInclude(x => x.MealList.OrderBy(x=> x.Index));
        }
    }
}