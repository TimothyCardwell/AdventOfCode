using System;
using System.Collections;

namespace Day15
{
    class Program
    {
        private const int GeneratorA = 699;
        private const int GeneratorAFactor = 16807;
        private const int GeneratorB = 124;
        private const int GeneratorBFactor = 48271;
        private const int Dividend = 2147483647;

        static void Main(string[] args)
        {
            long previousAValue = GeneratorA;
            long previousBValue = GeneratorB;

            #region Part1
            // Part 1
            /*
            int count = 0;
            for (int i = 0; i < 40000000; i++)
            {
                // Get next values
                long nextAValue = CalculateNextValue(previousAValue, GeneratorAFactor, Dividend);
                long nextBValue = CalculateNextValue(previousBValue, GeneratorBFactor, Dividend);

                // Convert to binary
                string nextABinary = ConvertToBinary(nextAValue);
                string nextBBinary = ConvertToBinary(nextBValue);

                // Check for equality
                if (AreLowestBitsEqual(nextABinary, nextBBinary))
                {
                    count++;
                }

                previousAValue = nextAValue;
                previousBValue = nextBValue;

                if (i % 1000000 == 0)
                {
                    Console.WriteLine(i);
                }
            }*/
            #endregion

            #region Part2
            int matchingCount = 0;
            int judgedCount = 0;
            bool generatorAValueReady = false;
            bool generatorBValueReady = false;
            long nextAValue = 0;
            long nextBValue = 0;
            while (judgedCount < 5000000)
            {
                // Continue finding values for generator A
                if (!generatorAValueReady)
                {
                    nextAValue = CalculateNextValue(previousAValue, GeneratorAFactor, Dividend);
                    previousAValue = nextAValue;
                    if (nextAValue % 4 == 0)
                    {
                        generatorAValueReady = true;
                    }
                }

                // Continue finding values for generator B
                if (!generatorBValueReady)
                {
                    nextBValue = CalculateNextValue(previousBValue, GeneratorBFactor, Dividend);
                    previousBValue = nextBValue;
                    if (nextBValue % 8 == 0)
                    {
                        generatorBValueReady = true;
                    }
                }

                // Both generators have found values, check for equality
                if (generatorAValueReady && generatorBValueReady)
                {
                    string nextABinary = ConvertToBinary(nextAValue);
                    string nextBBinary = ConvertToBinary(nextBValue);

                    judgedCount++;
                    if (AreLowestBitsEqual(nextABinary, nextBBinary))
                    {
                        matchingCount++;
                    }

                    generatorAValueReady = false;
                    generatorBValueReady = false;
                }

                if (judgedCount % 1000 == 0)
                {
                    Console.WriteLine(judgedCount);
                }
            }
            #endregion

            Console.WriteLine(matchingCount);
            Console.ReadLine();
        }

        /// <summary>
        /// Logic for calculating next value
        /// </summary>
        /// <param name="currentValue"></param>
        /// <param name="factor"></param>
        /// <param name="dividend"></param>
        /// <returns></returns>
        private static long CalculateNextValue(long currentValue, int factor, int dividend)
        {
            return (currentValue * factor) % dividend;
        }

        /// <summary>
        /// Logic for converting long to binary string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string ConvertToBinary(long value)
        {
            return Convert.ToString(value, 2).PadLeft(32, '0');
        }

        /// <summary>
        /// Logic for comparing the lowest 16 bits of two binary string
        /// </summary>
        /// <param name="stringOne"></param>
        /// <param name="stringTwo"></param>
        /// <returns></returns>
        private static bool AreLowestBitsEqual(string stringOne, string stringTwo)
        {
            return stringOne.Substring(stringOne.Length - 16) == stringTwo.Substring(stringOne.Length - 16);
        }
    }
}
