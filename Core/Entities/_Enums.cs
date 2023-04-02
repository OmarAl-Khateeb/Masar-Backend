using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public static class _Enums
    {
        public enum UserType { Roller, Student }
        public enum Genders { Male, Female }
        public enum AdmissionTypes { General, Central, Direct, TeacherPermit, Distinguished, Elite, IslamicSchool, Martyrs, Talented, OutofIraq, Others }
        public enum StudentTypes { Graduate, Undergrad, Deleted }
        public enum StudentStatuses { Active, Dropped, Suspended, Deleted }//tbd
        public enum Departments { }//temp
        public enum SchoolBranches { Practical, Biologist, Literary, Islamic, Commerc, Industrial, Institute, Nursing }
        public enum Colleges { Law, Engineering, IslamicSciences, EconomicNManagement, EducationF, Education, Medicine, Dentistry, Media, Arts }
        public enum Religions { Muslim, Christian, Jewish, Sabian, Others }
        public enum Nationalismes { Arabic, Kurdish, Turkmen, Assyrian, Chaldean, Others }
        public enum MartialStatuses { Single, Married, Divorced, Widowed }
        public enum ActivityStatuses { pending, finished, }
    }
}