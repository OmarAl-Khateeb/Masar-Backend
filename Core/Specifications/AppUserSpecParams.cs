using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class AppUserSpecParams : BaseSpecParams
    {
        public int? GymId { get; set; }
        public int? SubscriptionTypeId { get; set; }
        public bool SubscriptionExpired { get; set; }
    }
}