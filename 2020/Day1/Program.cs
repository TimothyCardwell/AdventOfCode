using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Day1
{
    class Program
    {
        /// <summary>
        /// Brute force approach. I'd imagine a better approach would be to sort the numbers and check and opposing ends of the list, but this completed in sub second time so whatevs
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var input = File.ReadAllText("./input.json");
            var expenseReport = JsonSerializer.Deserialize<List<int>>(input);

            Part1(expenseReport);
            Part2(expenseReport);

            Console.ReadLine();
        }

        private static void Part1(List<int> expenses)
        {
            for (var i = 0; i < expenses.Count; i++)
            {
                for (var j = i + 1; j < expenses.Count; j++)
                {
                    var number1 = expenses[i];
                    var number2 = expenses[j];
                    if (number1 + number2 == 2020)
                    {
                        Console.WriteLine($"number1: {number1}, number2: {number2}, number1 * number2: {number1 * number2}");
                    }
                }
            }
        }

        private static void Part2(List<int> expenses)
        {
            for (var i = 0; i < expenses.Count; i++)
            {
                for (var j = i + 1; j < expenses.Count; j++)
                {
                    for (var k = j + 1; k < expenses.Count; k++)
                    {
                        var number1 = expenses[i];
                        var number2 = expenses[j];
                        var number3 = expenses[k];
                        if (number1 + number2 + number3 == 2020)
                        {
                            Console.WriteLine($"number1: {number1}, number2: {number2}, number3: {number3}, number1 * number2 * number3: {number1 * number2 * number3}");
                        }
                    }
                }
            }
        }
    }
}