using System;

namespace SoftUni_SpaceBattle
{
    class Ship
    {
        string name;
        int shieldStrength;
        Weapon laser;
        Weapon photonTorpedo;
        int chanceToEscape = 50;
        BelongsTo belongs;

        public string Name { get => name; }
        internal Weapon Laser { get => laser; set => laser = value; }
        internal Weapon PhotonTorpedo { get => photonTorpedo; set => photonTorpedo = value; }

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
                Laser = new Weapon("EnmyLaser", 10, 15, 70, Weapon.Expendable.infinite);
                PhotonTorpedo = null;
            }
            else if (belongs == BelongsTo.Player)
            {
                name = "Enterschpaise";
                shieldStrength = 100;
                Laser = new Weapon("Laser", 10, 20, 80, Weapon.Expendable.infinite);

                PhotonTorpedo = new Weapon("Photon Torpedo", 30, 40, 90, Weapon.Expendable.limitedAmount);
            }

        }


        public Status ReceiveDamage(int damage)
        {
            shieldStrength -= damage;
            return (shieldStrength > 0) ? Status.alive : Status.destroyed;
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
