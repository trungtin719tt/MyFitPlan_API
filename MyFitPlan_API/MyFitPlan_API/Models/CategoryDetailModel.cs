using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFitPlan_API.Models
{
    public class CategoryDetailModel
    {
        public int ID { get; set; }

        public int? PersonalCategoryID { get; set; }

        public int FoodID { get; set; }

        public FoodModel Food { get; set; }

        public PersonalCategoryModel PersonalCategory { get; set; }
    }
}