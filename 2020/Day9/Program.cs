using System;
using System.IO;
using System.Linq;

namespace Day9
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataStrings = File.ReadAllLines("./input.txt");
            var data = dataStrings.Select(x => Convert.ToInt64(x)).ToArray();
            var exmaHack = new ExmaHack(data);

            Part1(exmaHack);
            Part2(exmaHack);

            Console.ReadLine();
        }

        public static void Part1(ExmaHack exmaHack)
        {
            var invalidItem = exmaHack.GetInvalidDataItem();
            Console.WriteLine($"Invalid Item: {invalidItem.Value}");
        }

        public static void Part2(ExmaHack exmaHack)
        {
            var encryptionWeakness = exmaHack.GetEncryptionWeakness();
            Console.WriteLine($"Encryption Weakness: {encryptionWeakness}");
        }
    }
}
