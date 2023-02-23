using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Meal;

namespace Core.Specifications
{
    public class MealsSpecification : BaseSpecification<Meal>
    {
        
        public MealsSpecification()
        {
        }

        public MealsSpecification(int id) : base(x => x.Id == id)
        {
        }
    }
}