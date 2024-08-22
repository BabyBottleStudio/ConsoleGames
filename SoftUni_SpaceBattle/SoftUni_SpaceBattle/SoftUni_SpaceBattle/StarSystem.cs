using System;

namespace SoftUni_SpaceBattle
{
    class StarSystem
    {
        string name;

        int chanceToSpawnEnemy;

        Ship enemyShip;

        internal Ship EnemyShip { get => enemyShip; }
        public string Name { get => name; }

        public StarSystem(string name)
        {
            this.name = name;
            chanceToSpawnEnemy = 80;
            SpawnEnemy();
        }

        private void SpawnEnemy()
        {
            if (Dice.Roll() <= chanceToSpawnEnemy)
            {
                enemyShip = new Ship(Ship.BelongsTo.Enemy);
            }
            else
            {
                enemyShip = null;
            }
        }


        public enum AreaType
        {
            hostile,
            peacefull
        }

        public AreaType CheckForEnemies() => (EnemyShip == null) ? AreaType.peacefull : AreaType.hostile;


        public void ScanStarSystem()
        {

                Console.WriteLine("        ~+");
                Console.WriteLine();
                Console.WriteLine("                 *       +");
                Console.WriteLine("           '                  |");
                Console.WriteLine("       ()    .-.,=\"``\"=.    - o -");
                Console.WriteLine("             '=/_       \\     |");
                Console.WriteLine("          *   |  '=._    |");
                Console.WriteLine("               \\     `=./`,        '");
                Console.WriteLine("            .   '=.__.=' `='      *");
                Console.WriteLine("   +                         +");
                Console.WriteLine("        O      *        '       .");
            

            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Your computer is scanning the star system.");
            Console.WriteLine($"System name: {name}");
            Console.WriteLine($"System status: {CheckForEnemies()}");
            Console.WriteLine("-------------------------------------");
        }

    }
}
