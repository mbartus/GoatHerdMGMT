using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace goatMGMT.Models
{
    public class LicenseViewModel
    {
        [Required]
        public string agreement { get; set; }
    }
}