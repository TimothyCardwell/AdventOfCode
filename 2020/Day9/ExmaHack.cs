using System;
using System.Collections.Generic;

namespace Day9
{
    public class ExmaHack
    {
        private const int _preambleLength = 25;
        private readonly long[] _data;

        public ExmaHack(long[] data)
        {
            _data = data;
        }

        /// <summary>
        /// For a given element in the data array, we need to find if two numbers within '_preambleLength'
        /// elements before that item sum up to the value of that item. If no two items are found, then it
        /// is considered an invalid item.
        /// 
        /// For this algorithm, note the following:
        /// currentItem is the element in the data array that is being checked for validity.
        /// x and y both represent two elements within '_preambleLength' elements before the currentItem.
        /// </summary>
        public long? GetInvalidDataItem()
        {
            // Will check every item in the array, but we need to start at the 'preambleLength' element
            for (var i = _preambleLength; i < _data.Length; i++)
            {
                var currentItem = _data[i];

                // Populate a hashset rather than a use nested for loops
                var hashSet = new HashSet<long>();
                for (var j = i - _preambleLength; j < i; j++)
                {
                    hashSet.Add(_data[j]);
                }

                var isValidItem = false;
                for (var j = i - _preambleLength; j < i; j++)
                {
                    var x = _data[j];
                    var y = currentItem - x;
                    if (hashSet.Contains(y))
                    {
                        isValidItem = true;
                        break;
                    }
                }

                if (!isValidItem)
                {
                    return currentItem;
                }
            }

            return null;
        }

        public long? GetEncryptionWeakness()
        {
            var invalidDataItem = GetInvalidDataItem();
            if (!invalidDataItem.HasValue) return null;

            var lowerBound = -1;
            var upperBound = -1;

            for (var i = 0; i < _data.Length; i++)
            {
                var currentTotal = _data[i];
                for (var j = i + 1; j < _data.Length; j++)
                {
                    currentTotal += _data[j];

                    if (currentTotal > invalidDataItem)
                    {
                        break;
                    }

                    if (currentTotal == invalidDataItem)
                    {
                        lowerBound = i;
                        upperBound = j;

                        // Force us out of the nested loops
                        i = _data.Length;
                        j = _data.Length;
                    }
                }
            }

            if (lowerBound == -1 || upperBound == -1) return null;

            var dataSubset = _data[lowerBound..upperBound];
            Array.Sort(dataSubset);
            return dataSubset[0] + dataSubset[dataSubset.Length - 1];
        }
    }
}