using System;

namespace TagGameLib
{
    public enum MoveDirection { Up, Down,Left,Right, None}
    public class ModelGame
    {
        Random random = new Random();

        int[,] map = new int[4, 4];

        int step;

        DateTime start = DateTime.Now;



        //public Action<int[,]> RePaint { get; internal set; }  // функционально тоже самое
        public event EventHandler<int[,]> RePaint;
        public event EventHandler<EventArgs> WinComplite;

        public int Step => step;
        public int this[int row,int col] => map[row,col];

        // public int Step { get{ return step; } } //тоже самое     //чтение    //запись
     

        public void Init()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    map[i, j] = (i * 4 + j + 1) % 16;

            Mix();
            step = 0;
            RePaint(this, map);
        }

        //public void KeyDown(ConsoleKey key)       //для консольного приложения
        //{
        //    switch (key)
        //    {
        //        case ConsoleKey.LeftArrow: ToLeft(); break;
        //        case ConsoleKey.RightArrow: ToRight(); break;
        //        case ConsoleKey.UpArrow: ToUp(); break;
        //        case ConsoleKey.DownArrow: ToDown(); break;
        //    }
        //    RePaint(this, map);
        //}


        public void KeyDown(MoveDirection key)
        {
            switch (key)
            {
                case MoveDirection.Left: ToLeft(); break;
                case MoveDirection.Right: ToRight(); break;
                case MoveDirection.Up: ToUp(); break;
                case MoveDirection.Down: ToDown(); break;
            }
            RePaint(this, map);
        }





        public void Print()
        {
            //Console.SetCursorPosition(0, 1);
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
                if (emt.c < pos.c) 
                    for(int i=emt.c;i<pos.c;i++) ToLeft();
                else
                    for (int i = emt.c; i > pos.c; i--) ToRight();
            else if (emt.c == pos.c)
                if (emt.r < pos.r)
                    for (int i = emt.r; i < pos.r; i++) ToUp();
                else for (int i = emt.r; i > pos.r; i--)  ToDown();
            RePaint(this, map);
            if (Win())
            {
                WinComplite?.Invoke(this, EventArgs.Empty);
            }
        }

        void Mix()
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
            //start = DateTime.Now;

        }

        public bool Win()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (map[i, j] != (i * 4 + j + 1) % 16)
                        return false;
            return true;

        }

        (int r, int c) FindSpace(int num = 0)
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (map[i, j] == num)
                        return (i, j);
            throw new ArgumentException("Ошибка!!");
           
        }

        static void swap(ref int a, ref int b) => (a, b) = (b, a);
        public void ToDown()
        {
            var (r, c) = FindSpace();
            if (r > 0)
            {
                swap(ref map[r - 1, c], ref map[r, c]);
                step++;
            }
        }

        public void ToUp()
        {
            var (r, c) = FindSpace();
            if (r < 3)
            {
                swap(ref map[r, c], ref map[r + 1, c]);
                step++;
            }
        }

        public void ToRight()
        {
            var (r, c) = FindSpace();
            if (c > 0)
            {
                swap(ref map[r, c], ref map[r, c - 1]);
                step++;
            }
        }

        public void ToLeft()
        {
            var (r, c) = FindSpace();
            if (c < 3)
            {
                swap(ref map[r, c], ref map[r, c + 1]);
                step++;
            }
        }
    }
}
