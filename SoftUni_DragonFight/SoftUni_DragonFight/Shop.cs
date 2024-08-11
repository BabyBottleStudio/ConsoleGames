using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftUni_DragonFight
{
    static class Shop
    {
        public static List<Weapon> weapon = new List<Weapon>()
        {
            new Weapon("Sword of the Painful Colonoskopy", 10, 16, 75),
            new Weapon("Dragon's Bane", 50, 80, 25),
            new Weapon("Golden Toothpick", 5, 10, 99)
        };

        public static List<Magic> magic = new List<Magic>()
        {
            new Magic("Lightning McStrike", 15, 31, 50),
            new Magic("Lightning of Doom", 40, 50, 30),
            new Magic("Armageddon", 90, 110, 5)
        };


        public static List<Item> potion = new List<Item>()
        {
            new Item("Potion of Infinite Botox", 2, (15, 35)),
            new Item("Small Potion", 5, (1, 10))
        };


        public static Weapon fireBreath = new Weapon("Inferno Breath", 7, 18, 100, 0); // reserved for Dragon



        /*
        public static void DisplayShopItems(Warrior customer)
        {
            int[] itemsCount = new int[] {weapon.Count, magic.Count, potion.Count};

            for (int i = 0; i < 3; i++)
            {
                switch (i)
                {
                    case 0:
                        Console.WriteLine("o=i===>   Swords   <===i=o");
                        break;

                    case 1:
                        Console.WriteLine("-----   Magic  -----");
                        break;

                    case 2:
                        Console.WriteLine("-----  Healing Potions  -----");
                        break;
                }

                for (int j = 0; j < itemsCount[i]; j++)
                {

                }


            }
        }
        */

        public static void DisplayWeapon()
        {
            Console.WriteLine("o=i===>   Swords   <===i=o");

            for (int i = 0; i < weapon.Count; i++)
            {
                if (weapon[i] == Player.equippedWeapon)
                {
                    var soldIndex = i + 1;
                    DisplayEmptySlot(soldIndex);
                }
                else
                {
                    weapon[i].Description(i + 1);
                }
            }
            Console.WriteLine();
        }


        public static void DisplayMagics()
        {
            var listStartsFrom = weapon.Count;
            Console.WriteLine("-----   Magic  -----");

            for (int i = 0; i < magic.Count; i++)
            {
                if (magic[i] == Player.equippedMagic)
                {
                    var soldIndex = listStartsFrom + i + 1;
                    DisplayEmptySlot(soldIndex);
                }
                else
                {
                    magic[i].Description(listStartsFrom + i + 1);
                }
            }
            Console.WriteLine();
        }


        public static void DisplayItems()
        {
            var listStartsFrom = weapon.Count + magic.Count;
            Console.WriteLine("-----  Healing Potions  -----");

            for (int i = 0; i < potion.Count; i++)
            {
                if (potion[i] == Player.equippedPotion)
                {
                    var soldIndex = listStartsFrom + i + 1;
                    DisplayEmptySlot(soldIndex);
                }
                else
                {
                    potion[i].Description(listStartsFrom + i + 1);
                }
            }
            Console.WriteLine();
        }

        private static void DisplayEmptySlot(int soldIndex)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"{soldIndex}. << sold >>");
            Console.ResetColor();
        }
    }
}
