using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFitPlan_API.Models
{
    public class FoodApiViewModel : FoodViewModel
    {
        public string CateName { get; set; }
        public List<NutritionInfoApiViewModel> NutriationInfo { get; set; }
    }
    public class NutritionInfoApiViewModel
    {
        public string Name { get; set; }
        public string Quantity { get; set; }

    }
}