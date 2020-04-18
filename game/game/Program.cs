using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace game
{
    public enum Direction
    {
        Stop = 0, Left, Right, Up, Down
    };

    class Program
    {
        static int[] tailX = new int[360];
        static int[] tailY = new int[360];
        static int ntail = 1;
        static public bool gameover;
        static public int width;
        static public int height;
        static public int x, y, fruitx, fruity, score;      
        static public Direction dir;
        

        static void Setup()//функция настройки нужных параметров
        {
            Random rand = new Random();
            int random = rand.Next();

            gameover = false;
            dir = Direction.Stop;
            x = width / 2;
            y = height / 2;           
            fruitx = random % (width - 2);
            fruity = random % (height - 2);
            score = 0;
        }

        static public void Draw()//функция прорисовки поля игры
        {
            Console.Clear();

            for (int i = 0; i <= width; ++i)
            {
                Console.Write("#");
            }
            Console.WriteLine(); 

            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < width; ++j)
                {
                    if (j == 0 || j == (width - 1))
                    {
                        Console.Write("#");
                    }
                    if (i == y && j == x)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("O");
                    }
                    else if (i == fruity && j == fruitx)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("@");
                    }
                    else
                    {
                        bool print = false;
                        for (int k = 1; k < ntail; ++k)
                        {
                            if (tailX[k] == j && tailY[k] == i)
                            {
                                print = true;
                                Console.Write("o");
                            }
                        }
                        if (!print)
                        {
                            Console.Write(" ");
                        }
                    }
                }
                Console.WriteLine();
            }

            for(int i = 0; i <= width; ++i)
            {
                Console.Write("#");
            }
            Console.WriteLine();
            Console.WriteLine("\nscore: " + score);
        }

        static void Input()//функция отслежки нажатий от игрока 
        {   
            ConsoleKey keyInfo = Console.ReadKey(true).Key;
            switch (keyInfo)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.DownArrow:
                    if (keyInfo == ConsoleKey.UpArrow)
                    {
                        dir = Direction.Up;
                    }
                    else if(keyInfo == ConsoleKey.DownArrow)
                    {
                        dir = Direction.Down;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                case ConsoleKey.RightArrow:
                    if (keyInfo == ConsoleKey.LeftArrow)
                    {
                        dir = Direction.Left;
                    }
                    else if (keyInfo == ConsoleKey.RightArrow)
                    {
                        dir = Direction.Right;
                    }
                    break;
                case ConsoleKey.Escape:
                    gameover = true;
                    break;
            }  
        }
        static public void Logic()//функция логики игры
        {
            tailX[0] = x;
            tailY[0] = y;
            int prevx = tailX[0];
            int prevy = tailY[0];
            int prev2X, prev2Y;
            for(int i = 0; i < ntail; ++i)
            {
                prev2X = tailX[i];
                prev2Y = tailY[i];
                tailX[i] = prevx;
                tailY[i] = prevy;
                prevx = prev2X;
                prevy = prev2Y;
            }

            switch (dir)
            {
                case Direction.Stop:
                    dir = 0;
                    break;
                case Direction.Up:
                    y--;
                    break;
                case Direction.Down:
                    y++;
                    break;
                case Direction.Left:
                    x--;
                    break;
                case Direction.Right:
                    x++;
                    break;
            }
            if (x == width || x < 0 || y == height || y < 0)
            {
                gameover = true;
                Console.WriteLine("!!!!!!!!!!!!!!GAMEOVER!!!!!!!!!!!!!!!");
            }
            if (x == fruitx && y == fruity)
            {
                score += 10;
                Random rand = new Random();
                int random = rand.Next();
                fruitx = random % width;
                fruity = random % height;
            }
        }
        static public void Main(string[] args)
        {
            int n;
            int Easy = 1;
            int Medium = 2;
            int Hard = 3;
            int Exit = 4;
            Console.WriteLine("Select level(1 - Easy, 2 - Medium, 3 - Hard, 4 - Exit): ");
            n = Int32.Parse(Console.ReadLine());
            Console.Clear();
            if (n == Easy)
            {
                width = 10;
                height = 10;
                Setup();
                while (!gameover)
                {
                    Draw();
                    Input();
                    Logic();
                }
            }
            else if (n == Medium)
            {
                width = 20;
                height = 20;
                Setup();
                while (!gameover)
                {
                    Draw();
                    Input();
                    Logic();
                }
            }
            else if (n == Hard)
            {
                width = 30;
                height = 30;
                Setup();
                while (!gameover)
                {
                    Draw();
                    Input();
                    Logic();
                }
            }
            else if(n == Exit)
            {
                gameover = true;
            }
            else
            {
                Console.WriteLine("Restart game and select correct level(Easy, Medium, Hard)!");
            }
            Console.ReadKey();

        }
    }
}

