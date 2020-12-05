using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            var boardingPassPartitions = File.ReadAllLines("./input.txt");
            var boardingPasses = boardingPassPartitions.Select(x => new BoardingPass(x));

            //Part1(boardingPasses);
            Part2(boardingPasses);

            Console.ReadLine();
        }

        static void Part1(IEnumerable<BoardingPass> boardingPasses)
        {
            var maxSeatId = 0;
            foreach (var boardingPass in boardingPasses)
            {
                if (boardingPass.SeatId > maxSeatId)
                {
                    maxSeatId = boardingPass.SeatId;
                }
            }

            Console.WriteLine(maxSeatId);
        }

        /// <summary>
        /// Seat ID = RowNum * 8 + ColNum
        /// 
        /// The seat ID will be sequential, meaning the next seatId is the previous seatId + 1. Just need 
        /// to sort and find the empty seat! This would be easily solved without loops if seats weren't missing.
        /// </summary>
        static void Part2(IEnumerable<BoardingPass> boardingPasses)
        {
            var sortedBoardingPasses = boardingPasses.OrderBy(x => x.SeatId).ToArray();
            for (var i = 0; i < sortedBoardingPasses.Count() - 1; i++)
            {
                var currentBoardingPass = sortedBoardingPasses[i];
                var nextBoardingPass = sortedBoardingPasses[i + 1];

                if (nextBoardingPass.SeatId - currentBoardingPass.SeatId != 1)
                {
                    var myBoardingPassColumn = currentBoardingPass.Column == 8 ? 0 : currentBoardingPass.Column + 1;
                    var myBoardingPassRow = currentBoardingPass.Column == 8 ? nextBoardingPass.Row : currentBoardingPass.Row;
                    var myBoardingPass = new BoardingPass(myBoardingPassRow, myBoardingPassColumn);
                    Console.WriteLine($"BoardingPassId: {myBoardingPass.SeatId}, Row: {myBoardingPass.Row}, Column: {myBoardingPass.Column}");
                }
            }
        }
    }
}
