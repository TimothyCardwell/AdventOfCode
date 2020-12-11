using System;
using System.IO;

namespace Day11
{
    class Program
    {
        static void Main(string[] args)
        {
            var seatConfigurationString = File.ReadAllLines("./input.txt");
            var seatingSystem = new SeatingSystem(seatConfigurationString);

            BothParts(seatingSystem);

            Console.ReadLine();
        }

        public static void BothParts(SeatingSystem seatingSystem)
        {
            var currentIteration = 0;
            while (currentIteration < 500 && !seatingSystem.IsStable)
            {
                seatingSystem.ExecuteSeatRules();
                currentIteration++;
            }

            Console.WriteLine(currentIteration);
            Console.WriteLine($"Occuped Seat Count: {seatingSystem.GetOccupiedSeatCount()}");
        }
    }
}
