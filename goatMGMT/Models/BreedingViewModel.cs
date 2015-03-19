using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace goatMGMT.Models
{
    public class BreedingViewModel
    {
        public Breeding breeding;

        public string mother_name;
        public string mother_tag;
        public string father_name;
        public string father_tag;

        // Animal
        public IEnumerable<Animal> maleList;
        public IEnumerable<Animal> femaleList;

    }
}