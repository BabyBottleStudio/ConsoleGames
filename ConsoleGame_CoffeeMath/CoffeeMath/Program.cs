using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CoffeeMath
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("************************************************************************************************************");
            Console.WriteLine("************************************** W E L C O M E  T O ********************************       ()      ***");
            Console.WriteLine("***    @@@@   @@@@    @@@@@  @@@@@  @@@@@  @@@@@   @     @    @@@@   @@@@@@@  @    @   ***      (  )     ***");
            Console.WriteLine("***   @      @    @   @      @      @      @       @@   @@   @    @     @     @    @   ***    [__ ___]   ***");
            Console.WriteLine("***   @      @    @   @@@@   @@@@   @@@    @@@     @ @ @ @   @@@@@@     @     @@@@@@   ***     |(*)(*)   ***");
            Console.WriteLine("***   @      @    @   @      @      @      @       @  @  @   @    @     @     @    @   ***     |    |    ***");
            Console.WriteLine("***    @@@@   @@@@    @      @      @@@@@  @@@@@   @     @   @    @     @     @    @   ***     |____|    ***");
            Console.WriteLine("****************************************************************************************** by BabyBottle ***");
            Console.WriteLine("************************ Rešite 20 jednostavnih zadataka za što kraće vreme!!! *****************************");
            Console.WriteLine();
            Console.WriteLine("Pravila: Dobićete 20 jednostavnih zadataka jedan po jedan. Cilj je rešiti ih za što kraće vreme.\nZa svaki netačan rezultat ćete dobiti 5 kaznenih sekundi koje će se sabrati sa krajnjim rezultatom!");
            Console.WriteLine("Vreme počinje da se meri čim unesete rezultat za prvi zadatak");
            Console.WriteLine();



            int tacnih = 0;
            int pogresnih = 0;

            Stopwatch timer = new Stopwatch();

            // generisanje zadataka

            string[] zadaciString = new string[20]; // zadaci generisani u string
            int[] zadaci = new int[20]; // zadaci generisani u integer
            int[] racOpArr = new int[20]; // racunske operacije 0-sabiranje; 1-oduzimanje; 2-mnozenje
            int[] ocekivaniRezultati = new int[20]; // array rezultata izgenerisanih zadataka



            int brojac = 0;
            while (brojac < 20)
            {
                Random randomBroj = new Random();
                int a = randomBroj.Next(10, 100); // generisanje broja cije ce cifre posluziti za generisanje zadataka

                string c = a.ToString();
                // racunska operacija
                Random operacija = new Random();
                int racOp = randomBroj.Next(3); // nasumicno biranje racunske operacije

                racOpArr[brojac] = racOp;                // belezenje operacije u poseban array

                if (!zadaciString.Contains(c))
                {
                    zadaciString[brojac] = c; // ubelezi ovog u string array
                    zadaci[brojac] = a; // ubelezi ovog u int array koji nam treba zbog matematike
                    brojac++;
                }
            }



            for (int i = 0; i < 20; i++)
            {
                // treba izvuci broj desetica i jedinica iz integera

                int a = zadaci[i] / 10; // desetice kao cifra
                int b = zadaci[i] % 10; // jedinice kao cifra



                switch (racOpArr[i])
                {
                    case 0: // sabiranje
                        zadaciString[i] = ($"{a} + {b}");  //zadaciString[i][0] + "+" + zadaciString[i][1];
                        ocekivaniRezultati[i] = a + b;
                        break;

                    case 1: // oduzimanje
                        if (a >= b)
                        {
                            zadaciString[i] = ($"{a} - {b}"); //zadaci[i][0] + "-" + zadaci[i][1];
                            ocekivaniRezultati[i] = a - b;
                        }
                        else
                        {
                            zadaciString[i] = ($"{b} - {a}");
                            ocekivaniRezultati[i] = b - a;
                        }
                        break;

                    case 2: // mnozenje
                        zadaciString[i] = ($"{a} * {b}");
                        ocekivaniRezultati[i] = a * b;
                        break;
                }
                //Console.WriteLine($"{zadaciString[i]} = {ocekivaniRezultati[i]}");
            }

            // generisanje zadataka end



            for (int i = 0; i < 20; i++)
            {

                Console.WriteLine($"{i + 1}) {zadaciString[i]}");
                int userInput;
                bool inputInt = int.TryParse(Console.ReadLine(), out userInput);



                if (i == 0)
                {
                    timer.Start();
                }

                if (userInput == ocekivaniRezultati[i] && inputInt)
                {
                    tacnih++;
                    Console.WriteLine("Tacno!!!");
                }
                else
                {
                    pogresnih++;
                    // dodatak na vreme
                    Console.WriteLine("Greska!!! +5 sekundi za vas");
                    Console.WriteLine($"Tačan odgovor je: {ocekivaniRezultati[i]}");
                }
            }

            timer.Stop();

            double seconds = timer.Elapsed.TotalSeconds;

            Console.WriteLine();
            Console.WriteLine("Kraj!");
            Console.WriteLine($"Bilo vam je potrebno {seconds:F2} sekundi.");
            //Console.WriteLine("{0:mm\\:ss\\:F2}", timer.Elapsed);

            Console.WriteLine($"Broj tačnih odgovora: {tacnih}.");
            Console.WriteLine($"Broj pogrešnih odgovora: {pogresnih}.");
            Console.WriteLine($"Vaše finalno vreme je {seconds:F2} sec + {pogresnih} * 5 (kaznenih s)  = {seconds + pogresnih * 5.0:F2} sekundi!");
            Console.WriteLine();

            Console.ReadKey();
            /*
            Console.WriteLine("Želite li da igrate ponovo? (y/n)");
            string odgovor = Console.ReadLine();
            if (odgovor == "y")
            {
                Main("");
            }
            */




        }
    }
}
