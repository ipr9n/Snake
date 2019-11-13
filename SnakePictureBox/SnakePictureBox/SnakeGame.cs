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
        public int width;
        public int height;
        public int squareSize;
        public int squareCount;
        private new Point food;

        public SnakeGame()
        {
            squareSize = 20;
            squareCount = 20;
            width = 100;
            height = 100;
        }
        public event Action Defeat = delegate { };
        private List<Point> snakeBody = new List<Point>();
        private Keys snakeDirection;
        Random random = new Random();

        public void Restart()
        {
            GenerateFood();
            snakeBody.Clear();
            snakeBody.Add(new Point(0, 0));
            snakeDirection = Keys.A;
        }

        private void GenerateFood()
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

        public void Update()
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

            if (snakeBody.Skip(1).Any(snakePart => snakePart == snakeBody[0]))
            {
                Defeat();
                Restart();
            }

            if (snakeBody[0].X * squareSize >= width
                || snakeBody[0].X * squareSize < 0
                || snakeBody[0].Y * squareSize < 0
                || snakeBody[0].Y * squareSize >= height)
            {
                Defeat();
                Restart();
            }
        }

        public void Draw(Graphics graphics)
        {
            for (int x = 0; x < width; x += squareSize)
                graphics.DrawLine(Pens.Black, x, 0, x, height);

            for (int y = 0; y < height; y += squareSize)
                graphics.DrawLine(Pens.Black, 0, y, width, y);

            foreach (var snakePart in snakeBody)
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
                    return Keys.A;
            }
        }

        public void TurnSnake(Keys direction)
        {
            if (snakeBody.Count > 1 && direction == GetOpositeDirection(snakeDirection))
                return;
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
