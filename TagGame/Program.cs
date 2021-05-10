using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TagGame
{
    class Program
    {
        static Random random = new Random();

        static int[,] map = new int[4,4];

        static int step;

        static DateTime start;

        static Timer timer;

        static void Init()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    map[i, j] = (i * 4 + j + 1) % 16;
            step = 0;
        }

        static void Mix()
        {
            for(int i=0; i<200; i++)
            {
                switch (random.Next(2) + i % 2 * 2)
                {
                    case 0: ToLeft(); break;
                    case 1: ToRight(); break;
                    case 2: ToUp(); break;
                    case 3: ToDown(); break;
                }
                //Анимация
                Thread.Sleep(30);
                Print();
            }
            start = DateTime.Now;
                
        }

        static bool Win()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (map[i, j] != (i * 4 + j + 1) % 16)
                        return false;
            return true;
                    
        }

        static void Print()
        {
            Console.SetCursorPosition(0, 1);
            Console.WriteLine($"Step: {step}");
            Console.WriteLine("");


            
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if(map[i,j]==0)
                        Console.Write("   ");
                    else
                        Console.Write($"{map[i, j],3}");
                }
                    
                Console.WriteLine();


            }
            //Console.WriteLine("__________________");
        }

        static void TimerCallback(object state)
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"{(DateTime.Now-start).TotalSeconds:F0}");
        }

        static ModelGame model;
        static void Main(string[] args)
        {
            model = new ModelGame();
         
             
            Console.CursorVisible = false;
            model.RePaint += Model_RePaint;
            model.Init();
            do
            {
                model.KeyDown(Console.ReadKey(true).Key);
  
            } while (!Win());
            Print();
            Console.WriteLine("You Win!!!");
            Console.ReadKey();
           
        }

        private static void Model_RePaint(object sender, int[,] e)
        {
            throw new NotImplementedException();
        }

        static void swap(ref int a, ref int b) => (a, b) = (b, a);

        static (int, int) FindSpace()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (map[i, j] == 0)
                        return (i, j);
            Console.WriteLine("Ошибка!!");
            return (0,0);
        }

        private static void ToDown()
        {
            var (r,c)=FindSpace();
            if (r > 0) swap(ref map[r - 1, c],ref map[r, c]);
        }

        private static void ToUp()
        {
            var (r, c) = FindSpace();
            if (r <3) swap(ref map[r , c], ref map[r+1, c]);
        }

        private static void ToRight()
        {
            var (r, c) = FindSpace();
            if (c>0) swap(ref map[r, c], ref map[r, c-1]);
        }

        private static void ToLeft()
        {
            var (r, c) = FindSpace();
            if (c < 3) swap(ref map[r, c], ref map[r, c+1]);
        }
    }
}
