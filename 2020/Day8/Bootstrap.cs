using System;
using System.Collections.Generic;
using System.Linq;

namespace Day8
{
    public class Bootstrap
    {
        private int _value;
        private Instruction[] _instructionSet;
        private HashSet<Guid> _visitedInstructions;

        public Bootstrap(Instruction[] instructionSet)
        {
            _value = 0;
            _instructionSet = instructionSet;
            _visitedInstructions = new HashSet<Guid>();
        }

        public bool BootstrapSystem()
        {
            var currentInstructionIndex = 0;
            while (true)
            {
                if (currentInstructionIndex >= _instructionSet.Length)
                {
                    return true;
                }

                var currentInstruction = _instructionSet[currentInstructionIndex];
                if (_visitedInstructions.Contains(currentInstruction.Id))
                {
                    Console.WriteLine($"Infinite Loop detected, current value: {_value}");
                    return false;
                }

                switch (currentInstruction.InstructionType)
                {
                    case InstructionType.Accumulator:
                        _value += currentInstruction.Argument.Value;
                        currentInstructionIndex++;
                        break;

                    case InstructionType.Jump:
                        currentInstructionIndex += currentInstruction.Argument.Value;
                        break;

                    case InstructionType.NoOperation:
                        currentInstructionIndex++;
                        break;
                }

                _visitedInstructions.Add(currentInstruction.Id);
            }
        }

        /// <summary>
        /// I just brute forced it :/
        /// </summary>
        public void FixCorruptedInstructionSet()
        {
            // Need to clone because C# creates a shallow copy here
            var originalInstructionSet = _instructionSet.Select(x => (Instruction)x.Clone()).ToArray();

            var currentInstructionIndex = 0;
            var isBootstrapComplete = BootstrapSystem();
            while (!isBootstrapComplete)
            {
                // All possible avenues explored, exit
                if (currentInstructionIndex >= _instructionSet.Length)
                {
                    throw new InvalidOperationException("No resolution exists to the bootstrap issue");
                }

                // Alway reset the instruction set to the original
                ResetBootstrapAttempt(originalInstructionSet);

                var currentInstruction = _instructionSet[currentInstructionIndex];
                if (currentInstruction.InstructionType != InstructionType.Accumulator)
                {
                    if (currentInstruction.InstructionType == InstructionType.Jump)
                    {
                        _instructionSet[currentInstructionIndex] = new Instruction(InstructionType.NoOperation, currentInstruction.Argument);
                    }
                    else if (currentInstruction.InstructionType == InstructionType.NoOperation)
                    {
                        _instructionSet[currentInstructionIndex] = new Instruction(InstructionType.Jump, currentInstruction.Argument);
                    }

                    // Check if we completed
                    isBootstrapComplete = BootstrapSystem();
                }

                currentInstructionIndex++;
            }

            Console.WriteLine($"Accumulator value: {_value}");
        }

        private void ResetBootstrapAttempt(Instruction[] instructionSet)
        {
            _instructionSet = instructionSet.Select(x => (Instruction)x.Clone()).ToArray();
            _value = 0;
            _visitedInstructions = new HashSet<Guid>();
        }
    }
}