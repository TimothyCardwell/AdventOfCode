using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day12
{
    class Program
    {
        private static Dictionary<int, List<int>> Input;

        /// <summary>
        /// Uses a hash set to maintain track of which values are part of a group
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Input = new Dictionary<int, List<int>>();
            using(StreamReader sr = new StreamReader("input.txt"))
            {
                string line;
                while((line = sr.ReadLine()) != null)
                {
                    string[] keyValues = line.Split(" <-> ");
                    int key = Convert.ToInt32(keyValues[0]);
                    List<int> values = keyValues[1].Split(", ").Select(x => Convert.ToInt32(x)).ToList();
                    Input.Add(key, values);
                }
            }

            int groupCount = 0;
            List<int> keys = Input.Keys.ToList();
            while(Input.Keys.Count() > 0)
            {
                HashSet<int> connectedValues = new HashSet<int>();

                // Part 1
                //PopulateHashSet(connectedValues, 0);

                int startingKey = Input.First().Key;
                PopulateHashSet(connectedValues, startingKey);
                foreach(int key in keys)
                {
                    if(connectedValues.Contains(key))
                    {
                        Input.Remove(key);
                    }
                }

                Console.WriteLine($"Group with starting key of {startingKey} has {connectedValues.Count()} members");

                groupCount++;
            }

            

            Console.WriteLine($"Total group count: {groupCount}");
            Console.ReadLine();
        }

        private static void PopulateHashSet(HashSet<int> connectedValues, int currentValue)
        {
            if(connectedValues.Add(currentValue))
            {
                List<int> values = Input[currentValue];
                foreach(int value in values)
                {
                    PopulateHashSet(connectedValues, value);
                }
            }
        }
    }
}
