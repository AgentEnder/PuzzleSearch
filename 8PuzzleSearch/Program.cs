using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8PuzzleSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            bool[] used = { false, false, false, false, false, false, false, false, false };

            int[,] data = new int[3, 3];

            int cellsSet = 0;
            //User input for grid layout
            while(cellsSet < 9)
            {
                Console.Clear();
                for (int y = 0; y < 3; y++)
                {
                    for (int x = 0; x < 3; x++)
                    {
                        if (x+3*y < cellsSet)
                        {
                            Console.Write($" {data[y, x].ToString()} ,");
                        }
                        else if (x+3*y == cellsSet)
                        {
                            Console.Write(" _ ,");
                        }
                        else
                        {
                            Console.Write(" ! ,");
                        }
                    }
                    Console.WriteLine();
                }

                Console.WriteLine();
                Console.Write($"Enter cell {cellsSet}: ");
                string input = Console.ReadLine();
                int intInput;
                if (Int32.TryParse(input, out intInput) && intInput >= 0 && intInput < 9  && used[intInput] == false)
                {
                    int y = cellsSet % 3;
                    int x = cellsSet / 3;
                    data[x, y] = intInput;
                    used[intInput] = true;
                    cellsSet++;
                }
            }

            //User input for heuristic used
            IHeuristic heuristic = new BFSHeuristic(); //Default
            bool valid = true;
            do
            {
                Console.Clear();
                Console.WriteLine("Which heurisic would you like to use?");
                Console.WriteLine("(1) h(n) = 0");
                Console.WriteLine("(2) h(n) = Number of Incorrect Tiles");
                Console.WriteLine("(3) h(n) = Sum of Manhattan Distances from Correct Space");
                string input = Console.ReadLine();
                int intInput;
                if (Int32.TryParse(input, out intInput))
                {
                    switch (intInput)
                    {
                        case 1:
                            valid = true; // Default heuristic
                            break;
                        case 2:
                            heuristic = new IncorrectHeurisic();
                            valid = true;
                            break;
                        case 3:
                            heuristic = new ManhattanHeuristic();
                            valid = true;
                            break;
                        default:
                            valid = false;
                            break;
                    }
                }
            } while (!valid);

            PuzzleState init = new PuzzleState(data);
            init.PrintState();
            Console.WriteLine();
            Console.WriteLine();

            Searcher searcher = new Searcher(init, heuristic);
            searcher.Solve();
            foreach (PuzzleState step in searcher.Sln.Path)
            {
                step.PrintState();
            }
            searcher.Sln.PrintState();
            Console.WriteLine($"Solution found in {searcher.NodesExpanded} steps");
            Console.WriteLine($"Solution path is at depth {searcher.Sln.Path.Count + 1}");
        }
    }
}
