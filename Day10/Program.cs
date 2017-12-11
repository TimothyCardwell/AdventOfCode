using System;
using System.Collections.Generic;
using System.Linq;

namespace Day10
{
    class Program
    {
        private const string Input = "230, 1, 2, 221, 97, 252, 168, 169, 57, 99, 0, 254, 181, 255, 235, 167";
        static void Main(string[] args)
        {
            int[] sparseHash = Enumerable.Range(0, 256).ToArray();

            #region Part1
            // Part 1
            /*
            int[] lengths = new int[] { 230, 1, 2, 221, 97, 252, 168, 169, 57, 99, 0, 254, 181, 255, 235, 167 };
            int[] input = Input.Replace(" ", "").Split(",").Select(x => Convert.ToInt32(x)).ToArray();
            Knot(sparseHash, input, 0, 0);
            Console.WriteLine(input[0] * input[1]);
            Console.ReadLine();
            */
            #endregion

            #region Part2
            // Part 2
            string inputString = Input.Replace(" ", ""); // Remove white space

            // Convert input to ASCII
            int[] inputAscii = new int[inputString.Length + 5];
            for(int i = 0; i < inputString.Length; i++)
            {
                inputAscii[i] = inputString[i];
            }
            inputAscii[inputString.Length] = 17;
            inputAscii[inputString.Length + 1] = 31;
            inputAscii[inputString.Length + 2] = 73;
            inputAscii[inputString.Length + 3] = 47;
            inputAscii[inputString.Length + 4] = 23;

            // Knot the input 64 times
            int skipCount = 0;
            int index = 0;
            for (int i = 0; i < 64; i++)
            {
                Tuple<int, int> response = Knot(sparseHash, inputAscii, index, skipCount);
                index = response.Item1;
                skipCount = response.Item2;
            }

            // Convert to dense hash
            int[] denseHash = new int[16];
            for(int i = 0; i < 16; i++)
            {
                denseHash[i] = 
                    sparseHash[(i * 16) + 0] ^ 
                    sparseHash[(i * 16) + 1] ^
                    sparseHash[(i * 16) + 2] ^
                    sparseHash[(i * 16) + 3] ^
                    sparseHash[(i * 16) + 4] ^
                    sparseHash[(i * 16) + 5] ^
                    sparseHash[(i * 16) + 6] ^
                    sparseHash[(i * 16) + 7] ^
                    sparseHash[(i * 16) + 8] ^
                    sparseHash[(i * 16) + 9] ^
                    sparseHash[(i * 16) + 10] ^
                    sparseHash[(i * 16) + 11] ^
                    sparseHash[(i * 16) + 12] ^
                    sparseHash[(i * 16) + 13] ^
                    sparseHash[(i * 16) + 14] ^
                    sparseHash[(i * 16) + 15];
            }

            // Translate to hex values
            string[] denseHashHex = denseHash.Select(x => {
                string hexValue = x.ToString("X");
                return hexValue.Length == 1 ? "0" + hexValue : hexValue;
                }).ToArray();
            #endregion

            Console.WriteLine(String.Join(String.Empty, denseHashHex));
            Console.ReadLine();
    }

        private static Tuple<int, int> Knot(int[] source, int[] lengths, int index, int skipCount)
        {
            for (int i = 0; i < lengths.Length; i++)
            {
                int currentGroupSize = lengths[i];

                for (int j = 0; j < currentGroupSize / 2; j++)
                {
                    // Determine left index
                    int leftSwapIndex = index + j;
                    if (leftSwapIndex >= source.Length)
                    {
                        leftSwapIndex = leftSwapIndex - source.Length;
                    }

                    // Determine right index
                    int rightSwapIndex = index + currentGroupSize - j - 1;
                    if (rightSwapIndex >= source.Length)
                    {
                        rightSwapIndex = rightSwapIndex - source.Length;
                    }

                    // Swap values
                    int temp = source[leftSwapIndex];
                    source[leftSwapIndex] = source[rightSwapIndex];
                    source[rightSwapIndex] = temp;
                }

                index = index + currentGroupSize + skipCount;
                if (index >= source.Length)
                {
                    index = index % source.Length;
                }
                skipCount++;
            }

            return new Tuple<int, int>(index, skipCount);
        }
    }
}
