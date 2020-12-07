using System;
using System.Collections.Generic;

namespace Day7
{
    public class BagNode
    {
        public readonly Guid Id;
        public readonly string Description;
        public readonly List<Tuple<BagNode, int>> ContainedIn;
        public readonly List<Tuple<BagNode, int>> Contains;
        public readonly bool IsLeafNode;

        public BagNode(string description)
        {
            Id = Guid.NewGuid();
            Description = description;
            ContainedIn = new List<Tuple<BagNode, int>>();
            Contains = new List<Tuple<BagNode, int>>();
        }
    }
}