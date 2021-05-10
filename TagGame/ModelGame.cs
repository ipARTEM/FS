using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TagGame
{
    public class ModelGame
    {
        Random random = new Random();

        int[,] map = new int[4, 4];

        int step;

        DateTime start =DateTime.Now;

        

        //public Action<int[,]> RePaint { get; internal set; }  // функционально тоже самое
        public event EventHandler<int[,]> RePaint;


        // public int Step { get{ return step; } } //тоже самое     //чтение    //запись
        public int Step => step;
        public int this[int row,int col]=>map[row,col];

        //public ModelGame()
        //{
        //    Init();
         
        //}


        public void KeyDown(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.LeftArrow: ToLeft(); break;
                case ConsoleKey.RightArrow: ToRight(); break;
                case ConsoleKey.UpArrow: ToUp(); break;
                case ConsoleKey.DownArrow: ToDown(); break;
            }
            RePaint(this, map);
        }

        public void Init()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    map[i, j] = (i * 4 + j + 1) % 16;
            
            Mix();
            step = 0;
            RePaint?.Invoke(this, map);
        }

        public void Mix()
        {
            for (int i = 0; i < 200; i++)
            {
                switch (random.Next(2) + i % 2 * 2)
                {
                    case 0: ToLeft(); break;
                    case 1: ToRight(); break;
                    case 2: ToUp(); break;
                    case 3: ToDown(); break;
                }
                
                //Thread.Sleep(30);//Анимация
                Print();
            }
            start = DateTime.Now;

        }

        public bool Win()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (map[i, j] != (i * 4 + j + 1) % 16)
                        return false;
            return true;

        }

        public void Print()
        {
            Console.SetCursorPosition(0, 1);
            Console.WriteLine($"Step: {step}");
            Console.WriteLine("");



            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (map[i, j] == 0)
                        Console.Write("   ");
                    else
                        Console.Write($"{map[i, j],3}");
                }

                Console.WriteLine();


            }
            //Console.WriteLine("__________________");
        }

        public void Press(int num)
        {
            var emt = FindSpace();
            var pos = FindSpace(num);

            if (emt.r == pos.r)
                if (emt.c < pos.c) ToLeft();
                else ToRight();
            else if (emt.c == pos.c)
                if (emt.r < pos.r) ToUp();
                else ToDown();
            RePaint(this, map);


        }

        public (int r, int c) FindSpace(int num = 0)
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (map[i, j] == num)
                        return (i, j);
            throw new ArgumentException("Ошибка!!");
            
        }

        void swap(ref int a, ref int b) => (a, b) = (b, a);

        public void ToDown()
        {
            var (r, c) = FindSpace();
            if (r > 0) swap(ref map[r - 1, c], ref map[r, c]);
            step++;
        }

        public void ToUp()
        {
            var (r, c) = FindSpace();
            if (r < 3) swap(ref map[r, c], ref map[r + 1, c]);
            step++;
        }

        public void ToRight()
        {
            var (r, c) = FindSpace();
            if (c > 0) swap(ref map[r, c], ref map[r, c - 1]);
            step++;
        }

        public void ToLeft()
        {
            var (r, c) = FindSpace();
            if (c < 3) swap(ref map[r, c], ref map[r, c + 1]);
            step++;
        }
    }
}
