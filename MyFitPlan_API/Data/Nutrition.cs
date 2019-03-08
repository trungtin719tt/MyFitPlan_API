namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Nutrition")]
    public partial class Nutrition
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Nutrition()
        {
            FoodNutritions = new HashSet<FoodNutrition>();
        }

        public int ID { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Note { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FoodNutrition> FoodNutritions { get; set; }
    }
}
