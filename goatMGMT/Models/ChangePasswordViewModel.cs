using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace goatMGMT.Models
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DisplayName("Old Password")]
        public string oldPassword { get; set; }

        [Required]
        [DisplayName("New Password")]
        public string newPassword { get; set; }

        [Required]
        [DisplayName("Confirm New Password")]
        public string newPasswordConfirm { get; set; }
    }
}