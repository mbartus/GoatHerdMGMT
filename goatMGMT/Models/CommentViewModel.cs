using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace goatMGMT.Models
{
    public class CommentViewModel
    {
        [DisplayName("Name")]
        [MaxLength(50)]
        public string name { get; set; }

        [DisplayName("Email")]
        [MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string email { get; set; }
        
        [DisplayName("Subject")]
        [MaxLength(50)]
        public string subject { get; set; }
        
        [DisplayName("Comment")]
        [Required]
        public string comment { get; set; }
    }
}