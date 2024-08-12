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
            bool replay = true;
            int attemptCount = Settings.AttemptCount;



            while (replay)
            {
                Display.Headder();
                Display.Description();

                
                string secretCombination = new SecretCombination().Value;
                
                               
                if (Settings.cheatMode)
                {
                    Display.SecretCombination(secretCombination);
                }


                bool isWin = false;

                for (int i = 1; i <= attemptCount; i++)
                {
                    string userInput; // = Console.ReadLine();

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



                    string tempUserInput = userInput; // test string koji ce se menjati kako prolaze testovi
                    string tempDobitniBroj = secretCombination; // test string koji ce se menjati kako prolaze testovi


                    int blackPins = 0;
                    for (int j = 0; j < 4; j++)
                    {
                        if (secretCombination[j] == userInput[j])
                        {
                            blackPins++;

                            tempUserInput = ReplaceDigit(tempUserInput, j, '0');
                            tempDobitniBroj = ReplaceDigit(tempDobitniBroj, j, '0');
                        }
                    }

                    if (blackPins == 4) // ukoliko je pogodjena kombinacija
                    {
                        isWin = true; // pobeda!!!
                        break;
                    }


                    Display.PinInfoBlack(blackPins);


                    int whitePins = 0;
                    for (int j = 0; j < 4; j++)
                    {
                        for (int k = 0; k < 4; k++)
                        {
                            if (tempUserInput[j] != '0' && tempDobitniBroj[k] != '0' && tempUserInput[j] == tempDobitniBroj[k])
                            {
                                whitePins++;
                                tempUserInput = ReplaceDigit(tempUserInput, j, '0');
                                tempDobitniBroj = ReplaceDigit(tempDobitniBroj, k, '0');
                                break;
                            }
                        }
                    }

                    Display.PinInfoWhite(whitePins);

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

                string userAnswer = Console.ReadLine();

                userAnswer = userAnswer.ToLower();

                if (userAnswer != "d")
                {
                    replay = false;
                    Display.ExitGame();
                }
                else
                {
                    Console.Clear();
                }


            }


        }

        // funkcije
        static string ReplaceDigit(string broj, int index, char a)
        {
            char[] chars = broj.ToCharArray();
            chars[index] = a;
            string newNumber = new string(chars);

            return newNumber;
        }

        static bool InputTest(string input)
        {
            //bool isValid = true;

            if (input.Length != 4)
            {
                //isValid = false;
                //Console.WriteLine("GRSKA!!! Unos nema cetiri karaktera!");
                return false;
            }
            //Console.WriteLine("Unos ima cetiri karaktera!");
            //Console.WriteLine("-- nastavak provere --");

            //int tempInteger;
            bool isValid = int.TryParse(input, out int tempInteger);

            if (isValid)
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
    }
}


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
