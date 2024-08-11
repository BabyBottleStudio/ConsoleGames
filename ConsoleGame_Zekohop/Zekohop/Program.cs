using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zekohop
{
    class Program
    {
        static void Main(string[] args)
        {
            Level.LevelIndex = 1;

            while (true)
            {
                // setting up the scene 
                Console.Clear();
                new GameGrid();
                GameGrid.ResetBunniesFoxesCount();
                Level.LoadLevel();
                GameGrid.selectedAnimal = 1;
                Display.GridAdvanced();
                // setting up the scene END

                while (true)
                {
                    Display.GameMenu();

                    int userInput = UserInput(GameGrid.selectedAnimal, out GameGrid.selectedAnimal);
                    GameGrid.userInput = userInput;

                    if (userInput == -99) // help menu
                    {
                        Console.Clear();
                        Display.HelpMenu();

                        Display.GridAdvanced();
                    }
                    else if (userInput == -66) // reset level
                    {
                        var userChoice = Display.ResetLevelMenu();

                        if (userChoice.Key == ConsoleKey.Y)
                        {
                            break;
                        }
                        else if (userChoice.Key == ConsoleKey.N)
                        {
                            continue;
                        }
                    }
                    else if (userInput == -23) // s - Choose the level
                    {
                        Display.EnterLevelIndex();
                        break;
                    }
                    else if (userInput == -22) // a load previous level
                    {
                        Level.JumpToPreviousLevel();
                        break;
                    }
                    else if (userInput == -24) // d load nex level
                    {
                        Level.JumpToNextLevel();
                        break;
                    }
                    else if (userInput == 666) // invalid input
                    {
                        Display.ErrorMessage();
                        Display.ControlsHelpMenu();
                    }

                    GameGrid.SetSelectedAnimal();
                    GameGrid.MoveSelectedAnimal();

                    Console.WriteLine($"{userInput}");

                    Console.Clear();
                    Display.GridAdvanced();

                    if (GameGrid.IsThereAWin())
                    {
                        Display.LevelWin();
                        Level.LevelIndex++;
                        break;
                    }
                }

            }
        }


        /// <summary>
        /// Handles the user input values and distribute them through the game.
        /// </summary>
        /// <param name="currentAnimal"></param>
        /// <param name="selectedAnimal"></param>
        /// <returns></returns>
        static int UserInput(int currentAnimal, out int selectedAnimal)
        {
            // User can input arrow keys, number keys 1 - 5, A,S,D 
            // numbers are changing the selection of a current animal in the scene
            // arrow keys are moving the selection
            // a - loads the previous level
            // s - opens the menu where you type in the level number you want to play.
            // d - loads the next level

            selectedAnimal = currentAnimal; // this way we change the current selection and it changes only if user enters a new number.; na ovaj nacin se unosi koja je trenutna selekcija, i menja se samo ako korisnik unese broj.

            ConsoleKeyInfo keyInfo = Console.ReadKey();

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    return -2;

                case ConsoleKey.DownArrow:
                    return 2;

                case ConsoleKey.LeftArrow:
                    return -1;

                case ConsoleKey.RightArrow:
                    return 1;

                case ConsoleKey.D1:
                    if (Bunny.BunnyCount > 0)
                    {
                        selectedAnimal = 1;
                    }
                    break;
                case ConsoleKey.D2:
                    if (Bunny.BunnyCount > 1)
                    {
                        selectedAnimal = 2;
                    }
                    //else
                    //{
                    //    selectedAnimal = 0;
                    //}
                    break;
                case ConsoleKey.D3:
                    if (Bunny.BunnyCount > 2)
                    {
                        selectedAnimal = 3;
                    }
                    //else
                    //{
                    //    selectedAnimal = 0;
                    //}
                    break;
                case ConsoleKey.D4:
                    if (Fox.FoxCount > 0)
                    {
                        selectedAnimal = 4;
                    }
                    //else
                    //{
                    //    selectedAnimal = 0;
                    //}
                    break;

                case ConsoleKey.D5:
                    if (Fox.FoxCount > 1)
                    {
                        selectedAnimal = 5;
                    }
                    //else
                    //{
                    //    selectedAnimal = 0;
                    //}
                    break;


                case ConsoleKey.R:
                    return -66;

                case ConsoleKey.S:
                    return -23;

                case ConsoleKey.A:
                    return -22;

                case ConsoleKey.D:
                    return -24;
                case ConsoleKey.H:
                    return -99;
                default:
                    return 666;
            }
            return 0;
        }
    }
}


/*
Grid je 5x5
fiksni elementi >   rupe => mogu biti i zatvorene pecurkama (imas listu pa je edituj ako treba)
                    pecurke (koje se menjaju od partije do partije)

mobilni elementi > 

lisice =>   krecu se samo napred, nazad. Udaraju u ivicu grida i udaraju u ostale elemente (zecevi i pecurke)
            Orjentacija lisice
            Zauzima dva polja

zecevi =>   mogu samo da preskacu objekte (lisice, pecurke i ostale zeceve)

sistem mora da prati koliko je zeceva na sceni i koliko je  u rupama. Win condition je kada su svi zecevi u rupama


/// sta treba dodati??? 
/// 
/// Idealno bi bilo da se legitimne koordinate sakupe i markiraju tako da kad se selektuje zec ili lisica, da odmah legitimna polja budu markirana.
/// 
/// kada se nivo zavrsi, da pita da li zelis da igras isti ponovo ili sledeci...
/// 
///  
/// 
///  

*/

