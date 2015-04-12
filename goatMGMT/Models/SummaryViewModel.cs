﻿using System;
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

        public int malesWeaned { get; set; }

        public int femalesWeaned { get; set; }

        public int totalWeaned { get; set; }

        public double avgBW { get; set; }

        public double maleAvgBW { get; set; }

        public double femaleAvgBW { get; set; }

        public double avgWW { get; set; }

        public double maleAvgWW { get; set; }

        public double femaleAvgWW { get; set; }

        public int lastYear { get; set; }

        public int currentYear { get; set; }

        public int damParity1Count { get; set; }

        public double damParity1BW { get; set; }

        public int damParity2Count { get; set; }

        public double damParity2BW { get; set; }

        public int damParity3Count { get; set; }

        public double damParity3BW { get; set; }

        public int damParity4Count { get; set; }

        public double damParity4BW { get; set; }

        public int matingCount { get; set; }

        public int kiddingCount { get; set; }

        public double kiddingPercentage { get; set; }

        public int kidsBornCount { get; set; }

        public int kidsAliveCount { get; set; }

        public double kidsAlivePercentage { get; set; }

        public int singleBirthCount { get; set; }

        public int twinBirthCount { get; set; }

        public int tripletBirthCount { get; set; }

        public int quadBirthCount { get; set; }

        public double singleBWAvg { get; set; }

        public double twinBWAvg { get; set; }

        public double tripletBWAvg { get; set; }

        public double quadBWAvg { get; set; }

        public double singleWWAvg { get; set; }

        public double twinWWAvg { get; set; }

        public double tripletWWAvg { get; set; }

        public double quadWWAvg { get; set; }
    }
}