using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind
{
    class SecretCombination
    {
        private readonly Random randomNumber; // udemy 96. it is better practice to initialize the random through constructor than here.


        int[] secretCode;
        public int[] SecretCode { get => secretCode; private set => secretCode = value; }

        string value;
        public string Value { get => value; }



        public SecretCombination(Random random)
        {
            randomNumber = random;
            SecretCode = GenerateCombination();
            value = ToString();
        }

        public override string ToString()
        {
            return String.Join("", SecretCode);
        }

        int[] GenerateCombination()
        {
            int[] output = new int[Settings.SecredCodeLength];
            return output.Select(x => x = randomNumber.Next(1, Settings.ColorsCount + 1)).ToArray();
        }

        /*
        string GenerateCombination()
        {
            StringBuilder output = new StringBuilder();
            for (int i = 0; i < Settings.SecredCodeLength; i++)
            {
                int a = randomNumber.Next(1, 7);
                
                output.Append(a);
            }
            return output.ToString();
        }
        */
    }
}
