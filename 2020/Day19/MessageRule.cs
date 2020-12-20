using System;
using System.Collections.Generic;
using System.Linq;

namespace Day19
{
    public abstract class MessageRule : IMessageRule
    {
        public readonly int Id;

        public MessageRule(int id)
        {
            Id = id;
        }

        public abstract List<string> GetPossibleMatches();

        protected List<string> ConcatResult(List<string> result, IMessageRule rule)
        {
            if (result.Count() == 0)
            {
                return rule.GetPossibleMatches();
            }

            var newResult = new List<string>();
            foreach (var item in result)
            {
                foreach (var match in rule.GetPossibleMatches())
                {
                    newResult.Add(item + match);
                }
            }

            return newResult;
        }
    }
}