using System;
using System.IO;
using System.Linq;

namespace Day8
{
    class Program
    {
        private static Bootstrap _bootstrap;
        static void Main(string[] args)
        {
            var instructionsString = File.ReadAllLines("./input.txt");
            var instructionSet = instructionsString.Select(x =>
            {
                var splitInstruction = x.Split(" ");
                var instructionType = splitInstruction[0];
                var argumentString = splitInstruction[1];
                var argumentSign = argumentString.Substring(0, 1);
                var argument = Convert.ToInt32(argumentString.Substring(1));
                if (argumentSign == "-")
                {
                    argument *= -1;
                }

                return new Instruction(instructionType, argument);
            }).ToArray();

            _bootstrap = new Bootstrap(instructionSet);

            Part1();
            Part2();

            Console.ReadLine();
        }

        private static void Part1()
        {
            _bootstrap.BootstrapSystem();
        }

        private static void Part2()
        {
            _bootstrap.FixCorruptedInstructionSet();
        }
    }
}
