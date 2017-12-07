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
            int[] currentConfiguration = Input;
            List<int[]> configurations = new List<int[]>() { currentConfiguration };

            // Part 1
            bool matchingConfiguration = false;
            while(!matchingConfiguration)
            {
                currentConfiguration = RedistributeValues(currentConfiguration);

                // Check for matching configurations
                if (DoesListContainItem(configurations, currentConfiguration))
                {
                    matchingConfiguration = true;
                }
                else
                {
                    configurations.Add(currentConfiguration);
                }
            }

            // Part 2
            int[] configurationToMatch = DeepCopyArray(currentConfiguration);
            bool configurationMatched = false;
            int redistributionCount = 0;
            while(!configurationMatched)
            {
                currentConfiguration = RedistributeValues(currentConfiguration);
                redistributionCount++;
                if (CompareArrays(currentConfiguration, configurationToMatch))
                {
                    configurationMatched = true;
                }
            }

            Console.WriteLine(redistributionCount);
            Console.ReadLine();
        }

        /// <summary>
        /// Takes an array of integers, find the max value and redistributes it's weight
        /// </summary>
        /// <param name="startingConfiguration"></param>
        /// <returns></returns>
        private static int[] RedistributeValues(int[] startingConfiguration)
        {
            // Deep copy array
            int[] finalConfiguration = DeepCopyArray(startingConfiguration);

            // Find max value and it's index
            int maxValue = int.MinValue;
            int index = 0;
            for (int i = 0; i < finalConfiguration.Length; i++)
            {
                if (finalConfiguration[i] > maxValue)
                {
                    maxValue = finalConfiguration[i];
                    index = i;
                }
            }

            // Redistribute value
            finalConfiguration[index] = 0;
            for (int i = 0; i < maxValue; i++)
            {
                if (index == finalConfiguration.Length - 1)
                {
                    index = 0;
                }
                else
                {
                    index += 1;
                }

                finalConfiguration[index] = finalConfiguration[index] + 1;
            }

            return finalConfiguration;
        }

        /// <summary>
        /// Determines if a list of integer array contains another integer array
        /// </summary>
        /// <param name="sourceList"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        private static bool DoesListContainItem(List<int[]> sourceList, int[] item)
        {
            foreach(int[] source in sourceList)
            {
                if(CompareArrays(source, item))
                {
                    return true;
                }
            };

            return false;
        }

        /// <summary>
        /// Compares two arrays for equality
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private static bool CompareArrays(int[] source, int[] target)
        {
            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] != target[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Returns a deep clone of an array
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private static int[] DeepCopyArray(int[] source)
        {
            int[] target = new int[source.Length];
            Array.Copy(source, target, source.Length);
            return target;
        }
    }
}
