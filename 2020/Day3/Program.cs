using System;
using System.IO;
using System.Linq;

namespace Day3
{
    class Program
    {
        private static char[][] _map;

        static void Main(string[] args)
        {
            var mapStrings = File.ReadAllLines("./input.txt");
            _map = mapStrings.Select(x => x.ToCharArray()).ToArray();

            Part1(3, 1);
            Part2();

            Console.ReadLine();
        }

        private static int Part1(int offsetX, int offsetY)
        {
            var currentX = 0;
            var currentY = 0;
            var treeCount = 0;
            var isTraversalComplete = false;

            while (!isTraversalComplete)
            {
                // Need to do map[Y][X] here because of how I built the map
                if (_map[currentY][currentX].Equals('#'))
                {
                    treeCount++;
                }

                currentX += offsetX;
                currentY += offsetY;

                // Exit condition
                if (currentY >= _map.Count())
                {
                    isTraversalComplete = true;
                }

                // Handle the 'repeated pattern to the right'
                if (currentX >= _map[0].Length)
                {
                    currentX -= _map[0].Length;
                }
            }

            Console.WriteLine($"Total tree count: {treeCount}");
            return treeCount;
        }

        private static void Part2()
        {
            var total = Part1(1, 1) * Part1(3, 1) * Part1(5, 1) * Part1(7, 1) * Part1(1, 2);
            Console.WriteLine(total);
        }
    }
}
