using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WMS.TableGenerate;
using unvell.ReoGrid;

namespace WMS.UI
{
    internal class Utilities
    {
        public static Table ReoGridWorkSheetToTable(Worksheet workSheet)
        {
            Table table = new Table(workSheet.RowCount, workSheet.ColumnCount);
            workSheet.IterateCells(RangePosition.EntireRange, (row, col, cell) =>
            {
                table[row, col].Data = cell.Data.ToString();
                table[row, col].Color = cell.Style.BackColor;
                return true;
            });
            return table;
        }

        public static Worksheet TableToReoGridWorkSheet(Table table, string worksheetName = "ResultTable")
        {
            var workBook = new ReoGridControl();
            var worksheet = workBook.Worksheets[0];
            worksheet.Name = worksheetName;
            worksheet.RowCount = table.LineCount;
            worksheet.ColumnCount = table.ColumnCount;
            for (int i = 0; i < table.LineCount; i++)
            {
                for (int j = 0; j < table.ColumnCount; j++)
                {
                    worksheet[i, j] = table[i, j].Data;
                    worksheet.GetCell(i, j).Style.BackColor = table[i, j].Color;
                }
            }
            workBook.RemoveWorksheet(worksheet); //将worksheet从当前workbook卸载
            return worksheet;
        }
    }
}
