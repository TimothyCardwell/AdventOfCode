using System;
using System.Collections.Generic;

namespace Day19
{
    public class SimpleRule : MessageRule
    {
        private readonly string _characterToMatch;

        public SimpleRule(int id, string characterToMatch) : base(id)
        {
            _characterToMatch = characterToMatch;
        }

        public override List<string> GetPossibleMatches()
        {
            Console.WriteLine(Id);
            return new List<string> { _characterToMatch };
        }
    }
}