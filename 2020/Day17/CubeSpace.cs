using System;
using System.Collections.Generic;
using System.Text;

namespace Day17
{
    public class CubeSpace
    {
        public HashSet<Cube> ActiveCubes { get; private set; }
        public Cube[,,] Space { get; private set; }
        private int _xOffset;
        private int _yOffset;
        private int _zOffset;

        public CubeSpace(int xLength, int yLength)
        {
            Space = new Cube[xLength, yLength, 1];
            ActiveCubes = new HashSet<Cube>();
            _xOffset = 0;
            _yOffset = 0;
            _zOffset = 0;

            InitializeCubeSpace();
        }

        public CubeSpace(int xStart, int xEnd, int yStart, int yEnd, int zStart, int zEnd)
        {
            Space = new Cube[Math.Abs(xEnd - xStart) + 1, Math.Abs(yEnd - yStart) + 1, Math.Abs(zEnd - zStart) + 1];
            ActiveCubes = new HashSet<Cube>();

            if (xStart < 0) _xOffset = Math.Abs(xStart);
            if (xStart > 0) _xOffset = -1 * xStart;
            if (yStart < 0) _yOffset = Math.Abs(yStart);
            if (yStart > 0) _yOffset = -1 * yStart;
            if (zStart < 0) _zOffset = Math.Abs(zStart);
            if (zStart > 0) _zOffset = -1 * zStart;

            InitializeCubeSpace();
        }

        public void AddActiveCube(int x, int y, int z)
        {
            x += _xOffset;
            y += _yOffset;
            z += _zOffset;

            if (Space[x, y, z].IsActive)
            {
                throw new InvalidOperationException($"Cube already exists at point {x}, {y}, {z}");
            }

            Space[x, y, z].IsActive = true;
            ActiveCubes.Add(Space[x, y, z]);
        }

        public IEnumerable<Cube> GetCubeSpaceIterator()
        {
            var result = new List<Cube>();
            for (int x = -1; x < Space.GetLength(0) + 1; x++)
            {
                for (int y = -1; y < Space.GetLength(1) + 1; y++)
                {
                    for (int z = -1; z < Space.GetLength(2) + 1; z++)
                    {
                        // Need to create cubes to represent neighbors that don't exist yet
                        if (x == -1 || x == Space.GetLength(0) || y == -1 || y == Space.GetLength(1) || z == -1 || z == Space.GetLength(2))
                        {
                            result.Add(new Cube(x + _xOffset, y + _yOffset, z + _zOffset));
                        }
                        else
                        {
                            result.Add(Space[x, y, z]);
                        }
                    }
                }
            }

            return result;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int z = 0; z < Space.GetLength(2); z++)
            {
                sb.Append($"z={Space[0, 0, z].Z}\n");
                for (int y = Space.GetLength(1) - 1; y >= 0; y--)
                {
                    for (int x = 0; x < Space.GetLength(0); x++)
                    {
                        if (Space[x, y, z].IsActive)
                        {
                            sb.Append("#");
                        }
                        else
                        {
                            sb.Append(".");
                        }
                    }

                    sb.Append("\n");
                }

                sb.Append("\n");
            }

            return sb.ToString();
        }

        private void InitializeCubeSpace()
        {
            for (int x = 0; x < Space.GetLength(0); x++)
            {
                for (int y = 0; y < Space.GetLength(1); y++)
                {
                    for (int z = 0; z < Space.GetLength(2); z++)
                    {
                        Space[x, y, z] = new Cube(x + _xOffset, y + _yOffset, z + _zOffset);
                    }
                }
            }
        }
    }
}