using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMacro
{
    public class Command
    {
        public class TEXT : Command
        {
            string text;
            public string Text { get => text; set => text = value; }

            public TEXT(string text)
            {
                this.text = text;
            }

            public override string ToString()
            {
                return String.Format("TEXT: {0}", text.Trim());
            }
        }

        public class WRITE : Command
        {
            string jsExpr;
            public string JsExpr { get => jsExpr; set => jsExpr = value; }

            public WRITE(string jsExpr)
            {
                this.JsExpr = jsExpr;
            }

            public override string ToString()
            {
                return String.Format("WRITE {0} END",JsExpr.Trim());
            }
        }

        public class REPEAT : Command
        {
            int rows;
            int columns;
            string varName;
            string range; //jsExpr

            public REPEAT(int rows, int columns, string varName, string range)
            {
                Rows = rows;
                Columns = columns;
                VarName = varName;
                Range = range;
            }

            public int Rows { get => rows; set => rows = value; }
            public int Columns { get => columns; set => columns = value; }
            public string VarName { get => varName; set => varName = value; }
            public string Range { get => range; set => range = value; }

            public override string ToString()
            {
                return String.Format("REPEAT AREA {0} {1} VAR {2} IN {3} END",Rows,Columns,VarName,Range.Trim());
            }
        }

        public class SET_COLOR : Command
        {
            string jsExpr;

            public SET_COLOR(string jsExpr)
            {
                this.JsExpr = jsExpr;
            }

            public string JsExpr { get => jsExpr; set => jsExpr = value; }

            public override string ToString()
            {
                return String.Format("SET COLOR {0} END",JsExpr.Trim());
            }
        }
    }
}
