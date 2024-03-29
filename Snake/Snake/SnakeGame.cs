﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Snake
{
    class SnakeGame
    {
        private Random random = new Random();
        private readonly int squareSize;
        private readonly int squareCount;

        private List<Point> snakeBody = new List<Point>();
        private Keys snakeDirection;
        private Point food;
        public event Action Defeat = delegate { };

        public SnakeGame(int squareSize, int squareCount)
        {
            this.squareSize = squareSize;
            this.squareCount = squareCount;
        }

        public void Restart()
        {
            GenerateFood();
            snakeBody.Clear();
            snakeBody.Add(new Point(0, 0));
        }

        public void TurnSnake(Keys direction)
        {
            switch (direction)
            {
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:
                    if (snakeBody.Count > 1 && direction == GetOpositeDirection(snakeDirection))
                        return;

                    snakeDirection = direction;
                    break;
            }
        }

        public void Update()
        {
            Point newHeadPosition = snakeBody[0];
            switch (snakeDirection)
            {
                case Keys.Right:
                    newHeadPosition.X += 1;

                    break;
                case Keys.Left:
                    newHeadPosition.X -= 1;

                    break;
                case Keys.Up:
                    newHeadPosition.Y -= 1;

                    break;
                case Keys.Down:
                    newHeadPosition.Y += 1;

                    break;
            }

            snakeBody.Insert(0, newHeadPosition);

            if (snakeBody[0] != food)
                snakeBody.RemoveAt(snakeBody.Count - 1);
            else
                GenerateFood();

            if (snakeBody.Skip(1).Contains(snakeBody[0]))
                Defeat();

            if (snakeBody[0].X >= squareCount ||
                snakeBody[0].Y >= squareCount ||
                snakeBody[0].Y < 0 ||
                snakeBody[0].X < 0
               )
                Defeat();
        }
        private void GenerateFood()
        {
            do
            {
                food = new Point(random.Next(squareSize), random.Next(squareSize));
            } while (snakeBody.Contains(food));
        }

        public void Draw(Graphics graphics)
        {
            for (int x = 0; x <= squareCount; x++)
                graphics.DrawLine(Pens.Black, x * squareSize, 0, x * squareSize, squareCount * squareSize);

            for (int y = 0; y <= squareCount; y++)
                graphics.DrawLine(Pens.Black, 0, y * squareSize, squareCount * squareSize, y * squareSize);

            foreach (Point snakePart in snakeBody)
                graphics.FillRectangle(Brushes.Green, snakePart.X * squareSize, snakePart.Y * squareSize, squareSize, squareSize);

            graphics.FillRectangle(Brushes.Blue, food.X * squareSize, food.Y * squareSize, squareSize, squareSize);
        }

        private Keys GetOpositeDirection(Keys direction)
        {
            switch (direction)
            {
                case Keys.Left:
                    return Keys.Right;
                case Keys.Right:
                    return Keys.Left;
                case Keys.Up:
                    return Keys.Down;
                case Keys.Down:
                    return Keys.Up;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
