using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind
{
    static class UserInput
    {
        public static string Input { get; set; }

        public static int[] userInput { get; set; }




        public static int[] ParseUserInput() => Input.Select(x => int.Parse(x.ToString())).ToArray();

        public static bool IsLengthValid() => Input.Length == Settings.SecredCodeLength;

        public static bool IsContentValid() => Input.All(x => char.IsDigit(x));


        public static void Validate(int guessNumber)
        {
            while (true)
            {
                Display.InputInstruction();
                Display.AttemptNumber(guessNumber);

                Input = Console.ReadLine();

                if (IsLengthValid() && IsContentValid())
                {
                    // prolaze cifre koje nisu od 1 do 6
                    userInput = ParseUserInput();
                    return;
                }
                else
                {
                    Display.InvalidEntryInformation();
                    
                }
            }
        }

    }
}
