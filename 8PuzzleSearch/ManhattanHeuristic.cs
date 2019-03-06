using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8PuzzleSearch
{
    class ManhattanHeuristic : IHeuristic
    {
        public int Score(PuzzleState state)
        {
            int s = 0;
            Tuple<int, int>[] correct = new Tuple<int, int>[9];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    correct[PuzzleState.GoalState[i, j]] = new Tuple<int, int>(i, j);
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int num = state.Data[i, j];
                    s += (Math.Abs(correct[num].Item1 - i) + Math.Abs(correct[num].Item2 - j));
                }
            }
            return s;
        }
    }
}
