using System;
using System.Collections.Generic;
using System.Linq;

namespace Day14
{
    public class Mask
    {
        private readonly string _bitmask;

        public Mask(string bitmask)
        {
            _bitmask = bitmask;
        }

        public Base36Integer MaskValue(Base36Integer value)
        {
            var maskedValue = value.BinaryValue.ToCharArray();
            for (var i = 0; i < maskedValue.Length; i++)
            {
                if (_bitmask[i] != 'X')
                {
                    maskedValue[i] = _bitmask[i];
                }
            }

            return new Base36Integer(String.Join("", maskedValue));
        }

        public List<Base36Integer> MaskValueFloating(Base36Integer value)
        {
            var result = new List<Base36Integer>();

            var maskedValue = value.BinaryValue.ToCharArray();
            for (var i = 0; i < maskedValue.Length; i++)
            {
                if (_bitmask[i] != '0')
                {
                    maskedValue[i] = _bitmask[i];
                }
            }

            return RecursiveMethod(String.Join("", maskedValue));
        }

        public List<Base36Integer> RecursiveMethod(string subMask)
        {
            // No more floating values, return value
            if (!subMask.Contains('X'))
            {
                return new List<Base36Integer> { new Base36Integer(subMask) };
            }

            var result = new List<Base36Integer>();

            var xIndex = subMask.IndexOf('X');
            var subMaskChars = subMask.ToCharArray();

            subMaskChars[xIndex] = '0';
            result = result.Concat(RecursiveMethod(String.Join("", subMaskChars))).ToList();

            subMaskChars[xIndex] = '1';
            result = result.Concat(RecursiveMethod(String.Join("", subMaskChars))).ToList();

            return result;
        }
    }
}