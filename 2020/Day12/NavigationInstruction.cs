using System;

namespace Day12
{
    public class NavigationInstruction
    {
        public NavigationAction NavigationAction { get; private set; }
        public int Value { get; private set; }

        public NavigationInstruction(string navigationInstruction)
        {
            switch (navigationInstruction.Substring(0, 1))
            {
                case "N":
                    NavigationAction = NavigationAction.MoveNorth;
                    break;
                case "E":
                    NavigationAction = NavigationAction.MoveEast;
                    break;
                case "S":
                    NavigationAction = NavigationAction.MoveSouth;
                    break;
                case "W":
                    NavigationAction = NavigationAction.MoveWest;
                    break;
                case "F":
                    NavigationAction = NavigationAction.MoveForward;
                    break;
                case "L":
                    NavigationAction = NavigationAction.TurnLeft;
                    break;
                case "R":
                    NavigationAction = NavigationAction.TurnRight;
                    break;
                default:
                    throw new InvalidOperationException($"{navigationInstruction.Substring(0, 1)} is not a valid navigation action");
            }

            Value = Convert.ToInt32(navigationInstruction.Substring(1));
        }
    }
}