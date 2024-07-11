﻿using ClosedXML.Excel;

namespace ExcelTask;

internal class Program
{
    static void Main(string[] args)
    {
        var persons = new List<Person>
        {
            new("Алексей", "Алексеев", 31, "89999999999"),
            new("Александр ", "Бирюков", 30, "89999999998"),
            new("Дмитрий", "Валюхов", 29, "89999999997"),
            new("Евгений", "Данилов", 28, "89999999996"),
            new("Евгений", "Журов", 27, "89999999995")
        };

        using var workbook = new XLWorkbook();
        var workSheet = workbook.Worksheets.Add("Persons");

        workSheet.Cell(1, 1).Value = "First name";
        workSheet.Cell(1, 2).Value = "Last name";
        workSheet.Cell(1, 3).Value = "Age";
        workSheet.Cell(1, 4).Value = "Phone number";

        workSheet.Cell(2, 1).InsertData(persons);

        workSheet.CellsUsed().Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);

        var firstRowUsed = workSheet.Range("A1:D1");
        firstRowUsed.Style
            .Font.SetFontSize(12)
            .Font.SetFontName("Arial")
            .Font.SetBold();

        firstRowUsed.Style.Fill
            .SetBackgroundColor(XLColor.Gamboge);

        workSheet.Columns().AdjustToContents();
        workbook.SaveAs("simple.xlsx");
    }
}