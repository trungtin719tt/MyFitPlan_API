using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyFitPlan_API.Models
{
    public class NutritionModel
    {
        public NutritionModel()
        {
            FoodNutritions = new HashSet<FoodNutritionModel>();
        }

        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Note { get; set; }

        public ICollection<FoodNutritionModel> FoodNutritions { get; set; }
    }
}