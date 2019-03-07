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
            PuzzleState init = new PuzzleState(new int[3, 3]
            {
                { 2, 8, 1 },
                { 4, 6, 3},
                { 0, 7, 5}
            });

            
            Searcher searcher = new Searcher(init, new IncorrectHeurisic());
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
