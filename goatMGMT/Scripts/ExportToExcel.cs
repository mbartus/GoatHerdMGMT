﻿using goatMGMT.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace goatMGMT.Scripts
{
    public static class ExportToExcel
    {
        public static void Export(int userID)
        {
            goatDBEntities db = new goatDBEntities();
            ExcelPackage package = new ExcelPackage();

            //Create Animals
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Animals");
            worksheet.Cells[1, 1].Value = "Name";
            worksheet.Cells[1, 2].Value = "Tag";
            worksheet.Cells[1, 3].Value = "Date of Birth";
            worksheet.Cells[1, 4].Value = "Sex";
            worksheet.Cells[1, 5].Value = "Breed Code";
            worksheet.Cells[1, 6].Value = "Species";
            worksheet.Cells[1, 7].Value = "Status";
            worksheet.Cells[1, 8].Value = "Child";
            worksheet.Cells[1, 9].Value = "Regulation Number";
            worksheet.Cells[1, 10].Value = "Microchip ID";
            worksheet.Cells[1, 11].Value = "Premise ID";
            worksheet.Cells[1, 12].Value = "Herd ID";
            worksheet.Cells[1, 13].Value = "Breed Registry";
            worksheet.Cells[1, 14].Value = "Weight at Birth";
            worksheet.Cells[1, 15].Value = "Weight at Weaning";
            worksheet.Cells[1, 16].Value = "Weaning Date";
            worksheet.Cells[1, 17].Value = "Post-Weaning Weight";
            worksheet.Cells[1, 18].Value = "Post-Weaning Date";
            worksheet.Cells[1, 19].Value = "Market Weight";
            worksheet.Cells[1, 20].Value = "Market Date";
            worksheet.Cells[1, 21].Value = "Disposal Date";
            worksheet.Cells[1, 22].Value = "Comments";

            // populate a row for each animal
            List<Animal> animals = db.Animals.Where(animal => animal.owner == userID).ToList();
            int row = 2;
            foreach (Animal animal in animals)
            {
                worksheet.Cells[row, 1].Value = animal.name;
                worksheet.Cells[row, 2].Value = animal.tag;
                worksheet.Cells[row, 3].Value = animal.dob;
                worksheet.Cells[row, 4].Value = animal.sex;
                worksheet.Cells[row, 5].Value = animal.breed_code;
                worksheet.Cells[row, 6].Value = animal.species;
                worksheet.Cells[row, 7].Value = animal.status_code;
                worksheet.Cells[row, 8].Value = animal.isChild;
                worksheet.Cells[row, 9].Value = animal.regulation_no;
                worksheet.Cells[row, 10].Value = animal.microchip_id;
                worksheet.Cells[row, 11].Value = animal.premise_id;
                worksheet.Cells[row, 12].Value = animal.herd_id_code;
                worksheet.Cells[row, 13].Value = animal.breed_registry;
                worksheet.Cells[row, 14].Value = animal.birth_weight;
                worksheet.Cells[row, 15].Value = animal.weaning_weight;
                worksheet.Cells[row, 16].Value = animal.weaning_date;
                worksheet.Cells[row, 17].Value = animal.post_weaning_weight;
                worksheet.Cells[row, 18].Value = animal.post_weaning_date;
                worksheet.Cells[row, 19].Value = animal.market_weight;
                worksheet.Cells[row, 20].Value = animal.market_date;
                worksheet.Cells[row, 21].Value = animal.disposal_date;
                worksheet.Cells[row, 22].Value = animal.remarks;
                row++;
            }
            worksheet.Cells.AutoFitColumns(0);

            //create Treatments
            worksheet = package.Workbook.Worksheets.Add("Health_Records");
            worksheet.Cells[1, 1].Value = "Animal's Tag";
            worksheet.Cells[1, 2].Value = "Animal's Name";
            worksheet.Cells[1, 3].Value = "Date of Treatment";
            worksheet.Cells[1, 4].Value = "Type of Treatment";
            worksheet.Cells[1, 5].Value = "Dosage";
            worksheet.Cells[1, 6].Value = "Product";
            worksheet.Cells[1, 7].Value = "Remarks";

            List<Treatment> treatments = db.Treatments.Where(treatment => treatment.Animal.owner == userID).ToList();
            row = 2;
            foreach (Treatment treatment in treatments)
            {
                worksheet.Cells[row, 1].Value = treatment.Animal.tag;
                worksheet.Cells[row, 2].Value = treatment.Animal.name;
                worksheet.Cells[row, 3].Value = treatment.date;
                worksheet.Cells[row, 4].Value = treatment.item_type;
                worksheet.Cells[row, 5].Value = treatment.dosage;
                worksheet.Cells[row, 6].Value = treatment.product;
                worksheet.Cells[row, 7].Value = treatment.remarks;
                row++;
            }
            worksheet.Cells.AutoFitColumns(0);

            //create Transactions
            worksheet = package.Workbook.Worksheets.Add("Transactions");
            worksheet.Cells[1, 1].Value = "Type";
            worksheet.Cells[1, 2].Value = "Name";
            worksheet.Cells[1, 3].Value = "Date";
            worksheet.Cells[1, 4].Value = "Quantity";
            worksheet.Cells[1, 5].Value = "Unit Price";
            worksheet.Cells[1, 6].Value = "Total";
            worksheet.Cells[1, 7].Value = "Notes";

            List<Transaction> transactions = db.Transactions.Where(transaction => transaction.userid == userID).ToList();
            row = 2;
            foreach (Transaction transaction in transactions)
            {
                worksheet.Cells[row, 1].Value = transaction.type;
                worksheet.Cells[row, 2].Value = transaction.item_type;
                worksheet.Cells[row, 3].Value = transaction.date;
                worksheet.Cells[row, 4].Value = transaction.quantity;
                worksheet.Cells[row, 5].Value = transaction.unit_price;
                worksheet.Cells[row, 6].Value = transaction.total_payment;
                worksheet.Cells[row, 7].Value = transaction.notes;
                row++;
            }
            worksheet.Cells.AutoFitColumns(0);

            //create Associates
            worksheet = package.Workbook.Worksheets.Add("Associates");
            worksheet.Cells[1, 1].Value = "Name";
            worksheet.Cells[1, 2].Value = "Street";
            worksheet.Cells[1, 3].Value = "City";
            worksheet.Cells[1, 4].Value = "State";
            worksheet.Cells[1, 5].Value = "Zip";
            worksheet.Cells[1, 6].Value = "Phone";
            worksheet.Cells[1, 7].Value = "Fax";
            worksheet.Cells[1, 8].Value = "Email";
            worksheet.Cells[1, 9].Value = "Notes";

            List<Associate> associates = db.Associates.Where(associate => associate.userid == userID).ToList();
            row = 2;
            foreach (Associate associate in associates)
            {
                worksheet.Cells[row, 1].Value = associate.name;
                worksheet.Cells[row, 2].Value = associate.street;
                worksheet.Cells[row, 3].Value = associate.city;
                worksheet.Cells[row, 4].Value = associate.state;
                worksheet.Cells[row, 5].Value = associate.zip;
                worksheet.Cells[row, 6].Value = associate.telephone;
                worksheet.Cells[row, 7].Value = associate.fax;
                worksheet.Cells[row, 8].Value = associate.email;
                worksheet.Cells[row, 9].Value = associate.notes;
                row++;
            }
            worksheet.Cells.AutoFitColumns(0);

            //create Breedings
            worksheet = package.Workbook.Worksheets.Add("Breedings");
            worksheet.Cells[1, 1].Value = "Mother's Tag";
            worksheet.Cells[1, 2].Value = "Father's Tag";
            worksheet.Cells[1, 3].Value = "Breeding Date";
            worksheet.Cells[1, 4].Value = "Pregnancy Check";
            worksheet.Cells[1, 5].Value = "Expected Kidding Date";
            worksheet.Cells[1, 6].Value = "Remarks";

            List<Breeding> breedings = db.Breedings.Where(breeding => breeding.Animal.owner == userID).ToList();
            row = 2;
            foreach (Breeding breeding in breedings)
            {
                worksheet.Cells[row, 1].Value = breeding.mother_id;
                worksheet.Cells[row, 2].Value = breeding.father_id;
                worksheet.Cells[row, 3].Value = breeding.date;
                worksheet.Cells[row, 4].Value = breeding.pregnancy_check;
                worksheet.Cells[row, 5].Value = breeding.expected_kidding_date;
                worksheet.Cells[row, 6].Value = breeding.remarks;
                row++;
            }
            worksheet.Cells.AutoFitColumns(0);

            //create Births
            worksheet = package.Workbook.Worksheets.Add("Birth_Records");
            worksheet.Cells[1, 1].Value = "Offspring's Tag";
            worksheet.Cells[1, 2].Value = "Mother's Tag";
            worksheet.Cells[1, 3].Value = "Father's Tag";
            worksheet.Cells[1, 4].Value = "Date of Birth";
            worksheet.Cells[1, 5].Value = "Score";
            worksheet.Cells[1, 6].Value = "Alive";
            worksheet.Cells[1, 7].Value = "Born";
            worksheet.Cells[1, 8].Value = "Notes";

            List<Birth> births = db.Births.Where(birth => birth.Animal.owner == userID).ToList();
            row = 2;
            foreach (Birth birth in births)
            {
                worksheet.Cells[row, 1].Value = birth.child_id;
                worksheet.Cells[row, 2].Value = birth.Breeding.mother_id;
                worksheet.Cells[row, 3].Value = birth.Breeding.father_id;
                worksheet.Cells[row, 4].Value = birth.date;
                worksheet.Cells[row, 5].Value = birth.score;
                worksheet.Cells[row, 6].Value = birth.alive;
                worksheet.Cells[row, 7].Value = birth.born;
                worksheet.Cells[row, 8].Value = birth.notes;
                row++;
            }
            worksheet.Cells.AutoFitColumns(0);

            // wrap it up
            package.Workbook.Properties.Title = "GoatMGMT: " + DateTime.Now;
            package.Workbook.Properties.Author = db.UserProfiles.Find(userID).Username; //TODO switch to First_name + Last_name

            package.Save();
        }
    }
}