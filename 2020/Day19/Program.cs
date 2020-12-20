using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day19
{
    class Program
    {
        private static List<string> _messages = new List<string>();

        /// <summary>
        /// I couldn't get part 2 working, used someone else's work
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // var messageRule0 = ParseInput(0);

            //Part1(messageRule0);

            // var messageRule42 = ParseInput(42);
            // var messageRule31 = ParseInput(31);
            // Part2(messageRule42, messageRule31);

            var otherPersonsCode = new Day19FromSomeoneElse();
            Console.WriteLine(otherPersonsCode.Solve());

            Console.ReadLine();
        }

        static void Part1(IMessageRule messageRule)
        {
            var validMessages = messageRule.GetPossibleMatches();
            var result = validMessages.Intersect(_messages).Count();
            Console.WriteLine(result);
        }

        static void Part2(IMessageRule messageRule42, IMessageRule messageRule31)
        {
            var validMessages42 = messageRule42.GetPossibleMatches();
            var validMessages31 = messageRule31.GetPossibleMatches();

            var result = 0;
            foreach (var message in _messages)
            {
                var rule42Matches = validMessages42.Select(x => Regex.Matches(message, x).Count()).Sum();
                var rule31Matches = validMessages31.Select(x => Regex.Matches(message, x).Count()).Sum();
                if (rule31Matches >= 1 && rule42Matches > rule31Matches)
                {
                    result++;
                }
            }

            Console.WriteLine(result);
        }

        static void Part2(IMessageRule messageRule)
        {
            var validMessages = messageRule.GetPossibleMatches();
            var result = validMessages.Intersect(_messages).Count();
            Console.WriteLine(result);
        }

        static IMessageRule ParseInput(int rootRule)
        {
            var input = File.ReadAllLines("./input.txt");
            var rules = new Dictionary<int, string>();

            bool parsingMessages = false;
            for (var i = 0; i < input.Length; i++)
            {
                var currentLine = input[i];
                if (string.IsNullOrEmpty(currentLine))
                {
                    parsingMessages = true;
                    continue;
                }

                if (parsingMessages)
                {
                    _messages.Add(currentLine);
                }
                else
                {
                    var ruleSplit = currentLine.Split(":");
                    var id = Convert.ToInt32(ruleSplit[0]);
                    var rule = ruleSplit[1].Substring(1);
                    rules.Add(id, rule);
                }
            }

            var messageRule0 = BuildRuleTree(rootRule, rules, 0);
            return messageRule0;
        }

        static IMessageRule BuildRuleTree(int id, Dictionary<int, string> rules, int counter)
        {
            var rule = rules[id];

            // Simple Rule
            if (rule.Contains("\""))
            {
                return new SimpleRule(id, rule[1].ToString());
            }

            // Dependent Rule
            else if (!rule.Contains("|"))
            {
                var dependentRules = rule.Split(" ").Select(x =>
                {
                    var dependentRuleId = Convert.ToInt32(x);
                    if (counter > 1) return null;
                    return BuildRuleTree(dependentRuleId, rules, id == dependentRuleId ? counter + 1 : 0);
                }).Where(x => x != null).ToList();

                return new DependentRule(id, dependentRules);
            }

            // Conjunction Rule
            else
            {
                var ruleSplit = rule.Split(" | ");

                var leftRules = ruleSplit[0].Split(" ").Select(x =>
                {
                    var dependentRuleId = Convert.ToInt32(x);
                    if (counter > 1) return null;
                    return BuildRuleTree(dependentRuleId, rules, id == dependentRuleId ? counter + 1 : 0);
                }).Where(x => x != null).ToList();

                var rightRules = ruleSplit[1].Split(" ").Select(x =>
                {
                    var dependentRuleId = Convert.ToInt32(x);
                    if (counter > 1) return null;
                    return BuildRuleTree(dependentRuleId, rules, id == dependentRuleId ? counter + 1 : 0);
                }).Where(x => x != null).ToList();

                return new ConjunctionRule(id, leftRules, rightRules);
            }
        }
    }
}
