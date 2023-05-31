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
            (string.IsNullOrEmpty(ActivityParams.Search) || x.Name.ToLower().Contains(ActivityParams.Search)) && 
            (string.IsNullOrEmpty(ActivityParams.ActivityType) || x.Name.ToLower().Contains(ActivityParams.ActivityType)) 
            )
        {
            AddInclude(x => x.Documents.OrderBy(x=> x.Id));
            AddInclude(x => x.Student);
        }

        public ActivitySpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Documents.OrderBy(x=> x.Id));
            AddInclude(x => x.Student);
        }
    }
}