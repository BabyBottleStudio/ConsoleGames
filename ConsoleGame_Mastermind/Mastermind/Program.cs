using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMind
{
    class Program
    {
        static void Main(string[] args)
        {
            bool ponovo = true;


            while (ponovo)
            {
                Console.WriteLine("*****************************************************************************************************************");
                Console.WriteLine("************************************** W E L C O M E  T O *************************************       ()      ***");
                Console.WriteLine("***   @     @      @      @@@@   @@@@@   @@@@@   @@@@     @     @   @@@   @    @   @@@@     ***      (  )     ***");
                Console.WriteLine("***   @@   @@     @@@    @         @     @       @   @    @@   @@    @    @@   @   @   @    ***    [__ ___]   ***");
                Console.WriteLine("***   @ @ @ @    @   @    @@@@     @     @@@     @@@@     @ @ @ @    @    @ @  @   @    @   ***     |(*)(*)   ***");
                Console.WriteLine("***   @  @  @   @@@@@@@       @    @     @       @   @    @  @  @    @    @  @ @   @    @   ***     |    |    ***");
                Console.WriteLine("***   @     @   @     @   @@@@     @     @@@@@   @    @   @     @   @@@   @    @   @@@@@    ***     |____|    ***");
                Console.WriteLine("*********************************************************************************************** by BabyBottle ***");
                Console.WriteLine("*****************************************************************************************************************");







                const int brojPokusaja = 12;




                Console.WriteLine("Dobro došli u igru <MASTERMIND>");
                Console.WriteLine();
                Console.WriteLine("Pravila:");
                Console.WriteLine("Računar će izgenerisati šifru od četiri broja, koristeći samo cifre od 1 do 6.");
                Console.WriteLine($"Igrač ima {brojPokusaja} pokušaja da je otkrije.");
                Console.WriteLine("Nakon svakog pokušaja, dobićete pinove.");
                Console.WriteLine("Broj crnih pinova predstavlja koliko ste brojeva u nizu postavili na pravo mesto.");
                Console.WriteLine("Broj belih pinova predstavlja upotrebljen pravi broj, ali na pogrešnom mestu.");
                Console.WriteLine();


                // generise se niz od 4 broja. U tom nizu ucestvuju brojevi od 1 do 6

                string dobitniBroj = "";
                Random randomBroj = new Random();

                for (int i = 0; i < 4; i++)
                {
                    // generisanje šifre
                    int a = randomBroj.Next(1, 7);

                    dobitniBroj += ($"{a}");
                    //Console.Write($"{a} ");
                }

                //ovde možete da dodelite broj koji želite i testirate algoritam.
                //dobitniBroj = "2511"; 
                //Console.WriteLine($"Dobitni broj je: {dobitniBroj} -- feature za testiranje");



                bool isWin = false;
                for (int i = 1; i <= brojPokusaja; i++)
                {
                    string userInput; // = Console.ReadLine();

                    while (true)
                    {
                        Console.WriteLine("Unesite četvorocifreni broj, koristeći cifre od 1 do 6!");
                        Console.Write($"{i}) ");

                        userInput = Console.ReadLine();

                        bool isInputValid = inputTest(userInput);

                        if (isInputValid)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Unos nije validan. Pokušajte ponovo.");
                            Console.WriteLine();
                        }

                    }

                    /*
                        ideja je da se temp stringovi menjaju i tako filtrira nepotrebno testiranje
                        čim se dodele crni pinovi, vrednosti tih cifara se vraćaju na 0 i tako izbegavaju dalje testove
                        Ovo je urađeno zbog belih pinova, jer su oni dosta kompleksniji za test. Ukoliko postoji više identičnih cifara, beli pinovi se zabroje i vraćaju veći broj.
                        npr:
                        traženi broj    1234
                        uneti broj      5111

                        tačan broj pinova 1
                        pre sam dobijao 3, šta je netačan rezultat
                        ovi stringovi koji se menjaju nakon svakog true, su (nadam se) rešili stvar
                     */
                    string tempUserInput = userInput; // test string koji ce se menjati kako prolaze testovi
                    string tempDobitniBroj = dobitniBroj; // test string koji ce se menjati kako prolaze testovi


                    int blackPins = 0;
                    for (int j = 0; j < 4; j++)
                    {
                        if (dobitniBroj[j] == userInput[j])
                        {
                            blackPins++;

                            tempUserInput = replaceDigit(tempUserInput, j, '0');
                            tempDobitniBroj = replaceDigit(tempDobitniBroj, j, '0');
                        }
                    }

                    if (blackPins == 4) // ukoliko je pogodjena kombinacija
                    {
                        isWin = true; // pobeda!!!
                        break;
                    }


                    Console.WriteLine($"Crnih pinova: {blackPins}");


                    int whitePins = 0;
                    for (int j = 0; j < 4; j++)
                    {
                        for (int k = 0; k < 4; k++)
                        {
                            if (tempUserInput[j] != '0' && tempDobitniBroj[k] != '0' && tempUserInput[j] == tempDobitniBroj[k])
                            {
                                whitePins++;
                                tempUserInput = replaceDigit(tempUserInput, j, '0');
                                tempDobitniBroj = replaceDigit(tempDobitniBroj, k, '0');
                                break;
                            }
                        }
                    }
                    Console.WriteLine($"Belih pinova: {whitePins}");

                }

                if (isWin)
                {
                    Console.WriteLine();
                    Console.WriteLine("Č E S T I T A M O !");
                    Console.WriteLine("-- Pobedili ste --");
                    Console.WriteLine($"Pogodili ste tačnu kombinaciju koja je:");
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("KRAJ!");
                    Console.WriteLine($"Iskoristili ste svih {brojPokusaja} i niste pogodili");
                    Console.WriteLine($"Tačna kombinacija je:");
                }
                Console.WriteLine($"{dobitniBroj} ");
                Console.WriteLine();

                Console.WriteLine("Da li želite da igrate još jedanput?");
                Console.WriteLine("D/N");
                string odgovor = Console.ReadLine();
                
                odgovor = odgovor.ToLower();



                if (odgovor != "d")
                {
                    ponovo = false;
                    Console.WriteLine("Hvala puno i doviđenja!");
                }
                else
                {
                    Console.Clear();
                }
                    

            }



            // funkcije
            string replaceDigit(string broj, int index , char a)
            {
                char[] chars = broj.ToCharArray();
                chars[index] = a;
                string newNumber = new string(chars);

                return newNumber;
            }
            bool inputTest(string input)
            {
                bool isValid = true;
                if (input.Length != 4)
                {
                    isValid = false;
                    //Console.WriteLine("GRSKA!!! Unos nema cetiri karaktera!");
                    return isValid;
                }
                //Console.WriteLine("Unos ima cetiri karaktera!");
                //Console.WriteLine("-- nastavak provere --");

                int tempInteger;
                isValid = int.TryParse(input, out tempInteger);

                if (isValid)
                {
                    //Console.WriteLine("Unos jeste ceo broj!");

                    for (int i = 0; i < 4; i++)
                    {
                        int cifra = int.Parse(input[i].ToString());

                        //if (char.IsDigit(input[i]) && cifra >= 1 && cifra <= 6)
                        if (cifra < 1 || cifra > 6)
                        {
                            //Console.WriteLine($"{input[i]} ne odgovara!");
                            isValid = false;
                            return isValid;
                        }
                    }
                }
                else
                {
                    //Console.WriteLine("Unos nije ceo broj!");
                    return isValid; // ovde je false
                }

                return isValid;
            }
        }
    }
}



/*
     На пример, ако је задана комбинација

                    црвена-црвена-плава-плава,
    а други играч покушава са
                    црвена-црвена-црвена-плава,

    први играч му даје две црне чиоде за погођене две црвене чиоде, и још једну црну чиоду за погођену плаву. За трећу црвену не добија ништа јер не постоји три црвене у решењу. Нема наговештаја да у комбинацији постоји још једна плава чиода. Што се тиче другог играча на то место може доћи било која од преосталих боја, осим црвене.[5]
 */
