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
        public string item_detail { get; set; }

        [Required]
        [DisplayName("Type")]
        public string item_type {get; set;}

        [Required]
        [DisplayName("Quantity")]
        public decimal quantity {get; set;}

        [Required]
        [DisplayName("Unit Price")]
        public decimal unit_Price {get; set;}

        [Required]
        [DisplayName("Total")]
        public decimal total_Payment { get; set; }

        [Required]
        [DisplayName("Notes")]
        public string notes { get; set; }
    }
}