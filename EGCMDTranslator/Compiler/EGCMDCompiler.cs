using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace EGCMD
{
    public class EGCMDTranslator
    {
        public List<EGCMDCommand> Compile(string command)
        {
            var antlrInputStream = new AntlrInputStream(command);
            var lexer = new EGCMDLexer(antlrInputStream);
            var tokenStream = new CommonTokenStream(lexer);
            var parser = new EGCMDParser(tokenStream);
            var tree = parser.stat();
            var walker = new ParseTreeWalker();
            List<EGCMDCommand> commandList = new List<EGCMDCommand>();
            walker.Walk(new EGCMDCompileListener(commandList), tree);
            return commandList;
        }
    }
}
