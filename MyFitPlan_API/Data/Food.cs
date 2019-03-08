namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Food")]
    public partial class Food
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Food()
        {
            CategoryDetails = new HashSet<CategoryDetail>();
            Diaries = new HashSet<Diary>();
            FoodNutritions = new HashSet<FoodNutrition>();
        }

        public int ID { get; set; }

        public int? CategoryID { get; set; }

        public string NameVN { get; set; }

        public string NameENG { get; set; }

        public string Description { get; set; }

        public string Note { get; set; }

        [StringLength(50)]
        public string Unit { get; set; }

        public short? Rate { get; set; }

        public int? FollowedBy { get; set; }

        public DateTime? DateCreated { get; set; }

        public int? CreatedBy { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CategoryDetail> CategoryDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Diary> Diaries { get; set; }

        [ForeignKey("CreatedBy")]
        public virtual AccUser AccUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FoodNutrition> FoodNutritions { get; set; }
    }
}
