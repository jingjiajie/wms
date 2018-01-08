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
        public bool Compile(string command,out List<Command> commands,out string errorMessage)
        {
            var antlrInputStream = new AntlrInputStream(command);
            //词法分析
            var lexer = new EMacroLexer(antlrInputStream);
            var tokenStream = new CommonTokenStream(lexer);
            //语法分析
            var parser = new EMacroParser(tokenStream);
            parser.ErrorHandler = new CompileErrorStrategy(); //错误自动退出
            parser.RemoveErrorListeners();
            CompileErrorListener compileErrorListener = new CompileErrorListener(); //输出错误信息
            parser.AddErrorListener(compileErrorListener);
            EMacroParser.StatContext tree = null; //分析树
            try
            {
                tree = parser.stat();
            }
            catch
            {
                errorMessage = compileErrorListener.GetErrorMessage();
                commands = new List<Command>();
                return false;
            }
            //遍历分析树
            var walker = new ParseTreeWalker();
            commands = new List<Command>();
            walker.Walk(new CompileListener(commands), tree);
            errorMessage = null;
            return true;
        }
    }
}
