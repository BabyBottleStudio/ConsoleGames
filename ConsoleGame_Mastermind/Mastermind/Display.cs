using System;


namespace Mastermind

{
    static class Display
    {
        public static void Headder()
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
        }

        public static void Description()
        {
            Console.WriteLine("Dobro došli u igru <MASTERMIND>");
            Console.WriteLine();
            Console.WriteLine("Pravila:");
            Console.WriteLine($"Računar će izgenerisati šifru od četiri broja, koristeći samo cifre od 1 do {Settings.ColorsCount}.");
            Console.WriteLine($"Igrač ima {Settings.AttemptCount} pokušaja da je otkrije.");
            Console.WriteLine("Nakon svakog pokušaja, dobićete pinove.");
            Console.WriteLine("Broj crnih pinova predstavlja koliko ste brojeva u nizu postavili na pravo mesto.");
            Console.WriteLine("Broj belih pinova predstavlja upotrebljen pravi broj, ali na pogrešnom mestu.");
            Console.WriteLine();
        }


        public static void SecretCombination(string secretCombination)
        {
            Console.WriteLine(secretCombination);
            Console.WriteLine();
        }


        public static void AttemptNumber(int attempt)
        {
            Console.Write($"{attempt}) ");
        }

        public static void PinInfoBlack(int blackPinCount)
        {
            Console.WriteLine($"Crnih pinova: {blackPinCount}");
        }

        public static void PinInfoWhite(int whitePinCount)
        {
            Console.WriteLine($"Belih pinova: {whitePinCount}");
        }


        public static void InputInstruction()
        {
            Console.WriteLine($"Unesite četvorocifreni broj, koristeći cifre od 1 do {Settings.ColorsCount}!");
        }

        public static void InvalidEntryInformation()
        {
            Console.WriteLine("Unos nije validan. Pokušajte ponovo.");
            Console.WriteLine();
        }


        public static void GameOverWin()
        {
            Console.WriteLine();
            Console.WriteLine("Č E S T I T A M O !");
            Console.WriteLine("-- Pobedili ste --");
            Console.WriteLine($"Pogodili ste tačnu kombinaciju koja je:");
        }

        public static void GameOverLose()
        {
            Console.WriteLine();
            Console.WriteLine("KRAJ!");
            Console.WriteLine($"Iskoristili ste svih {Settings.AttemptCount} i niste pogodili");
            Console.WriteLine($"Tačna kombinacija je:");
        }

        public static void PlayAgainQuestion()
        {
            Console.WriteLine("Da li želite da igrate još jedanput?");
            Console.WriteLine("D/N");
        }

        public static void ExitGame()
        {
            Console.WriteLine("Hvala puno i doviđenja!");
        }

    }
}
