using System;
using System.Collections.Generic;
using System.Text;

namespace Day3
{
    /// <summary>
    /// Single linked node, always points 'inwards'
    /// </summary>
    public class Node
    {
        public int Value { get; set; }
        public Node Left { get; set; }
        public Node Top { get; set; }
        public Node Right { get; set; }
        public Node Bottom { get; set; }

        public Node(int value)
        {
            Value = value;
        }
    }
}
