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
    
    public partial class Child
    {
        public int id { get; set; }
        public System.DateTime dob { get; set; }
        public string farm_name { get; set; }
        public string tag { get; set; }
        public string species { get; set; }
        public string name { get; set; }
        public string regulation_no { get; set; }
        public string breed_code { get; set; }
        public string microchip_id { get; set; }
        public string premise_id { get; set; }
        public string herd_id_code { get; set; }
        public string breed_registry { get; set; }
        public string status_code { get; set; }
        public Nullable<System.DateTime> disposal_date { get; set; }
        public Nullable<double> current_weight { get; set; }
        public Nullable<double> weaning_weight { get; set; }
        public Nullable<double> birth_weight { get; set; }
        public Nullable<double> market_weight { get; set; }
        public Nullable<System.DateTime> market_date { get; set; }
        public string remarks { get; set; }
    }
}
