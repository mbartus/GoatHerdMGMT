using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace goatMGMT.Models
{
    [MetadataType(typeof(TransactionMetadata))]
    public partial class Transaction { }
    public class TransactionMetadata
    {
        [Required]
        [DisplayName("Name")]
        public string item_type { get; set; }

        [Required]
        [DisplayName("Type")]
        public bool type {get; set;}

        
        [DisplayName("Quantity")]
        public decimal quantity {get; set;}

        
        [DisplayName("Unit Price")]
        public decimal unit_price {get; set;}

        [Required]
        [DisplayName("Total")]
        public decimal total_payment { get; set; }

       
        [DisplayName("Notes")]
        public string notes { get; set; }
    }
}