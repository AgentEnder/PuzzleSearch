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
            bool[] used = { false, false, false, false, false, false, false, false, false }; //Bool flags, used[i] indicates whether i has been put in the grid already

            int[,] data = new int[3, 3];

            int cellsSet = 0; //Number of cells in the grid that are set
            //User input for grid layout
            while(cellsSet < 9)
            {
                Console.Clear();
                for (int y = 0; y < 3; y++)
                {
                    for (int x = 0; x < 3; x++)
                    {
                        if (x+3*y < cellsSet) //Cell already set, grab it an display
                        {
                            Console.Write($" {data[y, x].ToString()} ,"); 
                        }
                        else if (x+3*y == cellsSet) //Current cell to input
                        {
                            Console.Write(" _ ,");
                        }
                        else //Cell to be set in the future
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
                if (Int32.TryParse(input, out intInput) && intInput >= 0 && intInput < 9  && used[intInput] == false) //Integer between 0 and 9 was input
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
                if (Int32.TryParse(input, out intInput)) //Input was an integer
                {
                    switch (intInput)
                    {
                        case 1:
                            valid = true; // Default heuristic
                            break;
                        case 2:
                            heuristic = new IncorrectHeurisic(); //Number of tiles in incorrect space
                            valid = true;
                            break;
                        case 3:
                            heuristic = new ManhattanHeuristic(); //Sum of manhattan distance away from correct space for each tile
                            valid = true;
                            break;
                        default:
                            valid = false;
                            break;
                    }
                }
            } while (!valid);

            PuzzleState init = new PuzzleState(data); //Create a puzzle state with the given data
            init.PrintState(); //Print the inital state
            Console.WriteLine();
            Console.WriteLine();

            Searcher searcher = new Searcher(init, heuristic); //Initialise a searcher with the given state and choosen heuristic
            searcher.Solve(); //Solve the puzzle
            foreach (PuzzleState step in searcher.Sln.Path) //Print the sln.
            {
                step.PrintState();
            }
            searcher.Sln.PrintState(); //Sln path doesn't include the solution
            Console.WriteLine($"Solution found in {searcher.NodesExpanded} steps");
            Console.WriteLine($"Solution path is at depth {searcher.Sln.Path.Count}");
        }
    }
}
