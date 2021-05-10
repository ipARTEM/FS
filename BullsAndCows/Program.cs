using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullsAndCows
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();


            string enigma = "";
            int bulls = 0, cows = 0, step=1;

            do
            {
                enigma = random.Next(1234, 6789).ToString();
            } while (!(enigma.Length==4
            && enigma.Distinct().Count()==4
            && enigma.All(d=>'0'<=d && d<='9')));

           

            do
            {
                Console.Write($"{step++}: ");
                string vers = Console.ReadLine();
                if (vers.Length != 4)
                {
                    Console.WriteLine("Должно быть 4 цифры!");
                }
                else if (!vers.All(c => char.IsDigit(c)))
                {
                    Console.WriteLine("Должны быть только цифры!");

                }
                else if (vers.Distinct().Count() != 4)
                {
                    Console.WriteLine("Все цифры должны различаться!!");
                }

                else
                {
                    bulls = 0;cows = 0;
                    for (int i = 0; i < 4; i++)
                    {
                        if (vers[i] == enigma[i]) bulls++;
                        else if (enigma.Contains(vers[i]))
                        {
                            cows++;
                        }

                    }
                  
                }
                Console.WriteLine($"Bulls: {bulls} Cows: {cows}");

                //if (vers == enigma)
                //{
                //    break;
                //}


            } while (bulls!=4);

            Console.WriteLine("You Win!!!");

        }
    }
}
