# 8 Tiles Puzzle

## Algorithm Design and Data Storage

For this project, I choose to use C# due to familiarity. C++ would be faster, but the algorithm implementation wouldve been more verbose.


### Heuristics
This project involved the use of best first search with 3 heuristics:
	H0: 0 (This is effectively breadth first search)
	h1: The number of tiles in the incorrect spot
	h2: The sum of the manhattan distance from the correct spot for each tile.
	
h0 and h1 are self explanatory, but h2 might not be.
The manhattan distance of a tile is defined as: |X_correct-X_current|+|Y_correct-Y_current|. h2 returns the sum of this for each tile in the puzzle.

These heuristics are implemented as a class of one function (Score(PuzzleState)) and that class implements the IHeuristic interface. Using an interface allows the code to reference IHeuristic rather than any of the 3 specific heuristics, and increases polymorphism.

### Search Algorithm
The implementation of Best First Search is achieved in the Searcher class. It uses a priority queue to look for what the current best option would be. The algorithm works by looking at the next best move, and if its not the solved puzzle adding its moves (and their difficulty) into the priority queue. If the priority queue is empty the puzzle is unsolvable. If the state is solved, the algorithm is done.

The Searcher class also contains a reference to the solved puzzle state and the heuristic it is using.


### Puzzle Data Storage
My puzzle states are stored in a PuzzleState class. Within this class, the data is stored in a 3x3 2D array. The class contains methods to initialize a state based on data (a 3x3 int array), another PuzzleState (a copy constructor), and based on a specific move in the puzzle. This allows for deep copys of a state after the move has been made easily.

The PuzzleState also contains a List<PuzzleState> called path. This is empty by default, but is used to track the path the Searcher took to get to the current state.

### Priority Queue
While C# contains definitions for List<t>, Queue<t>, and Stack<t>, a priority queue is not in the standard library. To overcome this, code uses a List<tuple<PuzzleState, int>>. While this is not the cleanest solution, it works by sorting the list based on the integer value in the tuple.

### Other Data Structures
Other than these objects, the code uses a bool array to act as a hash table when checking if a value has been used more than once.