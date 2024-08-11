using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftUni_DragonFight
{
    class Hero
    {

        /// <summary>
        /// Name of our delija.
        /// </summary>
        public string Name { get; set; }

        public string ClasName;

        /// <summary>
        /// Represents the amount of health that warrior has.
        /// </summary>
        public int HealthPoints { get; set; }

        public Hero(int hp, string name)
        {
            HealthPoints = hp;
            Name = name;
            
        }


        public void GetHit(int hitPoints)
        {
            HealthPoints -= hitPoints;
        }

        /// <summary>
        /// Checks the status of health points. Important for the GameOver condition.
        /// </summary>
        /// <returns></returns>
        public bool IsAlive() => HealthPoints > 0;

    }
}
