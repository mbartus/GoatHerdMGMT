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

        [DisplayName("Dam's Name")]
        public string mother_name { get; set; }

        [DisplayName("Dam's Tag")]
        public string mother_tag { get; set;}

        [DisplayName("Sire's Name")]
        public string father_name { get; set; }

        [DisplayName("Sire's Tag")]
        public string father_tag { get; set; }

        [DisplayName("Mortality Rate")]
        public int mortalityRate { get; set; }

        [DisplayName("Conception Rate")]
        public int conceptionRate { get; set; }

        [DisplayName("Total Offspring Born")]
        public int totBorn { get; set; }

        [DisplayName("Total Offspring Alive")]
        public int totAlive { get; set; }

        public IEnumerable<BreedingViewModel> ien;

        // Animal
        public IEnumerable<Animal> maleList;
        public IEnumerable<Animal> femaleList;

    }
}