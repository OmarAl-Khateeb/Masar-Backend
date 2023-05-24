using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class TransactionTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Steps { get; set; }
        
    }
    public class TransactionTypeCDto
    {
        public string Name { get; set; }
        public List<string> Steps { get; set; }
        
    }
}