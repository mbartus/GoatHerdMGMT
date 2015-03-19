using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace goatMGMT.Models
{
    public class TreatmentViewModel
    {
        public Treatment treatment;

        public string animal_name;

        public string animal_tag;

        // Animal
        public IEnumerable<Animal> animalList;

    }
}