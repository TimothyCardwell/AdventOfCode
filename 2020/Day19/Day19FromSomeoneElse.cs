using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day19
{
    public class Day19FromSomeoneElse
    {
        public string Solve()
        {
            var lines = File.ReadAllLines("input.txt");

            var part1result = SolvePart1(lines.ToList());
            var part2result = SolvePart2(lines.ToList());

            return $"Part 1: {part1result}, Part 2: {part2result}";
        }

        private static string SolvePart1(List<string> input)
        {
            (var ruleValidator, var messages) = ParseInput(input);

            var numberOfValidMessages = ruleValidator.NumberOfValidMessages(messages, 0);

            return numberOfValidMessages.ToString();
        }

        private static string SolvePart2(List<string> input)
        {
            (var ruleValidator, var messages) = ParseInput(input);

            ruleValidator.Modify(8, "42 | 42 8");
            ruleValidator.Modify(11, "42 31 | 42 11 31");

            var numberOfValidMessages = ruleValidator.NumberOfValidMessages(messages, 0);

            return numberOfValidMessages.ToString();
        }

        private static (RuleValidator, List<string>) ParseInput(List<string> input)
        {
            List<string> rawRules = new(), messages = new();

            var rulesBeingProcess = true;
            foreach (var line in input)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    rulesBeingProcess = false;
                    continue;
                }
                (rulesBeingProcess ? rawRules : messages).Add(line);
            }
            return (new RuleValidator(rawRules), messages);
        }

        private class RuleValidator
        {
            private Dictionary<int, Rule> Rules { get; set; } = new();

            public RuleValidator(List<string> rawRules)
            {
                foreach (var rawRule in rawRules)
                {
                    var split = rawRule.Split(":");
                    var index = int.Parse(split[0]);
                    var rule = new Rule(split[1]);
                    Rules.Add(index, rule);
                }
            }

            public int NumberOfValidMessages(List<string> messages, int rule)
            {
                return messages.Count(x => ValidMessage(x, rule));
            }

            public void Modify(int ruleIndex, string newRule)
            {
                Rules[ruleIndex] = new Rule(newRule);
            }

            private bool ValidMessage(string message, int ruleIndex)
            {
                return MatchesRule(message, (ruleIndex, null, null), 0).Any(x => x == message.Length);
            }

            private List<int> MatchesRule(string message, (int r1, int? r2, int? r3) ruleIndex, int usedUpCharacters)
            {
                var results = MatchesRule(message, ruleIndex.r1, usedUpCharacters);
                if (!results.Any()) { return new List<int>(); }

                if (ruleIndex.r2 != null)
                {
                    List<int> ruleTwoResults = new();
                    foreach (var ruleOneUsedCharacters in results)
                    {
                        ruleTwoResults = ruleTwoResults.Concat(MatchesRule(message, ruleIndex.r2.Value, ruleOneUsedCharacters)).ToList();
                    }
                    if (!ruleTwoResults.Any()) { return new List<int>(); }
                    results = ruleTwoResults;
                }

                if (ruleIndex.r3 != null)
                {
                    List<int> ruleThreeResults = new();
                    foreach (var ruleTwoUsedCharacters in results)
                    {
                        ruleThreeResults = ruleThreeResults.Concat(MatchesRule(message, ruleIndex.r3.Value, ruleTwoUsedCharacters)).ToList();
                    }
                    results = ruleThreeResults;
                }

                return results;
            }

            private List<int> MatchesRule(string message, int ruleIndex, int usedUpCharacters)
            {
                var rule = Rules[ruleIndex];

                if (rule.IsLetter)
                {
                    var success = message.Skip(usedUpCharacters).FirstOrDefault() == rule.Letter;
                    if (!success) return new List<int>();
                    return new List<int> { usedUpCharacters + 1 };
                }

                List<int> results = new();
                foreach (var orRule in rule.OrRules)
                {
                    results = results.Concat(MatchesRule(message, (orRule.Item1, orRule.Item2, orRule.Item3), usedUpCharacters)).ToList();
                }
                return results;
            }
        }

        private class Rule
        {
            public bool IsLetter { get; set; } = false;
            public char Letter { get; set; }
            public List<(int, int?, int?)> OrRules { get; set; } = new();

            public Rule(string rawRule)
            {
                rawRule = rawRule.Trim();
                if (rawRule.Contains("\""))
                {
                    IsLetter = true;
                    Letter = rawRule.Split('"')[1].Take(1).First();
                }
                else
                {
                    var rules = rawRule.Split("|");

                    foreach (var rule in rules)
                    {
                        var ruleRefs = rule.Trim().Split(" ");
                        int firstRule = int.Parse(ruleRefs[0]);
                        int? secondRule = ruleRefs.Length > 1 ? int.Parse(ruleRefs[1]) : null;
                        int? thirdRule = ruleRefs.Length > 2 ? int.Parse(ruleRefs[2]) : null;
                        OrRules.Add((firstRule, secondRule, thirdRule));
                    }
                }
            }
        }
    }
}