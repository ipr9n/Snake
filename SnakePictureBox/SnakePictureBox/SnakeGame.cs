using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    class SnakeGame
    {
        private const int squareSize = 20;
        public List<Point> snakeBody = new List<Point>();
        private Point food = new Point(7, 10);
        private Keys snakeDirection;
        Random random = new Random();

        public void Restart()
        {
            snakeBody.Clear();
            snakeBody.Add(new Point(0, 0));
            snakeDirection = Keys.A;
        }

        public void GenerateFood()
        {
            food = new Point(random.Next(squareSize), random.Next(squareSize));
            foreach (var snakePart in snakeBody)
            {
                if (food == snakePart)
                {
                    GenerateFood();
                    break;
                }
            }
        }

        public void Update(Point WindowSize)
        {
            Point headLocation = snakeBody[0];
            switch (snakeDirection)
            {
                case Keys.Right:
                    headLocation.X += 1;

                    break;
                case Keys.Left:
                    headLocation.X -= 1;

                    break;
                case Keys.Up:
                    headLocation.Y -= 1;

                    break;
                case Keys.Down:
                    headLocation.Y += 1;

                    break;
            }

            snakeBody.Insert(0, headLocation);

            if (snakeBody[0] != food)
                snakeBody.RemoveAt(snakeBody.Count - 1);
            else
                GenerateFood();

            // if (snakeBody.Skip(1).Any(snakePart => snakePart == snakeBody[0]))
            //  GameOver();
            //if (snakeBody[0].X * squareSize >= WindowSize.X || snakeBody[0].X * squareSize < 0 || snakeBody[0].Y * squareSize < 0 ||
            //  snakeBody[0].Y * squareSize >= WindowSize.Y)
            //  GameOver();
        }

        public void Draw(Graphics graphics, Point WindowSize)
        {
            for (int x = 0; x < WindowSize.X; x += squareSize)
                graphics.DrawLine(Pens.Black, x, 0, x, WindowSize.Y);

            for (int y = 0; y < WindowSize.Y; y += squareSize)
                graphics.DrawLine(Pens.Black, 0, y, WindowSize.X, y);

            foreach (var Snake in snakeBody)
                graphics.FillRectangle(Brushes.Green, Snake.X * squareSize, Snake.Y * squareSize, squareSize, squareSize);

            graphics.FillRectangle(Brushes.Blue, food.X * squareSize, food.Y * squareSize, squareSize, squareSize);
        }
        public Keys GetOpositeDirection(Keys dirrection)
        {
            switch (dirrection)
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
                    return Keys.A;
            }
        }

        public void TurnSnake(Keys direction)
        {
            if (snakeBody.Count > 1 && direction == GetOpositeDirection(snakeDirection)) return;
            switch (direction)
            {
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:
                    snakeDirection = direction;
                    break;
            }

        }
    }
}
