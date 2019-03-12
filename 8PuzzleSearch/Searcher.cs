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

        List<Tuple<PuzzleState, int>> pqueue; //Used to store states in the search, acts as a priority queue when sorting by item2
        PuzzleState sln; //The found sln
        public PuzzleState Sln { get => sln; }
        int nodesExpanded = 0; //number of nodes expanded


        public Searcher(PuzzleState init, IHeuristic heuristic)
        {
            h = heuristic;
            pqueue = new List<Tuple<PuzzleState, int>>();
            pqueue.Add(new Tuple<PuzzleState, int>(init, h.Score(init))); // add the inital state to the priority queue
        }

        public int NodesExpanded { get => nodesExpanded; }

        public bool Solve() //Ease of access call
        {
            return Solve(pqueue[0]);
        }

        public bool Solve(Tuple<PuzzleState, int> state)
        {
            List<PuzzleState> explored = new List<PuzzleState>(); //Loop prevention
            while (pqueue.Count > 0)
            {
                state = pqueue[0]; //Get the lowest cost node
                //state.Item1.PrintState();
                if (state.Item1.getSolved()) //Solution found
                {
                    sln = state.Item1;
                    return true;
                }
                nodesExpanded++; //Expand a node
                explored.Add(state.Item1); //Loop prevention
                pqueue.Remove(state);
                foreach (var move in state.Item1.getValidMoves()) //Node expansion
                {
                    PuzzleState moved = new PuzzleState(state.Item1, move);
                    if (!explored.Contains(moved))
                    {
                        pqueue.Add(new Tuple<PuzzleState, int>(moved, h.Score(moved)+moved.Path.Count)); //add it to the queue
                    }
                }
                pqueue = pqueue.OrderBy(s => s.Item2).ToList(); //resort the queue
            }
            return false;
        }
    }
}
