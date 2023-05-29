using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.Identity;
using static Core.Entities._Enums;

namespace API.Dtos
{
    public class StudentDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string FullName { get; set; }
        public string LastName { get; set; }
        public string MotherFullName { get; set; }
        public string StudentPhotoUrl { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
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
        public DateTime RegisteredDate { get; set; }
        public string Gender { get; set; }
        public string AdmissionType { get; set; }
        public string StudentType { get; set; }
        public string StudentStatus { get; set; }
        public string Department { get; set; }
        public string SchoolBranch { get; set; }
        public string College { get; set; }
        public string Religion { get; set; }
        public string Nationalism { get; set; }
        public string MartialStatus { get; set; }
        public List<DocumentDto> Documents { get; set; }
        public List<ActivityDto> Activities { get; set; }
        public List<NoteDto> Notes { get; set; }
        // public List<Installment> Installments { get; set; }
        public UserDto AppUser { get; set; }
        
    }
}