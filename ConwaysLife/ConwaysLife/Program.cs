using System;
using System.Collections.Generic;

namespace ConwaysLifeConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //string[] startConfiguration = {
            //                     "    *    ",
            //                     "    *    ",
            //                     "    *    ",
            //                     " *** *** ",
            //                     "    *    ",
            //                     "    *    ",
            //                     "    *    "
            //                 };

            //string[] startConfiguration = {
            //                     " ***** ***** ***** "
            //                 };

            string[] startConfiguration = {
                                 " ***** ***** ***** ",
                                 "                   ",
                                 "                   ",
                                 "                   ",
                                 "                   ",
                                 " ***** ***** ***** "
                             };

            List<Point> initialCellConfiguration = new List<Point>();

            int x, y = 0;
            foreach (string row in startConfiguration)
            {
                x = 0;
                foreach (char c in row)
                {
                    if (c == '*')
                    {
                        initialCellConfiguration.Add(new Point(x, y));
                    }

                    ++x;
                }

                ++y;
            }

            //foreach (KeyValuePair<ConwaysGameofLife.Point, ConwaysGameofLife.Cell> kvp in cells)
            //{
            //    Console.WriteLine("X: {0}, Y {1}, Count {2}", kvp.Key.X.ToString(), kvp.Key.Y.ToString(), kvp.Value.Neighbours.ToString());
            //}

            ConwaysGameofLife life = new ConwaysGameofLife(initialCellConfiguration);
            ConsoleKeyInfo keyPressed;

            int generationCount = 0;
            do
            {
                Console.Write("\nGeneration: {0}\n", (++generationCount).ToString());

                char[,] output = life.Draw();

                for (int yPos = 0; yPos < output.GetLength(1); ++yPos)
                {
                    for (int xPos = 0; xPos < output.GetLength(0); ++xPos)
                    {
                        Console.Write(output[xPos, yPos]);
                    }
                    Console.Write("\n");
                }

                life.RunGeneration();

                Console.WriteLine("\nHit Escape key to exit.\n");
                keyPressed = Console.ReadKey();
            } while (keyPressed.Key != ConsoleKey.Escape);
        }
    }

}
