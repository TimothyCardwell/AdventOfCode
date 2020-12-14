using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day14
{
    class Program
    {
        static void Main(string[] args)
        {
            var addressSpace = new AddressSpace();
            Mask mask = null;

            var input = File.ReadAllLines("./input.txt");
            foreach (var line in input)
            {
                var value = line.Split(" ").Last();
                if (line.StartsWith("mask"))
                {
                    mask = new Mask(value);
                }
                else
                {
                    var index = Convert.ToInt32(Regex.Match(line, "\\[(\\d+)\\]").Groups[1].Value);
                    var intValue = new Base36Integer(Convert.ToInt64(value));

                    // Part 1
                    //addressSpace.CommitValueToMemory(index, mask.MaskValue(intValue));

                    // Part 2
                    var indexes = mask.MaskValueFloating(new Base36Integer(index));
                    addressSpace.CommitValueToMemory(indexes, intValue);
                }
            }

            Part1And2(addressSpace);

            Console.ReadLine();
        }

        public static void Part1And2(AddressSpace addressSpace)
        {
            Console.WriteLine(addressSpace.GetSumOfAddressSpace());
        }
    }
}
