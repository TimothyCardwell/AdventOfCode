using System;

namespace Day11
{
    public class Seat
    {
        public readonly bool IsSeat; // Or else floor
        public bool IsOccupied;

        public Seat(char identifier)
        {
            switch (identifier)
            {
                case 'L':
                case '#':
                    IsSeat = true;
                    break;

                case '.':
                    IsSeat = false;
                    break;

                default:
                    throw new ArgumentException($"Identifier {identifier} is not a valid grid position value");
            }
        }

        /// <summary>
        /// Used for the clone method
        /// </summary>
        private Seat(bool isSeat, bool isOccupied)
        {
            IsSeat = isSeat;
            IsOccupied = isOccupied;
        }

        public object Clone()
        {
            var clone = new Seat(IsSeat, IsOccupied);
            return clone;
        }
    }
}