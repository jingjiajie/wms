using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMS.TableGenerator
{
    public class Table
    {
        private Cell[,] cells;

        public Cell[,] Cells { get => cells; private set => cells = value; }

        public Cell this[int line, int column]
        {
            get{
                return this.Cells[line, column];
            }
            set
            {
                this.Cells[line, column] = value;
            }
        }

        public Table(int countOfLine, int countOfColumn)
        {
            this.Cells = new Cell[countOfLine, countOfColumn];
            for(int i = 0; i < this.Cells.GetLength(0); i++)
            {
                for(int j = 0; j < this.Cells.GetLength(1); j++)
                {
                    this.Cells[i, j] = new Cell();
                }
            }
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
