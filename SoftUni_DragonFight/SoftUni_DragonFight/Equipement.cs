using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftUni_DragonFight
{
    class Equipement
    {
        private Random random = new Random();
        public string Name { get; set; }

        public int MinEffect { get; set; }
        public int MaxEffect { get; set; }
        public int ChanceToHit { get; set; }


        public int ChanceForCritical { get; set; }
        public int CriticalDamagePercent { get; set; }




        public int DealDamage(Hero hero)
        {
            if (TryToHit())
            {
                var attackDamage = random.Next(MinEffect, MaxEffect);
                attackDamage = TryCriticalHit(attackDamage);

                Display.AttackInfo(hero.Name, Name, attackDamage);

                return attackDamage;
            }
            else
            {
                Display.MissInfo(hero.Name, Name);
            }
            return 0;
        }


        private bool TryToHit() => random.Next(100) < ChanceToHit;

        /// <summary>
        /// Performs the test if the attack is going to deal critical damage.
        /// </summary>
        /// <returns></returns>
        private bool IsAttackCritical()
        {
            if (ChanceForCritical == 0)
            {
                return false;
            }
            else if (ChanceForCritical == 100)
            {
                return true;
            }
            else
            {
                return (random.Next(0, 100) < ChanceForCritical) ? true : false;
            }

        }

        /// <summary>
        /// If it is critical, it returns the 50% higher damage. If not, initial value is returned.
        /// </summary>
        /// <param name="attackDamage"></param>
        /// <returns></returns>
        private int TryCriticalHit(int intitialAttackDamage)
        {
            if (IsAttackCritical())
            {
                var damage = (int)(intitialAttackDamage * (1.0 + ((double)CriticalDamagePercent / 100)));
                
                Display.CriticalHitInfo(intitialAttackDamage, damage);
                
                return damage;
            }
            return intitialAttackDamage;
        }

        
    }
}
