using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tag16
{
    struct Point
    {
        public int X, Y;
        public Point(int x, int y) => (X, Y) = (x, y);

        public override string ToString()
        {
            return $"({X},{Y})";
        }
    }


    class Program
    {
        static int Len2(Point A, Point B)
        {
            int dx = A.X - B.X;
            int dy = A.Y - B.Y;
            return dx * dx + dy * dy;
        }

        static int Angle(Point A, Point B, Point C, Point D)
        {
            var sp = (B.X - A.X) * (D.X - C.X) + (B.Y - A.Y) * (D.Y - C.Y);
            var len = Len2(A, B) * Len2(C, D);
            var cos = sp / Math.Sqrt(len);
            return (int)Math.Round(Math.Acos(cos) * 180 / Math.PI);
        }

        static void Main(string[] args)
        {
            Point A = new Point(1, 1);

            Point B = new Point(1, 5);

            Point C = new Point(5, 5);

            Point D = new Point(5, 1);

            Console.WriteLine(Angle(A,B,C,D));

            int AB = Len2(A, B);
            int BC = Len2(B, C);
            int CD = Len2(C, D);
            int DA = Len2(D, A);
        }
    }
}
