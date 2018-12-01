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
            KnotHash hash = new KnotHash(Input);

            Console.WriteLine(hash.Hash);
            Console.ReadLine();
        }
    }
}
