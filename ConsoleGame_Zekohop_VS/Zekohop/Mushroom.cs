using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zekohop
{
    class Mushroom
    {

        public readonly static string Icon = "Q";
        public readonly static ConsoleColor IconColor = ConsoleColor.DarkRed;
        
        (int row, int col) _pos;

        public Mushroom((int row, int col) pos)
        {
            Position = pos;
        }

        public (int row, int col) Position { get => _pos; set => _pos = value; }

        /// <summary>
        /// Method for adding the Mushroom objects into the level.
        /// </summary>
        /// <param name="theMushroom"></param>
        public static void AddMushroom(Mushroom theMushroom)
        {
            // if the hole is covered with the mushroom, remove it from the list
            (int row, int col) mushroomPos = (theMushroom.Position.row, theMushroom.Position.col);
            GameGrid.Grid[mushroomPos.row, mushroomPos.col] = 9;

            GameGrid.HoleList.Remove(mushroomPos); // if mushroom covers the hole, delete the hole from the list
        }
    }
}
