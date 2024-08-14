using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// renamed UserInput class to Input
// renamed userInput variabile to usersCode
// fixed validation of the user input to work with array, and to work in the game

namespace Mastermind
{
    static class Input
    {
        public static string input { get; set; }

        public static int[] usersCode { get; set; }




        public static int[] ParseUserInput() => input.Select(x => int.Parse(x.ToString())).ToArray();

        public static bool AreValuesValid() => usersCode.All(x => x > 0 && x <= Settings.ColorsCount);

        public static bool IsLengthValid() => input.Length == Settings.SecredCodeLength;

        public static bool IsContentValid() => input.All(x => char.IsDigit(x));



        public static void Validate(int guessNumber)
        {
            while (true)
            {
                Display.InputInstruction();
                Display.AttemptNumber(guessNumber);

                input = Console.ReadLine();

                if (IsLengthValid() && IsContentValid())
                {
                    usersCode = ParseUserInput();
                    if (AreValuesValid())
                    {
                        return;
                    }
                }
                Display.InvalidEntryInformation();
            }
        }



    }
}
