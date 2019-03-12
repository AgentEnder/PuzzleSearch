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
            Tuple<int, int>[] correct = new Tuple<int, int>[9]; //Array represents the correct x,y coordinates of the index. correct[i] gives the coordinates of i in the goal.
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++) //Loop through the goal state
                {
                    correct[PuzzleState.GoalState[i, j]] = new Tuple<int, int>(i, j); //Store the correct coordinates
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int num = state.Data[i, j]; //num is the idx of the int in correct
                    s += (Math.Abs(correct[num].Item1 - i) + Math.Abs(correct[num].Item2 - j)); //subtract the x and y coords.
                }
            }
            return s;
        }
    }
}
