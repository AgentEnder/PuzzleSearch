using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8PuzzleSearch
{
    interface IHeuristic //Use an interface since a heuristic is only a function, allows for polymorphic code
    {
        int Score(PuzzleState state); //returns score of current puzzlestate
    }
}
