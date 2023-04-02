using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;
using static API.Entities._Enums;

namespace API.Entities
{
    public class Student
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string MotherFullName { get; set; }
        public string StudentPhotoUrl { get; set; }
        public string Address { get; set; }
        public string BirthPlace { get; set; }
        public string SchoolName { get; set; }
        public string DirectorateName { get; set; }
        public string Nationality { get; set; }
        public int ExamNumber { get; set; }
        public int PrepTotal { get; set; }
        public double PrepAverage { get; set; }
        public bool IsEvening { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime RegisteredDate { get; set; }
        public ICollection<Document> Documents { get; set; }
        public ICollection<Activity> Activities { get; set; }
        public ICollection<Note> Notes { get; set; }
        public ICollection<Installment> Installments { get; set; }
        public Genders Gender { get; set; }
        public AdmissionTypes AdmissionType { get; set; }
        public StudentTypes StudentType { get; set; }
        public StudentStatuses StudentStatus { get; set; }
        public Departments Department { get; set; }
        public SchoolBranches SchoolBranch { get; set; }
        public Colleges College { get; set; }
        public Religions Religion { get; set; }
        public Nationalismes Nationalism { get; set; }
        public MartialStatuses MartialStatus { get; set; }
        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }
        // public int GenderId { get => (int)this.Gender; set =>  Gender = (Genders)value; }// temp for testing
    }
}