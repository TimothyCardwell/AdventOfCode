using System;
using System.Collections.Generic;
using System.Text;

namespace Day16
{
    public class SpinDanceMove : DanceMove
    {
        public int Size { get; set; }
        public override string DanceMoveKey { get; set; }

        public SpinDanceMove(int size)
        {
            Size = size;
            DanceMoveKey = "Spin";
        }

        public override void PerformMove(ref string[] currentPositions)
        {
            string[] finalPositions = new string[currentPositions.Length];
            for(int i = 0; i < currentPositions.Length; i++)
            {
                int targetIndex = i >= Size ? i - Size : currentPositions.Length - Size + i;
                finalPositions[i] = currentPositions[targetIndex];
            }

            currentPositions = finalPositions;
        }
    }
}
