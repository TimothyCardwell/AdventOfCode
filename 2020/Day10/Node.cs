using System;

namespace Day10
{
    public class Node
    {
        public readonly int Value;
        public Node LeftNode { get; private set; }
        public Node MiddleNode { get; private set; }
        public Node RightNode { get; private set; }
        public bool IsLeafNode
        {
            get
            {
                return LeftNode == null && MiddleNode == null && RightNode == null;
            }
        }

        public Node(int value)
        {
            Value = value;
        }

        public Node AddNode(int value)
        {
            if (LeftNode == null)
            {
                LeftNode = new Node(value);
                return LeftNode;
            }
            else if (MiddleNode == null)
            {
                MiddleNode = new Node(value);
                return MiddleNode;
            }
            else if (RightNode == null)
            {
                RightNode = new Node(value);
                return RightNode;
            }
            else
            {
                throw new InvalidOperationException("Cannot add another node - max of 3 nodes have already been added!");
            }
        }
    }
}