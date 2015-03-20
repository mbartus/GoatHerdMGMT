using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

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

        public List<SelectListItem> questions = new List<SelectListItem>()
        {
             new SelectListItem() { Text = "What is your mother's maiden name?", Value = "What is your mother's maiden name?"},
             new SelectListItem() { Text = "What is your father's middle name?", Value = "What is your father's middle name?"},
             new SelectListItem() { Text = "What is the name of the place your wedding reception was held?", Value = "What is the name of the place your wedding reception was held?"},
             new SelectListItem() { Text = "What is the make of your first car?", Value = "What is the make of your first car?"},
             new SelectListItem() { Text = "Where was your first job?", Value = "Where was your first job?"},
             new SelectListItem() { Text = "What is the name of your first pet?", Value = "What is the name of your first pet?"}
        };
    }
}