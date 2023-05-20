using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class DocumentSpecification : BaseSpecification<Document>
    {
        public DocumentSpecification(DocumentSpecParams DocumentParams)
            // : base(x => 
            // (!DocumentParams.Type.HasValue || (int) x.Type == DocumentParams.Type) &&
            // (!DocumentParams.Status.HasValue || (int) x.Status == DocumentParams.Status)
            // )
        {
            
        }

        public DocumentSpecification(int id) : base(x => x.Id == id)
        {
        }
    }
}