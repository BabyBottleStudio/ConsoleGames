using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mastermind
{
    class Grid
    {
        int[] secretCode = new int[] { 1, 2, 4, 5 };

        int[,] gridData = new int[Settings.AttemptCount, Settings.SecredCodeLength];



        public void DisplayGrid()
        {
            gridData.Initialize();

            for (int i = 0; i < gridData.GetLength(0); i++)
            {
                Console.WriteLine(" --- --- --- ---");
                for (int j = 0; j < gridData.GetLength(1); j++)
                {
                    Console.Write($"| {gridData[i,j]} ");
                }
                Console.WriteLine("|");
            }
        }

    }
}
