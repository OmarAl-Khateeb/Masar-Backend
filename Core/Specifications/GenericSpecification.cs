using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class GenericSpecification<T> : BaseSpecification<T> where T: BaseEntity
    {
        public GenericSpecification()
        {
        }

        public GenericSpecification(int id) : base(x => x.Id == id)
        {
        }
    }
}