using System;
using System.Collections.Generic;
using System.Linq;

namespace Day19
{
    public class ConjunctionRule : MessageRule
    {
        private readonly List<IMessageRule> _leftRules;
        private readonly List<IMessageRule> _rightRules;
        public ConjunctionRule(int id, List<IMessageRule> leftRules, List<IMessageRule> rightRules) : base(id)
        {
            _leftRules = leftRules;
            _rightRules = rightRules;
        }

        public override List<string> GetPossibleMatches()
        {
            Console.WriteLine(Id);
            var leftResult = new List<string>();
            foreach (var rule in _leftRules)
            {
                leftResult = ConcatResult(leftResult, rule);
            }

            var rightResult = new List<string>();
            foreach (var rule in _rightRules)
            {
                rightResult = ConcatResult(rightResult, rule);
            }

            return leftResult.Concat(rightResult).ToList();
        }
    }
}