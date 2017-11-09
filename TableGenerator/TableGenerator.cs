using System.Data;
using System;
using System.Collections.Generic;
using FCSlib;

namespace WMS.TableGenerator
{
    class TableGenerator
    {
        class TablePosition
        {
            public int Line;
            public int Column;
            public TablePosition(int line,int column)
            {
                this.Line = line;
                this.Column = column;
            }
        };

        class ParseAttribute
        {
            public bool InRepeat = false;
        }

        enum CellState
        {
            Unhandled = 0,NormalHandled,RepeatHandled
        }

        class LogicError : Exception
        {
            public LogicError(int line, int column, string message)
                : base(String.Format("Error at ({0},{1}): {2}", line, column, message)){}
        }

        private Jint.Engine jsEngine;

        private DataSet dataSource = null; //数据源

        private Table patternTable = null; //模式表
        private CellState[,] stateMatrix = null; //状态矩阵

        private Table resultTable = null; //目标表
        private int[] lengthLineResult; //目标表各行已有元素的数量
        private int[] lengthColumnResult; //目标表各列已有元素的数量

        public Table PatternTable { get => patternTable; set => patternTable = value; }
        public DataSet DataSource { get => dataSource; set => this.UpdateDataSource(value); }
        public Table ResultTable { get => resultTable; set => resultTable = value; }

        private TableGenerator()
        {
            this.jsEngine = new Jint.Engine();
            var basicFunctions = new JsBasicFunctions();
            jsEngine.Execute(basicFunctions.GetAllJsFuncStr());
        }

        public TableGenerator(Table patternTable, DataSet dataSource):this()
        {
            this.PatternTable = patternTable;
            if(dataSource != null) this.DataSource = dataSource;
        }

        public Table GenerateTable(int countOfLine,int countOfColumn)
        {
            int patternLines = this.patternTable.Cells.GetLength(0); //获取模式表行数
            int patternColumns = this.patternTable.Cells.GetLength(1); //获取模式表列数

            this.stateMatrix = new CellState[patternLines, patternColumns]; //初始化状态矩阵
            this.ResultTable = new Table(countOfLine, countOfColumn); //初始化目标表
            this.lengthColumnResult = new int[countOfColumn]; //目标表各行长度
            this.lengthLineResult = new int[countOfLine]; //目标表各列长度
            for(int i = 0; i < patternLines; i++) //顺序解析模式表单元格
            {
                for(int j = 0; j < patternColumns; j++)
                {
                    this.ParseCell(i, j);
                }
            }

            return this.ResultTable; //返回目标表
        }

        private void UpdateDataSource(DataSet ds) //更新数据源（将DataSet转换为js对象）
        {
            this.dataSource = ds;
            foreach(var item in this.DataSetToDictionary(ds))
            {
                this.jsEngine.SetValue(item.Key, item.Value);
            }
        }

        private void ParseCell(int line,int column)
        {
            ParseCell(line,column,new ParseAttribute());
        }

        private void ParseCell(int line,int column,ParseAttribute attribute)
        {
            //Console.WriteLine("处理位置：{0},{1} InRepeat:{2}",line,column,attribute.InRepeat);
            //PrintStateMatrix();
            //Console.WriteLine();
            if (this.PatternGetState(line,column) == CellState.NormalHandled)
            {
                return;
            }else if(attribute.InRepeat == false && this.PatternGetState(line,column) == CellState.RepeatHandled)
            {
                return;
            }
            string curData = this.PatternGetData(line,column);
            if(curData == null || curData.Length == 0) //单元格为空则直接向目标表加入单元格内容
            {
                if (this.PatternGetState(line, column) == CellState.Unhandled)
                {
                    if (!attribute.InRepeat)
                    {
                        this.ResultAddDataByColumn(column, curData);
                        this.PatternSetState(line, column, CellState.NormalHandled);
                    }
                    else
                    {
                        this.ResultAddDataByColumn(column, curData);
                    }
                }
                else if (this.PatternGetState(line, column) == CellState.RepeatHandled)
                {
                    this.ResultAddDataByColumn(column, curData);
                }
                return;
            }
            string[] cmdList = curData.Split(' ');
            switch (cmdList[0]) //分析单元格内容，如果为指令则运行，否则直接向目标表写入
            {
                case "WRITE":
                    {
                        if (cmdList.Length < 2)
                        {
                            throw new LogicError(line,column,"Expected WRITE <Expression> statement");
                        }
                        string expr = cmdList[1];
                        string result;
                        try
                        {
                            result = jsEngine.Execute(expr).GetCompletionValue().ToString();
                        }catch(Exception e)
                        {
                            throw new LogicError(line, column,e.Message);
                        }

                        if (this.PatternGetState(line, column) == CellState.Unhandled)
                        {
                            if (!attribute.InRepeat)
                            {
                                this.ResultAddDataByColumn(column, result);
                                this.PatternSetState(line, column, CellState.NormalHandled);
                            }
                            else
                            {
                                this.ResultAddDataByColumn(column, result);
                            }
                        }
                        else if (this.PatternGetState(line, column) == CellState.RepeatHandled)
                        {
                            this.ResultAddDataByColumn(column, result);
                        }
                        break;
                    }
                case "REPEAT":
                    {
                        if (cmdList.Length <= 7 || cmdList[1] != "AREA" || cmdList[4] != "VAR" || cmdList[6] != "IN")
                        {
                            throw new LogicError(line,column,"Expected REPEAT AREA <lines,columns> VAR <variable> IN <Expression> statement");
                        }
                        int posJsExpr = Functional.FoldL((int len, string cur) => len + cur.Length + 1, 0, Functional.Take(7, cmdList));
                        int countLines = Convert.ToInt32(cmdList[2]);
                        int countColumns = Convert.ToInt32(cmdList[3]);
                        string varName = cmdList[5];
                        object[] range; //循环范围
                        try
                        {
                            range = (object[])jsEngine.Execute(curData.Substring(posJsExpr)).GetCompletionValue().ToObject();
                        }catch
                        {
                            throw new LogicError(line,column,"Repeat range must be iterable");
                        }
                        //Console.Write("循环开始位置：{0},{1}", line, column);
                        //Console.WriteLine("循环次数：{0}", range.Length);
                        for (int i = column + 1; i < column + countColumns; i++) //将REPEAT单元格同一行右面的REPEAT范围内的单元格状态设为NormalHandled
                        {
                            this.PatternSetState(line, i, CellState.NormalHandled);
                        }
                        foreach (object time in range)
                        {
                            jsEngine.SetValue(varName, time);
                            for (int i = 0; i < countLines; i++)
                            {
                                for (int j = 0; j < countColumns; j++)
                                {
                                    this.ParseCell(line + 1 + i, column + j, new ParseAttribute() { InRepeat = true });
                                }
                            }
                        }
                        for (int i = 0; i < countLines + 1; i++) //将Repeat块的所有未处理状态的单元格的状态设置成RepeatHandled
                        {
                            for (int j = 0; j < countColumns; j++)
                            {
                                if (this.PatternGetState(line + i, column + j) == CellState.Unhandled)
                                {
                                    this.PatternSetState(line + i, column + j, CellState.RepeatHandled);
                                }
                            }
                        }
                        break;
                    };
                default:
                    {
                        if (this.PatternGetState(line, column) == CellState.Unhandled)
                        {
                            if (!attribute.InRepeat)
                            {
                                this.ResultAddDataByColumn(column, curData);
                                this.PatternSetState(line, column, CellState.NormalHandled);
                            }
                            else
                            {
                                this.ResultAddDataByColumn(column, curData);
                            }
                        }
                        else if (this.PatternGetState(line, column) == CellState.RepeatHandled)
                        {
                            this.ResultAddDataByColumn(column, curData);
                        }
                        break;
                    }

            }
        }

        private void ResultAddDataByColumn(int column,string data)
        {
            //Console.WriteLine("ResultAddDataByColumn({0},{1})", column, data);
            this.lengthColumnResult[column]++;
            this.ResultTable.Cells[this.lengthColumnResult[column] - 1, column] = data;
        }

        private void ResultAddDataByLine(int line, string data)
        {
            this.lengthLineResult[line]++;
            this.ResultTable.Cells[this.lengthLineResult[line] - 1, line] = data;
        }

        private string PatternGetData(int line,int column)
        {
            return this.PatternTable.Cells[line, column];
        }

        private void PatternSetData(int line,int column,string value)
        {
            this.PatternTable.Cells[line, column] = value;
        }

        private CellState PatternGetState(int line,int column)
        {
            return this.stateMatrix[line,column];
        }

        private void PatternSetState(int line,int column,CellState state)
        {
            this.stateMatrix[line, column] = state;
        }

        private void PrintStateMatrix()
        {
            int count = 0;
            foreach (var item in this.stateMatrix)
            {
                count++;
                Console.Write(item + " ");
                if (count % this.stateMatrix.GetLength(1) == 0)
                {
                    Console.WriteLine();
                }
            }
        }

        private Dictionary<string, Dictionary<string, object[]>> DataSetToDictionary(DataSet ds)
        {
            var dataSetDictionary = new Dictionary<string, Dictionary<string, object[]>>();
            foreach (DataTable table in ds.Tables)
            {
                var tableDictionary = new Dictionary<string, object[]>();
                dataSetDictionary.Add(table.TableName, tableDictionary);
                int rowCount = table.Rows.Count;
                foreach (DataColumn column in table.Columns)
                {
                    var valueArray = new object[rowCount];
                    tableDictionary.Add(column.ColumnName, valueArray);
                    for (int i = 0; i < rowCount; i++)
                    {
                        valueArray[i] = table.Rows[i][column];
                    }
                }
            }
            return dataSetDictionary;
        }
    }
}
