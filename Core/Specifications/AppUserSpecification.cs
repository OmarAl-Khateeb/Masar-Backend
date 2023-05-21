using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;

namespace Core.Specifications
{
    public class AppUserSpecification : BaseSpecification<AppUser>
    {
        public AppUserSpecification(AppUserSpecParams specParams)
            : base(x =>
                (string.IsNullOrEmpty(specParams.Search) || x.FullName.ToLower().Contains(specParams.Search))) // apply the filter based on SubscriptionExpired?
        {
            ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort.ToLower())
                {
                    case "createddesc":
                        AddOrderByDescending(x => x.CreatedAt);
                        break;
                    case "created":
                        AddOrderBy(x => x.CreatedAt);
                        break;
                    default:
                        AddOrderByDescending(x => x.FullName);
                        break;
                }
            }
            else
            {
                AddOrderByDescending(x => x.FullName);
            }
        }

        public AppUserSpecification(int id)
            : base(x => x.Id == id)
        {
        }
    }
}