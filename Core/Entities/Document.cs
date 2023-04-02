namespace API.Entities
{
    public class Document
    {
        public string Name { get; set; }
        public string DocumentUrl { get; set; }
        public string MyProperty { get; set; }
        public string DocumentType { get; set; }//can be enum
        public string Tags { get; set; }
        public string Id { get; set; }
        public string Note { get; set; }
        public string StudentId { get; set; }
        public Student Student { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}