using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFitPlan_API.Models
{
    public class FoodViewModel
    {
        public int ID { get; set; }
        public string FoodCode { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public string NameVN { get; set; }
        public string NameENG { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public string Unit { get; set; }
        public short? Rate { get; set; }
        public int? FollowedBy { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? CreatedBy { get; set; }
    }
}