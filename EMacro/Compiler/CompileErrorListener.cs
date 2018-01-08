using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Antlr4.Runtime;

namespace EMacro
{
    class CompileErrorListener : BaseErrorListener
    {
        string errorMessage = null;

        public override void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            errorMessage = string.Format(msg);
            return;
        }

        public string GetErrorMessage()
        {
            return this.errorMessage;
        }
    }
}
