using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Excercise;
using Core.Entities.Meal;

namespace Core.Specifications
{
    public class ExcercisePlanSpecification : BaseSpecification<ExcercisePlan>
    {
        public ExcercisePlanSpecification(string appUserId, int? day)
            : base(x =>
                (string.IsNullOrEmpty(appUserId) || x.AppUserId == appUserId) &&
                (!day.HasValue || x.Day == day)
            )
        {
            AddInclude(x => x.ExcerciseList.OrderBy(x=> x.Index));
        }
    }
}