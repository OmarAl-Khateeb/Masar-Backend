using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Meal;

namespace Infrastructure.Data.Course
{
    public class CourseContextSeed
    {
        public static async Task SeedMealAsync(CourseContext course)
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
                    CaloriesTotal = 4000,
                    AppUserId = 1,
                    MealList = new List<Meal>()
                };
                mealPlan.MealList.Add(meala);
                course.MealPlans.Add(mealPlan);
            }
        await course.SaveChangesAsync();
        }
        
    }
}