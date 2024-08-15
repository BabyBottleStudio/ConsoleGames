using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

// switched to Regular Expression input test. It is much easier and everything is check withing one test, instead of 3.
// ditched the int[] system. System is checking strings and chars now
// created the PinCalculatorStaticClass and migrated the methods for returning pin values there.
// black and white pins are now a touple of numbers

namespace Mastermind
{
    static class UserInput
    {
        public static string GuessCode { get; set; }

        //public static int[] UsersCode { get; set; }

        private static string regex = @"[1-" + Settings.ColorsCount + "]{" + Settings.SecredCodeLength + "}";
        //string regex = $"[1-{Settings.ColorsCount}]{Settings.SecredCodeLength}"; // [1-6]{4} // use 4 numbers from 1 - 6 
        //public static int[] ParseUserInput() => input.Select(x => int.Parse(x.ToString())).ToArray();

        public static void Validate(int currentGuessAttempt)
        {
            while (true)
            {
                Display.InputInstruction();
                Display.AttemptNumber(currentGuessAttempt);

                string input = Console.ReadLine();

                if (Regex.IsMatch(input, regex))
                {
                    //Console.WriteLine("true");
                    GuessCode = input;
                    return;
                }
                Display.InvalidEntryInformation();
            }
        }

        //public static bool AreValuesValid() => UsersCode.All(x => x > 0 && x <= Settings.ColorsCount);

        //public static bool IsLengthValid() => input.Length == Settings.SecredCodeLength;

        //public static bool IsContentValid() => input.All(x => char.IsDigit(x));

        /*
        public static void Validate2(int currentGuessAttempt)
        {
            while (true)
            {
                Display.InputInstruction();
                Display.AttemptNumber(currentGuessAttempt);

                input = Console.ReadLine();

                if (IsLengthValid() && IsContentValid())
                {
                    UsersCode = ParseUserInput();
                    if (AreValuesValid())
                    {
                        return;
                    }
                }
                Display.InvalidEntryInformation();
            }
        }

        */

    }
}
