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
            CategoryDetails = new HashSet<CategoryDetailModel>();
        }

        public int ID { get; set; }

        public int? AccUserID { get; set; }

        public string Name { get; set; }

        public bool? IsActive { get; set; }

        public ICollection<CategoryDetailModel> CategoryDetails { get; set; }

        public AccUserModel AccUser { get; set; }
    }
}