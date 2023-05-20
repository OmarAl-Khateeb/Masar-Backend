using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class NoteSpecification : BaseSpecification<Note>
    {
        public NoteSpecification(int StudentId) : base(x => (x.StudentId == StudentId)) {}
    }
}