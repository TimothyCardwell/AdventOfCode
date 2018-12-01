using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Day9
{
    class Program
    {
        private const string SampleInput = "{{{<{oi!a,<{i<a>},{},{{}}}}";
        static void Main(string[] args)
        {
            string input = string.Empty;
            //input = SampleInput;
            using(StreamReader sr = new StreamReader("input.txt"))
            {
                input = sr.ReadToEnd();
            }
            Stack<char> characterStack = new Stack<char>();
            bool inGarbageBlock = false;
            int garbageCount = 0;
            for (int i = 0; i < input.Length; i++)
            {
                char currentCharacter = input[i];
                if (currentCharacter == '<' && !inGarbageBlock)
                {
                    inGarbageBlock = true;
                }

                else if (currentCharacter == '>' && inGarbageBlock)
                {
                    inGarbageBlock = false;
                }

                else if (currentCharacter == '!')
                {
                    i = i + 1;
                }

                else if ((currentCharacter == '{' || currentCharacter == '}') && !inGarbageBlock)
                {
                    characterStack.Push(currentCharacter);
                }

                else if(inGarbageBlock)
                {
                    garbageCount++;
                }
            }

            int score = 0;
            int level = 0;
            while(characterStack.Count > 0)
            {
                char character = characterStack.Pop();
                if(character == '}')
                {
                    level++;
                }
                else
                {
                    score += level;
                    level--;
                }
                Console.Write(character);
            }
            Console.WriteLine(garbageCount);
            Console.ReadLine();
        }
    }
}
