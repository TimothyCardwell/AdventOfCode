using System;

namespace Day5
{
    public class BoardingPass
    {
        public readonly int SeatId;
        public int Row { get; private set; }
        public int Column { get; private set; }

        public BoardingPass(string boardingPassPartition)
        {
            var rowPartition = boardingPassPartition.Substring(0, 7);
            var columnPartition = boardingPassPartition.Substring(7);
            Row = ParsePartitionString(rowPartition, 0, 127);
            Column = ParsePartitionString(columnPartition, 0, 7);
            SeatId = CalculateSeatId();
        }

        public BoardingPass(int row, int column)
        {
            Row = row;
            Column = column;
            SeatId = CalculateSeatId();
        }

        private int CalculateSeatId()
        {
            return (Row * 8) + Column;
        }

        private int ParsePartitionString(string partitionString, int start, int end)
        {
            var halfOfRange = (int)Math.Floor((end - start) / 2m);
            switch (partitionString[0])
            {
                case 'F':
                case 'L':
                    // Base Case
                    if (partitionString.Length == 1)
                    {
                        return start;
                    }

                    return ParsePartitionString(partitionString.Substring(1), start, start + halfOfRange);

                case 'B':
                case 'R':
                    // Base Case
                    if (partitionString.Length == 1)
                    {
                        return end;
                    }

                    return ParsePartitionString(partitionString.Substring(1), end - halfOfRange, end);

                default:
                    throw new InvalidOperationException("Invalid partition value");
            }
        }
    }
}