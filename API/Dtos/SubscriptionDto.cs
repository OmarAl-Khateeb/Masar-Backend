using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class SubscriptionDto
    {
        public string Name { get; set; }
        public string Details { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }
        
    }
}