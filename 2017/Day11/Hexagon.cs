using System;
using System.Collections.Generic;
using System.Text;

namespace Day11
{
    public class Hexagon
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Hexagon(string value)
        {
            switch(value)
            {
                case "n":
                    X = 0;
                    Y = -1;
                    break;
                case "s":
                    X = 0;
                    Y = 1;
                    break;
                case "ne":
                    X = 1;
                    Y = -1;
                    break;
                case "se":
                    X = 1;
                    Y = 0;
                    break;
                case "nw":
                    X = -1;
                    Y = 0;
                    break;
                case "sw":
                    X = -1;
                    Y = 1;
                    break;
                default:
                    throw new ArgumentException($"Invalid value: {value}");
            }
        }
    }
}
