using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMS.TableGenerator
{
    public class Table
    {
        private string[,] cells;

        public string[,] Cells { get => cells; set => cells = value; }
    }
}
