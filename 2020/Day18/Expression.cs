using System;

namespace Day18
{
    public static class Expression
    {
        public static long EvaluateExpression(string expression)
        {
            if (expression.Contains("(") || expression.Contains(")"))
            {
                throw new InvalidOperationException($"Cannot run expression on grouped input: {expression}");
            }

            var expressionTerms = expression.Split(" ");
            var value = Convert.ToInt64(expressionTerms[0]);
            for (var i = 1; i < expressionTerms.Length; i++)
            {
                var currentTerm = expressionTerms[i];
                switch (currentTerm)
                {
                    case "+":
                        value += Convert.ToInt64(expressionTerms[i + 1]);
                        break;

                    case "*":
                        value *= Convert.ToInt64(expressionTerms[i + 1]);
                        break;

                    default:
                        throw new InvalidOperationException($"{currentTerm} is not a valid operator");
                }

                i++;
            }


            return value;
        }

        public static long EvaluateExpressionPart2(string expression)
        {
            if (expression.Contains("(") || expression.Contains(")"))
            {
                throw new InvalidOperationException($"Cannot run expression on grouped input: {expression}");
            }

            string[] expressionTerms;

            var indexOfPlus = -1;
            while ((indexOfPlus = expression.IndexOf("+")) > -1)
            {
                expressionTerms = expression.Split(" ");
                var expressionTermsIndexOfPlus = Array.IndexOf(expressionTerms, "+");
                var leftValue = Convert.ToInt64(expressionTerms[expressionTermsIndexOfPlus - 1]);
                var rightValue = Convert.ToInt64(expressionTerms[expressionTermsIndexOfPlus + 1]);

                var startIndex = indexOfPlus - 1 - leftValue.ToString().Length;
                var endIndex = indexOfPlus + 1 + rightValue.ToString().Length + 1;
                var leftRange = expression[0..startIndex];
                var rightRange = expression[endIndex..expression.Length];

                expression = leftRange + (leftValue + rightValue) + rightRange;
            }

            expressionTerms = expression.Split(" ");
            var value = Convert.ToInt64(expressionTerms[0]);
            for (var i = 1; i < expressionTerms.Length; i++)
            {
                var currentTerm = expressionTerms[i];
                switch (currentTerm)
                {
                    case "*":
                        value *= Convert.ToInt64(expressionTerms[i + 1]);
                        break;

                    default:
                        throw new InvalidOperationException($"{currentTerm} is not a valid operator");
                }

                i++;
            }


            return value;
        }
    }
}