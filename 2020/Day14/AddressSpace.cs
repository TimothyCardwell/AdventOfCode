using System;
using System.Collections.Generic;
using System.Linq;

namespace Day14
{
    public class AddressSpace
    {
        private const int _addressSpaceSize = 1000;
        private readonly List<long> _addressSpace;
        private readonly Dictionary<long, int> _committedIndexes;

        public AddressSpace()
        {
            _addressSpace = new List<long>();
            _committedIndexes = new Dictionary<long, int>();
        }

        public void CommitValueToMemory(int index, Base36Integer value)
        {
            if (_committedIndexes.ContainsKey(index))
            {
                _addressSpace[_committedIndexes[index]] = value.DecimalValue;
            }
            else
            {
                _addressSpace.Add(value.DecimalValue);
                _committedIndexes.Add(index, _addressSpace.Count() - 1);
            }
        }

        public void CommitValueToMemory(List<Base36Integer> indexes, Base36Integer value)
        {
            foreach (var index in indexes)
            {
                if (_committedIndexes.ContainsKey(index.DecimalValue))
                {
                    _addressSpace[_committedIndexes[index.DecimalValue]] = value.DecimalValue;
                }
                else
                {
                    _addressSpace.Add(value.DecimalValue);
                    _committedIndexes.Add(index.DecimalValue, _addressSpace.Count() - 1);
                }
            }
        }

        public long GetSumOfAddressSpace()
        {
            return _addressSpace.Sum();
        }
    }
}