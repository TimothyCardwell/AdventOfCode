using System;
using System.Collections.Generic;
using System.Linq;

namespace Day10
{
    class Program
    {
        private static int[] Knots = Enumerable.Range(0, 256).ToArray();
        private static int[] Groupings = new int[] { 230, 1, 2, 221, 97, 252, 168, 169, 57, 99, 0, 254, 181, 255, 235, 167 };
        static void Main(string[] args)
        {
            int currentIndex = 0;
            int skipCount = 0;
            for(int i = 0; i < Groupings.Length; i++)
            {
                int currentGroupSize = Groupings[i];

                for(int j = 0; j < currentGroupSize / 2; j++)
                {
                    // Determine left index
                    int leftSwapIndex = currentIndex + j;
                    if(leftSwapIndex >= Knots.Length)
                    {
                        leftSwapIndex = leftSwapIndex - Knots.Length;
                    }

                    // Determine right index
                    int rightSwapIndex = currentIndex + currentGroupSize - j - 1;
                    if (rightSwapIndex >= Knots.Length)
                    {
                        rightSwapIndex = rightSwapIndex - Knots.Length;
                    }

                    // Swap values
                    int temp = Knots[leftSwapIndex];
                    Knots[leftSwapIndex] = Knots[rightSwapIndex];
                    Knots[rightSwapIndex] = temp;
                }

                //Console.WriteLine(String.Join(", ", Knots));
                
                currentIndex = currentIndex + currentGroupSize + skipCount;
                if(currentIndex >= Knots.Length)
                {
                    currentIndex = currentIndex % Knots.Length;
                }
                skipCount++;
            }

            Console.WriteLine(Knots[0] * Knots[1]);
            Console.ReadLine();
        }
    }
}
