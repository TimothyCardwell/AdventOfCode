using System;
using System.Collections.Generic;

namespace Day17
{
    public class Hypercube
    {

        public readonly int X;
        public readonly int Y;
        public readonly int Z;
        public readonly int W;
        public bool IsActive;

        public Hypercube(int x, int y, int z, int w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
            IsActive = false;
        }

        public List<Hypercube> GetPossibleNeighbors()
        {
            var result = new List<Hypercube>();

            result.Add(new Hypercube(X, Y - 1, Z, W));
            result.Add(new Hypercube(X, Y + 1, Z, W));
            result.Add(new Hypercube(X, Y, Z - 1, W));
            result.Add(new Hypercube(X, Y, Z + 1, W));
            result.Add(new Hypercube(X, Y - 1, Z - 1, W));
            result.Add(new Hypercube(X, Y + 1, Z + 1, W));
            result.Add(new Hypercube(X, Y - 1, Z + 1, W));
            result.Add(new Hypercube(X, Y + 1, Z - 1, W));
            result.Add(new Hypercube(X, Y, Z, W + 1));
            result.Add(new Hypercube(X, Y - 1, Z, W + 1));
            result.Add(new Hypercube(X, Y + 1, Z, W + 1));
            result.Add(new Hypercube(X, Y, Z - 1, W + 1));
            result.Add(new Hypercube(X, Y, Z + 1, W + 1));
            result.Add(new Hypercube(X, Y - 1, Z - 1, W + 1));
            result.Add(new Hypercube(X, Y + 1, Z + 1, W + 1));
            result.Add(new Hypercube(X, Y - 1, Z + 1, W + 1));
            result.Add(new Hypercube(X, Y + 1, Z - 1, W + 1));
            result.Add(new Hypercube(X, Y, Z, W - 1));
            result.Add(new Hypercube(X, Y - 1, Z, W - 1));
            result.Add(new Hypercube(X, Y + 1, Z, W - 1));
            result.Add(new Hypercube(X, Y, Z - 1, W - 1));
            result.Add(new Hypercube(X, Y, Z + 1, W - 1));
            result.Add(new Hypercube(X, Y - 1, Z - 1, W - 1));
            result.Add(new Hypercube(X, Y + 1, Z + 1, W - 1));
            result.Add(new Hypercube(X, Y - 1, Z + 1, W - 1));
            result.Add(new Hypercube(X, Y + 1, Z - 1, W - 1));
            result.Add(new Hypercube(X - 1, Y, Z, W));
            result.Add(new Hypercube(X - 1, Y - 1, Z, W));
            result.Add(new Hypercube(X - 1, Y + 1, Z, W));
            result.Add(new Hypercube(X - 1, Y, Z - 1, W));
            result.Add(new Hypercube(X - 1, Y, Z + 1, W));
            result.Add(new Hypercube(X - 1, Y - 1, Z - 1, W));
            result.Add(new Hypercube(X - 1, Y + 1, Z + 1, W));
            result.Add(new Hypercube(X - 1, Y - 1, Z + 1, W));
            result.Add(new Hypercube(X - 1, Y + 1, Z - 1, W));
            result.Add(new Hypercube(X - 1, Y, Z, W + 1));
            result.Add(new Hypercube(X - 1, Y - 1, Z, W + 1));
            result.Add(new Hypercube(X - 1, Y + 1, Z, W + 1));
            result.Add(new Hypercube(X - 1, Y, Z - 1, W + 1));
            result.Add(new Hypercube(X - 1, Y, Z + 1, W + 1));
            result.Add(new Hypercube(X - 1, Y - 1, Z - 1, W + 1));
            result.Add(new Hypercube(X - 1, Y + 1, Z + 1, W + 1));
            result.Add(new Hypercube(X - 1, Y - 1, Z + 1, W + 1));
            result.Add(new Hypercube(X - 1, Y + 1, Z - 1, W + 1));
            result.Add(new Hypercube(X - 1, Y, Z, W - 1));
            result.Add(new Hypercube(X - 1, Y - 1, Z, W - 1));
            result.Add(new Hypercube(X - 1, Y + 1, Z, W - 1));
            result.Add(new Hypercube(X - 1, Y, Z - 1, W - 1));
            result.Add(new Hypercube(X - 1, Y, Z + 1, W - 1));
            result.Add(new Hypercube(X - 1, Y - 1, Z - 1, W - 1));
            result.Add(new Hypercube(X - 1, Y + 1, Z + 1, W - 1));
            result.Add(new Hypercube(X - 1, Y - 1, Z + 1, W - 1));
            result.Add(new Hypercube(X - 1, Y + 1, Z - 1, W - 1));
            result.Add(new Hypercube(X + 1, Y, Z, W));
            result.Add(new Hypercube(X + 1, Y - 1, Z, W));
            result.Add(new Hypercube(X + 1, Y + 1, Z, W));
            result.Add(new Hypercube(X + 1, Y, Z - 1, W));
            result.Add(new Hypercube(X + 1, Y, Z + 1, W));
            result.Add(new Hypercube(X + 1, Y - 1, Z - 1, W));
            result.Add(new Hypercube(X + 1, Y + 1, Z + 1, W));
            result.Add(new Hypercube(X + 1, Y - 1, Z + 1, W));
            result.Add(new Hypercube(X + 1, Y + 1, Z - 1, W));
            result.Add(new Hypercube(X + 1, Y, Z, W + 1));
            result.Add(new Hypercube(X + 1, Y - 1, Z, W + 1));
            result.Add(new Hypercube(X + 1, Y + 1, Z, W + 1));
            result.Add(new Hypercube(X + 1, Y, Z - 1, W + 1));
            result.Add(new Hypercube(X + 1, Y, Z + 1, W + 1));
            result.Add(new Hypercube(X + 1, Y - 1, Z - 1, W + 1));
            result.Add(new Hypercube(X + 1, Y + 1, Z + 1, W + 1));
            result.Add(new Hypercube(X + 1, Y - 1, Z + 1, W + 1));
            result.Add(new Hypercube(X + 1, Y + 1, Z - 1, W + 1));
            result.Add(new Hypercube(X + 1, Y, Z, W - 1));
            result.Add(new Hypercube(X + 1, Y - 1, Z, W - 1));
            result.Add(new Hypercube(X + 1, Y + 1, Z, W - 1));
            result.Add(new Hypercube(X + 1, Y, Z - 1, W - 1));
            result.Add(new Hypercube(X + 1, Y, Z + 1, W - 1));
            result.Add(new Hypercube(X + 1, Y - 1, Z - 1, W - 1));
            result.Add(new Hypercube(X + 1, Y + 1, Z + 1, W - 1));
            result.Add(new Hypercube(X + 1, Y - 1, Z + 1, W - 1));
            result.Add(new Hypercube(X + 1, Y + 1, Z - 1, W - 1));

            return result;
        }

        public override bool Equals(object obj)
        {
            return obj is Hypercube point &&
                   X == point.X &&
                   Y == point.Y &&
                   Z == point.Z &&
                   W == point.W;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z, W);
        }
    }
}