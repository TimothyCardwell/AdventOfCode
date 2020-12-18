using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day16
{
    class Program
    {
        private static TicketField[] _ticketFields;
        private static Ticket _myTicket;
        private static List<Ticket> _nearbyTickets;

        static void Main(string[] args)
        {
            ParseInput();

            Part1();
            Part2();

            Console.ReadLine();
        }

        public static void Part1()
        {
            var ticketScanningErrorRate = 0;
            foreach (var ticket in _nearbyTickets)
            {
                var invalidValue = ticket.IsTicketValid();
                if (invalidValue.HasValue)
                {
                    ticketScanningErrorRate += invalidValue.Value;
                }
            }

            Console.WriteLine(ticketScanningErrorRate);
        }

        /// <summary>
        /// Don't look at this code, it will hurt your eyes.
        /// 
        /// This is why you don't do procedural programming
        /// </summary>
        public static void Part2()
        {
            // Filter out invalid tickets
            var validTickets = new List<Ticket>();
            foreach (var ticket in _nearbyTickets)
            {
                var invalidValue = ticket.IsTicketValid();
                if (!invalidValue.HasValue)
                {
                    validTickets.Add(ticket);
                }
            }

            // Fucking magic
            var fieldValueMap = new Dictionary<int, HashSet<int>>();
            for (var i = 0; i < _ticketFields.Length; i++)
            {
                var matchingValueIndexes = validTickets[0].GetValidValueIndexes(i);
                for (var j = 1; j < validTickets.Count; j++)
                {
                    var result = validTickets[j].GetValidValueIndexes(i);
                    matchingValueIndexes.Intersect(result);
                    matchingValueIndexes = matchingValueIndexes.Intersect(result).ToHashSet();
                }

                fieldValueMap[i] = matchingValueIndexes;
            }

            // The previous for loop gives us a map of fields with their potential values... this will just flatten it out so that every field maps to the actual value
            var iterator = 0;
            var isProcessingComplete = false;
            while (!isProcessingComplete)
            {
                if (fieldValueMap[iterator].Count() == 1)
                {
                    for (var i = 0; i < _ticketFields.Length; i++)
                    {
                        var item = fieldValueMap[iterator].Single();
                        if (fieldValueMap[i].Contains(item) && i != iterator)
                        {
                            fieldValueMap[i].Remove(item);
                        }
                    }
                }

                if (fieldValueMap.All(x => x.Value.Count() == 1))
                {
                    isProcessingComplete = true;
                }

                iterator++;
                if (iterator >= _ticketFields.Length)
                {
                    iterator = 0;
                }
            }

            foreach (var fieldValueMapItem in fieldValueMap)
            {
                Console.WriteLine($"Field Name: {_ticketFields[fieldValueMapItem.Key].Name}, Value Index: {fieldValueMapItem.Value.Single()}");
            }

            // Get requested output
            long product = 1;
            for (var i = 0; i < _ticketFields.Length; i++)
            {
                var ticketFieldName = _ticketFields[i].Name;
                if (ticketFieldName.StartsWith("departure"))
                {
                    var ticketFieldValue = _myTicket._values[fieldValueMap[i].Single()];
                    product *= ticketFieldValue;
                }
            }

            Console.WriteLine(product);
        }

        private static void ParseInput()
        {
            var input = File.ReadAllLines("./input.txt");
            var firstWhiteSpace = 0;
            var secondWhiteSpace = 0;

            for (var i = 0; i < input.Length; i++)
            {
                if (string.IsNullOrEmpty(input[i]))
                {
                    firstWhiteSpace = i;
                    break;
                }
            }

            for (var i = firstWhiteSpace + 1; i < input.Length; i++)
            {
                if (string.IsNullOrEmpty(input[i]))
                {
                    secondWhiteSpace = i;
                    break;
                }
            }

            // Parse ticket fields
            _ticketFields = new TicketField[firstWhiteSpace];
            for (var i = 0; i < firstWhiteSpace; i++)
            {
                //// For example:
                // row: 6-11 or 33-44
                var ticketFieldSplit = input[i].Split(":");
                var name = ticketFieldSplit[0];

                var rangeSplits = ticketFieldSplit[1].Substring(1).Split(" ");

                var range1Split = rangeSplits[0].Split("-");
                var range1Start = Convert.ToInt32(range1Split[0]);
                var range1End = Convert.ToInt32(range1Split[1]);

                var range2Split = rangeSplits[2].Split("-");
                var range2Start = Convert.ToInt32(range2Split[0]);
                var range2End = Convert.ToInt32(range2Split[1]);
                _ticketFields[i] = new TicketField(name, range1Start, range1End, range2Start, range2End);
            }

            // Parse my ticket values
            var ticketValues = input[firstWhiteSpace + 2].Split(",").Select(x => Convert.ToInt32(x)).ToArray();
            _myTicket = new Ticket(_ticketFields, ticketValues);

            // Parse nearby ticket values
            _nearbyTickets = new List<Ticket>();
            for (var i = secondWhiteSpace + 2; i < input.Length; i++)
            {
                ticketValues = input[i].Split(",").Select(x => Convert.ToInt32(x)).ToArray();
                _nearbyTickets.Add(new Ticket(_ticketFields, ticketValues));
            }
        }
    }
}
