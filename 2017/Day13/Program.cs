using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Day13
{
    class Program
    {
        static void Main(string[] args)
        {
            // Parse input into dictionary
            Dictionary<int, int> depthsAndRanges;
            using (StreamReader streamReader = new StreamReader("input.txt"))
            {
                depthsAndRanges = streamReader.ReadToEnd().Split("\n").Select(x => x.Split(": "))
                    .ToDictionary(x => Convert.ToInt32(x[0]), x => Convert.ToInt32(x[1]));
            }

            // Part 1
            //List<Tuple<int, int>> timesCaught = FindNumberTimesCaught(depthsAndRanges, 0);

            // Part 2
            int delay = -1;
            List<Tuple<int, int>> timesCaught = null;

            // Increment the delay by one until the number of times caught is 0
            // TODO Is there a mathematical way to determine this?
            while (timesCaught == null || timesCaught.Count > 0)
            {
                delay++;
                timesCaught = FindNumberTimesCaught(depthsAndRanges, delay);
            }

            // Write to output
            foreach (Tuple<int, int> caught in timesCaught)
            {
                Console.WriteLine($"Picosecond: {caught.Item1}, Depth: {caught.Item2}");
            }
            Console.WriteLine($"Delay: {delay}");
            Console.WriteLine(timesCaught.Select(x => x.Item1 * x.Item2).Sum());
            Console.ReadLine();
        }

        private static List<Tuple<int, int>> FindNumberTimesCaught(Dictionary<int, int> depthsAndRanges, int delay)
        {
            List<Tuple<int, int>> timesCaught = new List<Tuple<int, int>>();
            int numberOfDepths = depthsAndRanges.Last().Key;

            // Iterate through each layer
            for (int i = 0; i <= numberOfDepths; i++)
            {
                // If a layer has no depth, it can be skipped
                if (depthsAndRanges.ContainsKey(i))
                {
                    int length = depthsAndRanges[i];

                    // The following formula indicates if a security scanner is at the 0th index
                    // i = the point in time (picosecond)
                    // length = the depth of the layer
                    if ((delay + i) % (2 * length - 2) == 0)
                    {
                        timesCaught.Add(new Tuple<int, int>(i, length));
                    }
                }
            }

            return timesCaught;
        }
    }
}
