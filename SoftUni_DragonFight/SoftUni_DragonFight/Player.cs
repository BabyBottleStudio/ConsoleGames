using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftUni_DragonFight
{
    class Player : Hero
    {
        /// <summary>
        /// Represents the maximum health of the object. During the healing, HP are clamped to this value.
        /// </summary>
        public int MaxHealth { get; private set; }

        private Weapon defaultWeapon = new Weapon("Slap of an Iron Forehand", 1, 5, 100);
        private Magic defaultMagic = new Magic("Smirk at the Face of a Certain Doom", 4, 8, 90);
        private Item defaultPotion = new Item("Tears of an Ex", 99, (1, 5));

        public static Weapon equippedWeapon;
        public static Magic equippedMagic;
        public static Item equippedPotion;

        /// <summary>
        /// Constructs the object of the class Warrior.
        /// </summary>
        /// <param name="hp"></param>
        /// <param name="name"></param>
        public Player(int hp, string name) : base(hp, name)
        {
            MaxHealth = hp;
            HealthPoints = hp;
            Name = name;
            ClasName = nameof(Player);
        }

        public void LoadDefaultWeaponAndItem()
        {
            if (equippedWeapon == null)
                equippedWeapon = defaultWeapon;

            if (equippedMagic == null)
                equippedMagic = defaultMagic;


            if (equippedPotion == null)
                equippedPotion = defaultPotion;
        }

        public void Equip(Weapon weapon)
        {
            if (equippedWeapon != null)
            {
                Display.PlayerDropsObjectFromTheInventory(Name, equippedWeapon.Name);
            }

            equippedWeapon = weapon;
        }

        public void Equip(Magic magicSpell)
        {
            if (equippedMagic != null)
            {
                Display.PlayerDropsObjectFromTheInventory(Name, equippedMagic.Name);
            }
            equippedMagic = magicSpell;
        }

        public void Equip(Item item)
        {
            if (equippedPotion != null)
            {
                Display.PlayerDropsObjectFromTheInventory(Name, equippedPotion.Name);
            }
            equippedPotion = item;
        }

        public void UnequipAll()
        {
            equippedWeapon = null;
            equippedMagic = null;
            equippedPotion = null;
        }

        private static bool NoPotionsLeft => equippedPotion.Count == 0;
        

        public static void RemovePotionIfEmpty()
        {
            if (NoPotionsLeft)
            {
                equippedPotion = null;
            }
        }
    }
}
