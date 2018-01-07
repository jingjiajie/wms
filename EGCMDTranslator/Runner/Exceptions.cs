using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EGCMD
{
    public class GenerateError : Exception
    {
        public GenerateError(string message) : base(message) { }
    }

    public class LogicError : GenerateError
    {
        public LogicError(int line, int column, string message)
            : base(String.Format("Error at ({0},{1}): {2}", line, column, message)) { }
    }
}
