using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Excercise;
using Core.Entities.Meal;

namespace Infrastructure.Data.Course
{
    public class CourseContextSeed
    {
        public static async Task SeedCourseAsync(CourseContext course)
        {
            if (!course.MealPlans.Any())
            {
                var meala = new Meal
                {
                    Name = "CHICKEN",
                    Description = "food",
                    Calories = 4000,
                    Weight = 200,
                };
                var mealPlan = new MealPlan
                {
                    AppUserId = "string",
                    MealList = new List<Meal>()
                };
                mealPlan.MealList.Add(meala);
                course.MealPlans.Add(mealPlan);
            }
            if (!course.ExcercisePlans.Any())
            {
                var excercisea = new Excercise
                {
                    Name = "Push Up",
                    Details = "you dont know what push ups are?",
                    ImageUrl = "https://static.vecteezy.com/system/resources/previews/000/162/096/non_2x/man-doing-push-up-vector-illustration.jpg"
                };
                var excerciseSet1 = new ExcerciseSet
                {
                    Index = 1,
                    Reps = "10, 10, 12, 12",
                    ExcerciseId = 1,
                };
                var excerciseSet2 = new ExcerciseSet
                {
                    Index = 2,
                    Reps = "10, 10, 12, 12",
                    ExcerciseId = 1,
                };
                var excerciseSet3 = new ExcerciseSet
                {
                    Index = 3,
                    Reps = "10, 10, 12, 12",
                    ExcerciseId = 1,
                };
                var excerciseSet4 = new ExcerciseSet
                {
                    Index = 4,
                    Reps = "10, 10, 12, 12",
                    ExcerciseId = 1,
                };
                var excerciseSet5 = new ExcerciseSet
                {
                    Index = 1,
                    Reps = "10, 10, 12, 12",
                    ExcerciseId = 1,
                };
                var excerciseSet6 = new ExcerciseSet
                {
                    Index = 2,
                    Reps = "10, 10, 12, 12",
                    ExcerciseId = 1,
                };
                var excerciseSet7 = new ExcerciseSet
                {
                    Index = 3,
                    Reps = "10, 10, 12, 12",
                    ExcerciseId = 1,
                };
                var excerciseSet8 = new ExcerciseSet
                {
                    Index = 4,
                    Reps = "10, 10, 12, 12",
                    ExcerciseId = 1,
                };
                var excercisePlan = new ExcercisePlan
                {
                    Day = 1,
                    AppUserId = "e36c3064-b1e0-493b-a9b5-dcba59c160fc",
                    ExcerciseList = new List<ExcerciseSet>()
                };
                course.Excercises.Add(excercisea);
                excercisePlan.ExcerciseList.Add(excerciseSet1);
                excercisePlan.ExcerciseList.Add(excerciseSet2);
                excercisePlan.ExcerciseList.Add(excerciseSet3);
                excercisePlan.ExcerciseList.Add(excerciseSet4);
                course.ExcercisePlans.Add(excercisePlan);
                
                await course.SaveChangesAsync();
                var excercisePlan2 = new ExcercisePlan
                {
                    Day = 2,
                    AppUserId = "e36c3064-b1e0-493b-a9b5-dcba59c160fc",
                    ExcerciseList = new List<ExcerciseSet>()
                };
                excercisePlan2.ExcerciseList.Add(excerciseSet5);
                excercisePlan2.ExcerciseList.Add(excerciseSet6);
                excercisePlan2.ExcerciseList.Add(excerciseSet7);
                excercisePlan2.ExcerciseList.Add(excerciseSet8);
                course.ExcercisePlans.Add(excercisePlan2);
            }
        await course.SaveChangesAsync();
        }
        
    }
}