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

        // dimenzije konzole
    }
}
