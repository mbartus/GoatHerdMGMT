using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Data.Entity;
using System.Linq;
using System.Web;

namespace goatMGMT.Models
{
    
    public class Transactions 
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity), Index("IX_id", 0, IsUnique = true)]
        public int id { get; set; }
	    public DateTime? date_of_transaction;
	    public char type; // i for income, e for expense
        public string item_detail { get; set; }
        public string item_type { get; set; }
        public decimal? quantity { get; set; }
        public decimal? unit_Price { get; set; }
        public decimal? total_Payment { get; set; }
        public string notes { get; set; }
    }


    public class Associates 
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity), Index("IX_id", 0, IsUnique = true)]
        public int id { get; set; }
        public string name { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string telephone { get; set; }
        public string fax { get; set; }
        public string email { get; set; }
        public string notes { get; set; } 
    }

    public class AnimalTreatments
    {
        [Key, Column(Order=0)] // Foreign key to Animals
        public int animal_id { get; set; }
        public virtual Animals Animals { get; set; }
        [Key, Column(Order=1)] // Foreign key to Treatments
        public int treatment_id { get; set; }
        public virtual Treatments Treatments { get; set; }
	    [Key, Column(Order=2)]
        public DateTime date_applied { get; set; }
        public string details { get; set; }
        public string product { get; set; }
        public string dosage { get; set; }
        public string remarks { get; set; }
    }


    public class Treatments
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity), Index("IX_id", 0, IsUnique = true)]
        public int id { get; set; }
        public string product { get; set; }
        public string dosage { get; set; }
        public string remarks { get; set; }
    }



    public class Breedings 
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity), Index("IX_id", 0, IsUnique = true)]
        public int id { get; set; }
        [Key, Column(Order=1)]
        public int mother_id { get; set; }
        public virtual Animals mother { get; set; }
        [Key, Column(Order=2)]
        public int father_id { get; set; }
        public virtual Animals father { get; set; }
        public DateTime date { get; set; }
        public string remarks { get; set; }
    }
    
    public class Births
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity), Index("IX_id", 0, IsUnique = true)]
        public int id { get; set; }
        [Key, Column(Order=1)]
        public int mother_id { get; set; }
        public virtual Animals mother { get; set; }
        [Key, Column(Order=2)]
        public int father_id { get; set; }
        public virtual Animals father { get; set; }
        public DateTime date { get; set; }
        public int? birth_type { get; set; }
        public int? birth_parity { get; set; }
	    public string remarks{ get; set; }
    }
    
    public class Children
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity), Index("IX_id", 0, IsUnique = true)]
        public int id { get; set; }
        [Key, Column(Order = 1)]
        public DateTime dob { get; set; }
        [Key, Column(Order = 2)]
        public string farm_name { get; set; }
        public string tag { get; set; } // This is where farms can specify their own tagging system
        public string species { get; set; }
        public string name { get; set; }
        //[Index("IX_FirstAndSecond", 6, IsUnique = true)]
        public string regulation_no { get; set; }
        public string breed_code { get; set; }
        public string microchip_id { get; set; }
        public string premise_id { get; set; } // what is this?
        public string herd_id_code { get; set; } // what is this?
        public string breed_registry { get; set; } // what is this?
        public char? sex { get; set; } // M for male, F for female
        public string status_code { get; set; }
        public DateTime? disposal_date { get; set; } //the day the animal dies?
        public double? current_weight { get; set; }
	    // Birth, need to figure out creep foster
        public double? weaning_weight { get; set; }
        public double? birth_weight { get; set; }
	    // Market
        public double? market_weight { get; set; }
        public DateTime? market_date { get; set; }
        public string remarks { get; set; }
	    // PRIMARY KEY (id, dob) --PRIMARY KEY (id, farm_name, dob)
    }

    public class Animals
    {
        [Key, Column(Order = 0), DatabaseGenerated(DatabaseGeneratedOption.Identity), Index("IX_id", 0, IsUnique = true)]
        public int id { get; set; }
        [Key, Column(Order = 1)]
        public DateTime dob { get; set; }
        [Key, Column(Order = 2)]
        public string farm_name { get; set; } 
        public string tag { get; set; } // This is where farms can specify their own tagging system
        public string species { get; set; }
        public string name { get; set; }
        //[Index("IX_FirstAndSecond", 6, IsUnique = true)]
        public string regulation_no { get; set; }
        public string breed_code { get; set; }
        public string microchip_id { get; set; }
        public string premise_id { get; set; } // what is this?
        public string herd_id_code { get; set; } // what is this?
        public string breed_registry { get; set; } // what is this?
        public char? sex { get; set; } // M for male, F for female
        public string status_code { get; set; }
        public DateTime? disposal_date { get; set; } //the day the animal dies?
        public double? current_weight { get; set; }
        public double? market_weight { get; set; }
        public DateTime? market_date { get; set; }
        public string remarks { get; set; }
    }
}