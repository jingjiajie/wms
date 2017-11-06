using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PortableFCSLib;

namespace WMS.TableGenerator
{
    public class Table
    {
        private string[,] cells;

        public string[,] Cells { get => cells; set => cells = value; }

        public Table(int countOfLine, int countOfColumn)
        {
            this.Cells = new string[countOfLine, countOfColumn];
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            int count = 0;
            foreach(var item in this.Cells)
            {
                count++;
                sb.Append(item+" ");
                if(count % this.Cells.GetLength(1) == 0)
                {
                    sb.AppendLine();
                }
            }
            return sb.ToString();
        }
    }
}
