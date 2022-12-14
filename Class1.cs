    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.ConstrainedExecution;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    namespace zmeika
    {
        internal class Snake
        {
            public bool StartGame = false;
            public char Body;
            public int PosY;
            public int PosX;

            private int HeplPosX;
            private int HeplPosY;

            private string DirectionSnake = "right";
            private int LastBodyPosX;
            private int LastBodyPosY;

            private Apple apple;

            public void AddApple(Apple ap)
            {
                apple = ap;
            }
            public void ChaseDirection(ConsoleKeyInfo key) // Направление змеи
            {
                if (key.Key == ConsoleKey.RightArrow && DirectionSnake != "left")
                {
                    DirectionSnake = "right";
                }
                else if (key.Key == ConsoleKey.LeftArrow && DirectionSnake != "right")
                {
                    DirectionSnake = "left";
                }
                else if (key.Key == ConsoleKey.UpArrow && DirectionSnake != "down")
                {
                    DirectionSnake = "up";
                }
                else if (key.Key == ConsoleKey.DownArrow && DirectionSnake != "up")
                {
                    DirectionSnake = "down";
                }
            }
            public List<Snake> MoveSnake(List<Snake> snake) // Движение
            {
                snake = Direction(snake);

                LastBodyPosX = snake[snake.Count() - 1].PosX;
                LastBodyPosY = snake[snake.Count() - 1].PosY;

                snake.RemoveAt(snake.Count() - 1);

                snake.Insert(1, new Snake
                {
                    Body = '#',
                    PosX = HeplPosX,
                    PosY = HeplPosY
                });
                return snake;
            }

            public void DrewSnake(List<Snake> snake) // Сама змея
            {
                foreach (var element in snake)
                {
                    Console.SetCursorPosition(element.PosX, element.PosY);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(element.Body);
                    Console.ResetColor();
                }
                Clear(LastBodyPosX, LastBodyPosY);
            }
            private void Clear(int x, int y) // Очищение тела змеи
            {
                Console.SetCursorPosition(x, y);
                Console.WriteLine(" ");
            }
            private List<Snake> AddTail(List<Snake> snake)  // Удлинение хвоста 
            {
                snake.Add(new Snake
                {
                    Body = '#',
                    PosX = HeplPosX,
                    PosY = HeplPosY
                });
                return snake;
            }
            private List<Snake> QualityPosApple(List<Snake> snake) // Проверка на смерть (или яблоко)
            {
                if (snake[0].PosX == apple.PosX && snake[0].PosY == apple.PosY)
                {
                    snake = AddTail(snake);
                    Clear(apple.PosX, apple.PosY);
                    apple = Apple.CreateApple();
                    foreach (var element in snake)
                    {
                        while (element.PosX == apple.PosX && element.PosY == apple.PosY)
                        {
                            Clear(apple.PosX, apple.PosY);
                            apple = Apple.CreateApple();
                        }
                    }
                }
                else if (snake[0].PosX > (int)WindowSize.Right - 2 || snake[0].PosY > (int)WindowSize.Left - 3 || snake[0].PosX < 0 || snake[0].PosY < 0)
                {
                    DirectionSnake = "right";
                    StartGame = false;
                }
                for (int i = 1; i < snake.Count(); i++)
                {
                    if (snake[0].PosX == snake[i].PosX && snake[0].PosY == snake[i].PosY)
                    {
                        DirectionSnake = "right";
                        StartGame = false;
                        break;
                    }
                }
                return snake;
            }
            private List<Snake> Direction(List<Snake> snake) // Проверка направления
            {
                if (DirectionSnake == "right")
                {
                    HeplPosX = snake[0].PosX++;
                    HeplPosY = snake[0].PosY;
                    snake = QualityPosApple(snake);
                }
                else if (DirectionSnake == "left")
                {
                    HeplPosX = snake[0].PosX--;
                    HeplPosY = snake[0].PosY;
                    snake = QualityPosApple(snake);
                }
                else if (DirectionSnake == "up")
                {
                    HeplPosX = snake[0].PosX;
                    HeplPosY = snake[0].PosY--;
                    snake = QualityPosApple(snake);
                }
                else if (DirectionSnake == "down")
                {
                    HeplPosX = snake[0].PosX;
                    HeplPosY = snake[0].PosY++;
                    snake = QualityPosApple(snake);
                }
                return snake;
            }
        }
    }