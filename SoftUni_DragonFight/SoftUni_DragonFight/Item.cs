using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftUni_DragonFight
{
    class Item
    {
        private Random random = new Random();

        public string Name { get; set; }

        public int MinEffect { get; set; }
        public int MaxEffect { get; set; }
        public int Count { get; set; }

        public readonly ConsoleColor ColorInUI = ConsoleColor.Green;

        public Item(string name, int count, (int min, int max) effect)
        {
            Name = name;
            Count = count;
            MinEffect = effect.min;
            MaxEffect = effect.max;
        }

        public void Heal(Player warrior)
        {
            var healAmt = random.Next(MinEffect, MaxEffect);

            warrior.HealthPoints += healAmt;

            if (warrior.HealthPoints > warrior.MaxHealth)
            {
                healAmt -= (warrior.HealthPoints - warrior.MaxHealth);
                warrior.HealthPoints = warrior.MaxHealth;
            }
            Count--;

            Display.HealInfo(Name, warrior.Name, healAmt, warrior.HealthPoints, Count);
        }

        public void Description(int index = 3)
        {
            Display.ColorUiElement($"{index}. \"{Name}\"", ColorInUI, false);
            Console.WriteLine($". Heal: ({MinEffect}-{MaxEffect}). Amount in the inventory: {Count}");
        }
    }
}
