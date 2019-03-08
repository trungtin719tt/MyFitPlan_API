namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CategoryDetail")]
    public partial class CategoryDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int? PersonalCategoryID { get; set; }
        
        public int FoodID { get; set; }

        public virtual Food Food { get; set; }

        public virtual PersonalCategory PersonalCategory { get; set; }
    }
}
