using System.Reflection.Metadata;

namespace zmeika
{
    enum WindowSize : int
    {
        Left = 20,
        Right = 40
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize((int)WindowSize.Right + 1, (int)WindowSize.Left + 1);
            Console.SetBufferSize((int)WindowSize.Right + 1, (int)WindowSize.Left + 1);
            Console.CursorVisible = false;
            bool start = true;

            Snake sn = new Snake();
            Thread th = new Thread(_ =>
            {
                Console.WriteLine();
                while (start)
                {
                    List<Snake> snake = new List<Snake>
                    {
                        new Snake
                        {
                            Body = '#',
                            PosY = 3,
                            PosX = 3
                        },
                        new Snake
                        {
                            Body = '#',
                            PosY = 3,
                            PosX = 2
                        },
                        new Snake
                        {
                            Body = '#',
                            PosY = 3,
                            PosX = 1
                        }
                    };
                    while (sn.StartGame)
                    {
                        sn.DrewSnake(snake);
                        Thread.Sleep(75);
                        snake = sn.MoveSnake(snake);
                        if (snake.Count() == (int)WindowSize.Left * (int)WindowSize.Right)
                        {
                            sn.StartGame = false;
                        }
                    }
                }
            });
            th.Start();
            while (start)
            {
                Console.Clear();
                Console.WriteLine("Начать - любая клавиша, Выйти - нажмите Ecape");
                ConsoleKeyInfo key = Console.ReadKey();
                Console.Clear();
                for (int i = 0; i < (int)WindowSize.Left - 1; i++)
                {
                    Console.SetCursorPosition((int)WindowSize.Right - 1, i);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("I");
                    Console.ResetColor();
                }
                for (int i = 0; i < (int)WindowSize.Right - 1; i++)
                {
                    Console.SetCursorPosition(i, (int)WindowSize.Left - 2);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("=");
                    Console.ResetColor();
                }
                sn.AddApple(Apple.CreateApple());
                sn.StartGame = true;
                if (key.Key == ConsoleKey.Escape)
                {
                    start = false;
                }
                while (sn.StartGame)
                {
                    sn.ChaseDirection(Console.ReadKey());
                }
            }
        }
    }
}