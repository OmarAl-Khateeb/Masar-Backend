using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class TransactionSpecification : BaseSpecification<Transaction>
    {
        public TransactionSpecification(TransactionSpecParams transactionParams)
            : base(x => 
            (!transactionParams.Type.HasValue || (int) x.TypeId == transactionParams.Type) &&
            (!transactionParams.Status.HasValue || (int) x.Status == transactionParams.Status)
            )
        {
            AddInclude(x => x.Document);
            
        }
        public TransactionSpecification(TransactionSpecParams transactionParams, int Id)
            : base(x => 
            (!transactionParams.Type.HasValue || (int) x.TypeId == transactionParams.Type) &&
            (!transactionParams.Status.HasValue || (int) x.Status == transactionParams.Status) &&
            (x.StudentId == Id)
            )
        {
            AddInclude(x => x.Document);
            
        }

        public TransactionSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Document);
        }
    }
}