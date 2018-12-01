using System;
using System.Collections.Generic;
using System.IO;

namespace Day11
{
    /// <summary>
    /// Didn't get this on my own, relied on Reddit:https://www.reddit.com/r/adventofcode/comments/7izym2/2017_day_11_solutions/
    /// I tried to build an actual hexagonal grid, with the value of each node being the distance from the center. Would have worked 
    /// however I had trouble figuring out how to add an outer ring
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            int x = 0;
            int y = 0;
            int dist = 0;
            int furthest = 0;
            using(StreamReader sr = new StreamReader("input.txt"))
            {
                string input = sr.ReadToEnd();
                string[] directionsArray = input.Split(",");
                for(int i = 0; i < directionsArray.Length; i++)
                {
                    string direction = directionsArray[i];
                    Hexagon hexagon = new Hexagon(direction);
                    x += hexagon.X;
                    y += hexagon.Y;

                    dist = hexDistance(x, y);
                    if (dist > furthest)
                    {
                        furthest = dist;
                    }
                }
            }

            Console.WriteLine($"x: {x}, y: {y}");
            Console.WriteLine(dist);
            Console.WriteLine(furthest);
            Console.ReadLine();
        }

        private static int hexDistance(int x, int y)
        {
            return ((Math.Abs(x) + Math.Abs(y) + Math.Abs(x + y)) / 2);
        }
    }
}
