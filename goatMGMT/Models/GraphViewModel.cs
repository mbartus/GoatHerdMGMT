using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace goatMGMT.Models
{
    public class GraphViewModel
    {
        public double income { get; set; }

        public double expense { get; set; }

        public double sfp { get; set; }

        public double sfc { get; set; }

        public double sfmk { get; set; }

        public double sfb { get; set; }

        public double fi { get; set; }

        public double other { get; set; }

        public double other2 { get; set; }

        public double fh { get; set; }

        public double fs { get; set; }

        public double eq { get; set; }

        public double vm { get; set; }

        public double fsp { get; set; }

        public double ap { get; set; }

        public double ut { get; set; }

        public double birthweight { get; set; }

        public double birthweightall { get; set; }

        public double weaningweight { get; set; }

        public double weaningweightall { get; set; }

        public double postweaningweight { get; set; }

        public double postweaningweightall { get; set; }

        public List<SelectListItem> graphs1 = new List<SelectListItem>()
        {
             new SelectListItem() { Text = "Compare Birth Weight", Value = "bw"},
             new SelectListItem() { Text = "Compare Weaning Weight", Value = "ww"},
             new SelectListItem() { Text = "Compare Post-Weaning Weight", Value = "pww"}
        };
    }
}