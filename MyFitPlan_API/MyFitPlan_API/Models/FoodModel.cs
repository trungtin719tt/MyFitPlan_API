using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyFitPlan_API.Models
{
    public class FoodModel
    {
        public FoodModel()
        {
        }
        
        public int ID { get; set; }

        public int? CategoryID { get; set; }

        public string NameVN { get; set; }

        public string NameENG { get; set; }

        public string Description { get; set; }

        public string Note { get; set; }

        public string Protein { get; set; }

        public string Fat { get; set; }

        public string Carbs { get; set; }

        public string Calories { get; set; }

        [StringLength(50)]
        public string Unit { get; set; }

        public short? Rate { get; set; }

        public int? FollowedBy { get; set; }

        public DateTime? DateCreated { get; set; }

        public int? CreatedBy { get; set; }
    }
}