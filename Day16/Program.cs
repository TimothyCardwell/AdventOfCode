using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Day16
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] dancerPositions = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p" };

            List<DanceMove> danceMoves = new List<DanceMove>();
            using (StreamReader sr = new StreamReader("input.txt"))
            {
                string[] instructions = sr.ReadToEnd().Split(",");
                foreach(string instruction in instructions)
                {
                    char moveType = instruction[0];
                    string moveDetails = instruction.Substring(1);
                    switch(moveType)
                    {
                        case 'p':
                            string[] partners = moveDetails.Split("/");
                            danceMoves.Add(new PartnerDanceMove(partners[0], partners[1]));
                            break;
                        case 'x':
                            string[] indexes = moveDetails.Split("/");
                            danceMoves.Add(new ExchangeDanceMove(Convert.ToInt32(indexes[0]), Convert.ToInt32(indexes[1])));
                            break;
                        case 's':
                            danceMoves.Add(new SpinDanceMove(Convert.ToInt32(moveDetails)));
                            break;
                        default:
                            throw new ArgumentException($"Unsupported dance move: {instruction[0]}");
                    }
                }
            }

            // Part 1
            /*
                foreach (DanceMove danceMove in danceMoves)
                {
                    danceMove.PerformMove(ref dancerPositions);
                }
            */


            // Part 2
            string startingPoint = String.Join("", dancerPositions);
            int iterations = 1000000000;
            for (int i = 0; i < iterations; i++)
            {
                foreach (DanceMove danceMove in danceMoves)
                {
                    danceMove.PerformMove(ref dancerPositions);
                }

                // If a sequence matches the starting sequence, then a cycle is found. Can skip to the end of the iterations
                if (String.Join("", dancerPositions) == startingPoint) {
                    i += ((int)Math.Floor(iterations / (double)(i + 1)) - 1) * (i + 1);
                }
            }

            Console.WriteLine(String.Join("", dancerPositions));
            Console.ReadLine();
        }
    }
}
