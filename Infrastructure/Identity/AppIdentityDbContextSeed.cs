using Core.Entities;
using Core.Entities.Identity;
using Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Identity;
using static Core.Entities._Enums;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            if (!userManager.Users.Any())
            {
                var roles = new List<AppRole>
                {
                    new AppRole{Name = "Student"},
                    new AppRole{Name = "Admin"},
                    new AppRole{Name = "Moderator"},
                };
                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(role);
                }
                var user = new AppUser
                {
                    FullName = "Bob",
                    Email = "bob@test.com",
                    UserName = "bob@test.com",
                    Address = "basmayah",
                    PhoneNumber = "77493829839",
                    DateOfBirth = DateTime.UtcNow.AddYears(-32),
                    // StudentId = 1,

                };
                await userManager.CreateAsync(user, "Pa$$w0rd");
                await userManager.AddToRoleAsync(user, "Student");
                var user2 = new AppUser
                {
                    FullName = "Bob2",
                    Email = "bob2@test.com",
                    UserName = "bob2@test.com",
                    Address = "bob2@test.com",
                    PhoneNumber = "77493324432",
                    DateOfBirth = DateTime.UtcNow.AddYears(-32),
                    // StudentId = 2,
                };

                await userManager.CreateAsync(user2, "Pa$$w0rd");
                await userManager.AddToRoleAsync(user2, "Admin");
            }
        }
        
        public static async Task SeedAsync(AppIdentityDbContext context)
        {
            if (!context.Students.Any())
            {
                List<string>  Steps = new List<string>();
                Steps.Add("Hello");
                Steps.Add("World");
                Steps.Add("OpenAI");
                Steps.Add("C#");
                var transactiontype = new TransactionType
                {
                    Name = "Registration",
                    
                    Steps = new List<string>(Steps),
                };
                var students = new List<Student>
                {
                    new Student
                    {
                        FullName = "John Smith",
                        LastName = "Smith",
                        MotherFullName = "Jane Smith",
                        StudentPhotoUrl = "https://i1.sndcdn.com/avatars-UbwhC4k2b9NaThnr-sdRlKw-t500x500.jpg",
                        Address = "123 Main St, Anytown USA",
                        BirthPlace = "Anytown USA",
                        SchoolName = "Anytown High School",
                        DirectorateName = "Anytown School District",
                        Nationality = "American",
                        ExamNumber = 123456,
                        PrepTotal = 95,
                        Stage = 12,
                        PrepAverage = 9.5,
                        IsEvening = false,
                        BirthDate = new DateTime(2002, 1, 1,0,0,0,DateTimeKind.Utc),
                        RegisteredDate = DateTime.UtcNow,
                        Gender = Genders.Male,
                        AdmissionType = AdmissionTypes.Regular,
                        StudentType = StudentTypes.Graduate,
                        StudentStatus = StudentStatuses.Active,
                        Department = Departments.Network,
                        SchoolBranch = SchoolBranches.Practical,
                        College = Colleges.Engineering,
                        Religion = Religions.Christian,
                        Nationalism = Nationalismes.Arabic,
                        MartialStatus = MartialStatuses.Single,
                    },
                    new Student
                    {
                        FullName = "Jane Doe",
                        LastName = "Doe",
                        MotherFullName = "Mary Doe",
                        StudentPhotoUrl = "https://i1.sndcdn.com/avatars-UbwhC4k2b9NaThnr-sdRlKw-t500x500.jpg",
                        Address = "456 Elm St, Anytown USA",
                        BirthPlace = "Anytown USA",
                        SchoolName = "Anytown High School",
                        DirectorateName = "Anytown School District",
                        Nationality = "American",
                        ExamNumber = 789012,
                        PrepTotal = 92,
                        Stage = 11,
                        PrepAverage = 9.2,
                        IsEvening = false,
                        BirthDate = new DateTime(2003, 2, 2,0,0,0,DateTimeKind.Utc),
                        RegisteredDate = DateTime.UtcNow,
                        Gender = Genders.Female,
                        AdmissionType = AdmissionTypes.Regular,
                        StudentType = StudentTypes.Graduate,
                        StudentStatus = StudentStatuses.Active,
                        Department = Departments.Network,
                        SchoolBranch = SchoolBranches.Practical,
                        College = Colleges.Engineering,
                        Religion = Religions.Muslim,
                        Nationalism = Nationalismes.Arabic,
                        MartialStatus = MartialStatuses.Single
                    }
                    // add more students as necessary
                };

                context.TransactionTypes.AddRange(transactiontype);
                context.Students.AddRange(students);
                await context.SaveChangesAsync();
            }
        }
    }
}