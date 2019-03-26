using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutriationCrawler.Model
{
    public class FoodViewModel
    {
        public string FoodCode { get; set; }
        public string FoodName { get; set; }
        public string Unit { get; set; }
        public string Calories { get; set; }
        public string Carbs { get; set; }
        public string Fat { get; set; }
        public string Protein { get; set; }
        public bool fromVN { get; set; }
    }
}
