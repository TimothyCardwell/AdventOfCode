using System;
using System.Collections.Generic;
using System.Linq;

namespace Day10
{
    public class Tree
    {
        private readonly Node _root;

        public Tree(List<int> sortedList)
        {
            _root = new Node(0);
            BuildTree(_root, sortedList);
        }

        public void BuildTree(Node node, List<int> values)
        {
            // Reached the end of the list, need to add on the phone adapter
            if (!values.Any())
            {
                node.AddNode(node.Value + 3);
                return;
            }

            if (values[0] - node.Value <= 3)
            {
                var newNode = node.AddNode(values[0]);
                BuildTree(newNode, values.Skip(1).ToList());
            }

            if (values.Count > 1 && values[1] - node.Value <= 3)
            {
                var newNode = node.AddNode(values[1]);
                BuildTree(newNode, values.Skip(2).ToList());
            }

            if (values.Count > 2 && values[2] - node.Value <= 3)
            {
                var newNode = node.AddNode(values[2]);
                BuildTree(newNode, values.Skip(3).ToList());
            }
        }

        public int CountLeafNodes()
        {
            return CountLeafNodes(_root);
        }

        private int CountLeafNodes(Node node)
        {
            // This is not a 'full' tree, so non-leaf nodes may have null children
            if (node == null)
            {
                return 0;
            }

            if (node.IsLeafNode)
            {
                return 1;
            }

            return CountLeafNodes(node.LeftNode) + CountLeafNodes(node.MiddleNode) + CountLeafNodes(node.RightNode);
        }
    }
}