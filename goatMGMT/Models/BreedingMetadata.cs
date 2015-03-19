using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace goatMGMT.Models
{
    [MetadataType(typeof(BreedingMetadata))]
    public partial class Breeding { }
    public class BreedingMetadata
    {
        [HiddenInput(DisplayValue = false)]
        [Key]
        public int id { get; set; }

        [Required]
        [DisplayName("Mother's Name")]
        public Int32 mother_id { get; set; }

        [Required]
        [DisplayName("Father's Name")]
        public Int32 father_id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DisplayName("Date of Breeding")]
        public DateTime date { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DisplayName("Date of Pregnancy Check")]
        public DateTime pregnancy_check { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DisplayName("Expected Kidding Date")]
        public DateTime expected_kidding_date { get; set; }

        [Required]
        [DisplayName("Remarks")]
        public string remarks { get; set; }
    }
}