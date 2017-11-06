using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PortableFCSLib;

namespace WMS.TableGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Table t = new Table(10,10);
            t.Cells[0, 0] = "start";
            t.Cells[5, 5] = "123";
            t.Cells[9, 9] = "end";
            TableGenerator tg = new TableGenerator(t,null);
            Console.WriteLine(tg.GenerateTable(10,10));
            Console.ReadLine();
        }
    }
}
