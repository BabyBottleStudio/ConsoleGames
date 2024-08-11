using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftUni_DragonFight
{
    class Program
    {
        static Player player;
        static Dragon dragon;
        static Hero winner;

        public static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine($"Enter the amount of health points for the player!");
            player = new Player(InitializeTheHero(), "Warrior");


            PlayerInShopLoop();

            Console.Clear();
            Console.WriteLine($"Enter the amount of health points for the Dragon");
            dragon = new Dragon(InitializeTheHero(), "Dragon");

            int roundCount = 0;

            while (true)
            {
                Console.Clear();

                Display.BattleHeadder(++roundCount, player, dragon);
                Display.PlayerMenu();
                Console.WriteLine();

                var playerDealsDmg = PlayerBattleControl();

                dragon.GetHit(playerDealsDmg); // dragon gets damage

                if (!dragon.IsAlive())
                {
                    winner = player;
                    break;
                }
                Display.AfterActionInfo(dragon);

                var dragonDealsDmg = dragon.FireBreath.DealDamage(dragon);   // ovo treba refaktorisati da ne prima parametar
                player.GetHit(dragonDealsDmg);

                if (!player.IsAlive()) // player gets the damage
                {
                    winner = dragon;
                    break;
                }
                Display.AfterActionInfo(player);

                Console.WriteLine("Press any key for the next round!");
                Console.ReadKey(true);

            }

            Display.GameOverScreen(winner);
        }

        private static int InitializeTheHero()
        {
            while (true)
            {
                var userInput = Console.ReadLine();
                var test = InputHandling.ParseUserInputToIntWithinRange(userInput, 0, int.MaxValue, out int health);

                if (test)
                {
                    return health;
                }
                else
                {
                    Display.InvalidInputErrorMessage();
                }
            }
        }

        public static void PlayerInShopLoop()
        {
            while (true)
            {
                Console.Clear();
                Display.ShopHeadder();
                Shop.DisplayWeapon();
                Shop.DisplayMagics();
                Shop.DisplayItems();

                Display.ShopAdditionalOptions();

                Console.WriteLine();

                Display.PlayersInventoryHeadder();

                Display.PlayerMenu();

                // user input
                var userInput = HandlePlayerInputInShop();
                // 

                if (userInput == -2)
                {
                    player.UnequipAll();

                }
                else if (userInput == -1)
                {
                    // test da li odlazi sa praznim slotovima
                    if (PlayerHasEmptySlots())
                    {
                        if (IsValidChoice_EmptySlots())
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }

                }
                else if (userInput > 0 && userInput <= Shop.weapon.Count) // 1 - 3
                {
                    player.Equip(Shop.weapon[userInput - 1]);
                }
                else if (userInput <= Shop.weapon.Count + Shop.magic.Count) // 4 - 6
                {
                    player.Equip(Shop.magic[userInput - 1 - Shop.weapon.Count]);
                }
                else if (userInput <= Shop.weapon.Count + Shop.magic.Count + Shop.potion.Count) // 7 - 8
                {
                    player.Equip(Shop.potion[userInput - 1 - (Shop.weapon.Count + Shop.magic.Count)]);
                }
            }
        }

        public static int HandlePlayerInputInShop()
        {
            int upperLimit = Shop.weapon.Count + Shop.magic.Count + Shop.potion.Count + 1;
            int outputValue;
            var validLetterInputs = new List<string>() { "e", "r" };

            while (true)
            {
                string stringInput = Console.ReadLine();

                var isUserPressedValidLetter = InputHandling.IsInputValidLetter(stringInput, validLetterInputs, out outputValue);

                if (isUserPressedValidLetter)
                {
                    return outputValue;
                }

                var isUserPressedValidNumber = InputHandling.ParseUserInputToIntWithinRange(stringInput, 0, upperLimit, out outputValue);

                if (isUserPressedValidNumber)
                {
                    return outputValue;
                }
                Display.InvalidInputErrorMessage();
            }
        }

        private static bool IsValidChoice_EmptySlots()
        {
            bool validInput = false;
            var validLetters = new List<string>() { "b", "d", "e" };

            while (!validInput)
            {
                Display.AreYouSureOptionsMenu();

                var userChoice = Console.ReadLine();

                validInput = InputHandling.IsInputValidLetter(userChoice, validLetters, out int outputValue);

                switch (outputValue)
                {
                    case -1:
                        //validInput = true;
                        continue;

                    case -2:
                        player.LoadDefaultWeaponAndItem();
                        //validInput = true;
                        return true;

                    case -3:
                        return true;
                    default:
                        Display.InvalidInputErrorMessage();
                        break;
                }
            }
            return false;
        }

        private static bool PlayerHasEmptySlots() => Player.equippedWeapon == null || Player.equippedMagic == null || Player.equippedPotion == null;
        

        private static int PlayerBattleControl()
        {
            while (true)
            {
                InputHandling.ParseUserInputToIntWithinRange(Console.ReadLine(), 0, 4, out int userInput);

                switch (userInput)
                {
                    case 1:
                        if (Player.equippedWeapon != null)
                        {
                            return Player.equippedWeapon.DealDamage(player);
                        }
                        else
                        {
                            Display.NoSwordErrorMessage();
                            return 0;
                        }

                    case 2:
                        if (Player.equippedMagic != null)
                        {
                            return Player.equippedMagic.DealDamage(player);   // ovo treba refaktorisati da ne prima parametar
                        }
                        else
                        {
                            Display.NoMagicErrorMessage();
                            return 0;
                        }

                    case 3:
                        if (Player.equippedPotion != null)
                        {
                            if (player.MaxHealth == player.HealthPoints)
                            {
                                Display.NoNeedToHeal();
                                break;
                            }
                            else
                            {
                                Player.equippedPotion.Heal(player);   // ovo treba refaktorisati da ne prima parametar
                                Player.RemovePotionIfEmpty();
                                return 0;
                            }
                        }
                        else
                        {
                            Display.NoHealingErrorMessage();
                            break;
                        }

                    default:
                        Display.InvalidInputErrorMessage();
                        break;
                }
            }
        }





    }
}
