using System;
using System.Collections.Generic;
using System.Linq;

namespace Day18
{
    public class BadMathEvaluator
    {
        private readonly string _ungroupedProblem;

        public BadMathEvaluator(string problem)
        {
            _ungroupedProblem = RecursiveMethod(problem);
        }

        public string RecursiveMethod(string group)
        {
            var groupsExist = group.Contains("(");
            while (groupsExist)
            {
                var tempValue = string.Empty;
                var startIndex = 0;
                var endIndex = 0;
                var parenthesisCount = 0;
                for (var i = 0; i < group.Length; i++)
                {
                    if (group[i] == '(')
                    {
                        startIndex = i;
                        parenthesisCount++;
                    }

                    if (group[i] == ')')
                    {
                        endIndex = i;
                        tempValue = RecursiveMethod(string.Join("", group.Skip(startIndex + 1).Take(endIndex - startIndex - 1)));
                        i = group.Length; // break out of for loop
                    }
                }

                // Part 1
                //group = group[0..(startIndex)] + Expression.EvaluateExpression(tempValue) + group[(endIndex + 1)..group.Length];

                // Part 2
                group = group[0..(startIndex)] + Expression.EvaluateExpressionPart2(tempValue) + group[(endIndex + 1)..group.Length];

                groupsExist = group.Contains("(");
            }

            return group;
        }

        public long Evaluate()
        {
            // Part 1
            //var value = Expression.EvaluateExpression(_ungroupedProblem);

            // Part 2
            var value = Expression.EvaluateExpressionPart2(_ungroupedProblem);

            Console.WriteLine(value);
            return value;
        }

        private void BuildExpressionStack()
        {

        }
    }
}