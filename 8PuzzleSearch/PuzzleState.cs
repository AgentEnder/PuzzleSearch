using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8PuzzleSearch
{
    class PuzzleState : IEquatable<PuzzleState>
    {
        private int[,] data = new int[3, 3];
        private int hole_x = 1;
        private int hole_y = 1;

        private List<PuzzleState> path = new List<PuzzleState>();
        public List<PuzzleState> Path { get => path; }

        public static int[,] GoalState = { { 1,2,3 },
                                           { 8,0,4 },
                                           { 7,6,5 } };

        public int[,] Data { get => data;  }
        

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
                    if (num == 0)
                    {
                        hole_x = i;
                        hole_y = j;
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
            path = new List<PuzzleState>(old.path);
            path.Add(old);
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
                    data[old.hole_x, hole_y] = old.data[hole_x, hole_y];
                    break;
                case MOVE.RIGHT:
                    hole_x = old.hole_x - 1;  //hole moves left
                    hole_y = old.hole_y;
                    data[old.hole_x, hole_y] = old.data[hole_x, hole_y];
                    break;
                default:
                    break;
            }
            data[hole_x, hole_y] = 0;
        }

        public List<MOVE> getValidMoves()
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

        public bool getSolved()
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

        public void PrintState()
        {
            Console.WriteLine("Puzzle State");
            Console.WriteLine($"Hole at {hole_x},{hole_y}");
            Console.WriteLine($"{data[0, 0]},{data[0, 1]},{data[0, 2]}");
            Console.WriteLine($"{data[1, 0]},{data[1, 1]},{data[1, 2]}");
            Console.WriteLine($"{data[2, 0]},{data[2, 1]},{data[2, 2]}");
        }

        public bool Equals(PuzzleState other)
        {
            return  data.Rank == other.data.Rank &&
                    Enumerable.Range(0, data.Rank).All(dimension => data.GetLength(dimension) == other.data.GetLength(dimension)) &&
                    data.Cast<int>().SequenceEqual(other.data.Cast<int>());
        }
    }
}
