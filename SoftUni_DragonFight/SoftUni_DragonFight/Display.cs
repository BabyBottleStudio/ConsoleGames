using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftUni_DragonFight
{
    static class Display
    {
        public static ConsoleColor ColorEmptySlot = ConsoleColor.Red;


        public static void InvalidInputErrorMessage()
        {
            Console.WriteLine($"Invalid input. Please try again.");
        }

        public static void NoSwordErrorMessage()
        {
            Console.WriteLine($"You have no idea how stupid you look while trying to hit the dragon with an empty hand!!! \nYou should know better than to attack the dragon with nothing.");
        }

        public static void NoMagicErrorMessage()
        {
            Console.WriteLine($"This is ridiculous!!! You were in shop and did not grab any of the free stuff there. \nI hope you learned valuable lesson.");
        }

        public static void NoHealingErrorMessage()
        {
            //Console.WriteLine();
            Console.WriteLine("Can't heal! No potions left!");
        }

        public static void NoNeedToHeal()
        {
            Console.WriteLine("No need to heal, save it for later!");
        }

        public static void HealInfo(string potionName, string whoUsedName, int healAmt, int remainingHealth, int countInfo)
        {
            Console.WriteLine();
            Console.WriteLine($"{potionName} used!!!");
            Console.WriteLine($"{whoUsedName} restored {healAmt} points and now have {remainingHealth} HP.");
            Console.WriteLine($"{whoUsedName} have {countInfo} healing potions left.");
        }



        /// <summary>
        /// Mehtod that displays the info about the atack to the player.
        /// </summary>
        /// <param name="attackDamage"></param>
        /// <param name="weaponName"></param>
        public static void AttackInfo(string heroName, string weaponName, int attackDamage)
        {
            Console.WriteLine();
            Console.WriteLine($"{heroName} used {weaponName} and dealt {attackDamage} points of damage!");
        }

        public static void CriticalHitInfo(int intitialAttackDamage, int damageAfterCrit)
        {
            Console.WriteLine();
            Console.WriteLine("Critical hit!!!");
            Console.WriteLine($"Initial damage {intitialAttackDamage}");
            Console.WriteLine($"Critical damage {damageAfterCrit}");
        }

        public static void AfterActionInfo(Hero hero)
        {
            Console.WriteLine();
            Console.WriteLine($"{hero.Name} has {hero.HealthPoints} HP left.");
        }

        /// <summary>
        /// Displays the info if no damage is dealt due to chance of missing.
        /// </summary>
        public static void MissInfo(string heroName, string weaponName)
        {
            Console.WriteLine();
            Console.WriteLine("Miss!!!");
            Console.WriteLine($"{heroName} used {weaponName} and dealt no damage!");
            Console.WriteLine("What a shame");
        }

        public static void GameOverScreen(Hero winner)
        {
            Console.WriteLine();

            Console.WriteLine("---------------------------");

            Console.WriteLine("Battle is over!!!");
            Console.WriteLine($"{winner.Name} is the winner.");

            if (winner.ClasName == "Dragon")
            {
                DrawDragon();
            }
            else
            {
                DrawWarrior();
            }

            Console.ReadKey(true);
        }

        public static void PlayerDropsObjectFromTheInventory(string heroName, string inventoryObjectName)
        {
            Console.WriteLine();
            Console.WriteLine($"{heroName} drops {inventoryObjectName}.");
        }


        public static void PlayerMenu()
        {
            if (Player.equippedWeapon != null)
            {
                Player.equippedWeapon.Description();
            }
            else
            {
                EmptySlot(1);
            }

            if (Player.equippedMagic != null)
            {
                Player.equippedMagic.Description();
            }
            else
            {
                EmptySlot(2);
            }

            if (Player.equippedPotion != null)
            {
                Player.equippedPotion.Description();
            }
            else
            {
                EmptySlot(3);
            }
        }

        private static void EmptySlot(int slotIndex)
        {
            Console.ForegroundColor = ColorEmptySlot;
            Console.WriteLine($"{slotIndex}. << empty >>");
            Console.ResetColor();
        }


        public static void ShopHeadder()
        {
            Console.WriteLine("*********************************************");
            Console.WriteLine("**             S  H  O  P                  **");
            Console.WriteLine("*********************************************");
            Console.WriteLine("Select the item by typing the order number.");
            Console.WriteLine();
        }

        public static void BattleHeadder(int round, Player warrior, Dragon dragon)
        {
            Console.WriteLine();
            Console.WriteLine("*************************************");
            Console.WriteLine($"**          R O U N D  {round:D2}.         **");
            Console.WriteLine("*************************************");
            Console.WriteLine($"Player:{warrior.HealthPoints}HP             Dragon:{dragon.HealthPoints}HP");
            Console.WriteLine();
        }

        public static void PlayersInventoryHeadder()
        {
            Console.WriteLine("             ~~~ I I ~~~"); // napravi headder
            Console.WriteLine("****   Players current inventory  ****"); // napravi headder
            Console.WriteLine("             ~~~ i_i ~~~"); // napravi headder
        }

        public static void ShopAdditionalOptions()
        {

            Console.WriteLine("[R]eset to default");
            Console.WriteLine("[E]xit to battle.");
        }

        public static void AreYouSureOptionsMenu()
        {
            Console.WriteLine("You did not take all the equipment that you need! Dragon is going to kick your butt.");
            Console.WriteLine("Are you sure you want to leave prematurely?");
            Console.WriteLine();
            Console.WriteLine("Options:");
            Console.WriteLine("[B]ack to the shop!");
            Console.WriteLine("[D]efault values for the empty slots and exit. But be warned, those are crap!");
            Console.WriteLine("[E]xit anyway.");
            Console.WriteLine();
        }

        public static void ColorUiElement(string text, ConsoleColor color, bool isWriteLine)
        {
            Console.ForegroundColor = color;
            if (isWriteLine)
            {
                Console.WriteLine(text);
            }
            else
            {
                Console.Write(text);
            }
            Console.ResetColor();

        }

        static void DrawDragon()
        {
            string[] dragon = new string[]
            {
            "             ___====-_  _-====___",
            "       _--^^^#####//      \\\\#####^^^--_",
            "    _-^##########// (    ) \\\\##########^-_",
            "   -############//  |\\^^/|  \\\\############-",
            " _/############//   (@::@)   \\\\############\\_",
            "/#############((     \\\\//     ))#############\\",
            "-###############\\\\    (oo)    //###############-",
            "-#################\\\\  / \"\" \\  //#################-",
            "-###################\\\\/  ()  \\\\//###################-",
            "_#/|##########/\\\\######( /\\ )######/\\\\##########|\\#_",
            "|/ |#/\\#/\\#/\\#/  \\#/\\#\\###/  \\#/\\#/\\#/\\#/\\#/\\|/"
            };

            foreach (string line in dragon)
            {
                Console.WriteLine(line);
            }
        }

        static void DrawWarrior()
        {
            string[] warrior = new string[]
            {
            "  / \\",
            "  | |",
            "  |.|",
            "  |.|",
            "  |:|      __",
            ",_|:|_,   /  )",
            "  (Oo    / _I_",
            "   +\\ \\  || __|",
            "      \\ \\||___|",
            "        \\ /.:.\\-\\",
            "         |.:. /-----\\",
            "         |___|::oOo::|",
            "         /   |:<_T_>:|",
            "        |_____\\ ::: /",
            "         | |  \\ \\:/",
            "         | |   | |",
            "         \\ /   | \\___",
            "         / |   \\_____\\",
            "         `-'"
            };

            foreach (string line in warrior)
            {
                Console.WriteLine(line);
            }
        }

    }
}
