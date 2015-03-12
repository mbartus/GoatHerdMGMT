using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace goatMGMT.Models
{
    public class ChangeSecurityQuestionViewModel
    {
        [Required]
        [DisplayName("Password")]
        public string Password { get; set; }

        [Required]
        [DisplayName("Security Question")]
        public string SecurityQuestion { get; set; }

        [Required]
        [DisplayName("Security Question Answer")]
        public string SecurityQuestionAnswer { get; set; }
    }
}