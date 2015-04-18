using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace goatMGMT.Models
{
    public class AnimalViewModel
    {
        //not in animal
        [DisplayName("Number of Active Sires")]
        public int numSires { get; set; }

        [DisplayName("Number of Active Dams")]
        public int numDams { get; set; }

        [DisplayName("Number of Active Offspring")]
        public int numOff { get; set; }

        public Animal animal;

        public IEnumerable<goatMGMT.Models.AnimalViewModel> ien;
    }
}