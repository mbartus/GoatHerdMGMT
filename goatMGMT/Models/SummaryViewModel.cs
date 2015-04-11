using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace goatMGMT.Models
{
    public class SummaryViewModel
    {
        public int totalSire { get; set; }

        public int totalDam { get; set; }

        public int activeSire { get; set; }

        public int activeDam { get; set; }

        public int malesBorn { get; set; }

        public int femalesBorn { get; set; }

        public int totalBorn { get; set; }

        public int lastYear { get; set; }

        public int currentYear { get; set; }

        public int damParity1Count { get; set; }

        public int damParity2Count { get; set; }

        public int damParity3Count { get; set; }

        public int damParity4Count { get; set; }

        public int matingCount { get; set; }

        public int kiddingCount { get; set; }

        public double kiddingPercentage { get; set; }

        public int kidsBornCount { get; set; }

        public int kidsAliveCount { get; set; }

        public double kidsAlivePercentage { get; set; }
    }
}