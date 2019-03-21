using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyFitPlan_API.Models
{
    public class AccUserModel
    {
        public AccUserModel()
        {
            //DailyProgressses = new HashSet<DailyProgresssModel>();
            //Diaries = new HashSet<DiaryModel>();
            //Foods = new HashSet<FoodModel>();
            //PersonalCategories = new HashSet<PersonalCategoryModel>();
            //UserProgresses = new HashSet<UserProgressModel>();
        }

        public int ID { get; set; }

        public string Email { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; }

        public string Name { get; set; }

        public short? Purpose { get; set; }

        [StringLength(1)]
        public string Gender { get; set; }

        public short? TrainingLevel { get; set; }

        //public ICollection<DailyProgresssModel> DailyProgressses { get; set; }

        //public ICollection<DiaryModel> Diaries { get; set; }

        //public ICollection<FoodModel> Foods { get; set; }

        //public ICollection<PersonalCategoryModel> PersonalCategories { get; set; }

        //public ICollection<UserProgressModel> UserProgresses { get; set; }

        //public ApplicationUserModel ApplicationUser { get; set; }
    }
}