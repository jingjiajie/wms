using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace WMS.TableGenerator
{
    public class Cell
    {
        string data = "";
        Color color = new Color();

        public string Data { get => data; set => data = value; }
        public Color Color { get => color; set => color = value; }

        public Cell() { }

        public Cell(string data)
        {
            this.Data = data;
        }

        public Cell(string data,Color color)
        {
            this.Data = data;
            this.Color = color;
        }

        public override string ToString()
        {
            return this.Data;
        }
    }
}
