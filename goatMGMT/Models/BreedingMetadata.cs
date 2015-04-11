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

        [DisplayName("Born")]
        public Int32 born { get; set; }

        [DisplayName("Alive")]
        public Int32 alive { get; set; }


        [Required]
        [DisplayName("Dam's Name")]
        public Int32 mother_id { get; set; }

        [Required]
        [DisplayName("Sire's Name")]
        public Int32 father_id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Date of Breeding")]
        public DateTime date { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Date of Pregnancy Check")]
        public DateTime pregnancy_check { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Expected Kidding Date")]
        public DateTime expected_kidding_date { get; set; }

        [DisplayName("Remarks")]
        public string remarks { get; set; }
    }
}