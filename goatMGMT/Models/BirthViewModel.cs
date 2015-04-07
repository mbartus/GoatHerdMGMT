using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace goatMGMT.Models
{
    public class BirthViewModel
    {
        public Birth birth {get; set;}

        [DisplayName("Dam's Tag")]
        public string mother_tag { get; set; }

        [DisplayName("Sire's Tag")]
        public string father_tag { get; set; }

        [DisplayName("Offspring's Tag")]
        public string offspring_tag { get; set; }

        public List<SelectListItem> offspring { get; set; }

        [DisplayName("Offspring's Name")]
        public int offspringChoice { get; set; }
        public IEnumerable<goatMGMT.Models.BirthViewModel> ien;
    }
}