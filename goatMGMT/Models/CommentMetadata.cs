using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace goatMGMT.Models
{
    [MetadataType(typeof(CommentMetadata))]
    public partial class Comment { }
    public class CommentMetadata
    {
        public int id {get;set;}

        [DisplayName("Name")]
        [MaxLength(50)]
        public string name { get; set; }

        [DisplayName("Email")]
        [MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string email { get; set; }

        [DisplayName("Date Sent")]
        [DataType(DataType.DateTime)]
        public DateTime date_sent { get; set; }

        [DisplayName("Subject")]
        [MaxLength(50)]
        public string subject { get; set; }
        
        [DisplayName("Message")]
        [Required]
        public string comment1 { get; set; }
    }
}