namespace Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity.Infrastructure;

    public partial class MyFitPlanDBContext : IdentityDbContext<ApplicationUser>
    {
        public MyFitPlanDBContext()
            : base("name=MyFitPlanDBContext")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CategoryDetail> CategoryDetails { get; set; }
        public virtual DbSet<DailyProgresss> DailyProgressses { get; set; }
        public virtual DbSet<Diary> Diaries { get; set; }
        public virtual DbSet<Food> Foods { get; set; }
        public virtual DbSet<FoodNutrition> FoodNutritions { get; set; }
        public virtual DbSet<Nutrition> Nutritions { get; set; }
        public virtual DbSet<PersonalCategory> PersonalCategories { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<UserProgress> UserProgresses { get; set; }
        public virtual DbSet<AccUser> AccUsers { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<IncludeMetadataConvention>();

        }
        public static MyFitPlanDBContext Create()
        {
            return new MyFitPlanDBContext();
        }
    }
}
