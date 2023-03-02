using System.Text.Json;
using Core.Entities;
using Core.Entities.Meal;
using Core.Entities.OrderAggregate;
using Infrastructue.Data;
using Infrastructure.Data.Course;

namespace Infrastructure.Data
{
    public class StoreContextSeed
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
                    AppUserId = "1",
                    MealList = new List<Meal>()
                };
                mealPlan.MealList.Add(meala);
            }
        
        await course.SaveChangesAsync();
        }
        public static async Task SeedAsync(StoreContext context)
        {
            if (!context.ProductBrands.Any())
            {
                var brandsData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                context.ProductBrands.AddRange(brands);
            }

            if (!context.ProductCategories.Any())
            {
                var typesData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                var types = JsonSerializer.Deserialize<List<ProductCategory>>(typesData);
                context.ProductCategories.AddRange(types);
            }

            if (!context.Products.Any())
            {
                var productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                context.Products.AddRange(products);
            }

            if (!context.DeliveryMethods.Any())
            {
                var deliveryData = File.ReadAllText("../Infrastructure/Data/SeedData/delivery.json");
                var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);
                context.DeliveryMethods.AddRange(methods);
            }

            if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
        }
    }
}