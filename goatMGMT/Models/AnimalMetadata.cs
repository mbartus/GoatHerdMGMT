﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace goatMGMT.Models
{
    [MetadataType(typeof(AnimalMetadata))]
    public partial class Animal { }
    public class AnimalMetadata
    {
        [Required]
        [DisplayName("Tag")]
        public string tag { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DisplayName("Date of Birth")]
        public DateTime dob { get; set; }

        [Required]
        [DisplayName("Sex")]
        public bool sex { get; set; }

        [Required]
        [DisplayName("Child")]
        public bool isChild { get; set; }

        [Required]
        [DisplayName("Breed Code")]
        public string breed_code { get; set; }

        [Required]
        [DisplayName("Status")]
        public string status_code { get; set; }

        [Required]
        [DisplayName("Name")]
        public string name { get; set; }

        [Required]
        [DisplayName("Regulation Number")]
        public string regulation_no { get; set; }

        [Required]
        [DisplayName("Microchip ID")]
        public string microchip_id { get; set; }

        [Required]
        [DisplayName("Premise ID")]
        public string premise_id { get; set; }

        [Required]
        [DisplayName("Herd ID")]
        public string herd_id_code { get; set; }

        [Required]
        [DisplayName("Breed Registry")]
        public string breed_registry { get; set; }

        [Required]
        [DisplayName("Species")]
        public string species { get; set; }

        [DisplayName("Weight at Birth")]
        public double birth_weight { get; set; }

        [DisplayName("Weight at Weaning")]
        public double weaning_weight { get; set; }

        [DisplayName("Weaning Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode=true, DataFormatString= "{0:MM/dd/yyyy}")]
        public DateTime weaning_date { get; set; }

        [DisplayName("Weaning Group")]
        public int weaning_group { get; set; }

        [DisplayName("Post-Weaning Weight")]
        public double post_weaning_weight { get; set; }

        [DisplayName("Post-Weaning Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime post_weaning_date { get; set; }

        [DisplayName("Market Weight")]
        public float market_weight { get; set; }

        [DisplayName("Market Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime market_date { get; set; }

        [DisplayName("Disposal Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime disposal_date { get; set; }

        [DisplayName("Comments")]
        public string remarks { get; set; }
    }
}