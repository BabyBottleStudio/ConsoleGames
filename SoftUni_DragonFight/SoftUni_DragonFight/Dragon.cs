using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftUni_DragonFight
{
    class Dragon : Hero
    {
        public Weapon FireBreath { get; private set; }

        /// <summary>
        /// Constructs the object of the class Warrior.
        /// </summary>
        /// <param name="hp"></param>
        /// <param name="name"></param>
        public Dragon(int hp, string name) : base(hp, name)
        {
            HealthPoints = hp;
            Name = name;
            FireBreath = new Weapon("Inferno Breath", 7, 18, 100, 0); // reserved for Dragon
            ClasName = nameof(Dragon);
        }


    }
}
