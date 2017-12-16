using System;
using System.Collections.Generic;
using System.Text;

namespace Day16
{
    public class ExchangeDanceMove : DanceMove
    {
        public int IndexA { get; set; }
        public int IndexB { get; set; }
        public override string DanceMoveKey { get; set; }

        public ExchangeDanceMove(int indexA, int indexB)
        {
            IndexA = indexA;
            IndexB = indexB;
            DanceMoveKey = "Exchange";
        }

        public override void PerformMove(ref string[] currentPositions)
        {
            string temp = currentPositions[IndexA];
            currentPositions[IndexA] = currentPositions[IndexB];
            currentPositions[IndexB] = temp;
        }
    }
}
