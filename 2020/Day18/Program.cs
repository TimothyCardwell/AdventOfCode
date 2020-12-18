using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day18
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("./input.txt");

            var evaluators = input.Select(x => new BadMathEvaluator(x));

            Part1And2(evaluators);

            Console.ReadLine();
        }

        public static void Part1And2(IEnumerable<BadMathEvaluator> evaluators)
        {
            long sum = 0;
            foreach (var evaluator in evaluators)
            {
                sum += evaluator.Evaluate();
            }

            Console.WriteLine(sum);
        }
    }
}
