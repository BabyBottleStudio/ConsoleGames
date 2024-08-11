using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFour
{
    public class Board
    {
        private int[,] grid = new int[6, 7];
        private int turnCount;
        private int playerIndex;
        private int[,] victoryFieldsForMarking;
        // opcija da kada neko pobedi da markira pobednicka polja!!
        // prilikom provere belezi koordinate u ovaj array koji ce na kraju da ima oblik
        // {
        //      {x0, x1, x2, x3},
        //      {y0, y1, y2, y3}
        // }
        // kad uradi proveru pobede, i skonta da je pobedio, da se ta polja kroz jedan lup izmene u -2 i 2
        // te vrednosti će se iskoristiti za vreme iscrtavanja grida da se markiraju polja koja su donela pobedu

        private bool[] isColumnFullBoolArray = new bool[7];


        public int TurnCount
        {
            get { return turnCount;  }
        }

        public int PlayerIndex
        {
            get { return playerIndex;  }
        }

        public void PlayerIndexSwitch()
        {
            playerIndex *= -1;
        }


        // inicijalizacija
        public Board()
        {
            grid.Initialize();
            turnCount = 42;
            playerIndex = 1;
            victoryFieldsForMarking = new int[2, 4];
            victoryFieldsForMarking.Initialize();
            isColumnFullBoolArray.Initialize();
        }

        private void RestIsColumnFullArray()
        {
            for (int i = 0; i<isColumnFullBoolArray.Length; i++)
            {
                isColumnFullBoolArray[i] = false;
            }
        }

        public int EditGrid(int col) // colona je int od 0 do 6; user unosi od 1 - 7, mora da se smanji za jedan. player je -1 ili 1;
        {
            col--;
            // dobija samo input o koloni
            // sistem treba da proveri gde "upada" zeton / vrednost
            for (int i = grid.GetLength(0)-1; i >= 0; i--)
            {
                if (grid[i, col] == 0)
                {
                    grid[i, col] = playerIndex;
                    turnCount--;

                    if (i == 0 && grid[i, col] != 0)
                    {
                        isColumnFullBoolArray[col] = true;
                    }
                    //Console.WriteLine("Edit sucessfull!!");
                    return CheckForWin( (i, col) );
                }
            }
            return 0;
        }

        public void DisplayGrid()
        {
            Console.Clear();
            //                                              -1      0      1  
            //List<string> displayField = new List<string> { "[O]", "[ ]", "[X]" };

            List<string> displayField = new List<string> { "O", " ", "X" };

            Console.WriteLine(" 1  2  3  4  5  6  7");   
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                
                for (int j = 0; j<grid.GetLength(1); j++)
                {
                    Console.ResetColor();

                    Console.Write("[");

                        if (grid[i, j] == -1)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        else if (grid[i, j] == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        else if (grid[i, j] == -2) // ukoliko je markirano kao pobednicko polje O igraca
                        {
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.ForegroundColor = ConsoleColor.Black;
                            grid[i, j] = -1; // resetuj vrednost jer pravilan displej zavisi od toga
                        }
                        else if(grid[i, j] == 2) // ako je markirano kao pobednicko polje X igraca
                        {
                            Console.BackgroundColor = ConsoleColor.Red;
                            grid[i, j] = 1; // resetuj vrednost jer pravilan displej zavisi od toga
                    }

                    Console.Write(displayField[ grid[i,j] + 1 ]);
                    Console.ResetColor();
                    Console.Write("]");
                }

                Console.WriteLine();
            }
        }

        
        public void ResetVictoryFields()
        {
            for (int i = 0; i < victoryFieldsForMarking.GetLength(0); i++)
            {
                for (int j = 0; j < victoryFieldsForMarking.GetLength(1); j++)
                {
                    victoryFieldsForMarking[i, j] = 0; // i + 1;
                }
            }
        }

        public void ResetGrid()
        {
            //Array.Fill(grid, 0);
            //grid.Initialize();

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    grid[i,j] = 0; // i + 1;
                }
            }

            turnCount = 42;
            playerIndex = 1;
            RestIsColumnFullArray();

        }

        public int ValidateUserInput()
        {
            Console.WriteLine();
            switch (playerIndex)
            {
                case -1:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Player two!!!");
                    Console.ResetColor();
                    break;
                case 1:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Player one!!!");
                    Console.ResetColor();
                    break;
            }

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Use the nubers from 1 - 7 to select the column!");
                //Console.WriteLine($"Turns left: {TurnCount}");
                Console.WriteLine();

                try
                {
                    int userInput = int.Parse(Console.ReadKey().KeyChar.ToString());

                    if (userInput >= 1 && userInput <= 7)
                    {
                        if (isColumnFullBoolArray[userInput-1] == true) // test da li je kolona puna. Proverava niz booleana koji ima za svaki red informaciju
                        {
                            Console.WriteLine();
                            Console.WriteLine($"Column {userInput} is full, try another!");
                        }
                        else
                        {
                            return userInput;
                        }
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid entry. Please use numbers from 1 to 7!");
                    continue;
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine("Došlo je do greške: " + ex.Message);
                    continue;
                }
            }
        }

        private void MarkVictoryFields()
        {
            for (int i = 0; i < 4; i++)
            {
                grid[victoryFieldsForMarking[0, i], victoryFieldsForMarking[1, i]] *= 2;
                // -1 * 2 = -2
                //  1 * 2 =  2
            }
        }
        
        public int CheckForWin( (int x, int y) userInputCoords )
        {
            // redovi
            // ovaj deo koda proverava redove da li postoji 4 usastopna markera
            // grid[coord.x => ostaje ista kroz loop jer se proverava red, i => se menja kroz loop jer ide po elementima istog reda]
            int sameSymbolCount = 0;
            for (int i = 0; i < grid.GetLength(1); i++)
            {
                if (grid[userInputCoords.x, i] == playerIndex)
                {
                    //      {x0, x1, x2, x3},
                    //      {y0, y1, y2, y3}
                    victoryFieldsForMarking[0, sameSymbolCount] = userInputCoords.x;
                    victoryFieldsForMarking[1, sameSymbolCount] = i;
                    sameSymbolCount++;

                    //Console.WriteLine($"Horizontal. Player: {playerIndex} count:{count}");
                    if (sameSymbolCount == 4)
                    {
                        MarkVictoryFields();
                        return playerIndex;
                    }
                }
                else
                {
                    ResetVictoryFields();
                    sameSymbolCount = 0;
                }
                
            }
            
            // kolone
            // ovaj deo koda proverava kolone da li postoji 4 usastopna markera
            // grid[i => se menja kroz loop od 0 do 5, coord.y => ostaje ista kroz loop]

            sameSymbolCount = 0;

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                if (grid[i, userInputCoords.y] == playerIndex)
                {
                    victoryFieldsForMarking[0, sameSymbolCount] = i;
                    victoryFieldsForMarking[1, sameSymbolCount] = userInputCoords.y;
                    
                    sameSymbolCount++;

                    if (sameSymbolCount == 4)
                    {
                        MarkVictoryFields();
                        return playerIndex;
                    }
                }
                else
                {
                    ResetVictoryFields();
                    sameSymbolCount = 0;
                }

            }

            //////////////////////////
            // Cekiranje Dijagonala //
            //////////////////////////
            // https://docs.google.com/spreadsheets/d/1lSz7_h4RfUBK7A1pCRUeebKbWOU2Bq4XKR9oRStfZ4I/edit#gid=0

            // ovde su primeri dok sam radio eksperiment da pronadjem pocetno polje dijagonale u ondosu na zadatu koordinatu
            //
            // mora da se obrati paznja na uslove koja je koordinata veca a koja manja
            // ako je (x > y) 
            // ako je (x < y)
            // (x, y)
            // (5, 6); x je manje od y; oduzimamo manjeg od veceg y = 6 - 5 = 1. Pocetne koordinate su (0,1)
            // (4, 2); x je vece od y; oduzimamo manjeg od veceg  x = 4 - 2 = 2. Pocetne koordinate su (2,0)
            // (3, 5); x je manje od y; oduzimamo manjeg od veceg y = 5 - 3 = 2.  Pocetne koordinate su (0,2)




            // ovo dobro generise pocetne koordinate za \ proveru dijagonale
            (int x, int y) startCoord1;

            if (userInputCoords.x >= userInputCoords.y)
            {
                startCoord1 = ((userInputCoords.x - userInputCoords.y), 0);
            }
            else
            {
                startCoord1 = (0, (userInputCoords.y - userInputCoords.x));
            }

            sameSymbolCount = 0; // ova promenljiva broji uzastopne iste simbole
            int index = 0;             

            while (true)
            {
              
                if (startCoord1.x + index == grid.GetLength(0) || startCoord1.y + index == grid.GetLength(1)) // uslov za proveru da li smo u okvirima grida
                {
                    // ukoliko izadjes iz okvira grida
                    break;
                }
                else // ukoliko nismo izasli iz grida
                {
                    // proverava da li je startna koordinata koju je izgenerisao gore + index  jednaka trenutnom indexu igraca.
                    // ako jeste , ubacice je u count. Ukoliko nije, count se resetuje jer nam trebaju uzastopne vrednosti

                    if (grid[startCoord1.x + index, startCoord1.y + index] == playerIndex)
                    {
                        // markiranje pobednickih polja
                        victoryFieldsForMarking[0, sameSymbolCount] = startCoord1.x + index;
                        victoryFieldsForMarking[1, sameSymbolCount] = startCoord1.y + index;

                        sameSymbolCount++;

                        if (sameSymbolCount == 4)
                        {
                            MarkVictoryFields();
                            return playerIndex;
                        }
                    }
                    else
                    {
                        ResetVictoryFields();
                        sameSymbolCount = 0;
                    }
                }

                index++;
            }


            sameSymbolCount = 0;
            index = 0;
            (int x, int y) startCoord2;
            // skica za proveru / dijagonale
            // ova druga dijagonala ima drugaciju matematiku za otkrivanje pocetne koordinate.
            // ova dijagonala se broji tako sto broj redova raste za jedan ali kolona se broji unazad


            // zbir koordinata
            // ako je manji od 6 ili jednak 6
            // (4, 1) = 5 => 0, 5
            // (3, 0) = 3 => 0, 3
            // 
            // ako je veci od 6
            // (4, 5) => 9; 9 - 6 = 3; (3, 6)
            // (4, 4) => 8; 8 - 6 = 2; (2, 6)
            // (5, 6) => 11; 11 - 6 = 5; (5, 6)

            startCoord2 = (userInputCoords.x + userInputCoords.y <= 6) ? (0, (userInputCoords.x + userInputCoords.y)) : ((userInputCoords.x + userInputCoords.y) - 6, 6);

            if (userInputCoords.x + userInputCoords.y <= 6)
            {
                startCoord2 = (0, (userInputCoords.x + userInputCoords.y));
            }
            else
            {
                startCoord2 = ((userInputCoords.x + userInputCoords.y) - 6, 6);
            }

            while (true)
            {
                if (startCoord2.x + index == grid.GetLength(0) || startCoord2.y - index < 0) 
                    // odbrana da ne izadju brojevi iz okvira arraya
                    // ako moraš da braniš kod od grešaka, onda si fejlovao :). Vidi da li ovo može nekako drugačije da se odbrani
                {
                    break;
                }
                else
                {
                    if (grid[startCoord2.x + index, startCoord2.y - index] == playerIndex)
                    {
                        // markiranje pobednickih polja
                        victoryFieldsForMarking[0, sameSymbolCount] = startCoord2.x + index;
                        victoryFieldsForMarking[1, sameSymbolCount] = startCoord2.y - index;
                        sameSymbolCount++;

                        if (sameSymbolCount == 4)
                        {
                            MarkVictoryFields();
                            return playerIndex;
                        }
                    }
                    else
                    {
                        ResetVictoryFields();
                        sameSymbolCount = 0;
                    }
                }

                index++;
            }
            PlayerIndexSwitch();
            return 0;
    }
        
    }


        class Program
        {
            static void Main(string[] args)
            {
                Console.Title = "Connect 4";
                Board theBoard = new Board();

                while (true)
                {
                    int winner;
                    theBoard.ResetGrid();
                    //int player = -1;

                    while (true) 
                    {
                        //Console.Clear();
                        theBoard.DisplayGrid();

                        var userInput = theBoard.ValidateUserInput();
                        winner = theBoard.EditGrid(userInput);                    
                        
                        if (Math.Abs(winner) == 1 || theBoard.TurnCount == 0)
                        {
                            break;
                        }
                 

                       
                    } // while true END



                        theBoard.DisplayGrid();
                        Console.WriteLine();
                    if (winner == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Player one wins");
                        Console.ResetColor();
                    }
                    else if (winner == -1)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Player two wins");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine("Everything is full!!! No one wins");
                    }
                    Console.WriteLine();
                    Console.WriteLine("Press any key to play again!");

                    Console.ReadKey();
                }  
            }
        }
}
