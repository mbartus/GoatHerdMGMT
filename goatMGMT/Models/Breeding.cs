//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace goatMGMT.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Breeding
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Breeding()
        {
            this.Births = new HashSet<Birth>();
        }
    
        public int id { get; set; }
        public int mother_id { get; set; }
        public int father_id { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public Nullable<System.DateTime> pregnancy_check { get; set; }
        public Nullable<System.DateTime> expected_kidding_date { get; set; }
        public string remarks { get; set; }
    
        public virtual Animal Animal { get; set; }
        public virtual Animal Animal1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Birth> Births { get; set; }
    }
}
