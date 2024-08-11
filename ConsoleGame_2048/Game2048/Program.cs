using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Game2048_v6
{
    class Grid
    {
        private int[,] grid;


        private int gameGridSize = 4;
        private int gameWinCondition = 2048;

        private List<(int, int)> emptyFieldsCoordsList = new List<(int x, int y)>();

        private List<int> workingList = new List<int>();

        private bool[] _isGridChangedBoolArr;
        private bool _isGridChanged;

        private bool isWin = false;

        private Random rand = new Random();
        public int score;


        /// <summary>
        /// Intilize the grid. If gridSize is passed, the grid will have that dimension. If not, default of 4 will be applied. winCondition signifies when the player is going to win.
        /// </summary>
        /// <param name="gridSize"></param>
        /// <param name="winCondition"></param>
        public Grid(int gridSize = 4, int winCondition = 2048)
        {
            if (gridSize < 4)
            {
                gridSize = 4;
                //gameGridSize = 4;
            }
            else if (gridSize > 16)
            {
                gridSize = 16;
            }

            gameGridSize = gridSize;

            grid = new int[gameGridSize, gameGridSize];
            grid.Initialize();

            _isGridChangedBoolArr = new bool[gameGridSize];
            _isGridChangedBoolArr.Initialize();

            _isGridChanged = false;
            isWin = false;
            score = 0;
            gameWinCondition = winCondition;
        }


        public bool IsGridChanged()
        {
            _isGridChanged = _isGridChangedBoolArr.Any(x => x == true);
            return _isGridChanged;
        }

        public bool IsWin
        {
            get { return isWin; }
            set { isWin = value; }
        }


        public int GameGridSize
        {
            get { return gameGridSize; }
        }

        public void DisplayGrid()
        {
            for (int i = 0; i < gameGridSize; i++)
            {

                // NODE 1 - displays the horizontal lines of the grid
                for (int j = 0; j < gameGridSize; j++)
                {
                    Console.Write(" ----");
                }
                Console.WriteLine();
                // ------------------------------------------------


                // NODE 2 - displays the vertical lines of the grid
                for (int j = 0; j < gameGridSize; j++)
                {
                    Console.Write("|    ");
                }
                Console.Write("|");
                Console.WriteLine();
                // ------------------------------------------------

                // NODE 3 - displays the line with numbers 
                for (int j = 0; j < gameGridSize; j++)
                {
                    string numberToDisplay = grid[i, j] == 0 ? "    " : grid[i, j].ToString().PadLeft(4);
                    Console.Write($"|{numberToDisplay}");
                }
                Console.WriteLine("|");
                // ------------------------------------------------

            }


            // NODE 4 - displays the horizontal lines of the grid at the end
            for (int i = 0; i < gameGridSize; i++)
            {
                Console.Write(" ----");
            }
            Console.WriteLine();

            Console.WriteLine($"Score: {score}");
            // ------------------------------------------------

        }

        /// <summary>
        /// By defaullt (no pareameters), resets the grid to 0 values. If testNum parameter is passed, the grid will be filled with that value.
        /// </summary>
        /// <param name="testNum"></param>
        public void ResetGrid(int testNum = 0)
        {

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    grid[i, j] = testNum;
                }
            }

            _isGridChanged = false;
            score = 0;
        }


        /// <summary>
        /// This method goes through the grid and collects the coordinates with the value of zero in the "emptyFieldsCoordsList"
        /// </summary>
        private void CollectEmptyCoordinates() // | 8||}o  Ovde ima lufta za refaktoring
        {
            emptyFieldsCoordsList.Clear();

            for (int i = 0; i < gameGridSize; i++)
            {
                for (int j = 0; j < gameGridSize; j++)
                {
                    if (grid[i, j] == 0)
                    {
                        emptyFieldsCoordsList.Add((i, j));
                    }
                }
            }
        }

        /// <summary>
        /// This method picks the random values from the "emptyFieldsCoordsList". Returns them as a tupple.
        /// </summary>
        /// <returns></returns>
        private (int x, int y) PickRandomFromList()
        {
            CollectEmptyCoordinates();
            int indeks = rand.Next(emptyFieldsCoordsList.Count);
            // dobijam index vrednosti iz liste. Sad mora da se ta vrednost iskoristi

            //pristup listi citanje koordinate koja je slobodna
            return emptyFieldsCoordsList[indeks];
        }

        /// <summary>
        /// This method generates the value of 2 or 4, picks random coordinates from the list, and writes the value into the grid.
        /// </summary>
        public void GenerateNewNumber()
        {
            var fieldToEdit = PickRandomFromList();
            // izaberi nasumicnu koordinatu koja je slobodna


            //Console.WriteLine($"Izabrana koordinata za upisivane broja je: {fieldToEdit}");
            int generatedInt = rand.Next(1000) % 2 == 0 ? 2 : 4; // ukoliko izgenerise neparan broj, vraca 4 a ako izgenerise paran vraca 2

            grid[fieldToEdit.x, fieldToEdit.y] = generatedInt; // upisivanje vrednosti 2 ili 4
        }



        /// <summary>
        /// This method checks if the line is empty or not. Index parameter defines which line is checked and the direction defines if it is row or a column.
        /// </summary>
        /// <param name="lineIndex"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public bool IsLineEmpty(int lineIndex, int direction)
        {
            switch (direction)
            {
                case -1:
                case 1:
                    // return Enumerable.Range(0, gameGridSize).All(i => grid[lineIndex, i] != 0);

                    for (int i = 0; i < gameGridSize; i++)
                    {
                        if (grid[lineIndex, i] != 0)
                        {
                            return false;
                        }
                    }
                    break;

                case -2:
                case 2:
                    // return Enumerable.Range(0, gameGridSize).All(i => grid[i, lineIndex] != 0); // zakomentarisano jer jos uvek ne kontam ovo sto posto!
                    for (int i = 0; i < gameGridSize; i++)
                    {
                        if (grid[i, lineIndex] != 0)
                        {
                            return false;
                        }
                    }
                    break;
            }
            return true;
        } // refaktorisano


        /// <summary>
        /// Collects non zero values from the row or column, depending on the given direction. Returns true if it collects any element and false on empty result.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="direction"></param>
        private bool CollectNonZeroElements(int index, int direction)
        {
            workingList.Clear();
            switch (direction)
            {
                case -1:
                case 1:
                    workingList = Enumerable.Range(0, gameGridSize)
                                            .Select(i => grid[index, i])
                                            .Where(value => value > 0)
                                            .ToList();

                    // prvi red pravi loop od 0 do gameGridSize - 1
                    // drugi red selektuje vrednost iz grida // Za svaki indeks i, uzima se vrednost iz reda index u koloni i
                    // Filtriraju se samo vrednosti koje su veće od nule
                    // Pretvara se rezultujuća kolekcija u listu
                    break;

                case -2:
                case 2:
                    workingList = Enumerable.Range(0, gameGridSize)
                                            .Select(i => grid[i, index])
                                            .Where(value => value > 0)
                                            .ToList();
                    break;
            }

            return workingList.Count > 0 ? true : false;
        }

        private void ElementAdition()
        {
            for (int i = 0; i < workingList.Count - 1; i++)
            {
                if (workingList[i] == workingList[i + 1])
                {
                    score += workingList[i];
                    workingList[i] *= 2;
                    workingList[i + 1] = 0;
                }
            }
        }

        private void AddZerosToList()
        {
            int currentWorkingListSize = workingList.Count(); // belezi koliko ima elemenata koji nisu nula - izbrisane su u prethodnom redu

            // test da li je red pun
            // dodavanje nula ako mu fali
            if (workingList.Count < gameGridSize)
            {
                for (int i = 0; i < gameGridSize - currentWorkingListSize; i++)
                {
                    workingList.Add(0);
                }
            }
        }

        private bool IsRowChanged(int index, int direction)
        {
            switch (direction)
            {
                case -1:
                case 1:
                    for (int i = 0; i < gameGridSize; i++)
                    {
                        if (grid[index, i] != workingList[i])
                        {
                            return true;
                        }
                    }
                    return false;

                case -2:
                case 2:

                    for (int i = 0; i < gameGridSize; i++)
                    {
                        if (grid[i, index] != workingList[i])
                        {
                            return true;
                        }
                    }
                    return false;
            }
            return false;
        }

        private void WriteFromListToLine(int index, int direction)
        {
            switch (direction)
            {
                case -1:
                case 1:
                    // upisivanje workingliste u grid liniju
                    for (int i = 0; i < gameGridSize; i++)
                    {
                        grid[index, i] = workingList[i];
                    }
                    break;

                case -2:
                case 2:
                    // upisivanje workingliste u grid liniju
                    for (int i = 0; i < gameGridSize; i++)
                    {
                        grid[i, index] = workingList[i];
                    }
                    break;
            }

        }


        public void ShiftLineHorizontal(int index, int direction)
        {
            _isGridChangedBoolArr[index] = false;

            if (!CollectNonZeroElements(index, direction))
                return;

            if (direction > 0)
                workingList.Reverse(); // ovde se obrne da bi matematika dole bila ista

            if (workingList.Count > 1) // ovo je deo koji ce da sabere susedne brojeve
            {
                ElementAdition();
            }

            workingList.RemoveAll(x => x == 0);
            AddZerosToList();


            if (direction > 0)
                workingList.Reverse(); // ponovno obrtanje niza da se vrati original


            _isGridChangedBoolArr[index] = IsRowChanged(index, direction);


            WriteFromListToLine(index, direction);

        }

        public void ShiftLineVertical(int index, int direction)
        {
            // ovde moze da dodje deo gde se sabiraju brojevi
            // horizontalno / red - sakuplja s leva na desno, sabira i kontrolise sa levo na desno
            // sabiranje treba da se desava sa brojem sa desne strane 
            // 4400 => 8000
            // 2244 => 4800

            workingList.Clear();

            _isGridChangedBoolArr[index] = false;

            for (int i = 0; i < gameGridSize; i++)
            {
                var currentFieldValue = grid[i, index]; // promena ovde | 8||}o
                if (currentFieldValue > 0)
                {
                    //Console.WriteLine($"Upisano {currentFieldValue}");
                    workingList.Add(currentFieldValue);
                }
            }

            if (workingList.Count == 0)
            {
                //Console.WriteLine("Prazna kolona");
                return;
            }

            if (direction == 2)  // promena ovde | 8||}o
                workingList.Reverse(); // ovde se obrne da bi matematika dole bila ista

            if (workingList.Count > 1) // ovo je deo koji ce da sabere susedne brojeve
            {
                for (int i = 0; i < workingList.Count - 1; i++)
                {
                    if (workingList[i] == workingList[i + 1])
                    {
                        score += workingList[i];
                        workingList[i] *= 2;
                        workingList[i + 1] = 0;
                    }
                }
            }

            workingList.RemoveAll(x => x == 0);

            int currentWorkingListSize = workingList.Count();

            // test da li je red pun
            // dodavanje nula ako mu fali
            if (workingList.Count < gameGridSize)
            {
                for (int i = 0; i < gameGridSize - currentWorkingListSize; i++)
                {
                    workingList.Add(0);
                }
            }

            if (direction == 2) // promena ovde | 8||}o
                workingList.Reverse(); // ponovno obrtanje niza da se vrati original

            // ovde treba da se uradi poredjenje da li se red, nakon userInputa promenio ili ne.
            // Taj deo treba da vrati varijabilu "true" or "false".
            // False sprecava generaciju novih brojeva jer se nista nije promenilo
            // True dozvoljava generaciju novih brojeva

            // compare workingList and gridLine
            for (int i = 0; i < gameGridSize; i++)
            {
                if (grid[i, index] != workingList[i])  // promena ovde | 8||}o
                {
                    _isGridChangedBoolArr[index] = true;
                    break;
                }
            }



            // upisivanje workingliste u grid liniju
            for (int i = 0; i < gameGridSize; i++)
            {
                grid[i, index] = workingList[i];  // promena ovde | 8||}o
            }

            //for (int i = 0; i < workingList.Count; i++)
            //{
            //    Console.Write(workingList[i]);
            //}
            //Console.WriteLine();
        }


        /// <summary>
        /// Method checks if there is a filed with gameWinCondition. Default is value of 2048.
        /// </summary>
        public void IsThereAWin()
        {
            foreach (var obj in grid)
            {
                if (obj == gameWinCondition)
                {
                    isWin = true;
                }
            }

        }


        /// <summary>
        /// This method checks if there is any valid move left. It goes through each row and column, compares the consequent members and returns the bool.
        /// </summary>
        /// <returns></returns>
        public bool NoMoreMovesLeft()
        {
            //checked rows;
            for (int i = 0; i < gameGridSize; i++)
            {
                int lastObj = 0;
                for (int j = 0; j < gameGridSize; j++)
                {
                    if (grid[i, j] == 0 || grid[i, j] == lastObj)
                    {
                        //Console.WriteLine("Ima jos poteza u horizontali");
                        return false;
                    }
                    else
                    {
                        lastObj = grid[i, j];
                    }
                }
            }

            // check columns
            for (int i = 0; i < gameGridSize; i++)
            {
                int lastObj = 0;
                for (int j = 0; j < gameGridSize; j++) // ovde ne mora da se testira nula jer je vec istestirao na horizontalama
                {
                    if (grid[j, i] == lastObj)
                    {
                        //Console.WriteLine("Ima jos poteza u vertikali");
                        return false;
                    }
                    else
                    {
                        lastObj = grid[j, i];
                    }
                }
            }
            return true;
        }


        public int ValidateUserInput() // testirano. Radi!
        {
            while (true)
            {
                // vrednosti 1 i 2 oznacavaju kretanje od nize ka visoj koordinati (0 => 3)
                // vrednosti -1 i -2 oznacavaju kretanje od nize ka visoj koordinati (3 => 0)


                ConsoleKeyInfo userInput = Console.ReadKey();

                switch (userInput.Key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                    case ConsoleKey.NumPad8:
                        //Console.WriteLine(userInput.Key);
                        return -2;
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                    case ConsoleKey.NumPad2:
                        //Console.WriteLine(userInput.Key);
                        return 2;
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.A:
                    case ConsoleKey.NumPad4:
                        //Console.WriteLine(userInput.Key);
                        return -1;
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.D:
                    case ConsoleKey.NumPad6:
                        //Console.WriteLine(userInput.Key);
                        return 1;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Invalid input. Use arrow keys, WASD or numpad 8642 to move blocks.");
                        break;
                }
            }
        }

    }

    class Program
    {
        static void Main(string[] args)
        {

            Console.SetWindowSize(30, 15);
            //Console.SetWindowPosition(100,100);

            Grid theGrid = new Grid(4, 2048);
            int userInput;

            theGrid.GenerateNewNumber();
            theGrid.GenerateNewNumber();

            theGrid.DisplayGrid();
            bool winHappened = false;

            while (true)
            {
                userInput = theGrid.ValidateUserInput();
                Console.WriteLine(userInput);

                for (int i = 0; i < theGrid.GameGridSize; i++)
                {
                    theGrid.ShiftLineHorizontal(i, userInput);
                }
                Console.Clear();

                theGrid.DisplayGrid();
                Thread.Sleep(250);
                // test da li se nesto promenilo
                if (theGrid.IsGridChanged())
                {
                    theGrid.GenerateNewNumber();
                }


                Console.Clear();
                theGrid.DisplayGrid();

                if (theGrid.NoMoreMovesLeft())
                {
                    break;
                }


                theGrid.IsThereAWin();

                if (theGrid.IsWin && winHappened == false)
                {
                    bool endSession = false;
                    while (true)
                    {
                        Console.WriteLine("You won!!!");
                        Console.WriteLine("Continue to play? (y/n)");

                        var continueToPlay = Console.ReadKey().KeyChar;

                        if (continueToPlay == 'y')
                        {
                            winHappened = true;

                            Console.WriteLine();
                            Console.WriteLine("Please continue to play. \nUse arrow keys, WASD or numpad 8642 to move blocks.");

                            break;
                        }
                        else if (continueToPlay == 'n')
                        {
                            endSession = true;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input.");
                        }
                    }

                    if (endSession)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Good Game!!!");
                        break;
                    }
                }
            }
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine("GameOver!");
            Console.ResetColor();


        }
    }
}
