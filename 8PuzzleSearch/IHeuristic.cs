using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8PuzzleSearch
{
    interface IHeuristic
    {
        int Score(PuzzleState state);
    }
}
