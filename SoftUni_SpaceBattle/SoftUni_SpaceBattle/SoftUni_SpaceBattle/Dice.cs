using System;

namespace SoftUni_SpaceBattle
{
    static class Dice
    {
        static Random rnd = new Random();

        public static int Roll() => rnd.Next(1, 101);

        public static int GetRandomIndex(int maxIndex) => rnd.Next(maxIndex);
        public static int GenerateDamage(int min, int max) => rnd.Next(min, max + 1);

    }
}
