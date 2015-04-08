using goatMGMT.Models;
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

            //TODO need a worksheet for animals, treatments, transactions, associates, breedings, and births. Can use website as basis.

            //Create and populate animals
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
            foreach (Animal animal in animals; row++)
            {
                
            }

            worksheet.Calculate();
            worksheet.Cells.AutoFitColumns(0);

            // wrap it up
            package.Workbook.Properties.Title = "Animals";
            package.Workbook.Properties.Author = "NadaBaja";

            package.Save();
        }
    }
}