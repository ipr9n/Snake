using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        const int SquareSize = 20;
        List<Point> snakeBody = new List<Point>();
        private Point food = new Point(7, 10);
        Keys snakeDirection;
        Random random = new Random();

        public Form1()
        {
            snakeBody.Add(new Point(0, 0));
                      InitializeComponent();
                      timer1.Interval = 250;
        }

        private void GenerateFood()
        {
            food = new Point(random.Next(SquareSize), random.Next(SquareSize));
            foreach (var snakePart in snakeBody)
            {
                if (food == snakePart)
                {
                   GenerateFood();
                    break;
                }
            }
        }

        public Keys GetOppositeDirrection(Keys dirrection)
        {
            if (dirrection == Keys.Left) return Keys.Right;
            if (dirrection == Keys.Right) return Keys.Left;
            if (dirrection == Keys.Up) return Keys.Down;
            if (dirrection == Keys.Down) return Keys.Up;
            else return Keys.A;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            timer1.Start();
            if (snakeBody.Count > 1 && e.KeyCode == GetOppositeDirrection(snakeDirection)) return;
            else
            {
                switch (e.KeyCode)
                {
                    case Keys.Left:
                    case Keys.Right:
                    case Keys.Up:
                    case Keys.Down:
                        snakeDirection = e.KeyCode;
                        break;
                }
            }
        }

        private void GameOver()
        {
            timer1.Stop();
            snakeBody.Clear();
            timer1.Interval = 250;
            snakeBody.Add(new Point(0, 0));
           snakeDirection = Keys.A;
            MessageBox.Show("GameOver");
        }

        private void timer1_Tick(object sender, EventArgs e)
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
            {
                timer1.Interval -= 5;
                GenerateFood();
            }
            if (snakeBody.Skip(1).Any(snakePart => snakePart == snakeBody[0]))
                GameOver();
            if (snakeBody[0].X * SquareSize >= pictureBox1.Width || snakeBody[0].X * SquareSize < 0 || snakeBody[0].Y * SquareSize < 0 ||
                snakeBody[0].Y * SquareSize >= pictureBox1.Height)
                GameOver();
            pictureBox1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            for (int x = 0; x < this.Width; x += SquareSize)
                e.Graphics.DrawLine(Pens.Black, x, 0, x, this.Height);
            for (int y = 0; y < this.Height; y += SquareSize)
                e.Graphics.DrawLine(Pens.Black, 0, y, this.Width, y);

           
            foreach (var Snake in snakeBody)
                e.Graphics.FillRectangle(Brushes.Green, Snake.X * SquareSize, Snake.Y * SquareSize, SquareSize, SquareSize);

            e.Graphics.FillRectangle(Brushes.Blue, food.X * SquareSize, food.Y * SquareSize, SquareSize, SquareSize);

        }
    }
}
