using System;
using System.Collections.Generic;

namespace Day17
{
    /// <summary>
    /// Wrapping the x, y, z index in an object so that I don't have to use tuples later.
    /// 
    /// This also allows me to store active cubes in a hashset
    /// </summary>
    public class Cube
    {
        public readonly int X;
        public readonly int Y;
        public readonly int Z;
        public bool IsActive;

        public Cube(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
            IsActive = false;
        }

        public List<Cube> GetPossibleNeighbors()
        {
            var result = new List<Cube>();


            result.Add(new Cube(X, Y - 1, Z));
            result.Add(new Cube(X, Y + 1, Z));
            result.Add(new Cube(X, Y, Z - 1));
            result.Add(new Cube(X, Y, Z + 1));
            result.Add(new Cube(X, Y - 1, Z - 1));
            result.Add(new Cube(X, Y + 1, Z + 1));
            result.Add(new Cube(X, Y - 1, Z + 1));
            result.Add(new Cube(X, Y + 1, Z - 1));

            result.Add(new Cube(X - 1, Y, Z));
            result.Add(new Cube(X - 1, Y - 1, Z));
            result.Add(new Cube(X - 1, Y + 1, Z));
            result.Add(new Cube(X - 1, Y, Z - 1));
            result.Add(new Cube(X - 1, Y, Z + 1));
            result.Add(new Cube(X - 1, Y - 1, Z - 1));
            result.Add(new Cube(X - 1, Y + 1, Z + 1));
            result.Add(new Cube(X - 1, Y - 1, Z + 1));
            result.Add(new Cube(X - 1, Y + 1, Z - 1));

            result.Add(new Cube(X + 1, Y, Z));
            result.Add(new Cube(X + 1, Y - 1, Z));
            result.Add(new Cube(X + 1, Y + 1, Z));
            result.Add(new Cube(X + 1, Y, Z - 1));
            result.Add(new Cube(X + 1, Y, Z + 1));
            result.Add(new Cube(X + 1, Y - 1, Z - 1));
            result.Add(new Cube(X + 1, Y + 1, Z + 1));
            result.Add(new Cube(X + 1, Y - 1, Z + 1));
            result.Add(new Cube(X + 1, Y + 1, Z - 1));

            return result;
        }

        public override bool Equals(object obj)
        {
            return obj is Cube point &&
                   X == point.X &&
                   Y == point.Y &&
                   Z == point.Z;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }
    }
}