namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DailyProgresss")]
    public partial class DailyProgresss
    {
        public int ID { get; set; }

        public int? AccUserID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        public double? GoalProtein { get; set; }

        public double? AbsorbedProtein { get; set; }

        public double? GoalCalories { get; set; }

        public double? AbsorbedCalories { get; set; }

        public double? GoalFat { get; set; }

        public double? AbsorbedFat { get; set; }

        public double? GoalCarbs { get; set; }

        public double? AbsorbedCarbs { get; set; }

        public virtual AccUser AccUser { get; set; }
    }
}
