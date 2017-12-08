using System;
using System.Collections.Generic;
using System.Text;

namespace Day8
{
    public class Instruction
    {
        public string Registry { get; set; }
        public Action Action { get; set; }
        public int Value { get; set; }
        public string ConditionalRegistry { get; set; }
        public Operator Operator { get; set; }
        public int ConditionalValue { get; set; }

        public Instruction(string registry, string action, int value, string conditonalRegistry, string oprator, int conditionalValue)
        {
            Registry = registry;
            switch(action)
            {
                case "inc":
                    Action = Action.Increase;
                    break;
                case "dec":
                    Action = Action.Decrease;
                    break;
                default:
                    throw new ArgumentException("Invalid action");
            }
            Value = value;
            ConditionalRegistry = conditonalRegistry;
            switch(oprator)
            {
                case "==":
                    Operator = Operator.Equal;
                    break;
                case "<":
                    Operator = Operator.LessThan;
                    break;
                case "<=":
                    Operator = Operator.LessThanOrEqual;
                    break;
                case ">":
                    Operator = Operator.GreaterThan;
                    break;
                case ">=":
                    Operator = Operator.GreaterThanOrEqual;
                    break;
                case "!=":
                    Operator = Operator.NotEqual;
                    break;
            }
            ConditionalValue = conditionalValue;
        }
    }

    public enum Action
    {
        Increase,
        Decrease
    }

    public enum Operator
    {
        LessThan,
        GreaterThan,
        LessThanOrEqual,
        GreaterThanOrEqual,
        Equal,
        NotEqual
    }
}
