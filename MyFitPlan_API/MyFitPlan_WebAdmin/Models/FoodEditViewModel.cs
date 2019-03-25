using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFitPlan_WebAdmin.Models
{
    public class FoodEditViewModel
    {
        public int ID { get; set; }
        public string FoodCode { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public string NameVN { get; set; }
        public string NameENG { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public string Unit { get; set; }
        public Nullable<short> Rate { get; set; }
        public Nullable<int> FollowedBy { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public string Protein { get; set; }
        public string Fat { get; set; }
        public string Carbs { get; set; }
        public string Calories { get; set; }
        public string CategoryName { get; set; }
    }
}