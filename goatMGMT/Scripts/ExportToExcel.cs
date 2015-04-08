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
            ExcelPackage package = new ExcelPackage();

            //TODO need a worksheet for animals, treatments, transactions, associates, 
            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Animals");
            worksheet.Cells[1, 1].Value = "Name";
            worksheet.Cells[1, 2].Value = "Age";
            worksheet.Cells[1, 3].Value = "Other";
            worksheet.Cells[1, 4].Value = "Dumb";
            worksheet.Cells[1, 5].Value = "Stuff";

            //Add a formula for the value-column
            worksheet.Cells["E2:E4"].Formula = "C2*D2";

            worksheet.Calculate();
            worksheet.Cells.AutoFitColumns(0);
            // set some document properties
            package.Workbook.Properties.Title = "Animals";
            package.Workbook.Properties.Author = "NadaBaja";

            package.Save();
        }
    }
}