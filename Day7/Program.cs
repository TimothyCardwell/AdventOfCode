using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day7
{
    class Program
    {
        private const string Input = @"pbga (66)
xhth (57)
ebii (61)
havc (66)
ktlj (57)
fwft (72) -> ktlj, cntj, xhth
qoyq (66)
padx (45) -> pbga, havc, qoyq
tknk (41) -> ugml, padx, fwft
jptl (61)
ugml (68) -> gyxo, ebii, jptl
gyxo (61)
cntj (57)";
        static void Main(string[] args)
        {
            Dictionary<string, Tuple<string, int, string[]>> parsedInput = new Dictionary<string, Tuple<string, int, string[]>>();
            HashSet<string> parentNodes = new HashSet<string>();
            HashSet<string> childNodes = new HashSet<string>();
            using(StreamReader streamReader = new StreamReader("input.txt"))
            //using(StringReader streamReader = new StringReader(Input))
            {
                string line;
                while((line = streamReader.ReadLine()) != null)
                {
                    string[] node;
                    string[] children;
                    if (line.Contains(" -> "))
                    {
                        string[] nodeAndChildren = line.Split(" -> ");
                        node = nodeAndChildren[0].Split(" ");
                        children = nodeAndChildren[1].Split(", ");


                    }
                    else
                    {
                        node = line.Split(" ");
                        children = null;
                    }

                    string value = node[0];
                    int weight = Convert.ToInt32(node[1].Substring(1, node[1].Length - 2));
                    parsedInput.Add(value, new Tuple<string, int, string[]>(value, weight, children));
                    if(children != null)
                    {
                        parentNodes.Add(value);
                        foreach(string child in children)
                        {
                            childNodes.Add(child);
                        }
                    }
                }
            }

            parentNodes.ExceptWith(childNodes);
            Tuple<string, int, string[]> rootNodeTuple = null;
            foreach(string key in parsedInput.Keys)
            {
                if(parentNodes.Contains(key))
                {
                    rootNodeTuple = parsedInput[key];
                    break;
                }
            }

            Node rootNode = BuildTree(rootNodeTuple, parsedInput);

            SumWeights(rootNode);

            Console.WriteLine(rootNode.Value);
            Console.ReadLine();
        }

        private static Node BuildTree(Tuple<string, int, string[]> rootNode, Dictionary<string, Tuple<string, int, string[]>> allNodes)
        {
            Node node = new Node(rootNode.Item1, rootNode.Item2, null);

            if (rootNode.Item3 != null)
            {
                foreach (string childNode in rootNode.Item3)
                {
                    node.Children.Add(BuildTree(allNodes[childNode], allNodes));
                }
            }

            return node;
        }

        private static int SumWeights(Node rootNode)
        {
            if(rootNode.Children.Count == 0)
            {
                return rootNode.Weight;
            }
            else
            {
                int totalWeight = 0;
                List<int> childWeights = new List<int>();
                foreach (Node childNode in rootNode.Children)
                {
                    int childWeight = SumWeights(childNode);
                    totalWeight += childWeight;
                    childWeights.Add(childWeight);
                }

                if(childWeights.Distinct().Count() > 1)
                {
                    // The first time this is hit will be the unbalanced program
                    // Specifically, the index of the mismatched value in childWeights will match the index
                    // of the rootNode.Children that is off balance. Then take the weight of that node and subtract
                    // it by the difference of the two weights in childWeights
                    Console.ReadLine();
                }
                return totalWeight + rootNode.Weight;
            }
        }
    }
}
