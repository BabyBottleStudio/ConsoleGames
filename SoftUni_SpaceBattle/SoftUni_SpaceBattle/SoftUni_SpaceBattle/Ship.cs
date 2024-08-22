using System;

namespace SoftUni_SpaceBattle
{
    class Ship
    {
        string name;
        int shieldStrength;
        Weapon primaryWeapon;
        Weapon secondaryWeapon;
        int secondaryWeaponCount;

        int chanceToEscape = 50;
        BelongsTo belongs;

        public string Name { get => name; }
        internal Weapon PrimaryWeapon { get => primaryWeapon; set => primaryWeapon = value; }
        internal Weapon SecondaryWeapon { get => secondaryWeapon; set => secondaryWeapon = value; }
        public int SecondaryWeaponCount { get => secondaryWeaponCount; set => secondaryWeaponCount = value; }

        public enum BelongsTo
        {
            Player,
            Enemy
        }

        public enum Status
        {
            destroyed,
            alive
        }

        public Ship(BelongsTo belongs) // enemy spawn
        {
            this.belongs = belongs;
            if (belongs == BelongsTo.Enemy)
            {
                this.name = Database.GetRandomShipName();
                shieldStrength = 50;
                PrimaryWeapon = new Weapon("EnmyLaser", 10, 15, 70); //, Weapon.Expendable.infinite);
                SecondaryWeapon = null;
                SecondaryWeaponCount = 0;
            }
            else if (belongs == BelongsTo.Player)
            {
                name = "Enterschpaise";
                shieldStrength = 100;
                PrimaryWeapon = new Weapon("Phaser", 10, 20, 80); //, Weapon.Expendable.infinite);

                SecondaryWeapon = new Weapon("Photon Torpedo", 30, 40, 90); //, Weapon.Expendable.limitedAmount);
                SecondaryWeaponCount = 3;
            }

        }


        public Status ReceiveDamage(int damage)
        {
            shieldStrength -= damage;
            return (shieldStrength > 0) ? Status.alive : Status.destroyed;
        }

        private bool SecondaryWeaponAvailable() => secondaryWeaponCount > 0;

        public bool FireSecondaryWeapon()
        {
            if (SecondaryWeaponAvailable())
            {
                --secondaryWeaponCount;
                Console.WriteLine($"{SecondaryWeapon.Name} sucsessfully fired.");
                Console.WriteLine($"You have {secondaryWeaponCount} left.");
                return true;
            }
            else
            {
                Console.WriteLine($"No more {SecondaryWeapon.Name}. You used all.");
                return false;
            }
        }

        public void GetStats()
        {
            Console.WriteLine($"-----------{belongs}'s Ship ------------------");
            Console.WriteLine($"--> Ship name: {Name}");
            Console.WriteLine($"--> Shield status: {shieldStrength}");
            Console.WriteLine("------------------------------------------------");
        }

    }
}
