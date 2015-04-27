using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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

        [RegularExpression(@"^\d+.\d{0,2}$")]
        public double avgBW { get; set; }

        [RegularExpression(@"^\d+.\d{0,2}$")]
        public double maleAvgBW { get; set; }

        [RegularExpression(@"^\d+.\d{0,2}$")]
        public double femaleAvgBW { get; set; }

        [RegularExpression(@"^\d+.\d{0,2}$")]
        public double avgWW { get; set; }

        [RegularExpression(@"^\d+.\d{0,2}$")]
        public double maleAvgWW { get; set; }

        [RegularExpression(@"^\d+.\d{0,2}$")]
        public double femaleAvgWW { get; set; }

        public int lastYear { get; set; }

        public int currentYear { get; set; }

        public int damParity1Count { get; set; }

        [RegularExpression(@"^\d+.\d{0,2}$")]
        public double damParity1BW { get; set; }

        [RegularExpression(@"^\d+.\d{0,2}$")]
        public double damParity1WW { get; set; }

        [RegularExpression(@"^\d+.\d{0,2}$")]
        public double damParity2WW { get; set; }

        [RegularExpression(@"^\d+.\d{0,2}$")]
        public double damParity3WW { get; set; }

        [RegularExpression(@"^\d+.\d{0,2}$")]
        public double damParity4WW { get; set; }

        public int damParity2Count { get; set; }

        [RegularExpression(@"^\d+.\d{0,2}$")]
        public double damParity2BW { get; set; }

        public int damParity3Count { get; set; }

        [RegularExpression(@"^\d+.\d{0,2}$")]
        public double damParity3BW { get; set; }

        public int damParity4Count { get; set; }

        [RegularExpression(@"^\d+.\d{0,2}$")]
        public double damParity4BW { get; set; }

        public int matingCount { get; set; }

        public int kiddingCount { get; set; }

        [RegularExpression(@"^\d+.\d{0,2}$")]
        public double kiddingPercentage { get; set; }

        public int kidsBornCount { get; set; }

        public int kidsAliveCount { get; set; }

        [RegularExpression(@"^\d+.\d{0,2}$")]
        public double kidsAlivePercentage { get; set; }

        public int singleBirthCount { get; set; }

        public int twinBirthCount { get; set; }

        public int tripletBirthCount { get; set; }

        public int quadBirthCount { get; set; }

        [RegularExpression(@"^\d+.\d{0,2}$")]
        public double singleBWAvg { get; set; }

        [RegularExpression(@"^\d+.\d{0,2}$")]
        public double twinBWAvg { get; set; }

        [RegularExpression(@"^\d+.\d{0,2}$")]
        public double tripletBWAvg { get; set; }

        [RegularExpression(@"^\d+.\d{0,2}$")]
        public double quadBWAvg { get; set; }

        [RegularExpression(@"^\d+.\d{0,2}$")]
        public double singleWWAvg { get; set; }

        [RegularExpression(@"^\d+.\d{0,2}$")]
        public double twinWWAvg { get; set; }

        [RegularExpression(@"^\d+.\d{0,2}$")]
        public double tripletWWAvg { get; set; }

        [RegularExpression(@"^\d+.\d{0,2}$")]
        public double quadWWAvg { get; set; }

        [RegularExpression(@"^\d+.\d{0,2}$")]
        public double allADGWeaning { get; set; }

        [RegularExpression(@"^\d+.\d{0,2}$")]
        public double allMaleADGWeaning { get; set; }

        [RegularExpression(@"^\d+.\d{0,2}$")]
        public double allFemaleADGWeaning { get; set; }

        [RegularExpression(@"^\d+.\d{0,2}$")]
        public double allADGPostWeaning { get; set; }

        [RegularExpression(@"^\d+.\d{0,2}$")]
        public double allMaleADGPostWeaning { get; set; }

        [RegularExpression(@"^\d+.\d{0,2}$")]
        public double allFemaleADGPostWeaning { get; set; }

        [RegularExpression(@"^\d+.\d{0,2}$")]
        public double ADGWeaning { get; set; }

        [RegularExpression(@"^\d+.\d{0,2}$")]
        public double maleADGWeaning { get; set; }

        [RegularExpression(@"^\d+.\d{0,2}$")]
        public double femaleADGWeaning { get; set; }

        [RegularExpression(@"^\d+.\d{0,2}$")]
        public double ADGPostWeaning { get; set; }

        [RegularExpression(@"^\d+.\d{0,2}$")]
        public double maleADGPostWeaning { get; set; }

        [RegularExpression(@"^\d+.\d{0,2}$")]
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