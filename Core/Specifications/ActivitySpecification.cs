using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class ActivitySpecification : BaseSpecification<Activity>
    {
        public ActivitySpecification(ActivitySpecParams ActivityParams)
            : base(x => 
            // (!ActivityParams.Type.HasValue || (int) x.Type == ActivityParams.Type) &&
            (!ActivityParams.Status.HasValue || (int) x.Status == ActivityParams.Status)
            )
        {
            AddInclude(x => x.Documents.OrderBy(x=> x.Id));
        }

        public ActivitySpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Documents.OrderBy(x=> x.Id));
        }
    }
}