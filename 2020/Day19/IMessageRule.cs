using System.Collections.Generic;

namespace Day19
{
    public interface IMessageRule
    {
        List<string> GetPossibleMatches();
    }
}