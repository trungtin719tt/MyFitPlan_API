using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFitPlan_API.Models
{
    public class FoodNutritionModel
    {
        public int ID { get; set; }

        public int? FoodID { get; set; }

        public int? NutritionID { get; set; }

        public double? Quantity { get; set; }

        public bool? IsActive { get; set; }

        public FoodModel Food { get; set; }

        public NutritionModel Nutrition { get; set; }
    }
}