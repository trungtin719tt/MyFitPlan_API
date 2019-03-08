namespace Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PersonalCategory")]
    public partial class PersonalCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonalCategory()
        {
            CategoryDetails = new HashSet<CategoryDetail>();
        }

        public int ID { get; set; }

        public int? AccUserID { get; set; }

        public string Name { get; set; }

        public bool? IsActive { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CategoryDetail> CategoryDetails { get; set; }

        public virtual AccUser AccUser { get; set; }
    }
}
