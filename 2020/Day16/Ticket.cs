using System;
using System.Collections.Generic;
using System.Linq;

namespace Day16
{
    public class Ticket
    {
        public readonly TicketField[] _ticketFields;
        public readonly int[] _values;

        public Ticket(TicketField[] ticketFields, int[] values)
        {
            _ticketFields = ticketFields;
            _values = values;
        }

        /// <summary>
        /// Gets a set of indexes which correspond to values that are valid for the field whose index is the passed in parameter
        /// </summary>
        public HashSet<int> GetValidValueIndexes(int indexOfTicketField)
        {
            var result = new HashSet<int>();
            for (var i = 0; i < _values.Length; i++)
            {
                var value = _values[i];
                if (_ticketFields[indexOfTicketField].IsValueValid(value))
                {
                    result.Add(i);
                }
            }

            return result;
        }

        public int? IsTicketValid()
        {
            foreach (var value in _values)
            {
                if (!_ticketFields.Any(x => x.IsValueValid(value)))
                {
                    return value;
                }
            }

            return null;
        }
    }
}