using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace goatMGMT.Models
{
    [MetadataType(typeof(FAQMetadata))]
    public partial class FAQ { }
    public class FAQMetadata
    {
        [Required]
        [DisplayName("Question")]
        public string question { get; set; }

        [Required]
        [DisplayName("Answer")]
        public string answer { get; set; }
    }
}