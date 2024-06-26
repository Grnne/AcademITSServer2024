using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace ExcelTask;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        var persons = new List<Person>
        {
            new("Алексей", "Алексеев", 31, "89999999999"),
            new("Александр ", "Бирюков", 30, "89999999998"),
            new("Дмитрий", "Валюхов", 29, "89999999997"),
            new("Евгений", "Данилов", 28, "89999999996"),
            new("Евгений", "Журов", 27, "89999999995")
        };

        var outputFile = new FileInfo("persons.xlsx");
        
        if (outputFile.Exists)
        {
            outputFile.Delete();
        }

        using var excelPackage = new ExcelPackage(outputFile);
        var workSheet = excelPackage.Workbook.Worksheets.Add("Persons");
        workSheet.Cells[1, 1].Value = "FirstName";
        workSheet.Cells[1, 2].Value = "LastName";
        workSheet.Cells[1, 3].Value = "Age";
        workSheet.Cells[1, 4].Value = "PhoneNumber";
        
        workSheet.Cells[2,1].LoadFromCollection(persons);
        workSheet.Cells.AutoFitColumns();
        var firstRow = workSheet.Row(1);
        firstRow.Style.Fill.PatternType = ExcelFillStyle.LightGray;
        firstRow.Style.Font.Bold = true;
        excelPackage.Save();
    }
}