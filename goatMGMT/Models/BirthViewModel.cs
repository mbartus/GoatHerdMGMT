using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace goatMGMT.Models
{
    public class BirthViewModel
    {
        public Birth birth {get; set;}
        public string mother_tag { get; set; }
        public string father_tag { get; set; }
        public string offspring_tag { get; set; }
        public List<SelectListItem> offspring { get; set; }
        public int offspringChoice { get; set; }
        public IEnumerable<goatMGMT.Models.BirthViewModel> ien;
    }
}