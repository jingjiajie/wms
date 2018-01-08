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

            if (compiler.Compile(input,out var commands,out string errorMessage) == false)
            {
                Console.WriteLine("错误:"+errorMessage);
                Console.Read();
                return;
            }
            foreach(var command in commands)
            {
                Console.WriteLine("识别到的命令："+command.ToString());
            }
            Console.Read();
        }
    }
}
