using System;
using System.Linq;
using Day10;

namespace Day14
{
    class Program
    {
        private const string Input = "jzgqcdpd";

        static void Main(string[] args)
        {
            int usedCount = 0;

            bool[,] matrix = new bool[128,128];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                string hashInput = $"{Input}-{i}";
                string hash = new KnotHash(hashInput).Hash;
                string[] hashBinary = hash.ToCharArray().Select(x => Convert.ToString(Convert.ToInt32(x.ToString(), 16), 2).PadLeft(4, '0')).ToArray();
                char[] hexHash = String.Join("", hashBinary).ToCharArray();

                for(int j = 0; j < hexHash.Length; j++)
                {
                    if (hexHash[j] == '1')
                    {
                        usedCount++;
                    }

                    matrix[i, j] = hexHash[j] == '1';
                }
            }

            int groups = 0;
            int?[,] seenMatrix = new int?[128,128];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] && !seenMatrix[i, j].HasValue)
                    {
                        DepthFirstSearch(matrix, seenMatrix, i, j, groups + 1);
                        groups++;
                    }
                }
            }

            Console.WriteLine(groups);
            Console.ReadLine();
        }

        private static void DepthFirstSearch(bool[,] sourceMatrix, int?[,] seenMatrix, int i, int j, int groupValue)
        {
            if (sourceMatrix[i, j] && !seenMatrix[i,j].HasValue)
            {
                seenMatrix[i, j] = groupValue;
            }
            else
            {
                return;
            }

            if (i > 0)
            {
                DepthFirstSearch(sourceMatrix, seenMatrix, i - 1, j, groupValue);
            }

            if (j > 0)
            {
                DepthFirstSearch(sourceMatrix, seenMatrix, i, j - 1, groupValue);
            }

            if (i < 127)
            {
                DepthFirstSearch(sourceMatrix, seenMatrix, i + 1, j, groupValue);
            }

            if (j < 127)
            {
                DepthFirstSearch(sourceMatrix, seenMatrix, i, j + 1, groupValue);
            }
        }
    }
}
