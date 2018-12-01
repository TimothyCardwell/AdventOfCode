using System;
using System.Collections.Generic;
using System.Text;

namespace Day16
{
    public abstract class DanceMove
    {
        public abstract string DanceMoveKey { get; set; }
        public abstract void PerformMove(ref string[] currentPositions);
    }
}
