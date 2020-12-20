using System;
using System.Collections.Generic;
using System.Linq;

namespace Day19
{
    public class DependentRule : MessageRule
    {
        private readonly List<IMessageRule> _dependentRules;

        public DependentRule(int id, List<IMessageRule> dependentRules) : base(id)
        {
            _dependentRules = dependentRules;
        }

        public override List<string> GetPossibleMatches()
        {
            Console.WriteLine(Id);
            var result = new List<string>();
            foreach (var rule in _dependentRules)
            {
                result = ConcatResult(result, rule);
            }

            return result;
        }
    }
}