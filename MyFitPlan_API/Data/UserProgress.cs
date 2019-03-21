namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserProgress")]
    public partial class UserProgress
    {
        public int ID { get; set; }

        public int? AccUserID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        public double? Weight { get; set; }

        public double? Height { get; set; }

        public int GoalCalories { get; set; }

        public int GoalFat { get; set; }

        public int GoalCarbs { get; set; }

        public int GoalProtein { get; set; }

        public virtual AccUser AccUser { get; set; }
    }
}
