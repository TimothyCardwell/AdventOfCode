using System;
using System.Collections.Generic;
using System.IO;

namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> maze = new List<int>();
            using(StreamReader sr = new StreamReader("input.txt"))
            {
                string line;
                while((line = sr.ReadLine()) != null)
                {
                    maze.Add(Convert.ToInt32(line));
                }
            }

            int steps = 0;
            int currentIndex = 0;
            int previousIndex = 0;
            while(true)
            {
                previousIndex = currentIndex;
                currentIndex = currentIndex + maze[currentIndex];
                maze[previousIndex] = maze[previousIndex]++;
                steps++;
                if(currentIndex < 0 || currentIndex >= maze.Count)
                {
                    Console.WriteLine(steps);
                    Console.ReadLine();
                }
            }
        }
    }
}
