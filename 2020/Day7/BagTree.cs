using System;
using System.Collections.Generic;
using System.Linq;

namespace Day7
{
    public class BagTree
    {
        public readonly BagNode _rootNode;

        public BagTree()
        {
            _rootNode = new BagNode("root");
        }

        public BagNode AddBagToTree(string description, List<Tuple<int, string>> containedBags)
        {
            var bag = FindBag(description, _rootNode);
            if (bag == null)
            {
                bag = new BagNode(description);

                // Set doubly linked list to parent bag... may be removed later if we find this isn't a root node
                bag.ContainedIn.Add(new Tuple<BagNode, int>(_rootNode, 1));
                _rootNode.Contains.Add(new Tuple<BagNode, int>(bag, 1));
            }

            foreach (var containedBag in containedBags)
            {
                var newContainedBag = AddBagToTree(containedBag.Item2, new List<Tuple<int, string>>());

                // It's possible the bag was already added to the root node
                // Need to remove that relationship because it's not a root bag
                if (newContainedBag.ContainedIn.Select(x => x.Item1).Contains(_rootNode))
                {
                    // Break the double link for the root node
                    newContainedBag.ContainedIn.Remove(newContainedBag.ContainedIn.Single(x => x.Item1 == _rootNode));
                    _rootNode.Contains.Remove(_rootNode.Contains.Single(x => x.Item1 == newContainedBag));
                }

                // Set doubly linked list to parent bag
                bag.Contains.Add(new Tuple<BagNode, int>(newContainedBag, containedBag.Item1));
                newContainedBag.ContainedIn.Add(new Tuple<BagNode, int>(bag, containedBag.Item1));
            }

            return bag;
        }

        public BagNode FindBag(string description)
        {
            return FindBag(description, _rootNode);
        }

        private BagNode FindBag(string description, BagNode currentBag)
        {
            if (string.Equals(currentBag.Description, description))
            {
                return currentBag;
            }

            if (currentBag == null || !currentBag.Contains.Any())
            {
                return null;
            }

            var bagsFound = new List<BagNode>();
            foreach (var bag in currentBag.Contains.Select(x => x.Item1))
            {
                var bagFound = FindBag(description, bag);
                if (bagFound != null)
                {
                    bagsFound.Add(bagFound);
                }
            }

            return bagsFound.FirstOrDefault();
        }
    }
}