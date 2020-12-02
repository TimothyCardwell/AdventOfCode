using System;
using System.Diagnostics;
using System.Linq;

namespace Day2
{
    public class TobogganPasswordPolicy : IPasswordPolicy
    {
        /// <summary>
        /// Handles parsing a single string into a PasswordPolicy object
        /// 
        /// Sample input:
        /// 8-15 b: zbxpbbbbbbhbbbpbp
        /// </summary>
        public TobogganPasswordPolicy(string passwordPolicyString)
        {
            var pieces = passwordPolicyString.Split(" ");
            if (pieces.Length != 3)
            {
                throw new InvalidOperationException($"Passwords can have whitespace! {passwordPolicyString}");
            }

            try
            {
                var minMaxValues = pieces[0].Split("-");
                FirstIndex = Convert.ToInt32(minMaxValues[0]) - 1;
                SecondIndex = Convert.ToInt32(minMaxValues[1]) - 1;
                RequiredLetter = pieces[1].TrimEnd(':').ToCharArray().Single();
                Password = pieces[2];
            }
            catch (Exception e)
            {
                Debug.WriteLine(passwordPolicyString);
                Debug.WriteLine("Problem parsing password policy string:");
                Debug.WriteLine(e.Message);
                throw;
            }
        }

        private readonly int FirstIndex;
        private readonly int SecondIndex;
        private readonly char RequiredLetter;
        private readonly string Password;

        public bool IsValidPassword()
        {
            return Password[FirstIndex] == RequiredLetter ^ Password[SecondIndex] == RequiredLetter;
        }
    }
}