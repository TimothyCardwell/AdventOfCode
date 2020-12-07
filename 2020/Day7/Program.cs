using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day7
{
    class Program
    {
        internal static BagTree _tree;

        static void Main(string[] args)
        {
            var bagRules = File.ReadAllLines("./input.txt");
            ParseInput(bagRules);

            Part1();
            Part2();

            Console.ReadLine();
        }

        public static void Part1()
        {
            var bag = _tree.FindBag("shiny gold");
            var allParentBags = Part1Recursive(bag);
            Console.WriteLine(allParentBags.Distinct().Count() - 1); // Minus one so that we don't count the 'shiny gold' bag itself
        }

        public static void Part2()
        {
            var bag = _tree.FindBag("shiny gold");
            var requiredBagCount = Part2Recursive(bag);
            Console.WriteLine(requiredBagCount);
        }

        private static void ParseInput(string[] input)
        {
            _tree = new BagTree();
            foreach (var bagRule in input)
            {
                var bagAndContainSplit = bagRule.Split(" bags contain ");
                var bagDescription = bagAndContainSplit[0];

                var containedBags = new List<Tuple<int, string>>();
                var contains = bagAndContainSplit[1].Split(", ");
                foreach (var containString in contains)
                {
                    var containPieces = containString.Split(" ");
                    if (containPieces.Length == 4)
                    {
                        var containedBagCount = Convert.ToInt32(containPieces[0]);
                        var containedBagDescription = containPieces[1] + " " + containPieces[2];
                        containedBags.Add(new Tuple<int, string>(containedBagCount, containedBagDescription));
                    }
                    else if (containPieces.Length == 3)
                    {
                        // Do nothing, this bag has no contained bags
                    }
                    else
                    {
                        Console.WriteLine($"Assumption is inaccurate, parsed string doesn't match 3 or 4 pieces: {containString}");
                    }
                }

                _tree.AddBagToTree(bagDescription, containedBags);
            }
        }

        private static List<Guid> Part1Recursive(BagNode currentBag)
        {
            var idList = new List<Guid>();

            // Root bag is fake, and shouldn't count
            if (currentBag.Description == "root")
            {
                return null;
            }

            idList.Add(currentBag.Id);

            foreach (var parentBag in currentBag.ContainedIn.Select(x => x.Item1))
            {
                var parentGuids = Part1Recursive(parentBag);
                if (parentGuids != null)
                {
                    idList = idList.Concat(parentGuids).ToList();
                }
            }

            return idList;
        }

        private static int Part2Recursive(BagNode currentBag)
        {
            if (!currentBag.Contains.Any())
            {
                return 0;
            }

            var totalContainsCount = 0;
            foreach (var bag in currentBag.Contains)
            {
                totalContainsCount += bag.Item2 + (bag.Item2 * Part2Recursive(bag.Item1));
            }

            return totalContainsCount;
        }
    }
}
