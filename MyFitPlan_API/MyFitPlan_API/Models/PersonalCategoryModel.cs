using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFitPlan_API.Models
{
    public class PersonalCategoryModel
    {
        public PersonalCategoryModel()
        {
            Foods = new List<FoodModel>();
        }

        public int ID { get; set; }

        public int? AccUserID { get; set; }

        public string Name { get; set; }

        public bool? IsActive { get; set; }

        public List<FoodModel> Foods { get; set; }
    }
}