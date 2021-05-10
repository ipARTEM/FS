using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = System.IO.File.ReadAllText("../../Program.cs");

            var cnt = text.Distinct().Count();  // Сколько разных символов
            var all = text.Length;  // Сколько всего символов

            var grp = text.ToUpper()
                .Where(x=>char.IsLetter(x))
                .GroupBy(x => x)
                .Select(x => new { x.Key, Cnt = x.Count() })
                .OrderBy(x=>x.Cnt)
                .ToArray();
        }
    }
}
