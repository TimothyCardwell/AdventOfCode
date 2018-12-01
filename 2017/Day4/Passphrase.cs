using System;
using System.IO;
using System.Linq;

namespace Day4
{
    class Passphrasess
    {
        static void Main(string[] args)
        {
            int validPassphrases = 0;
            using (StreamReader sr = new StreamReader("input.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] phrases = line.Split(" ");

                    // Part 1
                    /*
                    if(phrases.Count() == phrases.Distinct().Count())
                    {
                        validPassphrases++;
                    }*/

                    // Part 2
                    for(int i = 0; i < phrases.Length; i++)
                    {
                        char[] sortedPhrase = phrases[i].ToCharArray();
                        Array.Sort(sortedPhrase);
                        phrases[i] = new string(sortedPhrase);
                    }
                    if(phrases.Count() == phrases.Distinct().Count())
                    {
                        validPassphrases++;
                    }
                }
            }

            Console.WriteLine(validPassphrases);
            Console.ReadLine();
        }
    }
}
