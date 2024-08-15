namespace Mastermind
{
    public static class PinCalculator
    {
        public static string tempUserInput;
        public static string tempSecretCode;

        public static (int, int) ReturnPins() => (Black(), White());


        private static int Black()
        {
            int blackPins = 0;

            for (int i = 0; i < Settings.SecredCodeLength; i++)
            {
                if (tempUserInput[i] == tempSecretCode[i])
                {
                    blackPins++;

                    tempUserInput = ReplaceDigit(tempUserInput, i, '0'); // tempUserInput.Replace(tempUserInput[i], '0');
                    tempSecretCode = ReplaceDigit(tempSecretCode, i, '0'); //tempSecretCode.Replace(tempSecretCode[i], '0');

                }
            }

            return blackPins;
        }

        //public static bool IsWin(int blackPins) => blackPins == Settings.SecredCodeLength;

        private static int White()
        {
            int whitePins = 0;
            for (int i = 0; i < Settings.SecredCodeLength; i++)
            {
                for (int j = 0; j < Settings.SecredCodeLength; j++)
                {
                    if (tempUserInput[i] != '0' && tempSecretCode[j] != '0' && tempUserInput[i] == tempSecretCode[j])
                    {
                        whitePins++;
                        tempUserInput = ReplaceDigit(tempUserInput, i, '0'); // tempUserInput.Replace(tempUserInput[i], '0');
                        tempSecretCode = ReplaceDigit(tempSecretCode, j, '0'); //tempSecretCode.Replace(tempSecretCode[i], '0');

                        break;
                    }
                }
            }
            return whitePins;
        }


        private static string ReplaceDigit(string stringToChange, int indexToReplace, char charReplacement)
        {
            char[] characterOutput = stringToChange.ToCharArray();
            characterOutput[indexToReplace] = charReplacement;

            return new string(characterOutput);
        }

    }
}