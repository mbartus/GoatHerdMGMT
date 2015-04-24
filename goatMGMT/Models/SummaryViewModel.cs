using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

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

        public double allADGWeaning { get; set; }

        public double allMaleADGWeaning { get; set; }

        public double allFemaleADGWeaning { get; set; }

        public double allADGPostWeaning { get; set; }

        public double allMaleADGPostWeaning { get; set; }

        public double allFemaleADGPostWeaning { get; set; }

        public double ADGWeaning { get; set; }

        public double maleADGWeaning { get; set; }

        public double femaleADGWeaning { get; set; }

        public double ADGPostWeaning { get; set; }

        public double maleADGPostWeaning { get; set; }

        public double femaleADGPostWeaning { get; set; }

        public double[,] myArray { get; set; } // FIRST INDEX: Breeds, SECOND: Info (weaning, weaning(m), weaning(f),post-w,pw(m),pw(f)
        public double[,] allArray { get; set; }

        [DisplayName("Select a breed to compare")]
        public string stringBreedCode { get; set; }

        public int breedCode { get; set; }

        public int numberWeanedMale { get; set; }
        public int numberWeanedFemale { get; set; }
        public int numberBornMale { get; set; }
        public int numberBornFemale { get; set; }

        [DisplayName("Select category of data to view")]
        public string graphAnswer { get; set; }
    }
}