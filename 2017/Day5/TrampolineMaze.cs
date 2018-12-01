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
            while(currentIndex >= 0 && currentIndex < maze.Count)
            {
                // Part 1
                //maze[currentIndex] = maze[currentIndex] + 1;
                //currentIndex = currentIndex + maze[currentIndex] - 1;

                // Part 2
                if(maze[currentIndex] >= 3)
                {
                    maze[currentIndex] = maze[currentIndex] - 1;
                    currentIndex = currentIndex + maze[currentIndex] + 1;
                }
                else
                {
                    maze[currentIndex] = maze[currentIndex] + 1;
                    currentIndex = currentIndex + maze[currentIndex] - 1;
                }

                steps++;
            }

            Console.WriteLine(steps);
            Console.ReadLine();
        }
    }
}
