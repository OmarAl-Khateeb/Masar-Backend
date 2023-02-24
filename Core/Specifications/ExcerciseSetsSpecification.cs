using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Excercise;

namespace Core.Specifications
{
    public class ExcerciseSetsSpecification : BaseSpecification<ExcerciseSet>
    {
        
        public ExcerciseSetsSpecification()
        {
        }

        public ExcerciseSetsSpecification(int id) : base(x => x.Id == id)
        {
        }
    }
}