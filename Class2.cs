using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zmeika
{
        internal class Apple
        {
            public char Body;
            public int PosX;
            public int PosY;

            public static Apple CreateApple()
            {
                Random rnd = new Random();
                Apple al = new Apple
                {
                    Body = 'O',
                    PosX = rnd.Next(1, Console.WindowWidth - 2),
                    PosY = rnd.Next(1, Console.WindowHeight - 3)
                };
                Console.SetCursorPosition(al.PosX, al.PosY); ;
                Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine(al.Body);
                Console.ResetColor();
            return al;
            }
        }
}
