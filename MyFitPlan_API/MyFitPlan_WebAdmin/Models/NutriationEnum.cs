using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyFitPlan_WebAdmin.Models
{
    public class NutriationEnum
    {
        public enum NutriationIdEnum
        {
            [Display(Name = "Protein")]
            Protein = 1,
            [Display(Name = "Fat")]
            Fat = 2,
            [Display(Name = "Carbs")]
            Carbs = 3,
            [Display(Name = "Calories")]
            Calories = 4,
        }
    }
}