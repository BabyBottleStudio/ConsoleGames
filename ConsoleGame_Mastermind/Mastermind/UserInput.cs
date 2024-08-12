using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind
{
    static class UserInput
    {
        static string input {get; set; }


        
        public static int[] Parse(string userInput) => userInput.Select(x => int.Parse(x.ToString())).ToArray();

        public static bool IsLengthValid(string userInput) => input.Length == 4;

        public static bool IsContentValid(string userInput) => userInput.All(x => char.IsDigit(x));
    }
}
