using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day15
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("./input.txt");
            var startingNumbers = input[0].Split(",").Select(x => Convert.ToInt32(x)).ToArray();

            Part1And2(startingNumbers, 2020);
            Part1And2(startingNumbers, 30000000);

            Console.ReadLine();
        }

        public static void Part1And2(int[] spokenNumbers, int target)
        {
            // Contains the spoken number for a given turn
            var numbersSpoken = new Dictionary<int, int>();

            // Contains the index a specific number was used last
            var indexMappings = new Dictionary<int, int>();

            var nextNumber = -1;
            for (var i = 0; i < target; i++)
            {
                var currentNumber = i < spokenNumbers.Length ? spokenNumbers[i] : nextNumber;
                numbersSpoken[i] = currentNumber;

                // Number has been spoken before!
                if (indexMappings.ContainsKey(currentNumber))
                {
                    nextNumber = i - indexMappings[currentNumber];
                    indexMappings[currentNumber] = i;
                }

                // Number has not been spoken before
                else
                {
                    nextNumber = 0;
                    indexMappings[currentNumber] = i;
                }
            }

            Console.WriteLine($"{target}th iteration: {numbersSpoken[target - 1]}");
        }
    }
}
