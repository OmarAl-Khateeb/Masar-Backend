using Core.Entities;

namespace API.Entities
{
    public class Document : BaseEntity
    {
        public string Name { get; set; }
        public string DocumentUrl { get; set; }
        public string DocumentType { get; set; }//can be enum
        public string Tags { get; set; }
        public string Note { get; set; }
        public Activity Activity { get; set; }
        public Student Student { get; set; }
    }
}