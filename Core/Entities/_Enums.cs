using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public static class _Enums
    {
        public enum UserType { Roller, Student }
        public enum Genders { Male, Female }
        public enum AdmissionTypes { Regular, Central, Direct, TeacherPermit, Distinguished, Elite, IslamicSchool, Martyrs, Talented, OutofIraq, Others }
        public enum StudentTypes { Graduate, Undergrad, Deleted }
        public enum StudentStatuses { Active, Dropped, Suspended, Deleted, Pending }//tbd
        public enum Departments { Network, Computer, Electric }//temp
        public enum SchoolBranches { Practical, Biologist, Literary, Islamic, Commerc, Industrial, Institute, Nursing }
        public enum Colleges { Law, Engineering, IslamicSciences, EconomicNManagement, EducationF, Education, Medicine, Dentistry, Media, Arts }
        public enum Religions { Muslim, Christian, Jewish, Sabian, Others }
        public enum Nationalismes { Arabic, Kurdish, Turkmen, Assyrian, Chaldean, Others }
        public enum MartialStatuses { Single, Married, Divorced, Widowed }
        public enum Statuses { Pending, Finished, Dropped}
        public enum ChannelTypes { Global, finished, }//need more thoughts
        // public enum TransactionType { Continuation, Registration, Request}//need more thoughts
    }
}