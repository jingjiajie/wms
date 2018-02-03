using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WMS.UI
{
    public partial class FormTest : Form
    {
        public FormTest()
        {
            InitializeComponent();
            this.reoGridControlMain.PreviewKeyDown += ReoGridControlMain_PreviewKeyDown;
            this.reoGridControlMain.KeyDown += ReoGridControlMain_KeyDown;
            this.reoGridControlMain.CurrentWorksheet.SelectionRangeChanging += CurrentWorksheet_SelectionRangeChanging;
            this.reoGridControlMain.CurrentWorksheet.SelectionRangeChanged += CurrentWorksheet_SelectionRangeChanged;
            this.reoGridControlMain.CurrentWorksheet.BeforeCellKeyDown += CurrentWorksheet_BeforeCellKeyDown;
            this.reoGridControlMain.CurrentWorksheet.AfterCellKeyDown += CurrentWorksheet_AfterCellKeyDown;
            this.reoGridControlMain.CurrentWorksheet.BeforeSelectionRangeChange += CurrentWorksheet_BeforeSelectionRangeChange;
            this.reoGridControlMain.MouseDown += ReoGridControlMain_MouseDown;
            this.reoGridControlMain.CurrentWorksheet.CellMouseDown += CurrentWorksheet_CellMouseDown;
        }

        private void CurrentWorksheet_CellMouseDown(object sender, unvell.ReoGrid.Events.CellMouseEventArgs e)
        {
            Console.WriteLine("CellMouseDown");
        }

        private void ReoGridControlMain_MouseDown(object sender, MouseEventArgs e)
        {
            Console.WriteLine("MouseDown");
        }

        private void CurrentWorksheet_BeforeSelectionRangeChange(object sender, unvell.ReoGrid.Events.BeforeSelectionChangeEventArgs e)
        {
            Console.WriteLine("Before SelectionRange Change");
        }

        private void CurrentWorksheet_AfterCellKeyDown(object sender, unvell.ReoGrid.Events.AfterCellKeyDownEventArgs e)
        {
            Console.WriteLine("AfterCellKeyDown");
        }

        private void CurrentWorksheet_BeforeCellKeyDown(object sender, unvell.ReoGrid.Events.BeforeCellKeyDownEventArgs e)
        {
            e.IsCancelled = true;
            Console.WriteLine("BeforeCellKeyDown");
        }

        private void CurrentWorksheet_SelectionRangeChanged(object sender, unvell.ReoGrid.Events.RangeEventArgs e)
        {
            Console.WriteLine("SelectionRangeChanged");
        }

        private void CurrentWorksheet_SelectionRangeChanging(object sender, unvell.ReoGrid.Events.RangeEventArgs e)
        {
            Console.WriteLine("SelectionRangeChanging");
        }

        private void ReoGridControlMain_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            Console.WriteLine("Preview Key Down!");
        }


        private void FormTest_Load(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlMain.CurrentWorksheet;
            // put data on spreadsheet
            worksheet["A1"] = new object[,] {
  { "1", "A", "D" },
  { "2", "B", "E" },
  { "3", "C", "F" },
  { "4", "D", "G" },
  { "5", "E", "H" },
};
            worksheet.SortColumn(1, new unvell.ReoGrid.RangePosition("A1:C5"), unvell.ReoGrid.SortOrder.Descending);
        }

        private void ReoGridControlMain_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("Key Down!");
        }

        private void reoGridControlMain_LostFocus(object sender, EventArgs e)
        {

        }

        private void FormTest_Click(object sender, EventArgs e)
        {
            var worksheet = this.reoGridControlMain.Worksheets[0];
            worksheet.EndEdit(new unvell.ReoGrid.EndEditReason());
        }

        private void reoGridControlMain_MouseDown(object sender, MouseEventArgs e)
        {
            Console.WriteLine("MouseDown");
        }
    }
}
