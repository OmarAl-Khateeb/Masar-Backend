using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Core.Entities._Enums;
using Swashbuckle.AspNetCore;
using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SwaggerFileUploadAttribute : Attribute {}

    public class StudentCDto
    {
        public string FullName { get; set; }
        public string LastName { get; set; }
        public string MotherFullName { get; set; }
        // public string Address { get; set; }
        public string EmailAddress { get; set; }
        public string BirthPlace { get; set; }
        public string SchoolName { get; set; }
        public string DirectorateName { get; set; }
        public string Nationality { get; set; }
        public int ExamNumber { get; set; }
        public int PrepTotal { get; set; }
        public int Stage { get; set; }
        public double PrepAverage { get; set; }
        public bool IsEvening { get; set; }
        public DateTime BirthDate { get; set; }
        public Genders Gender { get; set; }
        public AdmissionTypes AdmissionType { get; set; }
        public StudentTypes StudentType { get; set; }
        public Departments Department { get; set; }
        public SchoolBranches SchoolBranch { get; set; }
        public Colleges College { get; set; }
        public Religions Religion { get; set; }
        public Nationalismes Nationalism { get; set; }
        public MartialStatuses MartialStatus { get; set; }
        

        [SwaggerFileUpload]
        [Required(ErrorMessage = "Please upload a file")]
        public IFormFile Photo { get; set; }
        public IFormFile IdCard { get; set; }
        public IFormFile AddressCard { get; set; }
        public IFormFile RationCard { get; set; }
        
    }
}