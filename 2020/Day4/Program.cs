using System;
using System.Collections.Generic;
using System.IO;

namespace Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("./input.txt");
            var passports = ParseInput(input);

            Part1(passports);
            Part2(passports);

            Console.ReadLine();
        }

        public static IEnumerable<Passport> ParseInput(string[] input)
        {
            var passports = new List<Passport>();

            string line;
            using (var file = new StreamReader("./input.txt"))
            {
                var passportValues = new Dictionary<string, string>();
                while ((line = file.ReadLine()) != null)
                {
                    // Represents a new passport entry
                    if (string.IsNullOrEmpty(line))
                    {
                        passports.Add(new Passport(passportValues));
                        passportValues = new Dictionary<string, string>();
                    }

                    else
                    {
                        var keyValuePairs = line.Split(" ");
                        foreach (var keyValuePair in keyValuePairs)
                        {
                            var keyValueSplit = keyValuePair.Split(":");
                            passportValues.Add(keyValueSplit[0], keyValueSplit[1]);
                        }
                    }
                }

                // Add the final one
                passports.Add(new Passport(passportValues));
            }

            return passports;
        }

        public static void Part1(IEnumerable<Passport> passports)
        {
            var validPassports = 0;
            foreach (var passport in passports)
            {
                if (passport.DoRequiredFieldsExist())
                {
                    validPassports++;
                }
            }

            // This is no longer correct after the part 2 changes
            Console.WriteLine($"Passports with required fields: {validPassports}");
        }

        public static void Part2(IEnumerable<Passport> passports)
        {
            var validPassports = 0;
            foreach (var passport in passports)
            {
                if (passport.IsPassportValid())
                {
                    validPassports++;
                }
            }

            Console.WriteLine($"Valid passport count: {validPassports}");
        }
    }
}
