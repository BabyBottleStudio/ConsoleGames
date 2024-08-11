using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftUni_DragonFight
{
    class Magic : Equipement
    {
        // all fields are inherited from theEquipment
        public readonly ConsoleColor ColorInUI = ConsoleColor.Cyan;

        public Magic(string name, int minDamage, int maxDamage, int chanceToHit, int chanceForCritical = 20, int criticalDamagePercent = 50)
        {

            Name = name;
            MinEffect = minDamage;
            MaxEffect = maxDamage;
            ChanceToHit = chanceToHit;
            ChanceForCritical = chanceForCritical;
            CriticalDamagePercent = criticalDamagePercent;

        }

        public void Description(int index = 2)
        {
            Display.ColorUiElement($"{index}. \"{Name}\"", ColorInUI, false);
            Console.WriteLine($". Damage: ({MinEffect}-{MaxEffect}). Chance to hit: {ChanceToHit}%");
        }
    }
}
