using static Core.Entities._Enums;

namespace Core.Specifications
{
    public class StudentSpecParams : BaseSpecParams
    {
        public int? Stage { get; set; }
        public Departments? Department { get; set; }
    }
}