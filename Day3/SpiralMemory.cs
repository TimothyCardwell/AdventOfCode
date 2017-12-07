using System;

namespace Day3
{
    class SpiralMemory
    {
        private const int Input = 361527;

        static void Main(string[] args)
        {
            int nearestOddSquare = (int)Math.Ceiling(Math.Sqrt(Input));
            if(nearestOddSquare % 2 == 0)
            {
                nearestOddSquare++;
            }
            int size = nearestOddSquare;
            int[,] matrix = new int[size, size];

            int x = (size - 1) / 2;
            int y = (size - 1) / 2;
            matrix[x, y] = 1;
            int stepsPerSide = 1;
            int numberStepsCurrentSide = 0;
            int numberTraversalsCurrentStepCount = 0;
            Direction direction = Direction.Right;
            for (int i = 2; i < Input + 1; i++)
            {
                switch (direction)
                {
                    case Direction.Right:
                        y++;
                        break;
                    case Direction.Up:
                        x--;
                        break;
                    case Direction.Left:
                        y--;
                        break;
                    case Direction.Down:
                        x++;
                        break;
                }

                int sum = GetTouchingCellsSum(matrix, x, y);
                if(sum > Input)
                {
                    Console.WriteLine(sum);
                    Console.ReadLine();
                    return;
                }

                matrix[x, y] = sum;

                numberStepsCurrentSide++;
                if(numberStepsCurrentSide == stepsPerSide)
                {
                    numberTraversalsCurrentStepCount++;
                    direction = GetNextDirection(direction);
                    numberStepsCurrentSide = 0;
                }

                if(numberTraversalsCurrentStepCount == 2)
                {
                    stepsPerSide++;
                    numberTraversalsCurrentStepCount = 0;
                }
            }

            int horizontalSteps = Math.Abs(x - (size / 2));
            int verticalSteps = Math.Abs(y - (size / 2));
            Console.WriteLine(horizontalSteps);
            Console.WriteLine(verticalSteps);
            Console.WriteLine(horizontalSteps + verticalSteps);
            Console.ReadLine();
        }

        private static Direction GetNextDirection(Direction currentDirection)
        {
            switch(currentDirection)
            {
                case Direction.Right:
                    return Direction.Up;
                case Direction.Up:
                    return Direction.Left;
                case Direction.Left:
                    return Direction.Down;
                case Direction.Down:
                    return Direction.Right;
                default:
                    throw new Exception("This will never be thrown");
            }
        }

        private static int GetTouchingCellsSum(int[,] matrix, int x, int y)
        {
            int sum = 0;

            // Left
            if(x > 0)
            {
                sum += matrix[x - 1, y];
            }

            // Right
            if(x < matrix.GetLength(0) - 1)
            {
                sum += matrix[x + 1, y];
            }

            // Top
            if(y > 0)
            {
                sum += matrix[x, y - 1];
            }

            // Bottom
            if(y < matrix.GetLength(1) - 1)
            {
                sum += matrix[x, y + 1];
            }

            // Top Left
            if(x > 0 && y > 0)
            {
                sum += matrix[x - 1, y - 1];
            }

            // Top Right
            if (x < matrix.GetLength(0) - 1 && y > 0)
            {
                sum += matrix[x + 1, y - 1];
            }

            // Bottom Left
            if (x > 0 && y < matrix.GetLength(1) - 1)
            {
                sum += matrix[x - 1, y + 1];
            }

            // Bottom Right
            if (x < matrix.GetLength(0) - 1 && y < matrix.GetLength(1) - 1)
            {
                sum += matrix[x + 1, y + 1];
            }

            return sum;
        }

        private static void PrintMatrix(int[,] matrix)
        {
            Console.Clear();
            int zero = matrix.GetLength(0);
            int one = matrix.GetLength(1);
            for(int i = 0; i < zero; i++)
            {
                for(int j = 0; j < one; j++)
                {
                    if(j == one - 1)
                    {
                        Console.Write($"{matrix[i, j]}");
                    }
                    else
                    {
                        Console.Write($"{matrix[i, j]}-");
                    }
                }

                Console.WriteLine();
            }
        }

        private enum Direction
        {
            Right,
            Up,
            Left,
            Down
        }
    }

}
