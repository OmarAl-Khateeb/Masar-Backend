using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Core.Entities.Excercise;
using Core.Entities.Meal;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Course
{
    public class CourseContext : DbContext
    {
        public CourseContext(DbContextOptions<CourseContext> options) : base(options)
        {
        }

        public DbSet<Meal> Meals { get; set; }
        public DbSet<MealPlan> MealPlans { get; set; }

        public DbSet<Excercise> Excercises { get; set; }
        public DbSet<ExcerciseSet> ExcerciseSets { get; set; }
        public DbSet<ExcercisePlan> ExcercisePlans { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}