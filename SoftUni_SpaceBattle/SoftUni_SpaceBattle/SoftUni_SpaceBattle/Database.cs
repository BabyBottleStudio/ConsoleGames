using System.Collections.Generic;
using System.Linq;

namespace SoftUni_SpaceBattle
{
    public static class Database
    {
        public static List<string> ShipNames = new List<string>()
        {
            "Pizzopevac",
            "Poyy!!!",
            "Lipizzaner",
            "Kikirez",
            "BlasterMaster"
        };


        public static List<string> starSystemNames = new List<string>()
        {
            "Kamino",
            "Alderaan",
            "Couruscant",
            "Taooin",
            "Kasheek"
        };

        public static int GetShipNamesCount() => ShipNames.Count();
        public static int GetSystemsCount() => starSystemNames.Count();

        // Refaktorisati ove dve metode dole. obe rade sličnu stvar


        public static string GetRandomShipName()
        {
            int index = Dice.GetRandomIndex(ShipNames.Count);
            if (index >=0 && index < ShipNames.Count)
            {
                return ShipNames.ElementAt(index);
            }
            else
            {
                return "Ship";
            }
        }


        public static string GetRandomSystemName()
        {
            int index = Dice.GetRandomIndex(starSystemNames.Count);
            if (index >= 0 && index < starSystemNames.Count)
            {
                return starSystemNames.ElementAt(index);
            }
            else
            {
                return "System";
            }
        }
    }
}
