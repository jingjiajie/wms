using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace EGCMD
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            var antlrInputStream = new AntlrInputStream(input);
            var lexer = new EGCMDLexer(antlrInputStream);
            var tokenStream = new CommonTokenStream(lexer);
            var parser = new EGCMDParser(tokenStream);
            var tree = parser.stat();
            var walker = new ParseTreeWalker();
            List<EGCMDCommand> commandList = new List<EGCMDCommand>();
            walker.Walk(new EGCMDTranslateListener(commandList), tree);
            foreach(var command in commandList)
            {
                Console.WriteLine("识别到的命令："+command.ToString());
            }
            Console.Read();
        }
    }
}
