using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day10
{
    class Program
    {
        static void Main(string[] args)
        {
            var adapterJoltsString = File.ReadAllLines("./input.txt");
            var adapterJolts = adapterJoltsString.Select(x => Convert.ToInt64(x)).ToList();

            Part1(adapterJolts);
            Part2(adapterJolts);

            Console.ReadLine();
        }

        static void Part1(List<long> adapterJolts)
        {
            adapterJolts.Sort();

            var differenceMap = new Dictionary<long, long>();
            var incomingJoltage = (long)0;
            foreach (var adapterJolt in adapterJolts)
            {
                var joltageDifference = adapterJolt - incomingJoltage;
                if (joltageDifference > 3)
                {
                    Console.WriteLine($"Passed in adapter jolt list is missing an adapter between {incomingJoltage} and {adapterJolt}");
                    return;
                }

                if (differenceMap.ContainsKey(joltageDifference))
                {
                    differenceMap[joltageDifference]++;
                }
                else
                {
                    differenceMap[joltageDifference] = 1;
                }

                incomingJoltage = adapterJolt;
            }

            // Don't forget your device's built in adapter rating!
            differenceMap[3]++;

            Console.WriteLine(differenceMap[1] * differenceMap[3]);
        }

        static void Part2(List<long> adapterJolts)
        {
            // Add wall adapater
            adapterJolts.Add(0);

            var sortedAdapterJolts = adapterJolts.OrderBy(x => x);

            var combinationsMap = new Dictionary<long, long>
            {
                { sortedAdapterJolts.Last(), 1 }
            };

            for (var i = sortedAdapterJolts.Last() - 1; i >= 0; i--)
            {
                if (adapterJolts.IndexOf(i) > -1)
                {
                    var add1 = combinationsMap.ContainsKey(i + 1) ? combinationsMap[i + 1] : 0;
                    var add2 = combinationsMap.ContainsKey(i + 2) ? combinationsMap[i + 2] : 0;
                    var add3 = combinationsMap.ContainsKey(i + 3) ? combinationsMap[i + 3] : 0;
                    combinationsMap[i] = add1 + add2 + add3;
                }
            }

            Console.WriteLine(combinationsMap[0]);
        }
    }
}
