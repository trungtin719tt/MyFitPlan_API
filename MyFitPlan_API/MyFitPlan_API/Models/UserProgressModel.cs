using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyFitPlan_API.Models
{
    public class UserProgressModel
    {
        public int ID { get; set; }

        public int? AccUserID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        public double? Weight { get; set; }

        public double? Height { get; set; }

        public int GoalCalories { get; set; }

        public int GoalFat { get; set; }

        public int GoalCarbs { get; set; }

        public int GoalProtein { get; set; }

        //public AccUserModel AccUser { get; set; }
    }
}