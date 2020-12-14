using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day13
{
    class Program
    {
        private static int _earliestDepartureTime;

        static void Main(string[] args)
        {
            var input = File.ReadAllLines("./input.txt");
            _earliestDepartureTime = Convert.ToInt32(input[0]);
            var busSchedulesSplit = input[1].Split(",");

            var buses = new List<Bus>();
            for (var i = 0; i < busSchedulesSplit.Length; i++)
            {
                if (busSchedulesSplit[i] != "x")
                {
                    buses.Add(new Bus(Convert.ToInt32(busSchedulesSplit[i]), i));
                }
            }

            // Literally copied Tyler's code for both parts https://github.com/thirschel/Advent-Of-Code/blob/master/2020/Day-13/solution.ts
            Part1(buses);
            Part2(buses);

            Console.ReadLine();
        }

        public static void Part1(IEnumerable<Bus> buses)
        {
            Bus busToTake = null;
            var minWaitingTime = Int32.MaxValue;
            foreach (var busSchedule in buses)
            {
                var waitingTime = busSchedule.Id * (int)(Math.Floor((decimal)_earliestDepartureTime / busSchedule.Id) + 1) - _earliestDepartureTime;
                if (waitingTime < minWaitingTime)
                {
                    minWaitingTime = waitingTime;
                    busToTake = busSchedule;
                }
            }

            Console.WriteLine($"BusId: {busToTake.Id} to take at {minWaitingTime} ==> {busToTake.Id * minWaitingTime}");
        }

        public static void Part2(IEnumerable<Bus> buses)
        {
            var multiplier = (long)buses.First().Id;
            var i = (long)0;

            foreach (var bus in buses.Skip(1))
            {
                while (true)
                {
                    if ((i + bus.Offset) % bus.Id == 0)
                    {
                        multiplier *= bus.Id;
                        break;
                    }
                    i += multiplier;
                }
            }

            Console.WriteLine(i);
        }
    }
}
