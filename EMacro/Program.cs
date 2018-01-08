using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace EMacro
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            Compiler compiler = new Compiler();
            var commandList = compiler.Compile(input);
            foreach(var command in commandList)
            {
                Console.WriteLine("识别到的命令："+command.ToString());
            }
            Console.Read();
        }
    }
}
