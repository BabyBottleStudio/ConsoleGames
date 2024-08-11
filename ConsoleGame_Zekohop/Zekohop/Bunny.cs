using System;
using System.Collections.Generic;

namespace Zekohop
{
    class Bunny
    {
        int _id; // Prilikom instanciranja, svaki zec dobija svoj id. Uglavnom je to redni broj instanciranja.

        (int x, int y) _startPos; // coordinates of the starting position of the bunny. Those are the coordinates embeded when the level is initialized.
        (int x, int y) _currentPos; //Coordinates of the bunny during the gameplay.

        /// <summary>
        /// Amount of the bunnies in the level. Important for calculating the win condition.
        /// </summary>
        private static int bunnyCount;

        /// <summary>
        /// List of colors used to represent bunnies graphics.
        /// </summary>
        public readonly static List<ConsoleColor> bunnyInterfaceColors = new List<ConsoleColor> 
        {
            ConsoleColor.White,
            ConsoleColor.DarkGray,
            ConsoleColor.Yellow,
        };

        /// <summary>
        /// Assigned color from the list to the instance of the bunny.
        /// </summary>
        private ConsoleColor interfaceColor;

        /// <summary>
        /// How to represent the text icon on the selected object.
        /// </summary>
        private static ConsoleColor colorIfSelected;

        /// <summary>
        /// Text icon of the bunny.
        /// </summary>
        public static readonly string DisplayIcon = "B";


        /// <summary>
        /// Initializing the bunny object. It assignes the startPos, currentPos, increases the bunnyCount, sets the Id, and interfaceColor from the list of colors.
        /// </summary>
        /// <param name="startPos"></param>
        public Bunny((int x, int y) startPos)
        {
            StartPos = startPos;
            _currentPos = startPos;
            bunnyCount++;
            _id = BunnyCount;
            InterfaceColor = bunnyInterfaceColors[Id-1];

        }

        /// <summary>
        /// Coordinates of the bunny during the gameplay.
        /// </summary>
        public (int row, int col) CurrentPos { get => _currentPos; set => _currentPos = value; }
        public int Id { get => _id;}

        /// <summary>
        /// Coordinates of the starting position of the bunny. Those are the coordinates embeded when the level is initialized.
        /// </summary>
        public (int row, int col) StartPos { get => _startPos; set => _startPos = value; }

        /// <summary>
        /// Gets and sets the count of the bunnies.
        /// </summary>
        public static int BunnyCount { get => bunnyCount; set => bunnyCount = value; }

        /// <summary>
        /// Sets the color from the list to the instance of the bunny. Gets the color from the object.
        /// </summary>
        public ConsoleColor InterfaceColor { get => interfaceColor; set => interfaceColor = value; }
        
        /// <summary>
        /// Gets the color of selected bunny used in graphics.
        /// </summary>
        public static ConsoleColor ColorIfSelected { get => colorIfSelected; }

        /// <summary>
        /// Resets the bunnie count to 0.
        /// </summary>
        public static void ResetBunniesCount()
        {
            BunnyCount = 0;
        }


        /// <summary>
        /// It writes the values of the bunny to the grid. Important for calculations.
        /// </summary>
        /// <param name="theBunny"></param>
        public static void WriteBunnyIdToTheGridInitial(Bunny theBunny)
        {
            GameGrid.Grid[theBunny.StartPos.row, theBunny.StartPos.col] = theBunny.Id;
            //BunnyList.Add(theBunny);
        }
                
        /// <summary>
        /// Test if bunnie is going to jump out of the grid, providint invalid index and causing the program to crash.
        /// </summary>
        /// <returns></returns>
        private static bool IsBunnyGoingToJupmOutOfTheGrid() // da li je ovaj test neophodan ili samo mi treba bolja logika dole u funkcijijijiji. Ili ovaj test sprecava dalje proracune ako se nije ispunio
        {
            int direction = GameGrid.userInput;
            //SetSelectedAnimal();
            // Initial test if the bunny is going to jump out of the grid
            (int y, int x) = GameGrid.currentBunny.CurrentPos;

            switch (direction)
            {
                case -1:
                    if (x <= 1) // if a bunny jumps from here to the left, it will jump out of the grid; odavde ako preskoci nesto na levo, ispada iz grida
                    {
                        return false;
                    }
                    break;

                case 1:

                    if (x >= GameGrid.GridSize - 1) // if a bunny jumps from here to the right, it will jump out of the grid; odavde ako preskoci nesto na desno, ispada iz grida
                    {
                        return false;
                    }
                    break;

                case -2:

                    if (y <= 1) // if a bunny jumps up from here, it will jump out of the grid; odavde ako preskoci nesto na gore, ispada iz grida
                    {
                        return false;
                    }
                    break;

                case 2:

                    if (y >= GameGrid.GridSize - 1) //  if a bunny jumps down from here, it will jump out of the grid; odavde ako preskoci nesto na dole, ispada iz grida
                    {
                        return false;
                    }
                    break;
            }
            return true;
        }
        

        /// <summary>
        /// Tests if bunny is legit to jump over something. In grid, those values are larger than zero.
        /// </summary>
        /// <returns></returns>
        private static bool IsBunnyLegitToJump() // this method checks if the selected bunny is going to jump over the fox, mushroom or another bunny and retunrs bool
        {
            int direction = GameGrid.userInput;

            // values are inverted because they have more logic that way. 
            (int y, int x) = GameGrid.currentBunny.CurrentPos; // ovo isto napraviti kao public parametar 

            switch (direction)
            {
                case -1:
                    if (GameGrid.Grid[y, x - 1] > 0) // if it jumps to the left, will it jump over something or not
                    {
                        return true;
                    }
                    break;

                case 1:
                    if (GameGrid.Grid[y, x + 1] > 0)  // if it jumps to the right, will it jump over something or not
                    {
                        return true;
                    }
                    break;

                case -2:
                    if (GameGrid.Grid[y - 1, x] > 0)  // if it jumps upwards, will it jump over something or not
                    {
                        return true;
                    }
                    break;

                case 2:
                    if (GameGrid.Grid[y + 1, x] > 0) // if it jumps downwards, will it jump over something or not
                    {
                        return true;
                    }
                    break;
            }
            return false;
        }

        /// <summary>
        /// Returns the coordinates of the first empty cell that bunny can hop to. If that cell does not exist, returns null and jump is blocked.
        /// </summary>
        /// <returns></returns>
        private static (int a, int b)? GetPlaceToHopTo()
        {
            int direction = GameGrid.userInput;
            (int y, int x) = GameGrid.currentBunny.CurrentPos; // ovo isto napraviti kao public parametar 

            switch (direction)
            {
                case -1:
                    for (int i = x; i >= 0; i--) // trazimo mesto doskoka kroz lup i+direction
                    {
                        if (GameGrid.Grid[y, i] == 0) // trazimo polje doskoka
                        {
                            return (y, i);
                        }
                    }
                    break;

                case 1:
                    for (int i = x; i < GameGrid.GridSize; i++) // trazimo mesto doskoka kroz lup 
                    {
                        if (GameGrid.Grid[y, i] == 0) // trazimo polje doskoka
                        {
                            return (y, i);
                        }
                    }
                    break;

                case -2:
                    for (int i = y; i >= 0; i--) // trazimo mesto doskoka kroz lup 
                    {
                        if (GameGrid.Grid[i, x] == 0) // trazimo polje doskoka
                        {
                            return (i, x);
                        }
                    }
                    break;

                case 2:
                    for (int i = y; i < GameGrid.GridSize; i++) // trazimo mesto doskoka kroz lup --- sta ako ga ne nadje!!!!!!!
                    {
                        if (GameGrid.Grid[i, x] == 0) // trazimo polje doskoka
                        {
                            return (i, x);
                        }
                    }
                    break;
            }
            return null;
        }

        /// <summary>
        /// After all of the tests, if bunny stays within a grid, jumps over something and has a place to jump to, it moves.
        /// </summary>
        public static void MoveBunny()
        {
            //int direction = GameGrid.userInput;
            // direction
            // <= -1  1 =>

            // -2 ^
            //  2 v

            //int x = GameGrid.currentBunny.CurrentPos.col;
            //int y = GameGrid.currentBunny.CurrentPos.row;

            (int a, int b)? hopTo = null;

            if (IsBunnyGoingToJupmOutOfTheGrid() && IsBunnyLegitToJump())
            {

                // a da napravimo privremenu listu koju mozemo da testiramo
                // ubaci koordinate koje ispunjavaju kriterijum
                // yec je recimo na koord 1 1. Cim se selektuje, da se sakupe validna polja za kretanje
                // 

                hopTo = GetPlaceToHopTo();
            }

            if (HopToField(hopTo))
            {
                GameGrid.IncreaseMovesCount();
            }
        }

        /// <summary>
        /// Returns true if Hop to coordinates are not null and bunny performs the jump. Returns false if conditions are not met.
        /// </summary>
        /// <param name="hopTo"></param>
        /// <returns></returns>
        static bool HopToField((int a, int b)? hopTo)
        {
            if (hopTo.HasValue)
            {
                GameGrid.Grid[GameGrid.currentBunny.CurrentPos.row, GameGrid.currentBunny.CurrentPos.col] = 0; // Erasing the old Rabbit position from the grid
                GameGrid.currentBunny.CurrentPos = (hopTo.Value.a, hopTo.Value.b); // write in the new position into the rabit instance
                GameGrid.Grid[GameGrid.currentBunny.CurrentPos.row, GameGrid.currentBunny.CurrentPos.col] = GameGrid.currentBunny.Id; // write in the new position into the grid

                return true;
            }
            return false;
        }
    }
}
