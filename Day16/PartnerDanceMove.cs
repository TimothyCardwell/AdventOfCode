using System;
using System.Collections.Generic;
using System.Text;

namespace Day16
{
    public class PartnerDanceMove : DanceMove
    {
        public string PartnerA { get; set; }
        public string PartnerB { get; set; }
        public override string DanceMoveKey { get; set; }

        public PartnerDanceMove(string partnerA, string partnerB)
        {
            PartnerA = partnerA;
            PartnerB = partnerB;
            DanceMoveKey = "Parter";
        }

        public override void PerformMove(ref string[] currentPositions)
        {
            int? partnerAIndex = null;
            int? partnerBIndex = null;
            for(int i = 0; i < currentPositions.Length; i++)
            {
                if(currentPositions[i] == PartnerA)
                {
                    partnerAIndex = i;
                }
                else if(currentPositions[i] == PartnerB)
                {
                    partnerBIndex = i;
                }

                if(partnerAIndex.HasValue && partnerBIndex.HasValue)
                {
                    break;
                }
            }

            if(!partnerAIndex.HasValue || !partnerBIndex.HasValue)
            {
                throw new Exception("Could not find partner A or B");
            }

            string temp = currentPositions[partnerAIndex.Value];
            currentPositions[partnerAIndex.Value] = currentPositions[partnerBIndex.Value];
            currentPositions[partnerBIndex.Value] = temp;
        }
    }
}
