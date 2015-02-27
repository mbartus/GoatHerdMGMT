using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace goatMGMT.Models
{
    [MetadataType(typeof(AssociateMetadata))]
    public partial class Associate { }
    public class AssociateMetadata
    {
        [Required]
        [DisplayName("Name")]
        public string name { get; set; }

        [Required]
        [DisplayName("Street")]
        public string street { get; set; }

        [Required]
        [DisplayName("City")]
        public string city { get; set; }

        [Required]
        [DisplayName("State")]
        public string state { get; set; }

        [Required]
        [DisplayName("Zip")]
        public string zip { get; set; }

        [Required]
        [DisplayName("Phone")]
        public string telephone { get; set; }

        [Required]
        [DisplayName("Fax")]
        public string fax { get; set; }

        [Required]
        [DisplayName("Email")]
        public string email { get; set; }
        
        [Required]
        [DisplayName("Notes")]
        public string notes { get; set; }
    }
}