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
        public int id { get; set; }
        public int mother_id { get; set; }
        public int father_id { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public Nullable<System.DateTime> pregnacy_check { get; set; }
        public Nullable<System.DateTime> expected_kidding_date { get; set; }
        public string remarks { get; set; }
    
        public virtual Animal Animal { get; set; }
        public virtual Animal Animal1 { get; set; }
    }
}
