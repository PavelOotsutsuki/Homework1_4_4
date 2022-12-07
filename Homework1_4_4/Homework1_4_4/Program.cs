using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Homework1_4_4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            bool isPlaying = true;
            bool haveKey = false;
            int playerX;
            int playerY;
            int keyX;
            int keyY;
            int chestX;
            int chestY;
            char[,] map = ReadMap("map1", out playerX, out playerY, out keyX, out keyY, out chestX, out chestY);

            DrawMap(map);
            Work(map, isPlaying, playerY, playerX, haveKey);

            Console.Clear();
            Console.WriteLine("Сундук открыт!");
            Console.Write("Для продолжения нажмите любую кнопку...");
            Console.ReadKey();
        }

        static void Work(char [,] map,bool isPlaying, int playerY, int playerX, bool haveKey)
        {
            int playerDX = 0;
            int playerDY = 0;
            const char WallSymbol = '&';
            const char KeySymbol = 'K';
            const char ChestSymbol = '0';

            while (isPlaying)
            {
                Console.SetCursorPosition(playerY, playerX);
                Console.Write("8");

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    switch (key.Key)
                    {
                        case ConsoleKey.UpArrow:
                            playerDX = -1;
                            playerDY = 0;
                            break;
                        case ConsoleKey.DownArrow:
                            playerDX = 1;
                            playerDY = 0;
                            break;
                        case ConsoleKey.LeftArrow:
                            playerDX = 0;
                            playerDY = -1;
                            break;
                        case ConsoleKey.RightArrow:
                            playerDX = 0;
                            playerDY = 1;
                            break;
                        default:
                            break;
                    }

                    if (map[playerX + playerDX, playerY + playerDY] == KeySymbol)
                    {
                        haveKey = true;
                    }

                    if ((map[playerX + playerDX, playerY + playerDY] != WallSymbol && map[playerX + playerDX, playerY + playerDY] != ChestSymbol) || (map[playerX + playerDX, playerY + playerDY] == ChestSymbol && haveKey == true))
                    {
                        if (map[playerX + playerDX, playerY + playerDY] == ChestSymbol && haveKey == true)
                        {
                            isPlaying = false;
                        }
                        else
                        {
                            Console.SetCursorPosition(playerY, playerX);
                            Console.Write(' ');
                            playerX += playerDX;
                            playerY += playerDY;
                        }
                    }

                    playerDX = 0;
                    playerDY = 0;
                }
            }
        }

        static void DrawMap(char[,] map)
        {
            for (int mapRow=0; mapRow<map.GetLength(0); mapRow++)
            {
                for (int mapColumn=0;mapColumn<map.GetLength(1); mapColumn++)
                {
                    Console.Write(map[mapRow, mapColumn]);
                }

                Console.WriteLine();
            }
        }

        static char [,] ReadMap(string mapName, out int playerX, out int playerY, out int keyX, out int keyY, out int chestX, out int chestY)
        {
            const char PlayerSymbol = '8';
            const char KeySymbol = 'K';
            const char ChestSymbol = '0';
            playerX = 0;
            playerY = 0;
            keyX = 0;
            keyY = 0;
            chestX = 0;
            chestY = 0;
            string[] newFile = File.ReadAllLines($"Maps/{mapName}.txt");
            char[,] map = new char[newFile.Length, newFile[0].Length];

            for (int mapRow=0;mapRow<map.GetLength(0);mapRow++)
            {
                for( int mapColumn=0;mapColumn<map.GetLength(1); mapColumn++)
                {
                    map[mapRow, mapColumn] = newFile[mapRow][mapColumn];
                    char symbol = map[mapRow, mapColumn];

                    switch (symbol)
                    {
                        case PlayerSymbol:
                            playerX = mapRow;
                            playerY = mapColumn;
                            break;
                        case KeySymbol:
                            keyX = mapRow;
                            keyY = mapColumn;
                            break;
                        case ChestSymbol:
                            chestX = mapRow;
                            chestY = mapColumn;
                            break;
                        default:
                            break;
                    }
                }
            }

            return map;
        }
    }
}