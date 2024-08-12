using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind
{
    class SecretCombination
    {
        Random randomBroj = new Random();

        string value;
        public string Value { get => value; }

        public SecretCombination()
        {
            value = GenerateCombination();
        }

        string GenerateCombination()
        {
            StringBuilder output = new StringBuilder();
            for (int i = 0; i < Settings.SecredCodeLength; i++)
            {
                int a = randomBroj.Next(1, 7);
                output.Append(a);
            }
            return output.ToString();
        }

    }
}
