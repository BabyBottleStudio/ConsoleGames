using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    class Brod
    {
        public string name;
        public string type;
        public string icon; // "[ ][ ][ ][ ]"
        public string iconDestroyed; // "[X][X][X][X]"

        public int health;
        public bool isDestroyed = false;
        public int size;
        public int isVertical; // 0 horizontalan, 1 je vertikalan
        public int code;
        /*
            - Razarac 1 kom, zauzima 4 polja. code {41 41 41 41}
            - korveta 2 kom, zauzimaju po 3 polja {31 31 31} {32 32 32}
            - krstarica, 3 kom, zauzimaju po 2 polja  {21 21} {22 22} {23 23}
            - Podmornice, 4 kom, zauzimaju po 1 polja {11} {12} {13} {14}

        Neophodno je da svaki objekat ima svoj unikatan kod, jer na taj nacin sistem moze da napravi razliku kada je brod ceo unisten.

        */
        public int[,] shape;
        public int[,] suroundingField;



        public void getHit()
        {
            health--;
            if (health == 0)
            {
                isDestroyed = true;
                icon = iconDestroyed;
            }
        }

    }

    /*
    class Podmornica : Brod
    {
        public string type = "Podmornica";
        public string icon = "[ ]";
        public string iconDestroyed = "[X]";
        public int health = 1;
        public int size = 1;
        
    }

    class Krstarica : Brod
    {
        public string type = "Krstarica";
        public string icon = "[ ][ ]";
        public string iconDestroyed = "[X][X]";
        public int health = 2;
        public int size = 2;
    }

    class Korveta : Brod
    {
        public string type = "Korveta";
        public string icon = "[ ][ ][ ]";
        public string iconDestroyed = "[X][X][X]";
        public int health = 3;
        public int size = 3;
    }
    class Razarac : Brod
    {
        public string type = "Razarac";
        public string icon = "[ ][ ][ ][ ]";
        public string iconDestroyed = "[X][X][X][X]";
        public int health = 4;
        public int size = 4;
    }
    */
    class Program
    {
        static void Main(string[] args)
        {
            /*
            sea grid koji je 12x12. Prvi(0) i poslednji(11) red i prva(0) i poslednja(11) kolona se zanemaruju. U njih se upisuju jedino polja koja kontrolisu da se objekti ne dodiruju, ili ti vrednost br 99.
            
            Objekti:
            - Razarac 1 kom, zauzima 4 polja
            - Brod 2 kom, zauzimaju po 3 polja
            - krstarica, 3 kom, zauzimaju po 2 polja
            - Podmornice, 4 kom, zauzimaju po 1 polja

            Osobine brodova:
            Imaju svoje koordinate
            imaju podatak o okolnim poljima koja su slobodna
            kada se objekat unisti, ta polja se markiraju kao slobodna
            imaju svoju orjentaciju - horizontal ili vertical
            svaki ima svoj health bar.

            */

            //int[,] test = new int[12, 12];

           
            int[,] seaGrid = new int[12, 12];
            seaGrid.Initialize();

            // grid je 12x12
            // aktivna povrsina je 10x10 (koristi se od 1 do 11)
            // prvi i zadnji red i prva i zadnja kolona se zanemaruju
            /*
            {
                {0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0,0,0,0,0}
            };*/
            
            // kontrolni grid sluzi da se edituje tokom igre. Njegova uloga je da se cita prilikom ispisa na ekranu.
            // On sadrzi informacije koje korisnik treba da vidi a sve sta treba da ostane u tajnosti, edituje se i ostaje u seaGridu.

            int[,] kontrolniGrid = new int[12, 12];
            kontrolniGrid.Initialize();

            /////////////////////
            Brod razarac = new Brod();
            razarac.type = "Razarac";
            razarac.name = "RZ-41";
            razarac.icon = "[ ][ ][ ][ ]";
            razarac.iconDestroyed = "[X][X][X][X]";
            razarac.health = 4;
            razarac.size = 4;
            razarac.code = 41;


            /////////////////////
            Brod korveta1 = new Brod();
            korveta1.type = "Korveta";
            korveta1.name = "KO-31";
            korveta1.icon = "[ ][ ][ ]";
            korveta1.iconDestroyed = "[X][X][X]";
            korveta1.health = 3;
            korveta1.size = 3;
            korveta1.code = 31;


            Brod korveta2 = new Brod();
            korveta2.type = "Korveta";
            korveta2.name = "KO-32";
            korveta2.icon = "[ ][ ][ ]";
            korveta2.iconDestroyed = "[X][X][X]";
            korveta2.health = 3;
            korveta2.size = 3;
            korveta2.code = 32;

            /////////////////////
            Brod krstarica1 = new Brod();
            krstarica1.type = "Krstarica";
            krstarica1.name = "KR-21";
            krstarica1.icon = "[ ][ ]";
            krstarica1.iconDestroyed = "[X][X]";
            krstarica1.health = 2;
            krstarica1.size = 2;
            krstarica1.code = 21;


            Brod krstarica2 = new Brod();
            krstarica2.type = "Krstarica";
            krstarica2.name = "KR-22";
            krstarica2.icon = "[ ][ ]";
            krstarica2.iconDestroyed = "[X][X]";
            krstarica2.health = 2;
            krstarica2.size = 2;
            krstarica2.code = 22;


            Brod krstarica3 = new Brod();
            krstarica3.type = "Krstarica";
            krstarica3.name = "KR-23";
            krstarica3.icon = "[ ][ ]";
            krstarica3.iconDestroyed = "[X][X]";
            krstarica3.health = 2;
            krstarica3.size = 2;
            krstarica3.code = 23;



            //////////////////////////////
            Brod podmornica1 = new Brod();
            podmornica1.type = "Podmornica";
            podmornica1.name = "PD-11";
            podmornica1.icon = "[ ]";
            podmornica1.iconDestroyed = "[X]";
            podmornica1.health = 1;
            podmornica1.size = 1;
            podmornica1.code = 11;


            Brod podmornica2 = new Brod();
            podmornica2.type = "Podmornica";
            podmornica2.name = "PD-12";
            podmornica2.icon = "[ ]";
            podmornica2.iconDestroyed = "[X]";
            podmornica2.health = 1;
            podmornica2.size = 1;
            podmornica2.code = 12;


            Brod podmornica3 = new Brod();
            podmornica3.type = "Podmornica";
            podmornica3.name = "PD-13";
            podmornica3.icon = "[ ]";
            podmornica3.iconDestroyed = "[X]";
            podmornica3.health = 1;
            podmornica3.size = 1;
            podmornica3.code = 13;

            Brod podmornica4 = new Brod();
            podmornica4.type = "Podmornica";
            podmornica4.name = "PD-14";
            podmornica4.icon = "[ ]";
            podmornica4.iconDestroyed = "[X]";
            podmornica4.health = 1;
            podmornica4.size = 1;
            podmornica4.code = 14;

            //Brod[] flotila = {podmornica1, podmornica2, podmornica3, podmornica4, bojnikorveta1, krstarica2, krstarica3, brod1, korveta2, razarac}; nije moglo ovako jer je ulazilo u infinite loop. Mora od najveceg ka najmanjem.
            Brod[] flotila = { razarac, korveta2, korveta1, krstarica3, krstarica2, krstarica1, podmornica4, podmornica3, podmornica2, podmornica1 };

            // ovde se sve generise
            //for (int i = 0; i < 10; i++)
            foreach (Brod obj in flotila)
            {

                while (true)
                {
                    bool test = placementBroda(obj);
                    if (test)
                    {
                        break;
                    }
                }
            }
            // ovde se sve generise

            bool placementBroda(Brod brod)
            {
                // Definisanje da li je horizontalan ili vertikalan
                Random randomBroj = new Random();
                brod.isVertical = randomBroj.Next(2); // odlucuje se o rotaciji broda

                int moreRed = 0; // buduce koordinate
                int moreKolona = 0;

                if (brod.isVertical == 0) // rotiranje broda i definisanje matrice shodno tome
                {
                    // horizontalan
                    brod.shape = new int[3, brod.size + 2];
                }
                else
                {
                    // vertikalan
                    brod.shape = new int[brod.size + 2, 3];
                }


                // upis odgovarajucih vrednosti u objekat. 99 za okolna mesta i brod.code u sredinu
                int y = brod.shape.GetLength(0); // 6
                                                 // Console.WriteLine(y);
                int x = brod.shape.GetLength(1); // 3
                // Console.WriteLine(x);




                for (int i = 0; i < y; i++)
                {
                    for (int j = 0; j < x; j++)
                    {
                        // ako su max i min onda 99 - odnosno ako su spoljnja polja, upsiuje se vredonst 99. Ona sprecavaju da se desi dodirivanje sa  brodovima koji ce biti kasnije smesteni u grid
                        // ako su u sredini onda brod.code
                        if (i == 0 || i == y - 1 || j == 0 || j == x - 1)
                        // ukoliko je prvo i zadnje polje onda uposi 99 a ako ne onda upisi broj
                        {
                            brod.shape[i, j] = 99; // upisivanje granicnika koji sprecava dodirivanje
                        }
                        else
                        {
                            brod.shape[i, j] = brod.code;
                        }
                    }
                }

                // ogranicavanje koordinata u odnosu na oblik broda
                // ogranicavanje je bitno jer horizontalni razarac, koji zauzima 4 polja, ne moze da krene da se generise od kolone 9, zato sto ce izaci iz okvira igre





                if (brod.isVertical == 0)
                {
                    // generisanje koordinata
                    moreRed = randomBroj.Next(1, 11);
                    moreKolona = randomBroj.Next(1, 11 - brod.size);
                }
                else
                {
                    // generisanje koordinata
                    moreRed = randomBroj.Next(1, 11 - brod.size);
                    moreKolona = randomBroj.Next(1, 11);
                }
                // Testiranje da li su koordinate zauzete
                // ukoliko se naidje na drugi brod ili okolno polje markirano 99, sistem vraca false i izlazi iz petlje
                for (int i = 0; i < brod.size; i++)
                {
                    if (brod.isVertical == 0)
                    {
                        if (seaGrid[(moreRed), (moreKolona) + i] != 0) // horizontalna varijanta
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (seaGrid[(moreRed) + i, (moreKolona)] != 0) // vertikalna varijanta
                        {
                            return false;
                        }
                    }
                } // test END
                  //////////////////////////
                  // ukoliko ne moze, sve ispocetka
                  // ukoliko moze, super.
                brod.suroundingField = new int[2, 2 * brod.size + 6];
                // testiranje formule
                // 2*4+6 = 14
                // 2*3+6 = 12
                // 2*2+6 = 10
                // 2*1+6 = 8

                int counter = 0;

                for (int i = 0; i < y; i++)
                {
                    for (int j = 0; j < x; j++)
                    {
                        seaGrid[(moreRed - 1) + i, (moreKolona - 1) + j] = brod.shape[i, j];

                        if (i == 0 || i == y - 1 || j == 0 || j == x - 1)
                        // ukoliko je prvo i zadnje polje onda uposi 9 a ako ne onda upisi broj
                        {
                            brod.suroundingField[0, counter] = (moreRed - 1) + i;
                            brod.suroundingField[1, counter] = (moreKolona - 1) + j;
                            counter++;
                        }
                    }
                }

                return true;

            } // placementBroda END



            /// vracanje devetki na nulu
            int[,] resetNineToZero(int[,] mdArray)
            {
                //Console.WriteLine("    A  B  C  D  E  F  G  H  I  J");
                for (int i = 0; i < mdArray.GetLength(0); i++)
                {
                    //Console.Write($"{i}: ");
                    for (int j = 0; j < mdArray.GetLength(1); j++)
                    {
                        if (mdArray[i, j] == 99)
                        {
                            mdArray[i, j] = 0;
                        }
                    }

                }
                return mdArray;
            } // resetNineToZero END
            string[,] convertGridToString(int[,] array)
            {
                // treba da se preskoci 1 red, poslednji red, prva kolona i poslednja kolona

                string[,] stringArray = new string[10, 10];
                Console.WriteLine("    A  B  C  D  E  F  G  H  I  J");
                for (int i = 1; i <= 10; i++)
                {
                    Console.Write($"{i:D2} ");
                    for (int j = 1; j <= 10; j++)
                    {
                        switch (array[i, j])
                        {
                            case 0:
                                Console.Write("[ ]");
                                break;
                            case 11:
                            case 12:
                            case 13:
                            case 14:
                                Console.Write("[P]");
                                break;
                            case 21:
                            case 22:
                            case 23:
                                Console.Write("[B]");
                                break;
                            case 31:
                            case 32:
                                Console.Write("[S]");
                                break;
                            case 41:
                                Console.Write("[R]");
                                break;
                            case 88:
                                Console.Write("[x]");
                                break;
                            case 99:
                                Console.Write("[-]");
                                break;
                        }
                    }
                    Console.Write($" {i:D2} ");
                    Console.WriteLine();
                }
                Console.WriteLine("    A  B  C  D  E  F  G  H  I  J");
                return stringArray;
            } // convertGridToString END

//
// probaj da rešiš sa FOREACH
//

            void ispisGridaInt(int[,] mdArray)
            {
                //Console.WriteLine("    A  B  C  D  E  F  G  H  I  J");
                for (int i = 0; i < mdArray.GetLength(0); i++)
                {
                    //Console.Write($"{i}: ");
                    for (int j = 0; j < mdArray.GetLength(1); j++)
                    {
                        Console.Write(mdArray[i, j]);
                    }
                    Console.WriteLine();
                }
                return;
            } // ispisGridaInt END



            void resetSeaGrid()
            {
                seaGrid.Initialize();
                /*
                for (int i = 0; i < seaGrid.GetLength(0); i++)
                {
                    //Console.Write($"{i}: ");
                    for (int j = 0; j < seaGrid.GetLength(1); j++)
                    {
                        seaGrid[i, j] = 0;
                    }
                }
                */
            } //  void resetSeaGrid() END
            /// <summary>
            /// Resets the seaGrid to array[,] of zeros
            /// </summary>



            //// LOGIKA IGRE
            //// user input

            int hitCount = 20;
            int sumaGadjanja = 0;
            int sumaPromasaja = 0;

            int podmorniceKolicina = 4;
            int krstaricaKolicina = 3;
            int brodKolicina = 2;
            int razaracKolicina = 1;

            Brod unisteniBrod = null;


            resetNineToZero(seaGrid);
            //ispisGridaInt(seaGrid);
            convertGridToString(kontrolniGrid);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            ispisInformacija();


            while (hitCount > 0)
            {
               

                //char[] rowsArray = { '@', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j' }; // @ je dummy da zauzme nulu
                int userInputCol = 0;
                int userInputRow = 0;

                while (true)
                {
                    Console.Write($"{sumaGadjanja + 1})");
                    string userInput = Console.ReadLine();


                    if (userInput.Length > 0)
                    {
                        if (userInput.ToLower() == "hint")
                        {
                            convertGridToString(seaGrid);
                        }

                        if (char.IsLetter(userInput[0]))
                        {
                            //userInputCol = Array.IndexOf(rowsArray, userInput[0]);

                            //reši ovo preko ascii koda
                            char letterInput = char.ToLower(userInput[0]);
                            userInputCol = letterInput - 'a' + 1;
                        }

                        string temp = userInput.Substring(1);
                        int.TryParse(temp,out userInputRow);
                    }

                    if (userInputRow > 0 && userInputRow < 11 && userInputCol > 0 && userInputCol < 11)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid Input");
                        Console.WriteLine("Format unosa bi trebalo da bude: \nkolona (slova od A do J), pa odmah zatim broj reda (1-10), bez razmaka.");
                    }
                }

                //int userInputRow = int.Parse(Console.ReadLine());
                //Console.WriteLine($"red: {userInputRow} kolona: {userInputCol}");

                // gadjanje
                if (seaGrid[userInputRow, userInputCol] == 0) // promasaj
                {

                    seaGrid[userInputRow, userInputCol] = 99;
                    kontrolniGrid[userInputRow, userInputCol] = 99;

                    Console.Clear();
                    //Console.Beep();

                    convertGridToString(kontrolniGrid);

                    Console.WriteLine();
                    Console.WriteLine("Promašaj!!!");
                    Console.WriteLine(); // placeholder za Unistili ste bla bla
                    sumaPromasaja++;
                    sumaGadjanja++;

                }
                else if (seaGrid[userInputRow, userInputCol] > 10 && seaGrid[userInputRow, userInputCol] < 42)
                {
                    switch (seaGrid[userInputRow, userInputCol])
                    {
                        case 11:
                            if (shipDestroyed(podmornica1))
                            {
                                podmorniceKolicina--;
                            }

                            break;

                        case 12:
                            if (shipDestroyed(podmornica2))
                            {
                                podmorniceKolicina--;
                            }
                            break;

                        case 13:
                            if (shipDestroyed(podmornica3))
                            {
                                podmorniceKolicina--;
                            }
                            break;

                        case 14:

                            if (shipDestroyed(podmornica4))
                            {
                                podmorniceKolicina--;
                            }

                            break;

                        case 21:
                            if (shipDestroyed(krstarica1))
                            {
                                krstaricaKolicina--;
                            }
                            break;

                        case 22:
                            if (shipDestroyed(krstarica2))
                            {
                                krstaricaKolicina--;
                            }
                            break;

                        case 23:

                            if (shipDestroyed(krstarica3))
                            {
                                krstaricaKolicina--;
                            }
                            break;

                        case 31:
                            if (shipDestroyed(korveta1))
                            {
                                brodKolicina--;
                            }
                            break;

                        case 32:

                            if (shipDestroyed(korveta2))
                            {
                                brodKolicina--;
                            }
                            break;

                        case 41:
                            if (shipDestroyed(razarac))
                            {
                                razaracKolicina--;
                            }
                            break;
                    }

                    seaGrid[userInputRow, userInputCol] = 88;
                    kontrolniGrid[userInputRow, userInputCol] = 88;

                    Console.Clear();

                    convertGridToString(kontrolniGrid);
                    Console.WriteLine();
                    Console.WriteLine("Pogodak!!!");
                    if (unisteniBrod != null)
                    {
                        Console.WriteLine($"Uništili ste: {unisteniBrod.type}");
                        unisteniBrod = null;
                    }
                    else
                    {
                        Console.WriteLine();
                    }
                    Console.Beep(150, 500);
                    hitCount--;
                    sumaGadjanja++;
                    //Console.WriteLine($"Razarac health: {razarac.health}");

                }
                else
                {
                    Console.Clear();
                    convertGridToString(kontrolniGrid);
                    Console.WriteLine();
                    Console.WriteLine("Polje je ranije gadjano! Pokušajte ponovo!");
                    Console.WriteLine();
                    
                }

                ispisInformacija();

            }

            Console.Clear();
            convertGridToString(kontrolniGrid);
            Console.WriteLine();
            Console.WriteLine("P O B E D I L I   S T E ! ! !");
            Console.WriteLine();

            ispisInformacija();

//            Console.WriteLine($"Ukupno pokusaja: {sumaGadjanja}");
//            Console.WriteLine($"Promasaji: {sumaPromasaja}");
            double preciznost = 100.0 * sumaPromasaja/sumaGadjanja;
            Console.WriteLine($"Preciznost: {100.0 - preciznost:F2}%");

            Console.WriteLine();



            void ispisInformacija()
            {


                Console.WriteLine();
                Console.WriteLine($"{razarac.icon}");

                Console.WriteLine($"{korveta1.icon} - {korveta2.icon}");

                Console.WriteLine($"{krstarica1.icon} - {krstarica2.icon} - {krstarica3.icon}");

                Console.WriteLine($"{podmornica1.icon} - {podmornica2.icon} - {podmornica3.icon} - {podmornica4.icon}");
                Console.WriteLine();
                Console.WriteLine($"Preostalo razarača: {razaracKolicina}");
                Console.WriteLine($"Preostalo korveta: {brodKolicina}");
                Console.WriteLine($"Preostalo krstarica: {krstaricaKolicina}");
                Console.WriteLine($"Preostalo podmornica: {podmorniceKolicina}");
                Console.WriteLine();

                Console.WriteLine($"Ukupno pokušaja: {sumaGadjanja}");
                Console.WriteLine($"Ukupno promašaja: {sumaPromasaja}");
                /// <summary>
                /// Funkcija ispisuje informacije korisniku
                /// </summary>
            }



            bool shipDestroyed(Brod theBrod)
            {
                theBrod.health--;

                if (theBrod.health == 0)
                {
                    //Console.WriteLine($"Unistili ste plovilo tipa: {theBrod.type}");

                    for (int i = 0; i < theBrod.suroundingField.GetLength(1); i++)
                    {
                        int x = theBrod.suroundingField[0, i];
                        int y = theBrod.suroundingField[1, i];

                        seaGrid[x, y] = 99;
                        kontrolniGrid[x, y] = 99;
                    }

                    for (int i = 0; i < 3; i++)
                    {
                        Console.Beep();
                    }
                    theBrod.icon = theBrod.iconDestroyed;
                    unisteniBrod = theBrod;
                    return true;
                }
                return false;
            }

        }
    }
}
