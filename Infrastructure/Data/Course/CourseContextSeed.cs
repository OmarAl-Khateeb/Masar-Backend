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
                    Wieght = 200,
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
                var excerciseSet = new ExcerciseSet
                {
                    Reps = 10,
                    ExcerciseId = 1,
                };
                var excercisePlan = new ExcercisePlan
                {
                    Day = 1,
                    AppUserId = "1",
                    Excerciselist = new List<ExcerciseSet>()
                };
                course.Excercises.Add(excercisea);
                excercisePlan.Excerciselist.Add(excerciseSet);
                course.ExcercisePlans.Add(excercisePlan);
            }
        await course.SaveChangesAsync();
        }
        
    }
}