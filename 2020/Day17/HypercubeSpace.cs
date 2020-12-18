using System;
using System.Collections.Generic;
using System.Text;

namespace Day17
{
    public class HypercubeSpace
    {

        public HashSet<Hypercube> ActiveCubes { get; private set; }
        public Hypercube[,,,] Space { get; private set; }
        private int _xOffset;
        private int _yOffset;
        private int _zOffset;
        private int _wOffset;

        public HypercubeSpace(int xLength, int yLength)
        {
            Space = new Hypercube[xLength, yLength, 1, 1];
            ActiveCubes = new HashSet<Hypercube>();
            _xOffset = 0;
            _yOffset = 0;
            _zOffset = 0;
            _wOffset = 0;

            InitializeCubeSpace();
        }

        public HypercubeSpace(int xStart, int xEnd, int yStart, int yEnd, int zStart, int zEnd, int wStart, int wEnd)
        {
            Space = new Hypercube[Math.Abs(xEnd - xStart) + 1, Math.Abs(yEnd - yStart) + 1, Math.Abs(zEnd - zStart) + 1, Math.Abs(wEnd - wStart) + 1];
            ActiveCubes = new HashSet<Hypercube>();

            if (xStart < 0) _xOffset = Math.Abs(xStart);
            if (xStart > 0) _xOffset = -1 * xStart;
            if (yStart < 0) _yOffset = Math.Abs(yStart);
            if (yStart > 0) _yOffset = -1 * yStart;
            if (zStart < 0) _zOffset = Math.Abs(zStart);
            if (zStart > 0) _zOffset = -1 * zStart;
            if (wStart < 0) _wOffset = Math.Abs(wStart);
            if (wStart > 0) _wOffset = -1 * wStart;

            InitializeCubeSpace();
        }

        public void AddActiveCube(int x, int y, int z, int w)
        {
            x += _xOffset;
            y += _yOffset;
            z += _zOffset;
            w += _wOffset;

            if (Space[x, y, z, w].IsActive)
            {
                throw new InvalidOperationException($"Cube already exists at point {x}, {y}, {z}, {w}");
            }

            Space[x, y, z, w].IsActive = true;
            ActiveCubes.Add(Space[x, y, z, w]);
        }

        public IEnumerable<Hypercube> GetCubeSpaceIterator()
        {
            var result = new List<Hypercube>();
            for (int x = -1; x < Space.GetLength(0) + 1; x++)
            {
                for (int y = -1; y < Space.GetLength(1) + 1; y++)
                {
                    for (int z = -1; z < Space.GetLength(2) + 1; z++)
                    {
                        for (int w = -1; w < Space.GetLength(3) + 1; w++)
                        {
                            // Need to create cubes to represent neighbors that don't exist yet
                            if (x == -1 || x == Space.GetLength(0) || y == -1 || y == Space.GetLength(1) || z == -1 || z == Space.GetLength(2) || w == -1 || w == Space.GetLength(3))
                            {
                                result.Add(new Hypercube(x + _xOffset, y + _yOffset, z + _zOffset, w + _wOffset));
                            }
                            else
                            {
                                result.Add(Space[x, y, z, w]);
                            }
                        }
                    }
                }
            }

            return result;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int w = 0; w < Space.GetLength(3); w++)
            {
                for (int z = 0; z < Space.GetLength(2); z++)
                {
                    sb.Append($"z={Space[0, 0, z, w].Z}, w={Space[0, 0, z, w].W}\n");
                    for (int y = Space.GetLength(1) - 1; y >= 0; y--)
                    {
                        for (int x = 0; x < Space.GetLength(0); x++)
                        {
                            if (Space[x, y, z, w].IsActive)
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
                        for (int w = 0; w < Space.GetLength(3); w++)
                        {
                            Space[x, y, z, w] = new Hypercube(x + _xOffset, y + _yOffset, z + _zOffset, w + _wOffset);
                        }
                    }
                }
            }
        }
    }
}