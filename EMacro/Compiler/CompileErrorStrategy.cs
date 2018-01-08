using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr4.Runtime;

namespace EMacro
{
    class CompileErrorStrategy : DefaultErrorStrategy
    {
        public override void Recover(Parser recognizer, RecognitionException e)
        {
            throw e;
        }

        public override IToken RecoverInline(Parser recognizer)
        {
            throw new InputMismatchException(recognizer);
        }

        public override void Sync(Parser recognizer)
        {
            
        }

        public override void ReportError(Parser recognizer, RecognitionException e)
        {
            base.ReportError(recognizer, e);
        }
    }
}
