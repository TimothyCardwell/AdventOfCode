using System;

namespace Day16
{
    public class FieldValueRange
    {
        public readonly int StartRange;
        public readonly int EndRange;

        public FieldValueRange(int startRange, int endRange)
        {
            StartRange = startRange;
            EndRange = endRange;

            if (StartRange >= EndRange)
            {
                throw new InvalidOperationException("This case shouldn't exist, throwing just in case");
            }
        }

        public bool IsValueInRange(int value)
        {
            return StartRange <= value && value <= EndRange;
        }
    }
}