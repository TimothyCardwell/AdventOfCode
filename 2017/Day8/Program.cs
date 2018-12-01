using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day8
{
    class Program
    {
        private const string SampleInput = @"b inc 5 if a > 1
a inc 1 if b < 5
c dec -10 if a >= 1
c inc -20 if c == 10";

        static void Main(string[] args)
        {
            List<Instruction> instructions = new List<Instruction>();
            Dictionary<string, int> registries = new Dictionary<string, int>();

            // Parse input
            using(StreamReader streamReader = new StreamReader("input.txt"))
            //using(StringReader streamReader = new StringReader(SampleInput))
            {
                string line;
                while((line = streamReader.ReadLine()) != null)
                {
                    string[] parsedInput = line.Split(" ");
                    string registry = parsedInput[0];
                    string action = parsedInput[1];
                    int value = Convert.ToInt32(parsedInput[2]);
                    string conditonalRegistry = parsedInput[4];
                    string oprator = parsedInput[5];
                    int conditionalValue = Convert.ToInt32(parsedInput[6]);
                    
                    instructions.Add(new Instruction(registry, action, value, conditonalRegistry, oprator, conditionalValue));
                    if (!registries.ContainsKey(registry))
                    {
                        registries.Add(registry, 0);
                    }
                }
            }

            // Execute instructions
            int highestValue = Int32.MinValue;
            foreach(Instruction instruction in instructions)
            {
                // Evaluate statement
                bool isStatementTrue = false;
                switch(instruction.Operator)
                {
                    case Operator.Equal:
                        isStatementTrue = registries[instruction.ConditionalRegistry] == instruction.ConditionalValue;
                        break;
                    case Operator.NotEqual:
                        isStatementTrue = registries[instruction.ConditionalRegistry] != instruction.ConditionalValue;
                        break;
                    case Operator.LessThan:
                        isStatementTrue = registries[instruction.ConditionalRegistry] < instruction.ConditionalValue;
                        break;
                    case Operator.LessThanOrEqual:
                        isStatementTrue = registries[instruction.ConditionalRegistry] <= instruction.ConditionalValue;
                        break;
                    case Operator.GreaterThan:
                        isStatementTrue = registries[instruction.ConditionalRegistry] > instruction.ConditionalValue;
                        break;
                    case Operator.GreaterThanOrEqual:
                        isStatementTrue = registries[instruction.ConditionalRegistry] >= instruction.ConditionalValue;
                        break;
                    default:
                        throw new ArgumentException("Invalid operator!");
                }

                // Modify registry
                if (isStatementTrue)
                {
                    int value = instruction.Action == Action.Increase ?
                        registries[instruction.Registry] + instruction.Value :
                        registries[instruction.Registry] - instruction.Value;
                    registries[instruction.Registry] = value;

                    // Keep track of highest value
                    if(value > highestValue)
                    {
                        highestValue = value;
                    }
                }
            }

            // Part 1
            //Console.WriteLine(registries.Aggregate((l, r) => l.Value > r.Value ? l : r).Value);


            // Part 2
            Console.WriteLine(highestValue);
            Console.ReadLine();
        }
    }
}
