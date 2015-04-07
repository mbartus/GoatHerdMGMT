using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace goatMGMT.Models
{
    [MetadataType(typeof(BirthMetadata))]
    public partial class Birth { }
    public class BirthMetadata
    {
        [HiddenInput(DisplayValue = false)]
        [Key]
        public int id { get; set; }

        [DisplayName("Born")]
        public Int32 born { get; set; }

        [DisplayName("Alive")]
        public Int32 alive { get; set; }

        [DisplayName("Score")]
        public Int32 score { get; set; }

        [DisplayName("Notes")]
        public string notes { get; set; }
    }
}