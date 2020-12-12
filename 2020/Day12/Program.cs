using System;
using System.IO;

namespace Day12
{
    class Program
    {
        static void Main(string[] args)
        {
            var navigationInstructions = File.ReadAllLines("./input.txt");


            Part1(navigationInstructions);
            Part2(navigationInstructions);

            Console.ReadLine();
        }

        public static void Part1(string[] navigationInstructions)
        {
            var navigation = new Navigation(navigationInstructions);
            Console.WriteLine(navigation.GetManhattanDistance());
        }

        public static void Part2(string[] navigationInstructions)
        {
            var navigation = new Navigation(navigationInstructions, true);
            Console.WriteLine(navigation.GetManhattanDistance());
        }
    }
}
