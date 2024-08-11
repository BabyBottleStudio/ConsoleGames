using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zekohop
{
    class GameGrid
    {
        private const int gridSize = 5; // defines the size of the grid. Odd numbers recomended
        static int[,] _grid; // game grid 5x5

        private static int movesCount = 0; // how many moves has player used to solve the puzzle. Updated in realtime.

        /// <summary>
        /// Hole list is a flexibile. It contains the coordinates of all possible holes in the game. If mushroom is spawned ower the hole, then the coordinates for this hole are deleted.
        /// </summary>
        static List<(int row, int col)> holeList;

        public static readonly string HoleIconLeftHalf = "(";
        public static readonly string HoleIconRightHalf = ")";

        public static readonly ConsoleColor HoleColorIfEmpty = ConsoleColor.DarkGreen;
        public static readonly ConsoleColor HoleColorIfFull = ConsoleColor.Green;

        public static int selectedAnimal; // int 1 - 5. (1-3) bunnies , (4,5) fox
        public static Bunny currentBunny; // da li da se ovo prebaci u klasu?
        public static Fox currentFox;

        public static int userInput;

        /// <summary>
        /// Gets the grid size.
        /// </summary>
        public static int GridSize => gridSize;

        /// <summary>
        /// Returns the list of coordinates that are marked as a "hole" element of a game.
        /// </summary>
        public static List<(int row, int col)> HoleList { get => holeList; set => holeList = value; }

        /// <summary>
        /// Gets and sets the 2-dimensional array that is is the Game Grid.
        /// </summary>
        public static int[,] Grid { get => _grid; set => _grid = value; }
        
        /// <summary>
        /// Count of moves performed by player.
        /// </summary>
        public static int MovesCount { get => movesCount; set => movesCount = value; }


        /// <summary>
        /// Initializing the grid with the "hole" elements.
        /// </summary>
        public GameGrid()
        {
            Grid = new int[GridSize, GridSize];
            Grid.Initialize();
            HoleList = new List<(int row, int col)> { (0, 0), (0, GridSize-1), ((GridSize - 1)/2, (GridSize - 1) / 2), ((GridSize - 1), 0), ((GridSize - 1), (GridSize - 1)) };
        }


        /// <summary>
        /// Increases the movesCount by 1.
        /// </summary>
        public static void IncreaseMovesCount()
        {
            MovesCount++;
        }

        /// <summary>
        /// Sets the selection of an animal. It can be rabbit 1 - 3 or Fox 4 and 5.
        /// </summary>
        public static void SetSelectedAnimal()
        {
            if (selectedAnimal > 3)
            {
                currentFox = Level.FoxList[selectedAnimal - 1 - 3];
            }
            else if (selectedAnimal > 0 && selectedAnimal < 4)
            {
                currentBunny = Level.BunnyList[selectedAnimal - 1];
            }
        }

        /// <summary>
        /// Moves the selected animal. Dah.
        /// </summary>
        public static void MoveSelectedAnimal()
        {
            if (selectedAnimal > 3)
            {
                // mrdas lisice
                Fox.MoveFox();
            }
            else if (selectedAnimal > 0 && selectedAnimal < 4)
            {
                Bunny.MoveBunny();
                //mrdas zeceve
            }
        }

        /// <summary>
        /// Checks if there is a win condition. It counts the bunnies and checks if their coordinates are matching the coordinates of the holes.
        /// </summary>
        /// <returns></returns>
        public static bool IsThereAWin()
        {
            int count = Level.BunnyList.Count(bunny => HoleList.Contains(bunny.CurrentPos));
            return count == Bunny.BunnyCount;


/*
            var count = 0;
            for (int i = 0; i < Level.BunnyList.Count; i++)
            {
                if (HoleList.Contains(Level.BunnyList[i].CurrentPos))
                {
                    count++;
                }
            }

            if (count == Bunny.BunnyCount)
            {
                return true;
            }
            return false;
*/
        }

        /// <summary>
        /// At the start of each level, the count of Bunnies, Foxes and Moves are reset to 0.
        /// </summary>
        public static void ResetBunniesFoxesCount()
        {
            Fox.ResetFoxCount();
            Bunny.ResetBunniesCount();
            MovesCount = 0;
        }
    }
}
