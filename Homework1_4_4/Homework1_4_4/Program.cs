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
            int playerPositionX;
            int playerPositionY;
            int keyPositionX;
            int keyPositionY;
            int chestPositionX;
            int chestPositionY;
            char[,] map = ReadMap("map1", out playerPositionX, out playerPositionY, out keyPositionX, out keyPositionY, out chestPositionX, out chestPositionY);

            DrawMap(map);
            Work(map, isPlaying, playerPositionY, playerPositionX, haveKey);

            Console.Clear();
            Console.WriteLine("Сундук открыт!");
            Console.Write("Для продолжения нажмите любую кнопку...");
            Console.ReadKey();
        }

        static void Work(char [,] map,bool isPlaying, int playerPositionY, int playerPositionX, bool haveKey)
        {
            const char WallSymbol = '&';
            const char KeySymbol = 'K';
            const char ChestSymbol = '0';
            int playerChangePositionX = 0;
            int playerChangePositionY = 0;

            while (isPlaying)
            {
                Console.SetCursorPosition(playerPositionY, playerPositionX);
                Console.Write("8");

                if (Console.KeyAvailable)
                {
                    Move(ref playerChangePositionX, ref playerChangePositionY);

                    if (map[playerPositionX + playerChangePositionX, playerPositionY + playerChangePositionY] == KeySymbol)
                    {
                        haveKey = true;
                    }

                    if ((map[playerPositionX + playerChangePositionX, playerPositionY + playerChangePositionY] != WallSymbol && map[playerPositionX + playerChangePositionX, playerPositionY + playerChangePositionY] != ChestSymbol) || (map[playerPositionX + playerChangePositionX, playerPositionY + playerChangePositionY] == ChestSymbol && haveKey == true))
                    {
                        if (map[playerPositionX + playerChangePositionX, playerPositionY + playerChangePositionY] == ChestSymbol && haveKey == true)
                        {
                            isPlaying = false;
                        }
                        else
                        {
                            Console.SetCursorPosition(playerPositionY, playerPositionX);
                            Console.Write(' ');
                            playerPositionX += playerChangePositionX;
                            playerPositionY += playerChangePositionY;
                        }
                    }

                    playerChangePositionX = 0;
                    playerChangePositionY = 0;
                }
            }
        }

        static void Move(ref int playerChangePositionX, ref int playerChangePositionY)
        {
            const ConsoleKey CaseUpArrow = ConsoleKey.UpArrow;
            const ConsoleKey CaseDownArrow = ConsoleKey.DownArrow;
            const ConsoleKey CaseLeftArrow = ConsoleKey.LeftArrow;
            const ConsoleKey CaseRightArrow = ConsoleKey.RightArrow;
            ConsoleKeyInfo key = Console.ReadKey(true);

            switch (key.Key)
            {
                case CaseUpArrow:
                    playerChangePositionX = -1;
                    playerChangePositionY = 0;
                    break;
                case CaseDownArrow:
                    playerChangePositionX = 1;
                    playerChangePositionY = 0;
                    break;
                case CaseLeftArrow:
                    playerChangePositionX = 0;
                    playerChangePositionY = -1;
                    break;
                case CaseRightArrow:
                    playerChangePositionX = 0;
                    playerChangePositionY = 1;
                    break;
                default:
                    break;
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

        static char [,] ReadMap(string mapName, out int playerPositionX, out int playerPositionY, out int keyPositionX, out int keyPositionY, out int chestPositionX, out int chestPositionY)
        {
            const char PlayerSymbol = '8';
            const char KeySymbol = 'K';
            const char ChestSymbol = '0';
            playerPositionX = 0;
            playerPositionY = 0;
            keyPositionX = 0;
            keyPositionY = 0;
            chestPositionX = 0;
            chestPositionY = 0;
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
                            playerPositionX = mapRow;
                            playerPositionY = mapColumn;
                            break;
                        case KeySymbol:
                            keyPositionX = mapRow;
                            keyPositionY = mapColumn;
                            break;
                        case ChestSymbol:
                            chestPositionX = mapRow;
                            chestPositionY = mapColumn;
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