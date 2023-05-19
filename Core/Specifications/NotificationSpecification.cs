using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class NotificationSpecification : BaseSpecification<Notification>
    {
        public NotificationSpecification(NotificationSpecParams NotificationParams)
            : base(x => 
            (!NotificationParams.Stage.HasValue || x.Stage == NotificationParams.Stage) &&
            (!NotificationParams.Department.HasValue || x.Department == NotificationParams.Department)
            )
        {
            AddInclude(x => x.Document);
            
        }

        public NotificationSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Document);
        }
    }
}