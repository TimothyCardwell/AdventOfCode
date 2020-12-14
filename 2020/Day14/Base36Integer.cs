using System;
using System.Linq;

namespace Day14
{
    public class Base36Integer
    {
        public readonly string BinaryValue;
        public readonly long DecimalValue;
        private const string _zero = "000000000000000000000000000000000000";

        public Base36Integer(string binaryValue)
        {
            BinaryValue = binaryValue;

            // Reverse the value's characters due to significant bit positioning
            var reversedValue = binaryValue.Reverse().ToArray();

            long convertedValue = 0;
            for (var i = 0; i < 36; i++)
            {
                if (reversedValue[i] == '1')
                {
                    convertedValue += (long)Math.Pow(2, i);
                }
            }

            DecimalValue = convertedValue;
        }

        public Base36Integer(long decimalValue)
        {
            DecimalValue = decimalValue;

            var convertedValue = _zero.ToCharArray();
            for (var i = 35; i >= 0; i--)
            {
                var powerValue = (long)Math.Pow(2, i);
                if (decimalValue - powerValue >= 0)
                {
                    decimalValue -= powerValue;
                    convertedValue[i] = '1';
                }
            }

            BinaryValue = String.Join("", convertedValue.Reverse());
        }
    }
}