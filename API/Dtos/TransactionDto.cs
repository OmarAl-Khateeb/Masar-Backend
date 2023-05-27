using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using static Core.Entities._Enums;

namespace API.Dtos
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int StudentId { get; set; }
        public string RollerId { get; set; }
        public string DocumentUrl { get; set; }
        public TransactionType Type { get; set; }
        public Statuses Status { get; set; }
        public int Index { get; set; }
        
    }
    public class TransactionCDto
    {
        public int StudentId { get; set; }

        // public TransactionType Type { get; set; } 
        public int TypeId { get; set; } 
        
        [SwaggerFileUpload]
        public IFormFile File { get; set; }
    }
    public class TransactionCUDto
    {
        public int TypeId { get; set; } 
        
        [SwaggerFileUpload]
        public IFormFile File { get; set; }
    }
}