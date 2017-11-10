using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WMS.TableGenerate
{
    public class Table
    {
        private Cell[,] cells;

        public Cell[,] Cells { get => cells; private set => cells = value; }
        public int LineCount { get => this.Cells.GetLength(0); }
        public int ColumnCount { get => this.Cells.GetLength(1); }

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
            Console.WriteLine("表格大小：{0},{1}",countOfLine,countOfColumn);
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

        public void ExpandLine(int addLines)
        {
            int oldLineCount = this.LineCount;
            int oldColumnCount = this.ColumnCount;
            Cell[,] newCells = new Cell[oldLineCount + addLines, oldColumnCount];
            for(int i = 0; i < oldLineCount; i++) //将旧的单元格拷贝到新的表格里
            {
                for(int j = 0; j < oldColumnCount; j++)
                {
                    newCells[i, j] = this.Cells[i,j];
                }
            }

            for (int i = oldLineCount; i < newCells.GetLength(0); i++) //将新的表格中未初始化的单元格初始化
            {
                for (int j = 0; j < newCells.GetLength(1); j++)
                {
                    newCells[i, j] = new Cell();
                }
            }
            this.Cells = newCells;
        }
    }
}
