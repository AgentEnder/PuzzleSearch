using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8PuzzleSearch
{
    class IncorrectHeurisic : IHeuristic
    {
        public int Score(PuzzleState state)
        {
            int s = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (state.Data[i,j] != PuzzleState.GoalState[i,j]) //Item is in wrong spot
                    {
                        s += 1;
                    }
                }
            }
            return s;
        }
    }
}
