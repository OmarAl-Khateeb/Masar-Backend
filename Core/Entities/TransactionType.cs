using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class TransactionType : BaseEntity
    {
        public string Name { get; set; }
        public List<string> Steps { get; set; }

    }
}