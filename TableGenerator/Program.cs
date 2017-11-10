using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace WMS.TableGenerate
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    Table t = new Table(0,0);
        //    t.Cells = new string[,]
        //    {
        //        {"REPEAT AREA 1 2 VAR i IN ['A','B','C']","LINE1","LINE1"},
        //        {"REPEAT AREA 1 2 VAR j IN [1,2,3]","LINE2","REPEAT AREA 1 1 VAR k IN [10,11,12]" },
        //        {"WRITE i","WRITE j","WRITE k"}
        //    };
        //    TableGenerator tg = new TableGenerator(t, null);
        //    Table result = tg.GenerateTable(27, 3);
        //    Console.Write(result.ToString());
        //    Console.ReadLine();
        //}

        private static void Main(string[] args)
        {
            var ds = new DataSet();
            var table = new DataTable();
            ds.Tables.Add(table);
            table.TableName = "table1";
            table.Columns.Add("ID");
            table.Columns.Add("Name");
            table.Rows.Add("1", "小明");
            table.Rows.Add("2", "小红");
            table.Rows.Add("3", "小华");
            Table t = new Table(2, 2);
            t[0, 0].Data = "REPEAT AREA 1 2 VAR i IN range(table1.ID.length)";
            t[1, 0].Data = "WRITE table1.ID[i]";
            t[1, 1].Data = "WRITE table1.Name[i]";
            
            var tg = new TableGenerator(t, ds);
            var result = tg.GenerateTable(10, 2);
            
            Console.WriteLine(result.ToString());
            Console.ReadLine();
        }

    }
}
