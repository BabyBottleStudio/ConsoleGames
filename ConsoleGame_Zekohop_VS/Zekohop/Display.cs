using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zekohop
{
    public static class Display
    {


        /// <summary>
        /// Draws the header of the game. It contains the info about the current level. Difficulty, current level index and least amount of moves to solve the puzzle.
        /// </summary>
        public static void GameHeader()
        {
            if (Level.LevelIndex < 13)
            {
                WriteInColor("+--- - Starter - ---+", ConsoleColor.DarkGreen);
                //Console.WriteLine("Starter");
            }
            else if (Level.LevelIndex < 25)
            {
                WriteInColor("+--- - Junior  - ---+", ConsoleColor.DarkYellow);
                //Console.WriteLine("Junior");
            }
            else if (Level.LevelIndex < 37)
            {

                WriteInColor("+--- - Expert  - ---+", ConsoleColor.DarkRed);
                //Console.WriteLine("Expert");
            }
            else if (Level.LevelIndex < 49)
            {
                WriteInColor("+--- - Master  - ---+", ConsoleColor.DarkMagenta);
                //Console.WriteLine("Master");
            }
            else
            {
                WriteInColor("+--- - Wizard  - ---+", ConsoleColor.DarkBlue);
                //Console.WriteLine("Wizard");
            }
            Console.WriteLine($"  Level {Level.LevelIndex}.    >>{Level.MinimalNumberOfMoves}.");
            Console.WriteLine();
        }

        /// <summary>
        /// Menu that is displayed when the player wins the level.
        /// </summary>
        public static void LevelWin()
        {
            Console.WriteLine();
            Console.WriteLine($"> >  L E V E L  - {Level.LevelIndex} -  C L E A R  < < ");
            WriteInColor($"        Minimum moves needed: {Level.MinimalNumberOfMoves}.", ConsoleColor.DarkGray);
            WriteInColor($"             Moves used: {GameGrid.MovesCount}.", ConsoleColor.DarkGray);
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine($"Press any key for the LEVEL - {Level.LevelIndex + 1} -");
            Console.ReadKey();
        }


        /// <summary>
        /// Responsive menu below the game screen. It informs the player about the current selection.
        /// </summary>
        public static void GameMenu() // treba ubaciti poruku kad korisnik pritisne pogresan broj
        {
            Console.WriteLine();
            Console.WriteLine("Press 'H' for Help.");
            Console.WriteLine("Use indicated [] number keys to select.");
            Console.WriteLine(" - Bunnies - ");


            for (int i = 0; i < Bunny.BunnyCount; i++)
            {
                if (i == GameGrid.selectedAnimal - 1)
                {
                    WriteInColor($"[{ i + 1}] - > ", ConsoleColor.DarkGreen, false);
                    DrawSelectedBunny(i, "");
                }
                else
                {
                    Console.Write($"[{ i + 1}] - ");
                    DrawBunny(i, "");
                }
                //WriteInColor("B", Level.BunnyList[i].InterfaceColor, false);
                Console.WriteLine();
            }

            if (Fox.FoxCount > 0)
            {
                Console.WriteLine(" - - Foxes - - ");
                for (int i = 0; i < Fox.FoxCount; i++)
                {
                    if (i + 4 == GameGrid.selectedAnimal)
                    {
                        WriteInColor($"[{ i + 4}] - > ", ConsoleColor.DarkGreen, false);
                        DrawSelectedFox(i);
                    }
                    else
                    {
                        Console.Write($"[{i + 4}] - ");
                        DrawFox(i);
                    }

                    //WriteInColor($"{Level.FoxList[i].DisplayIcon}", Level.FoxList[i].InterfaceColor, false);
                    Console.WriteLine();
                }

            }
            Console.WriteLine("Use ← ↑ ↓ → to move!");
            Console.WriteLine();
        }

        /// <summary>
        /// Writes the text in color. It uses Console.WriteLine() by default. If optional bool "false", method uses the Console.Write() command.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="color"></param>
        /// <param name="writeLine"></param>
        private static void WriteInColor(string text, ConsoleColor color, bool writeLine = true)
        {
            Console.ForegroundColor = color;
            if (writeLine)
            {
                Console.WriteLine(text);
            }
            else
            {
                Console.Write(text);
            }

            Console.ResetColor();
        }

        /// <summary>
        /// This method returns the bool if the current field is a "Hole". Parameters are the coordinates of the current field. Those will be compared to the Hole List. Returns true if coordinates are match.
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <returns></returns>        
        private static bool IsFieldAHole(int rowIndex, int columnIndex)
        {
            // ova metoda treba da vrati informaciju da li je polje rupa ili ne
            var hole = (rowIndex, columnIndex);
            return GameGrid.HoleList.Contains(hole);
        }

        /// <summary>
        /// Returns the index of a Bunny that is in the current hole. Info important for correct drawing on the screen.
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        private static int GetBunnyInTheHoleIndex(int rowIndex, int columnIndex)
        {
            var hole = (rowIndex, columnIndex);

            for (int k = 0; k < Level.BunnyList.Count; k++)
            {
                if (Level.BunnyList[k].CurrentPos == hole)
                {
                    return Level.BunnyList[k].Id;
                }
            }
            return 0;
        }


        /// <summary>
        /// Draws the one cell of a grid. Parameters define the coordinates of the cell that is drawn.
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        private static void DrawTheSingleCell(int rowIndex, int columnIndex)
        {
            string gridFieldToDrawPref = " ";
            string gridFieldToDrawSufx = "";

            if (columnIndex == 0) // starting column; ukoliko je pocetna kolona
            {
                gridFieldToDrawPref = "|";

            }
            else if (columnIndex == GameGrid.GridSize - 1) // end column; ukoliko je krajnja
            {
                gridFieldToDrawSufx = "|";

                if (rowIndex % 2 == 0) // even rows; ako je red paran
                {
                    gridFieldToDrawPref = "|";
                }
            }
            else
            {
                if (rowIndex % 2 == 0)
                {
                    gridFieldToDrawPref = "|";
                }
            }


            var isBunnyIn = GetBunnyInTheHoleIndex(rowIndex, columnIndex) != 0;
            var isAHole = IsFieldAHole(rowIndex, columnIndex);

            Console.Write($"{gridFieldToDrawPref}");

            if (isAHole && isBunnyIn) // if the cell is a hole with the rabbit in it
            {
                WriteInColor($"{GameGrid.HoleIconLeftHalf}", GameGrid.HoleColorIfFull, false);
                DrawBunny(GameGrid.Grid[rowIndex, columnIndex] - 1, "");
                WriteInColor($"{GameGrid.HoleIconRightHalf}", GameGrid.HoleColorIfFull, false);
            }
            else if (isAHole) // empty hole cell
            {
                WriteInColor($" {GameGrid.HoleIconLeftHalf}{GameGrid.HoleIconRightHalf}", ConsoleColor.DarkGreen, false);
            }
            else
            {
                switch (GameGrid.Grid[rowIndex, columnIndex])
                {
                    case 0:
                        Console.Write("   "); // empty cell
                        break;

                    case 1:
                    case 2:
                    case 3:
                        DrawBunny(GameGrid.Grid[rowIndex, columnIndex] - 1, " ");
                        break;

                    case 4:
                    case 5:
                        DrawFox(GameGrid.Grid[rowIndex, columnIndex] - 4); // 
                        break;

                    case 9:
                        WriteInColor($" {Mushroom.Icon} ", Mushroom.IconColor, false);
                        break;
                }
            }
            Console.Write($"{gridFieldToDrawSufx}");
        }


        /// <summary>
        /// Draws the top and bottom border of the game grid.
        /// </summary>
        private static void DrawTopBottomBorder()
        {
            for (int i = 0; i < GameGrid.GridSize; i++)
            {
                Console.Write("+---");
            }
            Console.Write("+");
            Console.WriteLine();
        }


        /// <summary>
        /// Draws the lines of the grid
        /// </summary>
        private static void DrawMiddleLines()
        {
            for (int i = 0; i < GameGrid.GridSize; i++)
            {
                if (i % 2 == 0)
                {
                    Console.Write("+---+");
                }
                else
                {
                    WriteInColor(" - ", ConsoleColor.DarkGray, false);
                }

            }
            //Console.Write("+");
            Console.WriteLine();
        }


        /// <summary>
        /// Final method that draws the grid completely.
        /// </summary>
        public static void GridAdvanced()
        {
            GameHeader();

            for (int i = 0; i < GameGrid.GridSize; i++)
            {

                if (i == 0)
                {
                    DrawTopBottomBorder();
                }
                else
                {
                    DrawMiddleLines();
                }

                for (int j = 0; j < GameGrid.GridSize; j++)
                {
                    DrawTheSingleCell(i, j);
                }
                Console.WriteLine();

                if (i == GameGrid.GridSize - 1)
                {
                    DrawTopBottomBorder();
                }
            }
            NumberOfMoves();
        }

        /// <summary>
        /// Draws the bunny with the active seletction.
        /// </summary>
        /// <param name="colorIndex"></param>
        /// <param name="prefSufix"></param>
        private static void DrawSelectedBunny(int colorIndex, string prefSufix)
        {
            Console.Write($"{prefSufix}");
            Console.BackgroundColor = Level.BunnyList[colorIndex].InterfaceColor;
            Console.ForegroundColor = Bunny.ColorIfSelected; // Level.BunnyList[colorIndex].InterfaceColor;

            Console.Write($"{Bunny.DisplayIcon}");
            Console.ResetColor();
            Console.Write($"{prefSufix}");
        }


        private static void DrawBunny(int colorIndex, string prefSufix)
        {
            if (Level.BunnyList[colorIndex].Id == GameGrid.selectedAnimal)
            {

                DrawSelectedBunny(colorIndex, prefSufix);
            }
            else
            {
                WriteInColor($"{prefSufix}{Bunny.DisplayIcon}{prefSufix}", Level.BunnyList[colorIndex].InterfaceColor, false);
            }
            //WriteInColor($"{prefSufix}{Bunny.DisplayIcon}{prefSufix}", Level.BunnyList[colorIndex].InterfaceColor, false);
            // WriteInColor($"{prefSufix}{Level.BunnyList[colorIndex].DisplayIcon}{prefSufix}", Level.BunnyList[colorIndex].InterfaceColor, false);
        }

        private static void DrawSelectedFox(int colorIndex)
        {
            var prefSufix = " ";
            Console.Write($"{prefSufix}");
            Console.BackgroundColor = Level.FoxList[colorIndex].InterfaceColor;
            Console.ForegroundColor = Fox.ColorIfSelected;  //Level.FoxList[colorIndex].InterfaceColor; //  //

            Console.Write($"{Level.FoxList[colorIndex].DisplayIcon}");
            Console.ResetColor();
            Console.Write($"{prefSufix}");
        }

        private static void DrawFox(int colorIndex)
        {

            if (Level.FoxList[colorIndex].Id == GameGrid.selectedAnimal)
            {
                DrawSelectedFox(colorIndex);
            }
            else
            {
                WriteInColor($" {Level.FoxList[colorIndex].DisplayIcon} ", Level.FoxList[colorIndex].InterfaceColor, false);
            }
            //Console.BackgroundColor = Level.FoxList[colorIndex].InterfaceColor;
            //Console.ForegroundColor = ConsoleColor.White;
            //Console.Write($" {Level.FoxList[colorIndex].DisplayIcon} ");

            //Console.ResetColor();

        }

        public static void NumberOfMoves()
        {
            Console.WriteLine($"Number of moves {GameGrid.MovesCount}");
        }

        public static (int x, int y) HandleWindowSize()
        {
            var x = Console.WindowWidth;
            var y = Console.WindowHeight;

            Console.SetWindowSize(120, 60);

            return (x, y);
        }

        public static void HelpMenu()
        {

            (int x, int y) = HandleWindowSize();


            WriteInColor("GAME RULES JUMP IN’", ConsoleColor.Yellow);
            Console.WriteLine();
            Console.Write($"The object of the game is to move the rabbits [ "); //{Bunny.DisplayIcon}] and foxes [{Fox.DisplayIconsList[0]}{Fox.DisplayIconsList[0]}] around the game board until all of the rabbits are safe in the holes ");

            for (int i = 0; i<Bunny.bunnyInterfaceColors.Count; i++)
            {
                WriteInColor($"{Bunny.DisplayIcon} ", Bunny.bunnyInterfaceColors[i], false);
            }
            Console.Write("], \nand foxes [ ");

            for (int i = 0; i < Fox.InterfaceColors.Count; i++)
            {
                for (int j = 0; j < Fox.DisplayIconsList.Count; j++)
                {
                    Console.Write("- ");
                    WriteInColor($"{Fox.DisplayIconsList[j]} {Fox.DisplayIconsList[j]} ", Fox.InterfaceColors[i], false);
                }
                
            }

            Console.Write("- ] around the game board \nuntil all of the rabbits are safe in the holes ");
            WriteInColor($"{GameGrid.HoleIconLeftHalf} {GameGrid.HoleIconRightHalf}", GameGrid.HoleColorIfEmpty, false);
            Console.WriteLine(".");
            Console.WriteLine();
            Console.WriteLine();
            WriteInColor("General rules", ConsoleColor.Yellow);
            Console.WriteLine("→ Foxes move by sliding forward or backward.Foxes cannot jump over obstacles.");
            Console.Write($"→ Mushrooms [");
            WriteInColor($"{Mushroom.Icon}", Mushroom.IconColor, false);
            Console.WriteLine("] are stationary and cannot be moved.");
            Console.WriteLine("→ Rabbits move by jumping horizontally or vertically over one or more spaces with obstacles: \nother rabbits, foxes, mushrooms or a combination of these.");
            Console.WriteLine("→ Rabbits must land on the first empty space after a jump - they can never move over empty spaces.");
            Console.WriteLine("→ Rabbits can never move without jumping over at least 1 obstacle, \nthus they can never move to an adjacent space.");
            Console.WriteLine("→ A hole with a rabbit inside is an obstacle, while empty holes are not obstacles.");
            Console.WriteLine("→ A rabbit can jump into – but not over – an empty hole.");
            Console.WriteLine("→ If needed, rabbits can jump out of holes they are already sitting in.");
            Console.WriteLine("→ Rabbits can jump over a fox no matter the orientation of the fox: \ntail to front, front to tail, or over the side.");
            Console.WriteLine("→ You have found a solution when all of the rabbits are inside the holes! \nThe end position of the foxes is not important.");

            ControlsHelpMenu();
            Console.SetWindowSize(x, y);
        }

        public static void ErrorMessage()
        {
            Console.WriteLine();
            Console.WriteLine("!!!  Invalid input  !!!");
        }

        public static void ControlsHelpMenu()
        {
            List<ConsoleColor> bunnyColors = new List<ConsoleColor> { };

            foreach (ConsoleColor obj in Bunny.bunnyInterfaceColors)
            {
                bunnyColors.Add(obj);
            }

            Console.WriteLine();
            Console.WriteLine();
            WriteInColor("Keyboard commands!", ConsoleColor.Yellow);

            Console.WriteLine();
            Console.WriteLine($"Bunnies: (select one and use ← ↑ ↓ → to jump over the obstacles)");
            Console.WriteLine();

            Console.Write($"Press [1] to select the {bunnyColors[0]} Bynny ");
            WriteInColor($"- {Bunny.DisplayIcon} -", bunnyColors[0]);

            Console.Write($"Press [2] to select the {bunnyColors[1]} Bynny ");
            WriteInColor($"- {Bunny.DisplayIcon} -", bunnyColors[1]);

            Console.Write($"Press [3] to select the {bunnyColors[2]} Bynny ");
            WriteInColor($"- {Bunny.DisplayIcon} -", bunnyColors[2]);
            Console.WriteLine();
            Console.WriteLine("Foxes: (select one and use  ← ↑ ↓ →  to slide them left and right or up and down)");
            Console.WriteLine($"Press [4] to select the {Fox.InterfaceColors[0]} Fox.");
            WriteInColor($"{Fox.DisplayIconsList[0]}{Fox.DisplayIconsList[0]}   {Fox.DisplayIconsList[2]}   {Fox.DisplayIconsList[3]}", Fox.InterfaceColors[0]);
            WriteInColor($"{Fox.DisplayIconsList[1]}{Fox.DisplayIconsList[1]}   {Fox.DisplayIconsList[2]}   {Fox.DisplayIconsList[3]}", Fox.InterfaceColors[0]);
            Console.WriteLine();                                                                           
            Console.WriteLine($"Press [5] to select the {Fox.InterfaceColors[1]} Fox.");                   
            WriteInColor($"{Fox.DisplayIconsList[0]}{Fox.DisplayIconsList[0]}   {Fox.DisplayIconsList[2]}   {Fox.DisplayIconsList[3]}", Fox.InterfaceColors[1]);
            WriteInColor($"{Fox.DisplayIconsList[1]}{Fox.DisplayIconsList[1]}   {Fox.DisplayIconsList[2]}   {Fox.DisplayIconsList[3]}", Fox.InterfaceColors[1]);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Press [S] to jump to the desired challenge.");
            Console.WriteLine("Press [D] to select the next challenge.");
            Console.WriteLine("Press [A] to select the previous challenge.");
            Console.WriteLine("Press [R] to reset the challenge.");
            Console.WriteLine();

            Console.WriteLine("Press any key to return to the game!");
            Console.ReadKey();
        }

        public static ConsoleKeyInfo ResetLevelMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Restart current level! y/n");

            return Console.ReadKey();
        }


        /// <summary>
        /// Opens the menu that allowes the user to enter the desired level index and warp to it.
        /// </summary>
        public static void EnterLevelIndex()
        {
            int userChoice;
            do
            {
                Console.WriteLine();
                Console.WriteLine($"Enter the level you want to play! (1 - {Level.AllLevelsCount})");
            }
            while (!int.TryParse(Console.ReadLine(), out userChoice));

            if (userChoice < 0)
            {
                userChoice = 1;
            }
            else if (userChoice > Level.AllLevelsCount)
            {
                userChoice = Level.AllLevelsCount;
            }

            Level.LevelIndex = userChoice;

            //return userChoice;
        }
    }
}
