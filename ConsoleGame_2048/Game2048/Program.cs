using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048
{
    // version 03

    class Grid
    {
        private int[,] grid = new int[4, 4];
        private List<(int, int)> emptyFieldsCoordsList = new List<(int x, int y)>();

        public List<int> workingList = new List<int>();

        public bool[] isRowChangedBoolArr = new bool[4]; // ovo resiti inicijalizacijom// dodati getere i settere



        private Random rand = new Random();

        public Grid()
        {
            grid.Initialize();
            isRowChangedBoolArr.Initialize();
        }


        public void ResetGrid()
        {

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    grid[i, j] = 0;
                }
            }
        }

        private void ResetLine(int index, int direction)
        {
            switch (direction)
            {
                case 4:
                case 6:
                    for (int i = 0; i < grid.GetLength(1); i++)
                    {
                        grid[index, i] = 0;
                    }
                    break;
                case 2:
                case 8:
                    for (int i = 0; i < grid.GetLength(0); i++)
                    {
                        grid[i, index] = 0;
                    }
                    break;
            }


        }

        public void DisplayGrid()
        {


            for (int i = 0; i < grid.GetLength(0); i++)
            {
                Console.WriteLine(" ---- ---- ---- ---- ");
                Console.WriteLine("|    |    |    |    |");
                for (int j = 0; j < grid.GetLength(1); j++)
                {

                    string numberToDisplay = grid[i, j] == 0 ? "    " : grid[i, j].ToString().PadLeft(4);
                    Console.Write($"|{numberToDisplay}");
                }
                Console.WriteLine("|");
            }
            Console.WriteLine(" ---- ---- ---- ---- ");
        }



        public void CollectEmptyCoordinates()
        {
            emptyFieldsCoordsList.Clear();

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == 0)
                    {
                        emptyFieldsCoordsList.Add((i, j)); // mozda je i (j, i) 
                    }
                }
            }
        }


        public void GenerateNewNumber()
        {
            var fieldToEdit = PickRandomFromList();
            // izaberi nasumicnu koordinatu koja je slobodna

            Console.WriteLine($"Izabrana koordinata za upisivane broja je: {fieldToEdit}");

            int generatedInt = rand.Next(1000) % 2 == 0 ? 2 : 4; // ukoliko izgenerise neparan broj, vraca 4 a ako izgenerise paran vraca 2

            grid[fieldToEdit.x, fieldToEdit.y] = generatedInt; // upisivanje vrednosti 2 ili 4
        }

        private (int x, int y) PickRandomFromList()
        {
            CollectEmptyCoordinates();
            int indeks = rand.Next(emptyFieldsCoordsList.Count);
            // dobijam index vrednosti iz liste. Sad mora da se ta vrednost iskoristi

            //pristup listi citanje koordinate koja je slobodna
            return emptyFieldsCoordsList[indeks];
        }

        private bool IsLineEmpty(int lineIndex, int direction)
        {
            for (int i = 0; i < grid.GetLength(1); i++)
            {
                if (direction == 4 || direction == 6)
                {
                    if (grid[lineIndex, i] != 0)
                    {
                        return false;
                    }
                }
                else if (direction == 2 || direction == 8)
                {
                    if (grid[i, lineIndex] != 0)
                    {
                        return false;
                    }
                }

            }
            return true;
        }

        private void CollectValuesToWorkingList(int index, int direction)
        {
            switch (direction)
            {
                case 4:
                case 6:
                    for (int i = 0; i < grid.GetLength(1); i++)
                    {
                        workingList.Add(grid[index, i]);
                        Console.WriteLine($"Napunjavam: {grid[index, i]}");
                    }
                    // za vrednost 6 treba da se uradi reverse. To se desava kasnije
                    break;
                case 2:
                case 8:
                    for (int i = 0; i < grid.GetLength(0); i++) // ovaj puni odozgo na dole, to je default vrednost 2 kad se sabira u tom smeru. 8 je suprotno
                    {
                        workingList.Add(grid[i, index]);
                        Console.WriteLine($"Napunjavam: {grid[index, i]}");
                    }
                    break;
            }
        }

        // funkcija za pomeranje reda
        // test da li je red prazan
        // jedan element
        // vise elemenata
        public bool ShiftLine(int index, int direction)
        {
            if (IsLineEmpty(index, direction)) // ukoliko je red prazan preskoci ga, odnosno iskoci iz funkcije
            {
                return false;
            }

            // punjenje vrednostima workingList!!!
            CollectValuesToWorkingList(index, direction);

            if (direction == 6 || direction == 2)
            {
                workingList.Reverse();
                AddSameNumsInTheLine();
                workingList.Reverse();
            }
            else if (direction == 4 || direction == 8)
            {
                AddSameNumsInTheLine();
            }

            ExpandListWithZeros(direction, grid.GetLength(1));

            bool isLineChanged = IsLineChanged(index, direction);

            if (isLineChanged)
            {
                ResetLine(index, direction);


                if (direction == 6 || direction == 8)
                {
                    for (int i = 0; i < grid.GetLength(1); i++)
                    {
                        grid[index, i] = workingList[i];
                    }
                }
                else if (direction == 4 || direction == 2)
                {
                    for (int i = 0; i < grid.GetLength(1); i++)
                    {
                        grid[i, index] = workingList[i];
                    }
                }

                return true;
            }

            return isLineChanged;
        }


        public bool IsLineChanged(int index, int direction)
        {
            //ubaci direction kao opciju

            switch (direction)
            {
                case 4:
                case 6:
                    for (int i = 0; i < grid.GetLength(1); i++)
                    {
                        if (workingList[i] != grid[index, i])
                        {
                            Console.WriteLine("Nisu isti!!!");
                            return true;
                        }
                    }
                    // za vrednost 6 treba da se uradi reverse. To se desava kasnije
                    break;
                case 2:
                case 8:
                    for (int i = 0; i < grid.GetLength(0); i++) // ovaj puni odozgo na dole, to je default vrednost 2 kad se sabira u tom smeru. 8 je suprotno
                    {
                        if (workingList[i] != grid[i, index])
                        {
                            Console.WriteLine("Nisu isti!!!");
                            return true;
                        }
                    }
                    break;
            }

            return false;

            //            return temp.Any(x => x == true);
        }



        public void ExpandListWithZeros(int direction, int rowLength)
        {
            int size = workingList.Count();


            for (int i = 0; i < (rowLength - size); i++)
            {
                if (direction == 6 || direction == 8)
                {
                    workingList.Insert(0, 0);
                }
                else if (direction == 4 || direction == 2)
                {
                    workingList.Add(0);
                }
            }
        }

        public void AddSameNumsInTheLine()
        {

            workingList.RemoveAll(x => x == 0); // brise sve nule

            if (workingList.Count > 1)
            {
                for (int i = 0; i < workingList.Count - 1; i++)
                {
                    if (workingList[i] == workingList[i + 1])
                    {
                        workingList[i] *= 2;
                        workingList[i + 1] = 0;
                    }
                }
                workingList.RemoveAll(x => x == 0); // brise sve nule
            }

            //return workingList;
        }


        //public void MoveElements(int pravac)
        //{
        //    // ovo sad mora da ide po redovima :)

        //    for (int row = 0; row < 4; row++) // hardkodovana cetvorka!!!!!!
        //    {
        //        if (IsLineEmpty(row))
        //        {
        //            break;
        //        }

        //        // postoji elegantniji nacin da se ovo resi
        //        List<int> tempList = new List<int>();

        //        for (int j = 0; j < 4; j++) // hardkodovano!!!!!!!!
        //        {
        //            tempList.Add(grid[row, j]);
        //        }

        //        tempList.RemoveAll(x => x == 0);

        //        if (pravac == 1)
        //            tempList.Reverse(); // ukoliko treba da se nalepi na desnu stranu, da ne bi pisao ceo loop koji sabira unazad, ovde obrnemo niz, odradimo matematiku u lupu dole i onda se reversuje nazad.

        //        if (tempList.Count > 0) // ako moras ovoliko da branis kod, nesto ne valja
        //        {
        //            for (int i = 0; i < tempList.Count - 1; i++)
        //            {
        //                if (tempList[i] == tempList[i + 1])
        //                {
        //                    tempList[i] *= 2;
        //                    tempList[i + 1] = 0;
        //                }
        //            }
        //        }

        //        if (tempList.Count > 0) // ako moras ovoliko da branis kod, nesto ne valja
        //        {
        //            tempList.RemoveAll(x => x == 0); // ovo radi dva puta a nebi trebalo

        //        if (pravac == 1)
        //            tempList.Reverse(); // ponovo se reversuje array da se vrati na prvobitno stanje

        //            ResetRow(row);

        //            for (int i = 0; i < tempList.Count; i++)
        //            {
        //                grid[row, i] = tempList[i];
        //            }
        //        }


        //        /*
        //        switch (pravac)
        //        {
        //            case 1:
        //                int j = tempList.Count - 1;
        //                for (int i = grid.GetLength(row) - 1; i > (grid.GetLength(row) - 1 - tempList.Count); i--)
        //                {
        //                    grid[row, i] = tempList[j];
        //                    j--;
        //                }
        //                break;

        //            case -1:
        //                for (int i = 0; i < tempList.Count; i++)
        //                {
        //                    grid[row, i] = tempList[i];
        //                }
        //                break;

        //        }*/
        //    }
        //}


        public int ValidateUserInput()
        {
            ConsoleKeyInfo userInput = Console.ReadKey();

            switch (userInput.Key)
            {
                case ConsoleKey.UpArrow:
                    return 8;
                case ConsoleKey.DownArrow:
                    return 2;
                case ConsoleKey.LeftArrow:
                    return 4;
                case ConsoleKey.RightArrow:
                    return 6;
                default:
                    return 0;
            }
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Grid theGrid = new Grid();


            bool checkForChanges = true;

            while (true)
            {
                if (checkForChanges)
                {
                    theGrid.GenerateNewNumber();
                    theGrid.GenerateNewNumber();
                }


                theGrid.DisplayGrid();
                int userInput;

                while (true)
                {
                    Console.WriteLine("Use arrows to move the numbers!");

                    userInput = theGrid.ValidateUserInput();

                    if (userInput > 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input!");
                    }
                }
                for (int i = 0; i < 4; i++)
                {
                    theGrid.isRowChangedBoolArr[i] = theGrid.ShiftLine(i, userInput);
                }
                checkForChanges = theGrid.isRowChangedBoolArr.Any(x => x == true);

                //Console.Clear();
            }



            // dodati funckiju za gore i dole
            // sabiranje bodova
            // gameOver
            // unique boje za brojeve



        }
    }
}
