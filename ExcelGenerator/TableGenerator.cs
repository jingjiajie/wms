using System.Data;
using System;

namespace WMS.TableGenerator
{
    class TableGenerator
    {
        private DataSet dataSource = null; //数据源

        private Table patternTable = null; //模式表
        private int indexLinePatternTable = 0; //模式表当前行指针
        private int indexColumnPatternTable = 0; //模式表当前列指针

        private Table resultTable = null; //目标表
        private int[] lengthColumnResult;

        public Table PatternTable { get => patternTable; set => patternTable = value; }
        public DataSet DataSource { get => dataSource; set => dataSource = value; }
        private Table ResultTable { get => resultTable; set => resultTable = value; }

        public TableGenerator(Table patternTable, DataSet dataSource)
        {
            this.PatternTable = patternTable;
            this.DataSource = dataSource;
        }

        public Table GenerateTable(int countOfLine,int countOfColumn)
        {
            this.ResultTable = new Table(countOfLine, countOfColumn);
            this.lengthColumnResult = new int[countOfColumn];
            this.ParseCell();
            return this.ResultTable;
        }

        private void ParseCell()
        {
            string curData = this.PatternGetCurData();
            this.ResultAddData(indexColumnPatternTable, curData);
            if (this.indexLinePatternTable == this.PatternTable.Cells.GetLength(0) - 1
                && this.indexColumnPatternTable == this.PatternTable.Cells.GetLength(1) - 1)
            {
                return;
            }
            this.PatternNextCell();
            ParseCell();
        }

        private void ResultAddData(int column,string data)
        {
            this.lengthColumnResult[column]++;
            this.ResultTable.Cells[this.lengthColumnResult[column] - 1, column] = data;
        }

        private string PatternGetCurData()
        {
            return this.PatternTable.Cells[indexLinePatternTable,indexColumnPatternTable];
        }

        private void PatternSetCurData(string value)
        {
            this.PatternTable.Cells[indexLinePatternTable, indexColumnPatternTable] = value;
        }

        private void PatternSetPosition(int x,int y)
        {
            this.indexLinePatternTable = x;
            this.indexColumnPatternTable = y;
        }

        private void PatternAddPosition(int deltaX,int deltaY)
        {
            this.indexLinePatternTable += deltaX;
            this.indexColumnPatternTable += deltaY;
        }

        private void PatternNextCell()
        {
            if(this.indexColumnPatternTable == this.PatternTable.Cells.GetLength(0) - 1)
            {
                this.indexColumnPatternTable = 0;
                this.indexLinePatternTable++;
            }
            else
            {
                this.indexColumnPatternTable++;
            }
        }

    }
}
