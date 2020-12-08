using System;

namespace Day8
{
    public class Instruction : ICloneable
    {
        public readonly Guid Id;
        public readonly InstructionType InstructionType;
        public readonly int? Argument;

        public Instruction(string instructionType, int? argument)
        {
            switch (instructionType)
            {
                case "nop":
                    InstructionType = InstructionType.NoOperation;
                    break;
                case "acc":
                    InstructionType = InstructionType.Accumulator;
                    break;
                case "jmp":
                    InstructionType = InstructionType.Jump;
                    break;
                default:
                    throw new InvalidOperationException($"InstructionType {instructionType} is not supported");
            }

            Argument = argument;
            Id = Guid.NewGuid();
        }

        public Instruction(InstructionType instructionType, int? argument)
        {
            InstructionType = instructionType;
            Argument = argument;
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Required for clone functionality
        /// </summary>
        private Instruction(Guid id, InstructionType instructionType, int? argument)
        {
            Id = id;
            InstructionType = instructionType;
            Argument = argument;
        }

        public object Clone()
        {
            var clone = new Instruction(Id, InstructionType, Argument);
            return clone;
        }
    }
}