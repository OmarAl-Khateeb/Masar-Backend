using static Core.Entities._Enums;

namespace Core.Specifications
{
    public class NotificationSpecParams : BaseSpecParams
    {
        public int? Stage { get; set; }
        public Departments? Department { get; set; }
        //student and notification spec param has same values currently
    }
}