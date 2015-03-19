using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace goatMGMT.Models
{
    [MetadataType(typeof(TreatmentMetadata))]
    public partial class Treatment { }
    public class TreatmentMetadata
    {
        [HiddenInput(DisplayValue = false)]
        [Key]
        public int id { get; set; }

        [Required]
        [DisplayName("Animal's Name")]
        public Int32 animal_id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DisplayName("Date of Treatment")]
        public DateTime date { get; set; }

        [Required]
        [DisplayName("Type of Treatment")]
        public string item_type { get; set; }

        [DisplayName("Dosage")]
        public string dosage { get; set; }

        [DisplayName("Product")]
        public string product { get; set; }

        [DisplayName("Remarks")]
        public string remarks { get; set; }
    }
}