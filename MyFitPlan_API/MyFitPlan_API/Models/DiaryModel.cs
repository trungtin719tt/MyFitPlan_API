using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyFitPlan_API.Models
{
    public class DiaryModel
    {
        public int ID { get; set; }

        public int? AccUserID { get; set; }

        public DateTime? Time { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        public int? FoodID { get; set; }

        public double? Quantity { get; set; }

        public FoodModel Food { get; set; }

        public AccUserModel AccUser { get; set; }
    }
}