using System.Collections.Generic;
using System;

namespace SoftUni_SpaceBattle
{
    class Game
    {
        Ship playerShip;
        int areasCount;

        List<StarSystem> areas;


        public Game(int areasCount)
        {
            playerShip = new Ship(Ship.BelongsTo.Player); // kreira plejera
            this.areasCount = areasCount;

            areas = new List<StarSystem>();
            for (int i = 0; i < areasCount; i++)
            {
                areas.Add(new StarSystem(Database.GetRandomSystemName()));
            }
        }

        public void Play()
        {

            for (int i = 0; i < areasCount; i++)
            {
                Console.WriteLine($"You endered the unknown star system number {i + 1} of {areasCount}.");
                areas[i].ScanStarSystem();

                playerShip.GetStats();

                if (areas[i].CheckForEnemies() == StarSystem.AreaType.hostile)
                {
                    Console.WriteLine($"Enemy ship \"{areas[i].EnemyShip.Name}\" approaching. You are so fucked!");
                    Console.WriteLine();
                    Console.WriteLine("This is where battle takes place!");
                    Console.WriteLine();

                    bool isAreaClear = false; // smisliti pametnije ime za promenljivu

                    // Refactoring -> user input treba da ide u posebnu klasu

                    while (!isAreaClear)
                    {
                        char userInput;
                        do
                        {
                            Console.WriteLine("Players move. Choose the option 1 2 3");
                            userInput = Console.ReadKey(true).KeyChar;
                        }
                        while (userInput != '1' && userInput != '2' && userInput != '3');




                        switch (userInput)
                        {
                            case '1':

                                // REFAKTORING -> Battle sistem treba da ide u Battle klasu. Ovde je samo zbog postavke. Mnogo koda se ponavlja sta nije dobro!!!
                                Console.WriteLine("Player fires a laser");

                                if (playerShip.PrimaryWeapon.IsHit())
                                {
                                    int damageToDeal = playerShip.PrimaryWeapon.DealDamage();
                                    Ship.Status enemyShipStatus = areas[i].EnemyShip.ReceiveDamage(damageToDeal);

                                    if (enemyShipStatus == Ship.Status.destroyed)
                                    {
                                        Console.WriteLine($"You destroyed enemy ship \"{areas[i].EnemyShip.Name}\". You can move on to the next system.");
                                        isAreaClear = true;
                                        Console.WriteLine("Press any key to continue!");
                                        Console.ReadKey();
                                        Console.Clear();
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Player dealt {damageToDeal} of damage.");
                                        Console.WriteLine($"\"{areas[i].EnemyShip.Name}\" is still in one peace.");
                                        //areas[i].EnemyShip.GetStats();
                                    }

                                }
                                else
                                {
                                    Console.WriteLine($"Player missed!");
                                }


                                break;
                            case '2':
                                Console.WriteLine("Player fires photon torpedo");
                                bool secondaryWeaponSucsess = playerShip.FireSecondaryWeapon();

                                if (secondaryWeaponSucsess && playerShip.SecondaryWeapon.IsHit())
                                {
                                    int damageToDeal = playerShip.SecondaryWeapon.DealDamage();
                                    Ship.Status enemyShipStatus = areas[i].EnemyShip.ReceiveDamage(damageToDeal);


                                    if (enemyShipStatus == Ship.Status.destroyed)
                                    {
                                        Console.WriteLine($"You destroyed enemy ship \"{areas[i].EnemyShip.Name}\". You can move on to the next system.");
                                        isAreaClear = true;
                                        Console.WriteLine("Press any key to continue!");
                                        Console.ReadKey();
                                        Console.Clear();
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Player dealt {damageToDeal} of damage.");
                                        Console.WriteLine($"\"{areas[i].EnemyShip.Name}\" is still in one peace.");
                                        //areas[i].EnemyShip.GetStats();
                                    }

                                }
                                else
                                {
                                    Console.WriteLine($"Player missed!");
                                }

                                
                                break;
                            case '3':
                                Console.WriteLine("Player decided to flee like a little bitch, and... ");
                                if (Dice.Roll() > 50)
                                {
                                    Console.WriteLine("... sucsesfully avoided the battle!");
                                    isAreaClear = true;
                                    Console.Clear();
                                }
                                else
                                {
                                    Console.WriteLine($"... failed. He remains in the {areas[i].Name}, to get his ass kicked.");
                                }

                                break;
                        }

                        if (isAreaClear == false)
                        {

                            // enemy turn
                            // REFACTORING -> Ovo treba da bude ista funkcija kao i kada player napada.
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.DarkMagenta;
                            Console.WriteLine("****  Enemys move ****");
                            areas[i].EnemyShip.GetStats();
                            Console.WriteLine($"\"{areas[i].EnemyShip.Name}\" fires a weapon.");

                            if (areas[i].EnemyShip.PrimaryWeapon.IsHit())
                            {
                                int damageToReceive = areas[i].EnemyShip.PrimaryWeapon.DealDamage();
                                Ship.Status playerShipStatus = playerShip.ReceiveDamage(damageToReceive);

                                Console.WriteLine($"Player received {damageToReceive} of damage!");

                                Console.ResetColor();
                                playerShip.GetStats();

                                if (playerShipStatus == Ship.Status.destroyed)
                                {
                                    Console.WriteLine($"You lost! Game Over!");
                                    return;
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Enemy missed!");
                            }
                            Console.WriteLine("*******************");
                            Console.ResetColor();
                        }
                    } // while end
                }
                else
                {
                    Console.WriteLine("You can peacefully move on to the next area! User input needed");
                    Console.WriteLine("Press any key to continue!");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }



    }
}
