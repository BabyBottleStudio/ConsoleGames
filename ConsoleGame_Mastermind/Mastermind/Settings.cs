using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind
{
    static class Settings
    {
        private static int attemptCount = 12;
        public static int AttemptCount { get => attemptCount; private set => attemptCount = value; }

        private static int secredCodeLength = 4;
        public static int SecredCodeLength { get => secredCodeLength; private set => secredCodeLength = value; }

        private static int colorsCount = 6;
        public static int ColorsCount { get => colorsCount; private set => colorsCount = value; }

        public readonly static bool cheatMode = true;
        //public static bool CheatMode() => cheatMode;

        // dimenzije konzole
    }
}
