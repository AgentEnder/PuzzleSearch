using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8PuzzleSearch
{
    class BFSHeuristic : IHeuristic
    {
        public int Score(PuzzleState state) //Depth is solely based on the depth, return 0
        {
            return 0;
        }
    }
}
