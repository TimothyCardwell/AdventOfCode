namespace Day16
{
    public class TicketField
    {
        public readonly string Name;
        public readonly FieldValueRange Range1;
        public readonly FieldValueRange Range2;

        public TicketField(string name, int range1Start, int range1End, int range2Start, int range2End)
        {
            Name = name;
            Range1 = new FieldValueRange(range1Start, range1End);
            Range2 = new FieldValueRange(range2Start, range2End);
        }

        public bool IsValueValid(int value)
        {
            var isValid = Range1.IsValueInRange(value) || Range2.IsValueInRange(value);
            return isValid;
        }
    }
}