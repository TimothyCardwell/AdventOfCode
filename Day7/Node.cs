using System;
using System.Collections.Generic;
using System.Text;

namespace Day7
{
    public class Node
    {
        public string Value { get; set; }
        public int Weight { get; set; }
        public List<Node> Children { get; set; }

        public Node(string value, int weight, List<Node> children)
        {
            Value = value;
            Weight = weight;
            Children = children ?? new List<Node>();
        }
    }
}
