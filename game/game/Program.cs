using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    class Program
    {
        static public bool gameover;
        static public int width = 10;
        static public int height = 10;
        static public int x, y, fruitx, fruity, score;
        public enum eDirection { Stop, Left, Right, Up, Down };
        static public eDirection dir;
        static eDirection Stop = 0;
        
        static public void setup()//функция настройки нужных параметров
        {
            gameover = false;
            dir = Stop;
            x = width / 2 - 1;
            y = height / 2 - 1;
            var rand = new Random();           
            fruitx = rand.Next() % width;
            fruity = rand.Next() % height;
            score = 0;
        }

        static public void draw()//функция прорисовки поля игры
        {
            Console.Clear();

            for(int i = 0; i < width; ++i)
            {
                Console.Write(" #");               
            }
            Console.Write(" \n");

            for (int i = 0; i < height; ++i)
            {
                for(int j = 0; j < width; ++j)
                {
                    if(j == 0 || j == width)
                    {
                        Console.Write("#");
                        Console.Write("                   ");
                        Console.Write("#");
                        if(i == y && j == x)
                        {
                            Console.WriteLine("0");
                        }
                        else if(i == fruity && j == fruitx)
                        {
                            Console.WriteLine("f");
                        }
                    }
                    Console.Write(" ");                                     
                }
                Console.Write("\n");
            }

            for (int i = 0; i < width; ++i)
            {
                Console.Write(" #");                
            }
            Console.Write(" ");
            Console.WriteLine("\nscore: " + score);
            Console.ReadLine();
        }
        static public void input()//функция отслежки нажатий от игрока 
        {
            ConsoleKey tap = Console.ReadKey(true).Key;
            switch(tap)
            {
                case ConsoleKey.UpArrow:
                    dir = eDirection.Up;
                    break;
                case ConsoleKey.DownArrow:
                    dir = eDirection.Down;
                    break;
                case ConsoleKey.LeftArrow:
                    dir = eDirection.Left;
                    break;
                case ConsoleKey.RightArrow:
                    dir = eDirection.Right;
                    break;
                case ConsoleKey.Escape:
                    gameover = true;
                    break;
            }
        }
        static public void logic()//функция логики игры
        {
            switch(dir)
            {
                case eDirection.Stop:
                    dir = 0;
                    break;
                case eDirection.Up:
                    y -= 1;
                    break;
                case eDirection.Down:
                    y += 1;
                    break;
                case eDirection.Left:
                    x -= 1;
                    break;
                case eDirection.Right:
                    x += 1;
                    break;            
            }
            if(x > width || x < 0 || y > height || y < 0)
            {
                gameover = true;
            }
            if(x == fruitx && y == fruity)
            {
                score += 1;
                var rand = new Random();
                fruitx = rand.Next() % width;
                fruity = rand.Next() % height;                
            }
        }
        static public void Main(string[] args)
        {
            setup();
            while(!gameover)
            {
                draw();
                input();
                logic();
            }
            Console.ReadKey();
        }
    }
}
