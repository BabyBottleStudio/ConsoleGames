using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Mastermind";
            bool replay = true;
            int attemptCount = Settings.AttemptCount;



            while (replay)
            {
                Display.Headder();
                Display.Description();

            Grid grid = new Grid();
            grid.DisplayGrid();

                Random randomNumber = new Random();

                SecretCombination secret = new SecretCombination(randomNumber);
                string secretCombination = secret.Value;


                if (Settings.cheatMode)
                {
                    Console.WriteLine(secret.ToString());
                    Display.SecretCombination(secretCombination);
                }


                bool isWin = false;

                for (int i = 1; i <= attemptCount; i++)
                {

                    // this method invokes the sytem for user input validation
                    UserInput.Validate(i);

                    PinCalculator.tempUserInput = UserInput.GuessCode; // test string koji ce se menjati kako prolaze testovi
                    PinCalculator.tempSecretCode = secretCombination; // test string koji ce se menjati kako prolaze testovi

                    (int black, int white) pins = PinCalculator.ReturnPins();

                    if (pins.black == Settings.SecredCodeLength) // ukoliko je pogodjena kombinacija
                    {
                        isWin = true; // pobeda!!!
                        break;
                    }

                    Display.PinInfoBlack(pins.black);
                    Display.PinInfoWhite(pins.white);
                }

                if (isWin)
                {
                    Display.GameOverWin();
                }
                else
                {
                    Display.GameOverLose();
                }

                Display.SecretCombination(secretCombination);
                Display.PlayAgainQuestion();

                char userAnswer = Console.ReadKey().KeyChar;

                if (userAnswer == 'd' || userAnswer == 'D')
                {
                    Console.Clear();
                }
                else
                {
                    replay = false;
                    Display.ExitGame();
                }
            } // while(replay) - END
        }
    }
}

/*
// funkcije
static string ReplaceDigit(string broj, int index, char a)
{
    char[] chars = broj.ToCharArray();
    chars[index] = a;
    string newNumber = new string(chars);

    return newNumber;
}

*/

/*
    ideja je da se temp stringovi menjaju i tako filtrira nepotrebno testiranje
    čim se dodele crni pinovi, vrednosti tih cifara se vraćaju na 0 i tako izbegavaju dalje testove
    Ovo je urađeno zbog belih pinova, jer su oni dosta kompleksniji za test. Ukoliko postoji više identičnih cifara, beli pinovi se zabroje i vraćaju veći broj.
    npr:
    traženi broj    1234
    uneti broj      5111

    tačan broj pinova 1
    pre sam dobijao 3, šta je netačan rezultat
    ovi stringovi koji se menjaju nakon svakog true, su (nadam se) rešili stvar
 */


/*
     На пример, ако је задана комбинација

                    црвена-црвена-плава-плава,
    а други играч покушава са
                    црвена-црвена-црвена-плава,

    први играч му даје две црне чиоде за погођене две црвене чиоде, и још једну црну чиоду за погођену плаву. За трећу црвену не добија ништа јер не постоји три црвене у решењу. Нема наговештаја да у комбинацији постоји још једна плава чиода. Што се тиче другог играча на то место може доћи било која од преосталих боја, осим црвене.[5]
 */

/*
while (true)
{
    Display.InputInstruction();
    Display.AttemptNumber(i);

    userInput = Console.ReadLine();

    bool isInputValid = InputTest(userInput);

    if (isInputValid)
    {
        break;
    }
    else
    {
        Display.InvalidEntryInformation();
    }

}
*/



/*
static bool InputTest(string input)
{
    //bool isValid = true;

    if (!UserInput.IsLengthValid(input))
    {
        return false;
    }

    //if (input.Length != 4)
    //{
    //isValid = false;
    //Console.WriteLine("GRSKA!!! Unos nema cetiri karaktera!");
    //return false;
    //}
    //Console.WriteLine("Unos ima cetiri karaktera!");
    //Console.WriteLine("-- nastavak provere --");

    //int tempInteger;
    //bool isValid = UserInput.IsContentValid(input);
    //bool isValid = int.TryParse(input, out int tempInteger);

    if (UserInput.IsContentValid(input))
    {
        //Console.WriteLine("Unos jeste ceo broj!");

        for (int i = 0; i < 4; i++)
        {
            int cifra = int.Parse(input[i].ToString());

            //if (char.IsDigit(input[i]) && cifra >= 1 && cifra <= 6)
            if (cifra < 1 || cifra > 6)
            {
                //Console.WriteLine($"{input[i]} ne odgovara!");
                //isValid = false;
                return false;
            }
        }
    }
    else
    {
        //Console.WriteLine("Unos nije ceo broj!");
        return false; // ovde je false
    }

    return true;
}
*/