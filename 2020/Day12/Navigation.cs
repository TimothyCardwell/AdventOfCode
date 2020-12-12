using System;
using System.ComponentModel;

namespace Day12
{
    public class Navigation
    {
        private int _x = 0;
        private int _y = 0;
        private int _xWaypoint = 10;
        private int _yWaypoint = 1;
        private int _directionFacingStore = 90; // Start facing east
        private int _directionFacing
        {
            get => _directionFacingStore;
            set
            {
                if (value >= 360)
                {
                    _directionFacingStore = value - 360;
                }
                else if (value < 0)
                {
                    _directionFacingStore = 360 + value;
                }
                else
                {
                    _directionFacingStore = value;
                }
            }
        }

        public Navigation(string[] navigationInstructions, bool isPart2 = false)
        {
            foreach (var navigationInstruction in navigationInstructions)
            {
                if (isPart2)
                {
                    HandleNavigationInstructionPart2(new NavigationInstruction(navigationInstruction));
                }
                else
                {
                    HandleNavigationInstructionPart1(new NavigationInstruction(navigationInstruction));
                }
            }
        }

        public int GetManhattanDistance()
        {
            return Math.Abs(_x) + Math.Abs(_y);
        }

        private void HandleNavigationInstructionPart1(NavigationInstruction navigationInstruction)
        {
            switch (navigationInstruction.NavigationAction)
            {
                case NavigationAction.TurnLeft:
                    _directionFacing -= navigationInstruction.Value;
                    break;

                case NavigationAction.TurnRight:
                    _directionFacing += navigationInstruction.Value;
                    break;

                case NavigationAction.MoveForward:
                    switch (_directionFacing)
                    {
                        // North
                        case 0:
                            _y += navigationInstruction.Value;
                            break;

                        // East
                        case 90:
                            _x += navigationInstruction.Value;
                            break;

                        // South
                        case 180:
                            _y -= navigationInstruction.Value;
                            break;

                        // West
                        case 270:
                            _x -= navigationInstruction.Value;
                            break;

                        default:
                            throw new NotImplementedException("Cannot move in diagonals (yet)");
                    }
                    break;

                case NavigationAction.MoveNorth:
                    _y += navigationInstruction.Value;
                    break;

                case NavigationAction.MoveEast:
                    _x += navigationInstruction.Value;
                    break;

                case NavigationAction.MoveSouth:
                    _y -= navigationInstruction.Value;
                    break;

                case NavigationAction.MoveWest:
                    _x -= navigationInstruction.Value;
                    break;

                default:
                    throw new InvalidEnumArgumentException($"{navigationInstruction.NavigationAction} is not a valid enum");
            }
        }

        private void HandleNavigationInstructionPart2(NavigationInstruction navigationInstruction)
        {
            switch (navigationInstruction.NavigationAction)
            {
                case NavigationAction.TurnLeft:
                    var iterations = navigationInstruction.Value / 90;
                    for (var i = 0; i < iterations; i++)
                    {
                        RotateLeft90Degrees();
                    }
                    break;

                case NavigationAction.TurnRight:
                    iterations = navigationInstruction.Value / 90;
                    for (var i = 0; i < iterations; i++)
                    {
                        RotateRight90Degrees();
                    }
                    break;

                case NavigationAction.MoveForward:
                    _x += _xWaypoint * navigationInstruction.Value;
                    _y += _yWaypoint * navigationInstruction.Value;
                    break;

                case NavigationAction.MoveNorth:
                    _yWaypoint += navigationInstruction.Value;
                    break;

                case NavigationAction.MoveEast:
                    _xWaypoint += navigationInstruction.Value;
                    break;

                case NavigationAction.MoveSouth:
                    _yWaypoint -= navigationInstruction.Value;
                    break;

                case NavigationAction.MoveWest:
                    _xWaypoint -= navigationInstruction.Value;
                    break;

                default:
                    throw new InvalidEnumArgumentException($"{navigationInstruction.NavigationAction} is not a valid enum");
            }
        }

        /// <summary>
        /// (A, B) ==> (-B, A)
        /// </summary>
        private void RotateLeft90Degrees()
        {
            var tempValue = _xWaypoint;
            _xWaypoint = -1 * _yWaypoint;
            _yWaypoint = tempValue;

        }

        /// <summary>
        /// (A, B) ==> (B, -A)
        /// </summary>
        private void RotateRight90Degrees()
        {
            var tempValue = _xWaypoint;
            _xWaypoint = _yWaypoint;
            _yWaypoint = -1 * tempValue;
        }
    }
}