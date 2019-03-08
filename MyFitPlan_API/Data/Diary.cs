namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Diary")]
    public partial class Diary
    {
        public int ID { get; set; }

        public int? AccUserID { get; set; }

        public DateTime? Time { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        public int? FoodID { get; set; }

        public double? Quantity { get; set; }

        public virtual Food Food { get; set; }

        public virtual AccUser AccUser { get; set; }
    }
}
