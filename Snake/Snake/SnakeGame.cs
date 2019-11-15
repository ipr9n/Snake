using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    class SnakeGame
    {
        private Random random = new Random();
        private readonly int SquareSize;
        private readonly int SquareCount;
        private Point food;

        public SnakeGame(int squareSize, int squareCount)
        {
            this.SquareSize = squareSize;
            this.SquareCount = squareCount;
        }

        private List<Point> snakeBody = new List<Point>();

        private Keys snakeDirection;

        public event Action Defeat = delegate { };

        public void Draw(Graphics graphics)
        {
            for (int x = 0; x < SquareCount; x++)
                graphics.DrawLine(Pens.Black, x * SquareSize, 0, x * SquareSize, SquareCount * SquareSize);

            for (int y = 0; y < SquareCount; y++)
                graphics.DrawLine(Pens.Black, 0, y * SquareSize, SquareCount * SquareSize, y * SquareSize);

            foreach (var snakePart in snakeBody)
                graphics.FillRectangle(Brushes.Green, snakePart.X * SquareSize, snakePart.Y * SquareSize, SquareSize, SquareSize);

            graphics.FillRectangle(Brushes.Blue, food.X * SquareSize, food.Y * SquareSize, SquareSize, SquareSize);
        }

        private void GenerateFood()
        {
            do
            {
                food = new Point(random.Next(SquareSize), random.Next(SquareSize));
            } while (snakeBody.Contains(food));
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

            if (snakeBody.Skip(1).Any(snakePart => snakePart == snakeBody[0]))
                Defeat();

            if (snakeBody[0].X >= SquareCount ||
                snakeBody[0].X < 0 ||
                snakeBody[0].Y < 0 ||
                snakeBody[0].Y >= SquareCount)
                Defeat();
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

        public void Restart()
        {
            GenerateFood();
            snakeBody.Clear();
            snakeBody.Add(new Point(0, 0));
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
