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
    
    public partial class Birth
    {
        public int id { get; set; }
        public int child_id { get; set; }
        public int mother_id { get; set; }
        public int father_id { get; set; }
        public System.DateTime date { get; set; }
        public Nullable<int> birth_type { get; set; }
        public Nullable<int> birth_parity { get; set; }
        public string remarks { get; set; }
        public Nullable<int> child_id1 { get; set; }
        public Nullable<System.DateTime> child_dob { get; set; }
        public string child_farm_name { get; set; }
        public Nullable<int> father_id1 { get; set; }
        public Nullable<System.DateTime> father_dob { get; set; }
        public string father_farm_name { get; set; }
        public Nullable<int> mother_id1 { get; set; }
        public Nullable<System.DateTime> mother_dob { get; set; }
        public string mother_farm_name { get; set; }
    
        public virtual Animal Animal { get; set; }
        public virtual Animal Animal1 { get; set; }
        public virtual Animal Animal2 { get; set; }
    }
}