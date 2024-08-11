using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zekohop
{
    public static class Level
    {
        // this class handles the data levels contain
        // it has 3 lists, for each type of objects in the game. Set level data handles the lists and creates instances with the proper coordinates. Impossible stuff like havint two objects on the same coordinate is not handled. Maybe if a level creator is added later :)
        
        /// <summary>
        /// Hardcode of the levels amount. Default is 60.
        /// </summary>
        private const int _allLevelsCount = 60;


        private static List<Mushroom> mushroomList = new List<Mushroom>();
        private static List<Bunny> bunnyList = new List<Bunny>();
        private static List<Fox> foxList = new List<Fox>();

        private static int levelIndex;
        private static int minimalNumberOfMoves;

        /// <summary>
        /// Returns the amount of levels hardcoded into the game.
        /// </summary>
        public static int AllLevelsCount => _allLevelsCount;

        /// <summary>
        /// List of mushrooms instatiated into the level.
        /// </summary>
        internal static List<Mushroom> MushroomList
        { get => mushroomList; set => mushroomList = value; }


        /// <summary>
        /// List of bunnies instatiated into the level.
        /// </summary>
        internal static List<Bunny> BunnyList
        { get => bunnyList; set => bunnyList = value; }

        /// <summary>
        /// List of Foxes instatiated into the level.
        /// </summary>
        internal static List<Fox> FoxList
        { get => foxList; set => foxList = value; }

        /// <summary>
        /// Index of a current level that player is playing.
        /// </summary>
        internal static int LevelIndex
        { get => levelIndex; set => levelIndex = value; } // handles the current level

        /// <summary>
        /// Number of minimum moves per level to solve the puzzle. Hardcoded. Found on the booklet of the game.
        /// </summary>
        internal static int MinimalNumberOfMoves
        { get => minimalNumberOfMoves; set => minimalNumberOfMoves = value; } // represents the number of minimal moves neededt to solve the level





        /// <summary>
        /// Adds the data to the level, and instantiate objects.
        /// </summary>
        public static void LoadLevel()
        {
            SetLevelData();

            foreach (object obj in MushroomList)
            {
                Mushroom.AddMushroom((Mushroom)obj);
            }

            foreach (object obj in BunnyList)
            {
                Bunny.WriteBunnyIdToTheGridInitial((Bunny)obj);
            }

            foreach (object obj in FoxList)
            {
                Fox.WriteFoxIdToTheGrid((Fox)obj);
            }
        }

        /// <summary>
        /// If user press A, previous level is loaded.
        /// </summary>
        public static void JumpToPreviousLevel()
        {
            LevelIndex--;
            if (LevelIndex < 1)
            {
                LevelIndex = AllLevelsCount;
            }
        }


        /// <summary>
        /// If user press D, next level is loaded.
        /// </summary>
        public static void JumpToNextLevel()
        {
            LevelIndex++;
            if (LevelIndex > AllLevelsCount)
            {
                LevelIndex = 1;
            }
        }


        /// <summary>
        /// Cleans up the old data and loads the new data into the level. Levels are hardcoded here. They contain all of the coordinates of the elements involved into the puzzle. They are instatiated here as well.
        /// </summary>
        internal static void SetLevelData()
        {
            // cleaning up the data before generating the level
            mushroomList.Clear();
            bunnyList.Clear();
            foxList.Clear();

            // levels are hardcoded data taken from the game booklet
            switch (LevelIndex)
            {
                case 1:
                    MinimalNumberOfMoves = 2;
                    mushroomList.Add(new Mushroom((0, 1)));
                    mushroomList.Add(new Mushroom((0, 2)));
                    mushroomList.Add(new Mushroom((1, 3)));
                    bunnyList.Add(new Bunny((2, 3)));
                    break;

                case 2:
                    MinimalNumberOfMoves = 3;
                    mushroomList.Add(new Mushroom((0, 3)));
                    mushroomList.Add(new Mushroom((1, 4)));
                    mushroomList.Add(new Mushroom((2, 3)));
                    bunnyList.Add(new Bunny((0, 4)));
                    bunnyList.Add(new Bunny((0, 2)));
                    break;

                case 3:
                    MinimalNumberOfMoves = 4;
                    mushroomList.Add(new Mushroom((0, 3)));
                    mushroomList.Add(new Mushroom((1, 2)));
                    mushroomList.Add(new Mushroom((3, 2)));
                    bunnyList.Add(new Bunny((0, 1)));
                    bunnyList.Add(new Bunny((4, 2)));
                    break;

                case 4:
                    MinimalNumberOfMoves = 4;
                    mushroomList.Add(new Mushroom((3, 0)));
                    mushroomList.Add(new Mushroom((3, 2)));
                    mushroomList.Add(new Mushroom((4, 3)));
                    bunnyList.Add(new Bunny((4, 1)));
                    bunnyList.Add(new Bunny((2, 0)));
                    break;

                case 5:
                    MinimalNumberOfMoves = 5;
                    mushroomList.Add(new Mushroom((1, 1)));
                    mushroomList.Add(new Mushroom((2, 0)));
                    mushroomList.Add(new Mushroom((3, 0)));
                    bunnyList.Add(new Bunny((4, 0)));
                    bunnyList.Add(new Bunny((0, 2)));
                    break;

                case 6:
                    MinimalNumberOfMoves = 6;
                    mushroomList.Add(new Mushroom((0, 1)));
                    mushroomList.Add(new Mushroom((1, 2)));
                    mushroomList.Add(new Mushroom((2, 1)));
                    bunnyList.Add(new Bunny((0, 0)));
                    bunnyList.Add(new Bunny((2, 3)));
                    break;

                case 7:
                    MinimalNumberOfMoves = 7;
                    mushroomList.Add(new Mushroom((3, 1)));
                    mushroomList.Add(new Mushroom((3, 2)));
                    mushroomList.Add(new Mushroom((4, 3)));
                    bunnyList.Add(new Bunny((4, 4)));
                    bunnyList.Add(new Bunny((2, 3)));
                    break;

                case 8:
                    MinimalNumberOfMoves = 7;
                    mushroomList.Add(new Mushroom((1, 4)));
                    mushroomList.Add(new Mushroom((2, 1)));
                    mushroomList.Add(new Mushroom((2, 3)));
                    bunnyList.Add(new Bunny((2, 2)));
                    bunnyList.Add(new Bunny((2, 4)));
                    bunnyList.Add(new Bunny((2, 0)));

                    break;

                case 9:
                    MinimalNumberOfMoves = 8;
                    mushroomList.Add(new Mushroom((0, 1)));
                    mushroomList.Add(new Mushroom((0, 3)));
                    mushroomList.Add(new Mushroom((1, 2)));
                    bunnyList.Add(new Bunny((4, 2)));
                    bunnyList.Add(new Bunny((0, 4)));
                    bunnyList.Add(new Bunny((0, 0)));
                    break;

                case 10:
                    MinimalNumberOfMoves = 9;
                    mushroomList.Add(new Mushroom((0, 1)));
                    mushroomList.Add(new Mushroom((0, 3)));
                    mushroomList.Add(new Mushroom((1, 0)));
                    bunnyList.Add(new Bunny((0, 4)));
                    bunnyList.Add(new Bunny((0, 2)));
                    bunnyList.Add(new Bunny((0, 0)));
                    break;

                case 11:

                    MinimalNumberOfMoves = 10;
                    mushroomList.Add(new Mushroom((0, 1)));
                    mushroomList.Add(new Mushroom((1, 2)));
                    mushroomList.Add(new Mushroom((3, 3)));
                    bunnyList.Add(new Bunny((2, 4)));
                    bunnyList.Add(new Bunny((0, 3)));
                    bunnyList.Add(new Bunny((0, 0)));
                    break;

                case 12:
                    MinimalNumberOfMoves = 9;
                    mushroomList.Add(new Mushroom((3, 1)));
                    mushroomList.Add(new Mushroom((4, 2)));
                    mushroomList.Add(new Mushroom((4, 3)));
                    bunnyList.Add(new Bunny((2, 4)));
                    bunnyList.Add(new Bunny((2, 1)));
                    bunnyList.Add(new Bunny((2, 0)));
                    break;

                case 13:
                    MinimalNumberOfMoves = 4;
                    mushroomList.Add(new Mushroom((0, 3)));
                    mushroomList.Add(new Mushroom((1, 1)));
                    mushroomList.Add(new Mushroom((2, 2)));

                    bunnyList.Add(new Bunny((1, 2)));
                    foxList.Add(new Fox((1, 3), "Horizontal Left"));
                    break;

                case 14:
                    MinimalNumberOfMoves = 6;
                    mushroomList.Add(new Mushroom((2, 1)));
                    mushroomList.Add(new Mushroom((2, 2)));
                    mushroomList.Add(new Mushroom((2, 4)));

                    bunnyList.Add(new Bunny((0, 3)));

                    foxList.Add(new Fox((1, 2), "Horizontal Right"));
                    break;

                case 15:
                    MinimalNumberOfMoves = 9;
                    mushroomList.Add(new Mushroom((0, 2)));
                    mushroomList.Add(new Mushroom((2, 2)));
                    mushroomList.Add(new Mushroom((3, 2)));

                    bunnyList.Add(new Bunny((3, 4)));

                    foxList.Add(new Fox((1, 3), "Vertical Down"));
                    break;

                case 16:
                    MinimalNumberOfMoves = 7;
                    mushroomList.Add(new Mushroom((1, 0)));
                    mushroomList.Add(new Mushroom((2, 0)));
                    mushroomList.Add(new Mushroom((3, 2)));

                    bunnyList.Add(new Bunny((0, 1)));

                    foxList.Add(new Fox((3, 1), "Vertical Up"));
                    break;

                case 17:
                    MinimalNumberOfMoves = 7;
                    mushroomList.Add(new Mushroom((1, 4)));
                    mushroomList.Add(new Mushroom((2, 0)));
                    mushroomList.Add(new Mushroom((2, 4)));

                    bunnyList.Add(new Bunny((3, 0)));

                    foxList.Add(new Fox((0, 1), "Vertical Up"));
                    foxList.Add(new Fox((1, 3), "Vertical Down"));
                    break;

                case 18:
                    MinimalNumberOfMoves = 8;
                    mushroomList.Add(new Mushroom((2, 0)));
                    mushroomList.Add(new Mushroom((2, 3)));
                    mushroomList.Add(new Mushroom((4, 0)));

                    bunnyList.Add(new Bunny((4, 3)));

                    foxList.Add(new Fox((1, 2), "Horizontal Left"));
                    foxList.Add(new Fox((3, 1), "Horizontal Right"));
                    break;

                case 19:
                    MinimalNumberOfMoves = 11;
                    mushroomList.Add(new Mushroom((0, 2)));
                    mushroomList.Add(new Mushroom((2, 4)));
                    mushroomList.Add(new Mushroom((3, 2)));

                    bunnyList.Add(new Bunny((1, 2)));

                    foxList.Add(new Fox((3, 3), "Vertical Down"));
                    break;

                case 20:
                    MinimalNumberOfMoves = 13;
                    mushroomList.Add(new Mushroom((2, 0)));
                    mushroomList.Add(new Mushroom((2, 2)));
                    mushroomList.Add(new Mushroom((3, 2)));

                    bunnyList.Add(new Bunny((0, 1)));

                    foxList.Add(new Fox((3, 1), "Vertical Up"));
                    break;

                case 21:
                    MinimalNumberOfMoves = 9;
                    mushroomList.Add(new Mushroom((2, 0)));
                    mushroomList.Add(new Mushroom((2, 3)));
                    mushroomList.Add(new Mushroom((3, 2)));

                    bunnyList.Add(new Bunny((0, 3)));

                    foxList.Add(new Fox((1, 1), "Horizontal Right"));
                    break;

                case 22:
                    MinimalNumberOfMoves = 11;
                    mushroomList.Add(new Mushroom((4, 1)));
                    mushroomList.Add(new Mushroom((4, 2)));
                    mushroomList.Add(new Mushroom((4, 4)));

                    bunnyList.Add(new Bunny((3, 1)));

                    foxList.Add(new Fox((2, 3), "Vertical Down"));
                    foxList.Add(new Fox((3, 3), "Horizontal Left"));
                    break;

                case 23:
                    MinimalNumberOfMoves = 16;
                    mushroomList.Add(new Mushroom((0, 4)));
                    mushroomList.Add(new Mushroom((2, 3)));
                    mushroomList.Add(new Mushroom((4, 2)));

                    bunnyList.Add(new Bunny((0, 3)));

                    foxList.Add(new Fox((1, 1), "Horizontal Right"));
                    foxList.Add(new Fox((3, 1), "Vertical Up"));
                    break;

                case 24:
                    MinimalNumberOfMoves = 15;
                    mushroomList.Add(new Mushroom((0, 4)));
                    mushroomList.Add(new Mushroom((1, 2)));
                    mushroomList.Add(new Mushroom((2, 2)));

                    bunnyList.Add(new Bunny((1, 4)));

                    foxList.Add(new Fox((2, 3), "Vertical Down"));
                    foxList.Add(new Fox((3, 3), "Horizontal Left"));
                    break;

                case 25:
                    MinimalNumberOfMoves = 13;
                    mushroomList.Add(new Mushroom((1, 4)));
                    mushroomList.Add(new Mushroom((2, 4)));
                    mushroomList.Add(new Mushroom((4, 4)));

                    bunnyList.Add(new Bunny((3, 0)));
                    bunnyList.Add(new Bunny((3, 3)));

                    foxList.Add(new Fox((1, 1), "Vertical Down"));
                    foxList.Add(new Fox((0, 3), "Vertical Up"));
                    break;

                case 26:
                    MinimalNumberOfMoves = 12;
                    mushroomList.Add(new Mushroom((0, 1)));
                    mushroomList.Add(new Mushroom((3, 1)));

                    bunnyList.Add(new Bunny((1, 3)));
                    bunnyList.Add(new Bunny((2, 2)));

                    foxList.Add(new Fox((1, 1), "Horizontal Right"));
                    break;

                case 27:
                    MinimalNumberOfMoves = 13;
                    mushroomList.Add(new Mushroom((0, 4)));
                    mushroomList.Add(new Mushroom((3, 0)));
                    mushroomList.Add(new Mushroom((4, 0)));

                    bunnyList.Add(new Bunny((0, 0)));
                    bunnyList.Add(new Bunny((1, 3)));

                    //foxList.Add(new Fox((1, 1), "Horizontal Left"));
                    foxList.Add(new Fox((1, 1), "Horizontal Right"));
                    //foxList.Add(new Fox((1, 1), "Vertical Up"));
                    foxList.Add(new Fox((3, 1), "Vertical Down"));
                    break;

                case 28:
                    MinimalNumberOfMoves = 12;
                    mushroomList.Add(new Mushroom((2, 3)));
                    mushroomList.Add(new Mushroom((3, 0)));
                    //mushroomList.Add(new Mushroom((4, 0)));

                    bunnyList.Add(new Bunny((1, 1)));
                    bunnyList.Add(new Bunny((2, 4)));

                    //foxList.Add(new Fox((1, 1), "Horizontal Left"));
                    //foxList.Add(new Fox((1, 1), "Horizontal Right"));
                    foxList.Add(new Fox((2, 1), "Vertical Up"));
                    //foxList.Add(new Fox((3, 1), "Vertical Down"));
                    break;

                case 29:
                    MinimalNumberOfMoves = 13;
                    mushroomList.Add(new Mushroom((4, 2)));
                    //mushroomList.Add(new Mushroom((3, 0)));
                    //mushroomList.Add(new Mushroom((4, 0)));

                    bunnyList.Add(new Bunny((3, 3)));
                    bunnyList.Add(new Bunny((4, 4)));

                    //foxList.Add(new Fox((1, 1), "Horizontal Left"));
                    //foxList.Add(new Fox((1, 1), "Horizontal Right"));
                    //foxList.Add(new Fox((2, 1), "Vertical Up"));
                    foxList.Add(new Fox((2, 3), "Vertical Down"));
                    break;

                case 30:
                    MinimalNumberOfMoves = 13;
                    mushroomList.Add(new Mushroom((2, 4)));
                    //mushroomList.Add(new Mushroom((3, 0)));
                    //mushroomList.Add(new Mushroom((4, 0)));

                    bunnyList.Add(new Bunny((3, 1)));
                    bunnyList.Add(new Bunny((4, 2)));

                    //foxList.Add(new Fox((1, 1), "Horizontal Right"));
                    //foxList.Add(new Fox((2, 1), "Vertical Up"));
                    foxList.Add(new Fox((1, 1), "Vertical Down"));
                    foxList.Add(new Fox((1, 3), "Horizontal Left"));
                    break;

                case 31:
                    MinimalNumberOfMoves = 17;
                    mushroomList.Add(new Mushroom((0, 1)));
                    mushroomList.Add(new Mushroom((0, 2)));
                    mushroomList.Add(new Mushroom((2, 2)));

                    bunnyList.Add(new Bunny((0, 4)));
                    bunnyList.Add(new Bunny((2, 3)));

                    //foxList.Add(new Fox((1, 1), "Horizontal Right"));
                    //foxList.Add(new Fox((2, 1), "Vertical Up"));
                    foxList.Add(new Fox((4, 3), "Vertical Down"));
                    //foxList.Add(new Fox((1, 3), "Horizontal Left"));
                    break;

                case 32:
                    MinimalNumberOfMoves = 15;
                    mushroomList.Add(new Mushroom((1, 0)));
                    mushroomList.Add(new Mushroom((1, 2)));
                    mushroomList.Add(new Mushroom((1, 4)));

                    bunnyList.Add(new Bunny((1, 3)));
                    bunnyList.Add(new Bunny((4, 4)));


                    //foxList.Add(new Fox((2, 1), "Vertical Up"));
                    foxList.Add(new Fox((1, 1), "Vertical Down"));
                    //foxList.Add(new Fox((1, 3), "Horizontal Left"));
                    foxList.Add(new Fox((3, 2), "Horizontal Right"));
                    break;

                case 33:
                    MinimalNumberOfMoves = 13;
                    mushroomList.Add(new Mushroom((2, 3)));
                    mushroomList.Add(new Mushroom((3, 3)));
                    mushroomList.Add(new Mushroom((4, 3)));

                    bunnyList.Add(new Bunny((1, 3)));
                    bunnyList.Add(new Bunny((4, 4)));

                    foxList.Add(new Fox((1, 1), "Horizontal Right"));
                    foxList.Add(new Fox((3, 1), "Vertical Up"));
                    //foxList.Add(new Fox((1, 1), "Vertical Down"));
                    //foxList.Add(new Fox((1, 3), "Horizontal Left"));

                    break;

                case 34:
                    MinimalNumberOfMoves = 15;
                    mushroomList.Add(new Mushroom((0, 4)));
                    mushroomList.Add(new Mushroom((1, 2)));
                    //mushroomList.Add(new Mushroom((4, 3)));

                    bunnyList.Add(new Bunny((0, 0)));
                    bunnyList.Add(new Bunny((4, 1)));

                    //foxList.Add(new Fox((1, 1), "Horizontal Right"));
                    foxList.Add(new Fox((1, 1), "Vertical Down"));
                    foxList.Add(new Fox((0, 3), "Vertical Up"));
                    //foxList.Add(new Fox((1, 3), "Horizontal Left"));
                    break;

                case 35:
                    MinimalNumberOfMoves = 14;
                    mushroomList.Add(new Mushroom((1, 1)));
                    mushroomList.Add(new Mushroom((2, 0)));
                    //mushroomList.Add(new Mushroom((4, 3)));

                    bunnyList.Add(new Bunny((1, 3)));
                    bunnyList.Add(new Bunny((2, 3)));

                    foxList.Add(new Fox((3, 1), "Horizontal Right"));
                    //foxList.Add(new Fox((3, 1), "Vertical Down"));
                    foxList.Add(new Fox((3, 3), "Vertical Up"));
                    //foxList.Add(new Fox((1, 3), "Horizontal Left"));
                    break;

                case 36:
                    MinimalNumberOfMoves = 19;
                    mushroomList.Add(new Mushroom((0, 3)));
                    mushroomList.Add(new Mushroom((2, 1)));
                    mushroomList.Add(new Mushroom((2, 2)));

                    bunnyList.Add(new Bunny((0, 0)));
                    bunnyList.Add(new Bunny((3, 3)));

                    foxList.Add(new Fox((1, 2), "Horizontal Right"));
                    //foxList.Add(new Fox((3, 1), "Vertical Down"));
                    //foxList.Add(new Fox((3, 3), "Vertical Up"));
                    //foxList.Add(new Fox((1, 3), "Horizontal Left"));
                    break;

                case 37:
                    MinimalNumberOfMoves = 21;
                    mushroomList.Add(new Mushroom((1, 0)));
                    mushroomList.Add(new Mushroom((2, 2)));
                    mushroomList.Add(new Mushroom((4, 0)));

                    bunnyList.Add(new Bunny((4, 2)));

                    foxList.Add(new Fox((1, 1), "Vertical Down"));
                    foxList.Add(new Fox((3, 1), "Horizontal Right"));
                    //foxList.Add(new Fox((3, 3), "Vertical Up"));
                    //foxList.Add(new Fox((1, 3), "Horizontal Left"));
                    break;

                case 38:
                    MinimalNumberOfMoves = 21;
                    mushroomList.Add(new Mushroom((0, 2)));
                    mushroomList.Add(new Mushroom((1, 2)));
                    mushroomList.Add(new Mushroom((2, 2)));

                    bunnyList.Add(new Bunny((3, 0)));
                    bunnyList.Add(new Bunny((4, 4)));

                    foxList.Add(new Fox((1, 1), "Vertical Down"));
                    //foxList.Add(new Fox((3, 1), "Horizontal Right"));
                    foxList.Add(new Fox((0, 3), "Vertical Up"));
                    //foxList.Add(new Fox((1, 3), "Horizontal Left"));
                    break;

                case 39:
                    MinimalNumberOfMoves = 7;
                    mushroomList.Add(new Mushroom((0, 3)));
                    mushroomList.Add(new Mushroom((2, 4)));
                    mushroomList.Add(new Mushroom((3, 3)));

                    bunnyList.Add(new Bunny((2, 1)));
                    bunnyList.Add(new Bunny((2, 2)));
                    bunnyList.Add(new Bunny((2, 3)));

                    //foxList.Add(new Fox((1, 1), "Vertical Down"));
                    foxList.Add(new Fox((1, 1), "Horizontal Right"));
                    //foxList.Add(new Fox((0, 3), "Vertical Up"));
                    foxList.Add(new Fox((1, 3), "Horizontal Left"));
                    break;

                case 40:
                    MinimalNumberOfMoves = 19;
                    mushroomList.Add(new Mushroom((0, 2)));
                    mushroomList.Add(new Mushroom((2, 1)));
                    mushroomList.Add(new Mushroom((4, 0)));

                    bunnyList.Add(new Bunny((1, 3)));
                    bunnyList.Add(new Bunny((4, 1)));
                    //bunnyList.Add(new Bunny((2, 3)));

                    //foxList.Add(new Fox((1, 1), "Vertical Down"));
                    foxList.Add(new Fox((3, 1), "Horizontal Right"));
                    foxList.Add(new Fox((3, 3), "Vertical Up"));
                    //foxList.Add(new Fox((1, 3), "Horizontal Left"));
                    break;

                case 41:
                    MinimalNumberOfMoves = 20;
                    mushroomList.Add(new Mushroom((1, 2)));
                    mushroomList.Add(new Mushroom((3, 2)));
                    mushroomList.Add(new Mushroom((4, 0)));

                    bunnyList.Add(new Bunny((4, 2)));
                    bunnyList.Add(new Bunny((4, 1)));
                    //bunnyList.Add(new Bunny((2, 3)));

                    foxList.Add(new Fox((1, 1), "Vertical Down"));
                    //foxList.Add(new Fox((3, 1), "Horizontal Right"));
                    //foxList.Add(new Fox((3, 3), "Vertical Up"));
                    //foxList.Add(new Fox((1, 3), "Horizontal Left"));
                    break;

                case 42:
                    MinimalNumberOfMoves = 17;
                    mushroomList.Add(new Mushroom((1, 2)));
                    mushroomList.Add(new Mushroom((1, 3)));
                    mushroomList.Add(new Mushroom((2, 3)));

                    bunnyList.Add(new Bunny((0, 0)));
                    bunnyList.Add(new Bunny((3, 4)));
                    //bunnyList.Add(new Bunny((2, 3)));

                    foxList.Add(new Fox((2, 1), "Vertical Down"));
                    //foxList.Add(new Fox((3, 1), "Horizontal Right"));
                    //foxList.Add(new Fox((3, 3), "Vertical Up"));
                    //foxList.Add(new Fox((1, 3), "Horizontal Left"));
                    break;

                case 43:
                    MinimalNumberOfMoves = 26;
                    mushroomList.Add(new Mushroom((2, 1)));
                    mushroomList.Add(new Mushroom((2, 2)));
                    mushroomList.Add(new Mushroom((2, 3)));

                    bunnyList.Add(new Bunny((1, 1)));
                    bunnyList.Add(new Bunny((1, 4)));
                    //bunnyList.Add(new Bunny((2, 3)));

                    //foxList.Add(new Fox((2, 1), "Vertical Down"));
                    //foxList.Add(new Fox((3, 1), "Horizontal Right"));
                    //foxList.Add(new Fox((3, 3), "Vertical Up"));
                    foxList.Add(new Fox((1, 2), "Horizontal Left"));
                    break;

                case 44:
                    MinimalNumberOfMoves = 19;
                    mushroomList.Add(new Mushroom((2, 2)));
                    mushroomList.Add(new Mushroom((3, 3)));
                    mushroomList.Add(new Mushroom((4, 4)));

                    bunnyList.Add(new Bunny((3, 1)));
                    bunnyList.Add(new Bunny((4, 1)));
                    bunnyList.Add(new Bunny((1, 4)));

                    //foxList.Add(new Fox((2, 1), "Vertical Down"));
                    //foxList.Add(new Fox((3, 1), "Horizontal Right"));
                    foxList.Add(new Fox((1, 1), "Vertical Up"));
                    foxList.Add(new Fox((1, 2), "Horizontal Left"));
                    break;

                case 45:
                    MinimalNumberOfMoves = 21;
                    mushroomList.Add(new Mushroom((0, 0)));
                    //mushroomList.Add(new Mushroom((3, 3)));
                    //mushroomList.Add(new Mushroom((4, 4)));

                    bunnyList.Add(new Bunny((2, 4)));
                    bunnyList.Add(new Bunny((3, 0)));
                    bunnyList.Add(new Bunny((3, 2)));

                    foxList.Add(new Fox((1, 2), "Horizontal Left"));
                    foxList.Add(new Fox((4, 3), "Vertical Down"));
                    //foxList.Add(new Fox((3, 1), "Horizontal Right"));
                    //foxList.Add(new Fox((1, 1), "Vertical Up"));
                    break;

                case 46:
                    MinimalNumberOfMoves = 19;
                    mushroomList.Add(new Mushroom((0, 1)));
                    mushroomList.Add(new Mushroom((1, 0)));
                    mushroomList.Add(new Mushroom((1, 1)));

                    bunnyList.Add(new Bunny((2, 0)));
                    bunnyList.Add(new Bunny((3, 0)));
                    bunnyList.Add(new Bunny((4, 3)));

                    //foxList.Add(new Fox((1, 2), "Horizontal Left"));
                    //foxList.Add(new Fox((4, 3), "Vertical Down"));
                    //foxList.Add(new Fox((3, 1), "Horizontal Right"));
                    foxList.Add(new Fox((0, 3), "Vertical Up"));
                    break;

                case 47:
                    MinimalNumberOfMoves = 24;
                    mushroomList.Add(new Mushroom((2, 2)));
                    mushroomList.Add(new Mushroom((2, 3)));
                    mushroomList.Add(new Mushroom((3, 2)));

                    bunnyList.Add(new Bunny((4, 2)));
                    bunnyList.Add(new Bunny((1, 4)));
                    //bunnyList.Add(new Bunny((4, 3)));

                    //foxList.Add(new Fox((1, 2), "Horizontal Left"));
                    //foxList.Add(new Fox((4, 3), "Vertical Down"));
                    foxList.Add(new Fox((1, 3), "Horizontal Right"));
                    //foxList.Add(new Fox((0, 3), "Vertical Up"));
                    break;

                case 48:
                    MinimalNumberOfMoves = 28;
                    mushroomList.Add(new Mushroom((0, 3)));
                    mushroomList.Add(new Mushroom((1, 1)));
                    mushroomList.Add(new Mushroom((1, 2)));

                    bunnyList.Add(new Bunny((0, 2)));
                    bunnyList.Add(new Bunny((0, 4)));
                    bunnyList.Add(new Bunny((3, 0)));

                    //foxList.Add(new Fox((1, 2), "Horizontal Left"));
                    //foxList.Add(new Fox((4, 3), "Vertical Down"));
                    //foxList.Add(new Fox((1, 3), "Horizontal Right"));
                    //foxList.Add(new Fox((0, 3), "Vertical Up"));
                    break;

                case 49:
                    MinimalNumberOfMoves = 36;
                    mushroomList.Add(new Mushroom((2, 2)));
                    mushroomList.Add(new Mushroom((4, 4)));
                    //mushroomList.Add(new Mushroom((1, 2)));

                    bunnyList.Add(new Bunny((0, 4)));
                    bunnyList.Add(new Bunny((4, 3)));
                    bunnyList.Add(new Bunny((1, 3)));

                    foxList.Add(new Fox((1, 1), "Horizontal Right"));
                    //foxList.Add(new Fox((1, 2), "Horizontal Left"));
                    foxList.Add(new Fox((3, 1), "Vertical Down"));
                    //foxList.Add(new Fox((0, 3), "Vertical Up"));
                    break;

                case 50:
                    MinimalNumberOfMoves = 23;
                    mushroomList.Add(new Mushroom((0, 1)));
                    mushroomList.Add(new Mushroom((1, 0)));
                    mushroomList.Add(new Mushroom((2, 2)));

                    bunnyList.Add(new Bunny((0, 3)));
                    bunnyList.Add(new Bunny((0, 2)));
                    bunnyList.Add(new Bunny((1, 2)));

                    //foxList.Add(new Fox((1, 1), "Horizontal Right"));
                    foxList.Add(new Fox((3, 0), "Horizontal Left"));
                    //foxList.Add(new Fox((3, 1), "Vertical Down"));
                    //foxList.Add(new Fox((0, 3), "Vertical Up"));
                    break;

                case 51:
                    MinimalNumberOfMoves = 27;
                    mushroomList.Add(new Mushroom((4, 0)));
                    mushroomList.Add(new Mushroom((4, 2)));
                    mushroomList.Add(new Mushroom((4, 4)));

                    bunnyList.Add(new Bunny((0, 0)));
                    bunnyList.Add(new Bunny((1, 0)));
                    bunnyList.Add(new Bunny((3, 2)));

                    foxList.Add(new Fox((1, 2), "Horizontal Right"));
                    //foxList.Add(new Fox((3, 0), "Horizontal Left"));
                    foxList.Add(new Fox((2, 3), "Vertical Down"));
                    //foxList.Add(new Fox((0, 3), "Vertical Up"));
                    break;

                case 52:
                    MinimalNumberOfMoves = 34;
                    mushroomList.Add(new Mushroom((0, 4)));
                    mushroomList.Add(new Mushroom((3, 2)));
                    mushroomList.Add(new Mushroom((4, 0)));

                    bunnyList.Add(new Bunny((1, 3)));
                    bunnyList.Add(new Bunny((2, 4)));
                    bunnyList.Add(new Bunny((4, 3)));

                    foxList.Add(new Fox((1, 1), "Horizontal Right"));
                    //foxList.Add(new Fox((3, 0), "Horizontal Left"));
                    foxList.Add(new Fox((3, 1), "Vertical Down"));
                    //foxList.Add(new Fox((0, 3), "Vertical Up"));
                    break;

                case 53:
                    MinimalNumberOfMoves = 33;
                    mushroomList.Add(new Mushroom((1, 0)));
                    mushroomList.Add(new Mushroom((2, 0)));
                    mushroomList.Add(new Mushroom((4, 0)));

                    bunnyList.Add(new Bunny((1, 4)));
                    bunnyList.Add(new Bunny((0, 1)));
                    bunnyList.Add(new Bunny((0, 3)));

                    //foxList.Add(new Fox((1, 1), "Horizontal Right"));
                    foxList.Add(new Fox((3, 1), "Horizontal Left"));
                    //foxList.Add(new Fox((3, 1), "Vertical Down"));
                    foxList.Add(new Fox((1, 1), "Vertical Up"));
                    break;

                case 54:
                    MinimalNumberOfMoves = 22;
                    mushroomList.Add(new Mushroom((0, 2)));
                    mushroomList.Add(new Mushroom((1, 0)));
                    mushroomList.Add(new Mushroom((3, 1)));

                    bunnyList.Add(new Bunny((0, 1)));
                    bunnyList.Add(new Bunny((4, 1)));
                    bunnyList.Add(new Bunny((4, 3)));

                    //foxList.Add(new Fox((1, 1), "Horizontal Right"));
                    //foxList.Add(new Fox((3, 1), "Horizontal Left"));
                    foxList.Add(new Fox((1, 3), "Vertical Down"));
                    //foxList.Add(new Fox((1, 1), "Vertical Up"));
                    break;

                case 55:
                    MinimalNumberOfMoves = 32;
                    mushroomList.Add(new Mushroom((1, 2)));
                    mushroomList.Add(new Mushroom((4, 0)));
                    mushroomList.Add(new Mushroom((4, 4)));

                    bunnyList.Add(new Bunny((2, 2)));
                    bunnyList.Add(new Bunny((3, 0)));
                    bunnyList.Add(new Bunny((3, 2)));

                    //foxList.Add(new Fox((1, 1), "Horizontal Right"));
                    //foxList.Add(new Fox((3, 1), "Horizontal Left"));
                    foxList.Add(new Fox((3, 3), "Vertical Down"));
                    //foxList.Add(new Fox((1, 1), "Vertical Up"));
                    break;

                case 56:
                    MinimalNumberOfMoves = 43;
                    mushroomList.Add(new Mushroom((0, 4)));
                    mushroomList.Add(new Mushroom((2, 2)));
                    mushroomList.Add(new Mushroom((3, 4)));

                    bunnyList.Add(new Bunny((3, 0)));
                    bunnyList.Add(new Bunny((1, 4)));
                    bunnyList.Add(new Bunny((4, 1)));

                    foxList.Add(new Fox((1, 1), "Horizontal Right"));
                    //foxList.Add(new Fox((3, 1), "Horizontal Left"));
                    //foxList.Add(new Fox((3, 3), "Vertical Down"));
                    foxList.Add(new Fox((1, 3), "Vertical Up"));
                    break;

                case 57:
                    MinimalNumberOfMoves = 55;
                    mushroomList.Add(new Mushroom((4, 4)));
                    mushroomList.Add(new Mushroom((2, 2)));
                    mushroomList.Add(new Mushroom((4, 1)));

                    bunnyList.Add(new Bunny((0, 0)));
                    bunnyList.Add(new Bunny((3, 1)));
                    bunnyList.Add(new Bunny((1, 1)));

                    foxList.Add(new Fox((1, 3), "Horizontal Left"));
                    foxList.Add(new Fox((3, 4), "Horizontal Right"));
                    //foxList.Add(new Fox((3, 3), "Vertical Down"));
                    //foxList.Add(new Fox((1, 3), "Vertical Up"));
                    break;

                case 58:
                    MinimalNumberOfMoves = 67;
                    mushroomList.Add(new Mushroom((0, 4)));
                    mushroomList.Add(new Mushroom((2, 4)));
                    mushroomList.Add(new Mushroom((4, 0)));

                    bunnyList.Add(new Bunny((2, 2)));
                    bunnyList.Add(new Bunny((0, 1)));
                    bunnyList.Add(new Bunny((2, 0)));

                    foxList.Add(new Fox((1, 3), "Vertical Up"));
                    foxList.Add(new Fox((3, 3), "Horizontal Left"));
                    //foxList.Add(new Fox((3, 4), "Horizontal Right"));
                    //foxList.Add(new Fox((3, 3), "Vertical Down"));
                    break;

                case 59:
                    MinimalNumberOfMoves = 63;
                    mushroomList.Add(new Mushroom((0, 3)));
                    mushroomList.Add(new Mushroom((2, 2)));
                    mushroomList.Add(new Mushroom((4, 4)));

                    bunnyList.Add(new Bunny((1, 3)));
                    bunnyList.Add(new Bunny((3, 0)));
                    bunnyList.Add(new Bunny((2, 4)));

                    //foxList.Add(new Fox((1, 3), "Vertical Up"));
                    foxList.Add(new Fox((1, 1), "Horizontal Right"));
                    foxList.Add(new Fox((3, 2), "Horizontal Left"));
                    //foxList.Add(new Fox((3, 3), "Vertical Down"));
                    break;

                case 60:
                    MinimalNumberOfMoves = 87;
                    mushroomList.Add(new Mushroom((0, 3)));
                    mushroomList.Add(new Mushroom((2, 2)));
                    mushroomList.Add(new Mushroom((3, 0)));

                    bunnyList.Add(new Bunny((1, 3)));
                    bunnyList.Add(new Bunny((4, 3)));
                    bunnyList.Add(new Bunny((2, 4)));

                    //foxList.Add(new Fox((1, 3), "Vertical Up"));
                    foxList.Add(new Fox((1, 1), "Horizontal Right"));
                    //foxList.Add(new Fox((3, 2), "Horizontal Left"));
                    //foxList.Add(new Fox((3, 3), "Vertical Down"));
                    break;


            }
        }
    }
}
