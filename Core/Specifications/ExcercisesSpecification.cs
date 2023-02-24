using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Excercise;

namespace Core.Specifications
{
    public class ExcercisesSpecification : BaseSpecification<Excercise>
    {
        
        public ExcercisesSpecification()
        {
        }

        public ExcercisesSpecification(int id) : base(x => x.Id == id)
        {
        }
    }
}