using System.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using FCSlib;
using EGCMD;
using System.Linq;
using unvell.ReoGrid;


namespace WMS.TableGenerate
{
    public class TableGenerator
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
            Unhandled = 0,NormalHandled
        }

        private Jint.Engine jsEngine = EGCMDJsEngine.GetJsEngine(); //JavaScript引擎
        private EGCMDTranslator egcmdTranslator = new EGCMDTranslator(); //EGCMD翻译器

        private DataSet dataSource = null; //数据源

        private Worksheet patternTable = null; //模式表
        private CellState[,] stateMatrix = null; //状态矩阵

        private Worksheet resultTable = null; //目标表
        private Dictionary<int,int> lengthLineResult = new Dictionary<int, int>(); //目标表各行已有元素的数量
        private Dictionary<int,int> lengthColumnResult = new Dictionary<int, int>(); //目标表各列已有元素的数量

        public Worksheet PatternTable { get => patternTable; set => patternTable = value; }
        public DataSet DataSource { get => dataSource; set => this.UpdateDataSource(value); }
        public Worksheet ResultTable { get => resultTable; set => resultTable = value; }

        public TableGenerator()
        {
        }

        public TableGenerator(Worksheet patternTable, DataSet dataSource = null):this()
        {
            this.PatternTable = patternTable;
            if(dataSource != null) this.DataSource = dataSource;
        }

        public string TryGenerateTable(out Worksheet resultTable)
        {
            try
            {
                resultTable = this.GenerateTable();
                return null;
            }catch(Exception e)
            {
                resultTable = null;
                return e.Message;
            }
        }

        public Worksheet GenerateTable()
        {
            if(this.PatternTable == null)
            {
                throw new GenerateError("Pattern table not setted");
            }
            int patternLines = this.patternTable.RowCount;  //获取模式表行数
            int patternColumns = this.patternTable.ColumnCount; //获取模式表列数
            this.stateMatrix = new CellState[patternLines, patternColumns]; //初始化状态矩阵

            var workbook = new ReoGridControl();
            this.ResultTable = workbook.Worksheets[0]; //初始化目标表
            this.ResultTable.ColumnCount = this.PatternTable.ColumnCount;
            this.ResultTable.RowCount = this.PatternTable.RowCount;
            workbook.Worksheets.Remove(this.ResultTable);
            this.lengthColumnResult.Clear(); //目标表各行长度
            this.lengthLineResult.Clear(); //目标表各列长度
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
            bool gotNextCell = false;
            Func<Cell> GetOperationResultCell = () =>
            {
                if (gotNextCell)
                {
                    return ResultGetCurCellByColumn(column);
                }
                else
                {
                    gotNextCell = true;
                    ResultMoveToNextCellByColumn(column);
                    ResultSetCurCellByColumn(column, PatternTable.Cells[line, column]);
                    var resultCell = ResultGetCurCellByColumn(column);
                    resultCell.Data = "";
                    if (resultCell.IsMergedCell)
                    {
                        for (int col = resultCell.Column; col <= resultCell.Column + resultCell.GetColspan(); col++)
                        {
                            this.ResultMoveToNextCellByColumn(col, resultCell.GetRowspan() - 1);
                        }
                    }
                    return resultCell;
                }
            };

            //单元格的状态为已经处理过，则跳过
            if (this.PatternGetState(line,column) == CellState.NormalHandled)
            {
                return;
            }

            //否则开始处理单元格数据
            Cell curPatternCell = this.PatternGetCell(line,column);

            //模式表单元格为null，则目标表单元格也为null
            if (curPatternCell == null)
            {
                this.ResultMoveToNextCellByColumn(column);
                this.UpdateToNextState(line, column, attribute);
                return;
            }

            //无效单元格，则直接跳过
            if(curPatternCell.IsValidCell == false)
            {
                this.UpdateToNextState(line, column, attribute);
                return;
            }

            //单元格为空则直接向目标表加入单元格内容
            if (curPatternCell.Data.ToString().Length == 0)
            {
                Cell curResultCell = GetOperationResultCell();
                this.UpdateToNextState(line, column, attribute);
                return;
            }

            List<EGCMDCommand> commandList = egcmdTranslator.Translate(curPatternCell.Data.ToString());
            foreach (EGCMDCommand command in commandList)
            {
                if (command is EGCMDCommand.WRITE)
                {
                    EGCMDCommand.WRITE writeCommand = command as EGCMDCommand.WRITE;
                    string exprResult; //表达式计算结果
                    try
                    {
                        exprResult = jsEngine.Execute(writeCommand.JsExpr).GetCompletionValue().ToString();
                    }
                    catch (Exception e)
                    {
                        throw new LogicError(line, column, e.Message);
                    }
                    Cell curResultCell = GetOperationResultCell();
                    curResultCell.Data += exprResult;
                    this.UpdateToNextState(line, column, attribute);
                }
                if (command is EGCMDCommand.REPEAT)
                {
                    var repeatCommand = command as EGCMDCommand.REPEAT;
                    int countLines = repeatCommand.Rows;
                    int countColumns = repeatCommand.Columns;
                    string varName = repeatCommand.VarName;
                    object[] range; //循环范围
                    try
                    {
                        range = (object[])jsEngine.Execute(repeatCommand.Range).GetCompletionValue().ToObject();
                    }
                    catch
                    {
                        throw new LogicError(line, column, "Repeat range must be iterable");
                    }
                    //Console.Write("循环开始位置：{0},{1}", line, column);
                    //Console.WriteLine("循环次数：{0}", range.Length);
                    for (int i = column + 1; i < column + countColumns; i++) //将REPEAT单元格同一行右面的REPEAT范围内的单元格状态设为NormalHandled
                    {
                        this.PatternSetState(line, i, CellState.NormalHandled);
                    }
                    for (int i = 0; i < countLines; i++) //将Repeat块部分设置成UnHandled
                    {
                        for (int j = 0; j < countColumns; j++)
                        {
                            this.PatternSetState(line + 1 + i, column + j, CellState.Unhandled);
                        }
                    }
                    foreach (object time in range) //开始循环
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
                    if (!attribute.InRepeat) //如果已经是最外层循环，则循环完毕后将自身设为NormalHandled
                    {
                        this.PatternSetState(line, column, CellState.NormalHandled);
                    }
                    for (int i = 0; i < countLines; i++) //将Repeat块的所有单元格的状态设置成NormalHandled
                    {
                        for (int j = 0; j < countColumns; j++)
                        {
                            this.PatternSetState(line + 1 + i, column + j, CellState.NormalHandled);
                        }
                    }
                }
                if(command is EGCMDCommand.TEXT)
                {
                    var textCommand = command as EGCMDCommand.TEXT;
                    Cell curResultCell = GetOperationResultCell();
                    curResultCell.Data += textCommand.Text;
                }
                if (command is EGCMDCommand.SET_COLOR)
                {
                    var setColorCommand = command as EGCMDCommand.SET_COLOR;
                    Cell curResultCell = GetOperationResultCell();
                    try
                    {
                        Color color = (Color)this.jsEngine.Execute(setColorCommand.JsExpr).GetCompletionValue().ToObject();
                        curResultCell.Style.BackColor = color;
                    }
                    catch
                    {
                        curResultCell.Data += "#UNKNOWN COLOR";
                        throw;
                    }
                }
            }
        }

        private void UpdateToNextState(int line,int column,ParseAttribute attribute)
        {
            if (attribute.InRepeat) //循环中不改变单元格状态
            {
                return;
            }
            if (this.PatternGetState(line, column) == CellState.Unhandled)
            {
                this.PatternSetState(line, column, CellState.NormalHandled);
            }
        }

        private void ResultMoveToNextCellByColumn(int column,int step = 1)
        {
            if(this.lengthColumnResult.ContainsKey(column) == false)
            {
                this.lengthColumnResult.Add(column, 0);
            }
            this.lengthColumnResult[column] += step;
            if(ResultTable.RowCount < this.lengthColumnResult[column])
            {
                resultTable.RowCount *= 2;
            }
        }

        private Cell ResultGetCurCellByColumn(int column)
        {
            return this.ResultTable.Cells[this.lengthColumnResult[column] - 1, column];
        }

        private void ResultSetCurCellByColumn(int column,Cell cell)
        {
            var resultCurCell = this.ResultGetCurCellByColumn(column);
            resultCurCell.Body = cell.Body;
            resultCurCell.Data = cell.Data;
            resultCurCell.DataFormat = cell.DataFormat;
            resultCurCell.DataFormatArgs = cell.DataFormatArgs;
            resultCurCell.Formula = cell.Formula;
            resultCurCell.IsReadOnly = cell.IsReadOnly;
            resultCurCell.Style = cell.Style;
            resultCurCell.Tag = cell.Tag;
            resultCurCell.TraceFormulaDependents = cell.TraceFormulaDependents;
            resultCurCell.TraceFormulaPrecedents = cell.TraceFormulaPrecedents;
            if (cell.IsMergedCell)
            {
                this.ResultTable.MergeRange(resultCurCell.Row, resultCurCell.Column, cell.GetRowspan(), cell.GetColspan());
            }
        }

        private Cell PatternGetCell(int line,int column)
        {
            var cell = this.PatternTable.GetCell(line, column);
            if (cell == null)
            {
                return null;
            }
            if (cell.Data == null)
            {
                cell.Data = "";
            }
            return cell;
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
