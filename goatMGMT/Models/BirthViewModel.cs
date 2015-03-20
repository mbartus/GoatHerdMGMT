using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace goatMGMT.Models
{
    public class BirthViewModel
    {
        public Birth birth;
        public string mother_tag;
        public string father_tag;
        public string offspring_tag;
        public List<SelectListItem> offspring;
        public int offspringChoice;
        public IEnumerable<goatMGMT.Models.BirthViewModel> ien;
    }
}