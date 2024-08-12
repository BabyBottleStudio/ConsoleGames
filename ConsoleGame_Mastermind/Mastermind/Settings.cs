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


        public readonly static bool cheatMode = false;
        //public static bool CheatMode() => cheatMode;

        // dimenzije konzole
    }
}
