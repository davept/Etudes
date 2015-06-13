using System;
using System.Collections.Generic;
using System.Linq;

namespace ConwaysLifeConsole
{
    public class ConwaysGameofLife
    {
        private class Cell
        {
            private int neighbours;

            public int Neighbours
            {
                get
                {
                    return neighbours;
                }
            }

            public Cell()
            {
                this.neighbours = 0;
            }

            public Cell(int neighbours)
            {
                this.neighbours = neighbours;
            }

            public void IncreaseNeighbourCount()
            {
                this.neighbours++;
            }
        }

        private Dictionary<Point, Cell> potentialBirths = new Dictionary<Point, Cell>();
        private Dictionary<Point, Cell> livingCells = new Dictionary<Point, Cell>();

        public ConwaysGameofLife(List<Point> cells)
        {
            this.livingCells.Clear();
            foreach (Point p in cells)
            {
                livingCells.Add(new Point(p.X, p.Y), new Cell());
            }
        }

        public void RunGeneration()
        {
            foreach (KeyValuePair<Point, Cell> cell in livingCells)
            {
                Point[] cellsNeighbourhood = GetNeighbourhood(cell.Key);

                foreach (Point neighbour in cellsNeighbourhood)
                {
                    if (potentialBirths.ContainsKey(neighbour))
                    {
                        potentialBirths[neighbour].IncreaseNeighbourCount();
                    }
                    else if (livingCells.ContainsKey(neighbour))
                    {
                        livingCells[neighbour].IncreaseNeighbourCount();
                    }
                    else
                    {
                        potentialBirths.Add(neighbour, new Cell(1));
                    }
                }
            }

            IEnumerable<KeyValuePair<Point, Cell>> newLife =
                livingCells.Where(cell => cell.Value.Neighbours == 2 || cell.Value.Neighbours == 3)
                .Concat(
                potentialBirths.Where(cell => cell.Value.Neighbours == 3));

            livingCells = newLife.ToDictionary(x => x.Key, x => new Cell());

            potentialBirths.Clear();
        }

        public char[,] Draw()
        {
            Tuple<Point, Point> bounds = GetBounds();

            int xRange = bounds.Item2.X - bounds.Item1.X,
                yRange = bounds.Item2.Y - bounds.Item1.Y;

            char[,] image = new char[xRange + 1, yRange + 1];

            foreach (KeyValuePair<Point, Cell> cell in livingCells)
            {
                image[cell.Key.X - bounds.Item1.X, cell.Key.Y - bounds.Item1.Y] = '*';
            }

            return image;
        }

        private Point[] GetNeighbourhood(Point p)
        {
            return new Point[]
            {
                new Point(p.X - 1, p.Y - 1),
                new Point(p.X, p.Y - 1),
                new Point(p.X + 1, p.Y - 1),
                new Point(p.X - 1, p.Y),
                new Point(p.X + 1, p.Y),
                new Point(p.X - 1, p.Y + 1),
                new Point(p.X, p.Y + 1),
                new Point(p.X + 1, p.Y + 1)
            };
        }

        private Tuple<Point, Point> GetBounds()
        {
            int minX = int.MaxValue,
                minY = int.MaxValue,
                maxX = int.MinValue,
                maxY = int.MinValue;

            foreach (KeyValuePair<Point, Cell> cell in livingCells)
            {
                minX = Math.Min(minX, cell.Key.X);
                maxX = Math.Max(maxX, cell.Key.X);
                minY = Math.Min(minY, cell.Key.Y);
                maxY = Math.Max(maxY, cell.Key.Y);
            }

            return new Tuple<Point, Point>(
                new Point(minX, minY),
                new Point(maxX, maxY));
        }
    }
}
