using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day17
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("./input.txt").Reverse().ToArray();

            var cubeSpace = new CubeSpace(input[0].Length, input.Length);
            for (var x = 0; x < input.Length; x++)
            {
                for (var y = 0; y < input.Length; y++)
                {
                    if (input[y][x] == '#')
                    {
                        cubeSpace.AddActiveCube(x, y, 0);
                    }
                }
            }

            Part1(cubeSpace);

            var hypercubeSpace = new HypercubeSpace(input[0].Length, input.Length);
            for (var x = 0; x < input.Length; x++)
            {
                for (var y = 0; y < input.Length; y++)
                {
                    if (input[y][x] == '#')
                    {
                        hypercubeSpace.AddActiveCube(x, y, 0, 0);
                    }
                }
            }

            Part2(hypercubeSpace);

            Console.ReadLine();
        }

        static void Part1(CubeSpace cubeSpace)
        {
            for (var i = 0; i < 6; i++)
            {
                cubeSpace = ExecuteCycle(cubeSpace);
                Console.WriteLine(cubeSpace.ToString());
            }

            Console.WriteLine(cubeSpace.ActiveCubes.Count());
        }

        static void Part2(HypercubeSpace hypercubeSpace)
        {
            for (var i = 0; i < 6; i++)
            {
                hypercubeSpace = ExecuteCycle(hypercubeSpace);
                Console.WriteLine(hypercubeSpace.ToString());
            }

            Console.WriteLine(hypercubeSpace.ActiveCubes.Count());
        }

        private static CubeSpace ExecuteCycle(CubeSpace cubeSpace)
        {
            var newActiveCubes = new List<Cube>();
            int? minX = null, maxX = null, minY = null, maxY = null, minZ = null, maxZ = null; // Used to build the new cube space later

            foreach (var cube in cubeSpace.GetCubeSpaceIterator())
            {
                var activeNeighbors = 0;
                foreach (var neighbor in cube.GetPossibleNeighbors())
                {
                    if (cubeSpace.ActiveCubes.Contains(neighbor))
                    {
                        activeNeighbors++;
                    }
                }

                if ((cube.IsActive && (activeNeighbors == 2 || activeNeighbors == 3)) || (!cube.IsActive && activeNeighbors == 3))
                {
                    newActiveCubes.Add(cube);
                    if (!minX.HasValue || cube.X < minX) minX = cube.X;
                    if (!minY.HasValue || cube.Y < minY) minY = cube.Y;
                    if (!minZ.HasValue || cube.Z < minZ) minZ = cube.Z;
                    if (!maxX.HasValue || cube.X > maxX) maxX = cube.X;
                    if (!maxY.HasValue || cube.Y > maxY) maxY = cube.Y;
                    if (!maxZ.HasValue || cube.Z > maxZ) maxZ = cube.Z;
                }
            }

            if (newActiveCubes.Count() == 0)
            {
                throw new InvalidOperationException("No more active cubes!!!!");
            }

            var newCubeSpace = new CubeSpace(minX.Value, maxX.Value, minY.Value, maxY.Value, minZ.Value, maxZ.Value);
            foreach (var cube in newActiveCubes)
            {
                newCubeSpace.AddActiveCube(cube.X, cube.Y, cube.Z);
            }

            return newCubeSpace;
        }

        private static HypercubeSpace ExecuteCycle(HypercubeSpace cubeSpace)
        {
            var newActiveCubes = new List<Hypercube>();
            int? minX = null, maxX = null, minY = null, maxY = null, minZ = null, maxZ = null, minW = null, maxW = null; // Used to build the new cube space later

            foreach (var cube in cubeSpace.GetCubeSpaceIterator())
            {
                var activeNeighbors = 0;
                foreach (var neighbor in cube.GetPossibleNeighbors())
                {
                    if (cubeSpace.ActiveCubes.Contains(neighbor))
                    {
                        activeNeighbors++;
                    }
                }

                if ((cube.IsActive && (activeNeighbors == 2 || activeNeighbors == 3)) || (!cube.IsActive && activeNeighbors == 3))
                {
                    newActiveCubes.Add(cube);
                    if (!minX.HasValue || cube.X < minX) minX = cube.X;
                    if (!minY.HasValue || cube.Y < minY) minY = cube.Y;
                    if (!minZ.HasValue || cube.Z < minZ) minZ = cube.Z;
                    if (!minW.HasValue || cube.W < minW) minW = cube.W;
                    if (!maxX.HasValue || cube.X > maxX) maxX = cube.X;
                    if (!maxY.HasValue || cube.Y > maxY) maxY = cube.Y;
                    if (!maxZ.HasValue || cube.Z > maxZ) maxZ = cube.Z;
                    if (!maxW.HasValue || cube.W > maxW) maxW = cube.W;
                }
            }

            if (newActiveCubes.Count() == 0)
            {
                throw new InvalidOperationException("No more active cubes!!!!");
            }

            var newCubeSpace = new HypercubeSpace(minX.Value, maxX.Value, minY.Value, maxY.Value, minZ.Value, maxZ.Value, minW.Value, maxW.Value);
            foreach (var cube in newActiveCubes)
            {
                newCubeSpace.AddActiveCube(cube.X, cube.Y, cube.Z, cube.W);
            }

            return newCubeSpace;
        }
    }
}
