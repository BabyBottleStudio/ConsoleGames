namespace SoftUni_SpaceBattle
{
    class Weapon
    {
        string name;
        int damageMin;
        int damageMax;
        int chanceToHit;

        int projectilesCount;
        Expendable weaponAvailability;

        public enum Expendable
        {
            infinite = -1,
            limitedAmount = 3
        }

        public Weapon(string name, int damageMin, int damageMax, int chanceToHit, Expendable weaponAvailability)
        {
            this.name = name;
            this.damageMax = damageMax;
            this.damageMin = damageMin;
            this.chanceToHit = chanceToHit;
            this.weaponAvailability = weaponAvailability;

            projectilesCount = (int)weaponAvailability;
        }

        public int DealDamage() => Dice.GenerateDamage(damageMin, damageMax);

        public bool IsHit() => Dice.Roll() < chanceToHit ? true : false;

        private bool IsWeaponAvailable() => projectilesCount == 0? false : true;
        

        public bool WeaponFire()
        {
            if (projectilesCount > 0)
            {
                --projectilesCount;
                return true;
            }
            return false;
        }
    }
}
