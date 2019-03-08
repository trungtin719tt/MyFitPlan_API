namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FoodNutrition")]
    public partial class FoodNutrition
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int? FoodID { get; set; }

        public int? NutritionID { get; set; }

        public double? Quantity { get; set; }

        public bool? IsActive { get; set; }

        public virtual Food Food { get; set; }

        public virtual Nutrition Nutrition { get; set; }
    }
}
