using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8PuzzleSearch
{
    class PuzzleState
    {
        private int[,] data = new int[3, 3];
        private int hole_x = 1;
        private int hole_y = 1;

        public static int[,] GoalState = { { 1,2,3 },
                                           { 8,0,4 },
                                           { 7,6,5 } };

        public enum MOVE { UP, DOWN, LEFT, RIGHT };

        public PuzzleState(int[,] vs)
        {
            if (vs.Length != 9)
            {
                throw new ArgumentException("Puzzle only holds 9 values");
            }
            bool[] containsFlags = { false, false, false, false, false, false, false, false, false };
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int num = data[i, j] = vs[i, j];
                    if (num < 0 || num > 8)
                    {
                        throw new ArgumentException("Each element must be between 0 and 8 (inclusive).");
                    }
                    if (containsFlags[num])
                    {
                        throw new ArgumentException("Each value must be used only once!");
                    }
                    containsFlags[num] = true;
                }
            }
        }

        public PuzzleState(PuzzleState old)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    data[i, j] = old.data[i, j];
                }
            }
        }

        public PuzzleState(PuzzleState old, MOVE move)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    data[i, j] = old.data[i, j];
                }
            }
            switch (move)
            {
                case MOVE.UP:
                    hole_x = old.hole_x;
                    hole_y = old.hole_y + 1; //hole moves down
                    data[hole_x, old.hole_y] = old.data[hole_x, hole_y];
                    break;
                case MOVE.DOWN:
                    hole_x = old.hole_x;
                    hole_y = old.hole_y - 1; //hole moves up
                    data[hole_x, old.hole_y] = old.data[hole_x, hole_y];
                    break;
                case MOVE.LEFT:
                    hole_x = old.hole_x + 1;  //hole moves right
                    hole_y = old.hole_y;
                    data[hole_x, old.hole_y] = old.data[hole_x, hole_y];
                    break;
                case MOVE.RIGHT:
                    hole_x = old.hole_x - 1;  //hole moves left
                    hole_y = old.hole_y;
                    data[hole_x, old.hole_y] = old.data[hole_x, hole_y];
                    break;
                default:
                    break;
            }
        }

        List<MOVE> getValidMoves()
        {
            List<MOVE> moves = new List<MOVE>();

            if (hole_x != 0)
                moves.Add(MOVE.RIGHT);
            if (hole_x != 2)
                moves.Add(MOVE.LEFT);
            if (hole_y != 0)
                moves.Add(MOVE.DOWN);
            if (hole_y != 2)
                moves.Add(MOVE.UP);

            return moves;
        }

        bool getSolved()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (data[i, j] != GoalState[i,j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

    }
}
