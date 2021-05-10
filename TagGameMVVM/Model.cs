using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace TagGameMVVM
{
    public class Model:ViewModel
    {
        Random random = new Random();
        int step;
        string timer;
        (int r, int c) space;

        ObservableCollection<Fishka> fishki = new ObservableCollection<Fishka>();

        public ObservableCollection<Fishka> Fishki =>fishki;

        //public int Step { get; private set;  }  // запрещено снаружи менять
        
        
        //public int Step // аналогична конструкции ниже
        //{
        //    get{ return step; }
        //    set
        //    {
        //        if (value != step)
        //        {
        //            step = value;
        //            Fire(nameof(Step));
        //        }
        //    }
        //}
        public int Step
        {
            get => step;
            private set => Set(ref step, value);
        }
        public void IncStep()=>Step++;

       

        //public void Fire()      //Вызов перерисовки, если изменился аргумент
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Step)));
        //}

        DateTime start = DateTime.Now;

        public event EventHandler<EventArgs> WinComplite;

    
       // public int this[int row, int col] => map[row, col];

        // public int Step { get{ return step; } } //тоже самое     //чтение    //запись

        public Model()
        {
            Init();
        }


        public void Init()
        {

            for (int k = 0; k < 15; k++)
            {
                var i = k / 4;
                var j = k % 4;
                fishki.Add(new Fishka(i, j,k + 1));
            }
            space = (3, 3);
            //Mix();
            step = 0;
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

        public Fishka FindFishka(int r, int c)
            => fishki.Where(f => f.IsHere(r, c)).FirstOrDefault();

        //{                             // выше это же сделано  через LINQ
        //    foreach(var f in fishki)
        //    {
        //        if(f.IsHere(r,c))
        //            return f;
        //    }

        //    return null;
        //}

        public void PressBy(Fishka fishka)
        {
            if (fishka.IsHere(space.r + 1, space.c)) 
                ToUp();
            if (fishka.IsHere(space.r - 1, space.c)) 
                ToDown();
            if (fishka.IsHere(space.r, space.c+1))
                ToLeft();
            if (fishka.IsHere(space.r, space.c -1)) 
                ToRight();
        }

        public void KeyDown(MoveDirection key)
        {
            switch (key)
            {
                case MoveDirection.Left:ToLeft(); break;
                case MoveDirection.Right:ToRight(); break;
                case MoveDirection.Up:ToUp(); break;
                case MoveDirection.Down:ToDown(); break;
            }
          
        }





        //public void Print()
        //{
        //    //Console.SetCursorPosition(0, 1);
        //    Console.WriteLine($"Step: {step}");
        //    Console.WriteLine("");



        //    for (int i = 0; i < 4; i++)
        //    {
        //        for (int j = 0; j < 4; j++)
        //        {
        //            if (map[i, j] == 0)
        //                Console.Write("   ");
        //            else
        //                Console.Write($"{map[i, j],3}");
        //        }

        //        Console.WriteLine();


        //    }
        //    //Console.WriteLine("__________________");
        //}

        //public void Press(int num)
        //{
        //    var emt = FindSpace();
        //    var pos = FindSpace(num);

        //    if (emt.r == pos.r)
        //        if (emt.c < pos.c)
        //            for (int i = emt.c; i < pos.c; i++) ToLeft();
        //        else
        //            for (int i = emt.c; i > pos.c; i--) ToRight();
        //    else if (emt.c == pos.c)
        //        if (emt.r < pos.r)
        //            for (int i = emt.r; i < pos.r; i++) ToUp();
        //        else for (int i = emt.r; i > pos.r; i--) ToDown();
       
        //    if (Win())
        //    {
        //        WinComplite?.Invoke(this, EventArgs.Empty);
        //    }
        //}

        void Mix()
        {
            for (int i = 0; i < 3; i++)
            {
                switch (random.Next(2) + i % 2 * 2)
                {
                    case 0: ToLeft(); break;
                    case 1: ToRight(); break;
                    case 2: ToUp(); break;
                    case 3: ToDown(); break;
                }
            }
        }

        public bool Win()
        {
           
            return true;

        }

        //(int r, int c) FindSpace(int num = 0)
        //{
        //    for (int i = 0; i < 4; i++)
        //        for (int j = 0; j < 4; j++)
        //            if (map[i, j] == num)
        //                return (i, j);
        //    throw new ArgumentException("Ошибка!!");

        //}
        //static void swap(ref int a, ref int b)
        //{
        //    (a, b) = (b, a);
        //    step++;
        //}

        Fishka MoveFrom(int r,int c)
        {
            var f = FindFishka(r, c);
            if (f != null)
            {
                space = (r, c);
            }
            return f;
        }


       void ToLeft()=> MoveFrom(space.r, space.c + 1)?.ToLeft();
       void ToRight()=> MoveFrom(space.r, space.c - 1)?.ToRight();
       void ToUp() => MoveFrom(space.r+1, space.c)?.ToUp();
       void ToDown() => MoveFrom(space.r-1, space.c)?.ToDown();
              
      
    }

    public class Fishka : ViewModel
    {
        int r, c;

        public int X =>c *110;
        public int Y => r * 110;

        public void ToDown()
        {
            if (r < 3)
            {
                r++;
                Fire(nameof(Y));
            }
        }

        public void ToUp()
        {
            if (r > 0)
            {
                r--;
                Fire(nameof(Y));
            }
        }

        public void ToRight()
        {
            if (c <3)
            {
                c++;
                Fire(nameof(X));
            }
        }

        public void ToLeft()
        {
            if (c >0)
            {
                c--;
                Fire(nameof(X));
            }
        }

        public bool IsHere(int r, int c) => r == this.r && c == this.c;

        public int Num { get; private set; }
        public Fishka(int r, int c,int num)
        {
            Num = num;
            this.r = r;
            this.c = c;
        }
    }

    public enum MoveDirection { Up, Down, Left, Right, None }
}
