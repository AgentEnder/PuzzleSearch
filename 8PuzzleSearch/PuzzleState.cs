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

        public static int[,] GoalState = { { 1,2,3 }, //Hardcoded goal state
                                           { 8,0,4 },
                                           { 7,6,5 } };

        public int[,] Data { get => data;  }
        

        public enum MOVE { UP, DOWN, LEFT, RIGHT };

        public PuzzleState(int[,] vs) //Constructor taking a data obj
        {
            if (vs.Length != 9) //Wrong number of elements in array
            {
                throw new ArgumentException("Puzzle only holds 9 values");
            }
            bool[] containsFlags = { false, false, false, false, false, false, false, false, false }; //flags that represent if idx has been used, arr[i] indicates if i is already in the puzzle
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int num = data[i, j] = vs[i, j]; //Assign the current number to data
                    if (num < 0 || num > 8) //Its out of range
                    {
                        throw new ArgumentException("Each element must be between 0 and 8 (inclusive).");
                    }
                    if (containsFlags[num]) //Its already used
                    {
                        throw new ArgumentException("Each value must be used only once!");
                    }
                    if (num == 0) //Its the hole
                    {
                        hole_x = i;
                        hole_y = j;
                    }
                    containsFlags[num] = true;
                }
            }
        }

        public PuzzleState(PuzzleState old) //Copy constructor
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    data[i, j] = old.data[i, j];
                }
            }
            hole_x = old.hole_x;
            hole_y = old.hole_y;
        }

        public PuzzleState(PuzzleState old, MOVE move) //Construct from old puzzle, moving a tile by move.
        {
            path = new List<PuzzleState>(old.path); //Deep copy the old path
            path.Add(old); //Add the old node to it
            for (int i = 0; i < 3; i++) //Copy the data point by point
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
                    data[hole_x, old.hole_y] = old.data[hole_x, hole_y]; //Perform the swap between hole and tile
                    break;
                case MOVE.DOWN:
                    hole_x = old.hole_x;
                    hole_y = old.hole_y - 1; //hole moves up
                    data[hole_x, old.hole_y] = old.data[hole_x, hole_y]; //Perform the swap between hole and tile
                    break;
                case MOVE.LEFT:
                    hole_x = old.hole_x + 1;  //hole moves right
                    hole_y = old.hole_y;
                    data[old.hole_x, hole_y] = old.data[hole_x, hole_y]; //Perform the swap between hole and tile
                    break;
                case MOVE.RIGHT:
                    hole_x = old.hole_x - 1;  //hole moves left
                    hole_y = old.hole_y;
                    data[old.hole_x, hole_y] = old.data[hole_x, hole_y]; //Perform the swap between hole and tile
                    break;
                default: //Should never happen, cases are complete.
                    break;
            }
            data[hole_x, hole_y] = 0;
        }

        public List<MOVE> getValidMoves() //Return valid moves based on hole location
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

        public bool getSolved() //Check if data matches the goal solution
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (data[i, j] != GoalState[i,j])
                    {
                        return false; //Early exit for efficiency.
                    }
                }
            }
            return true;
        }

        public void PrintState() //Log the state to the console.
        {
            Console.WriteLine("Puzzle State");
            Console.WriteLine($"Hole at {hole_x},{hole_y}");
            Console.WriteLine($"{data[0, 0]},{data[0, 1]},{data[0, 2]}");
            Console.WriteLine($"{data[1, 0]},{data[1, 1]},{data[1, 2]}");
            Console.WriteLine($"{data[2, 0]},{data[2, 1]},{data[2, 2]}");
        }

        public bool Equals(PuzzleState other) //Overload the equality operator
        {
            return  data.Rank == other.data.Rank &&
                    Enumerable.Range(0, data.Rank).All(dimension => data.GetLength(dimension) == other.data.GetLength(dimension)) &&
                    data.Cast<int>().SequenceEqual(other.data.Cast<int>());
        }
    }
}
