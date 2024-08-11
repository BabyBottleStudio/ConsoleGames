using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftUni_DragonFight
{
    class InputHandling
    {
        public static bool ParseUserInputToIntWithinRange(string input, int min, int max, out int result)
        {
            var isInputValidInteger = int.TryParse(input, out int outputValue);

            if (isInputValidInteger)
            {
                if (outputValue > min && outputValue < max)
                {
                    result = outputValue;
                    return true;
                }
            }
            result = -99;
            return false;
        }



        public static bool IsInputValidLetter(string input, List<string> validLetterInputs, out int outputValue)
        {
            input = input.ToLower();

            if (validLetterInputs.Contains(input))
            {
                outputValue = (validLetterInputs.IndexOf(input) + 1) * -1; // returns negative numbers
                return true;
            }
            outputValue = -99;
            return false;
        }//  |_8||}o




    }
}

/*
        public static bool IsInputValidLetter(string input, List<string> validLetterInputs, out int outputValue)
{
    input = input.ToLower();

    if (validLetterInputs.Contains(input))
    {
        outputValue = (validLetterInputs.IndexOf(input) + 1) * -1; // returns negative numbers
        return true;
    }
    outputValue = 0;
    return false;
}//  |_8||}o


         public static int ParseUserInputToIntWithinRange(int min, int max)
{
    while (true)
    {
        var isInputValidInteger = int.TryParse(Console.ReadLine(), out int userInput);

        if (isInputValidInteger)
        {
            if (userInput > min && userInput < max)
            {
                return userInput;
            }
        }
        Console.WriteLine($"Input must be positive number, larger than {min} and smaller than {max}!");
    }
}



 */