using System;
using System.Collections.Generic;
using System.Linq;

namespace Day6
{
    class Program
    {
        private static int[] Input = new int[] { 10, 3, 15, 10, 5, 15, 5, 15, 9, 2, 5, 8, 5, 2, 3, 6 };

        static void Main(string[] args)
        {
            List<int[]> configurations = new List<int[]>();

            int reconfigurationCount = 0;
            while(true)
            {
                if(DoesListContainItem(configurations, Input))
                {
                    Console.WriteLine(reconfigurationCount);
                    Console.ReadLine();
                }
                else
                {
                    int[] deepCopy = new int[Input.Length];
                    Array.Copy(Input, deepCopy, Input.Length);
                    configurations.Add(deepCopy);
                }

                int maxValue = int.MinValue;
                int index = 0;
                for(int i = 0; i < Input.Count(); i++)
                {
                    if(Input[i] > maxValue)
                    {
                        maxValue = Input[i];
                        index = i;
                    }
                }

                Input[index] = 0;
                for(int i = 0; i < maxValue; i++)
                {
                    if(index == Input.Length - 1)
                    {
                        index = 0;
                    }
                    else
                    {
                        index += 1;
                    }

                    Input[index] = Input[index] + 1;
                }

                reconfigurationCount += 1;
            }
        }

        private static bool DoesListContainItem(List<int[]> sourceList, int[] item)
        {
            foreach(int[] source in sourceList)
            {
                bool equal = true;
                for(int i = 0; i < source.Length; i++)
                {
                    if(source[i] != item[i])
                    {
                        equal = false;
                        break;
                    }
                }

                if(equal)
                {
                    return true;
                }
            };

            return false;
        }
    }
}
