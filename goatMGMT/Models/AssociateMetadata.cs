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

        
        [DisplayName("Street")]
        public string street { get; set; }

   
        [DisplayName("City")]
        public string city { get; set; }

       
        [DisplayName("State")]
        public string state { get; set; }

        
        [DisplayName("Zip")]
        public string zip { get; set; }

      
        [DisplayName("Phone")]
        public string telephone { get; set; }

        [DisplayName("Fax")]
        public string fax { get; set; }

       
        [DisplayName("Email")]
        public string email { get; set; }
        
       
        [DisplayName("Notes")]
        public string notes { get; set; }
    }
}