using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            var passwordPolicyStrings = File.ReadAllLines("./input.txt");

            Part1(passwordPolicyStrings);
            Part2(passwordPolicyStrings);

            Console.ReadLine();
        }

        private static void Part1(string[] passwordPolicyStrings)
        {
            var passwordPolicies = passwordPolicyStrings.Select(x => new SledRentalPasswordPolicy(x));

            var correctPasswords = 0;
            foreach (var passwordPolicy in passwordPolicies)
            {
                if (passwordPolicy.IsValidPassword())
                {
                    correctPasswords++;
                }
            }

            Console.WriteLine($"Correct Passwords: {correctPasswords}");
        }

        private static void Part2(string[] passwordPolicyStrings)
        {
            var passwordPolicies = passwordPolicyStrings.Select(x => new TobogganPasswordPolicy(x));

            var correctPasswords = 0;
            foreach (var passwordPolicy in passwordPolicies)
            {
                if (passwordPolicy.IsValidPassword())
                {
                    correctPasswords++;
                }
            }

            Console.WriteLine($"Correct Passwords: {correctPasswords}");
        }
    }
}
