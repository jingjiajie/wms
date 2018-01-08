using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace EMacro
{
    public class Compiler
    {
        public List<Command> Compile(string command)
        {
            var antlrInputStream = new AntlrInputStream(command);
            var lexer = new EMacroLexer(antlrInputStream);
            var tokenStream = new CommonTokenStream(lexer);
            var parser = new EMacroParser(tokenStream);
            var tree = parser.stat();
            var walker = new ParseTreeWalker();
            List<Command> commandList = new List<Command>();
            walker.Walk(new CompileListener(commandList), tree);
            return commandList;
        }
    }
}
