using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8PuzzleSearch
{
    class Searcher
    {
        IHeuristic h;

        List<Tuple<PuzzleState, int>> pqueue;
        int nodesExpanded = 0;


        public Searcher(PuzzleState init, IHeuristic heuristic)
        {
            h = heuristic;
            pqueue = new List<Tuple<PuzzleState, int>>();
            pqueue.Add(new Tuple<PuzzleState, int>(init, h.Score(init)));
        }

        public int NodesExpanded { get => nodesExpanded; }

        public bool Solve()
        {
            return Solve(pqueue[0]);
        }

        public bool Solve(Tuple<PuzzleState, int> state)
        {
            List<PuzzleState> explored = new List<PuzzleState>();
            while (pqueue.Count > 0)
            {
                state = pqueue[0];
                state.Item1.PrintState();
                if (state.Item1.getSolved())
                {
                    return true;
                }
                nodesExpanded++;
                explored.Add(state.Item1);
                pqueue.Remove(state);
                foreach (var move in state.Item1.getValidMoves())
                {
                    PuzzleState moved = new PuzzleState(state.Item1, move);
                    if (!explored.Contains(moved))
                    {
                        pqueue.Add(new Tuple<PuzzleState, int>(moved, h.Score(moved)));
                    }
                }
                pqueue = pqueue.OrderBy(s => s.Item2).ToList();
            }
            return false;
        }
    }
}
