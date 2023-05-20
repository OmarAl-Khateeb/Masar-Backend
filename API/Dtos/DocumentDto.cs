using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class DocumentDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Name { get; set; }
        public string DocumentUrl { get; set; }
        public string DocumentType { get; set; }
        public int StudentId { get; set; }
        public string Tags { get; set; }
        public string Note { get; set; }
    }
    public class DocumentCDto
    {
        // public string Name { get; set; }
        //asset id vs name?
        public string DocumentType { get; set; }
        public int StudentId { get; set; }
        public string Tags { get; set; }
        public string Note { get; set; }
        
        [SwaggerFileUpload]
        [Required(ErrorMessage = "Please upload a file")]
        public IFormFile File { get; set; }
    }
}