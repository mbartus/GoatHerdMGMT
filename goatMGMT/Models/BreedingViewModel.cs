using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace goatMGMT.Models
{
    public class BreedingViewModel
    {
        public Int32 id { get; set; }

        [Required]
        [DisplayName("Mother's Name")]
        public Int32 mother_id { get; set; }

        public string mother_name;
        public string father_name;

        [Required]
        [DisplayName("Father's Name")]
        public Int32 father_id {get; set;}

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DisplayName("Date of Breeding")]
        public DateTime date {get; set;}

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DisplayName("Date of Pregnancy Check")]
        public DateTime pregnancy_check {get; set;}

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DisplayName("Expected Kidding Date")]
        public DateTime expected_kidding_date { get; set; }

        [Required]
        [DisplayName("Remarks")]
        public string remarks { get; set; }

        [DisplayName("Mother Tag")]
        public string mother_tag { get; set; }

        [DisplayName("Father Tag")]
        public string father_tag { get; set; }

        // Animal
        public IEnumerable<Animal> maleList;
        public IEnumerable<Animal> femaleList;

    }
}