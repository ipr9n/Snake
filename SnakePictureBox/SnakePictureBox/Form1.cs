using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        const int SquareSize = 20;
        Point headLocation = new Point(0, 0);
        List<Point> snakeBody = new List<Point>();
        private new Point food = new Point(7, 10);
        Keys snakeDirection;
        Random random = new Random();

        public Form1()
        {
            snakeBody.Add(new Point(0, 0));
            InitializeComponent();
        }

        private void GenerateFood()
        {
            timer1.Interval -= 5;
            food = new Point(random.Next(SquareSize), random.Next(SquareSize));
            foreach (var snakePart in snakeBody)
            {
                if (food == snakePart)
                {
                    food = new Point(random.Next(SquareSize), random.Next(SquareSize));
                    break;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = 250;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            timer1.Start();
            switch (e.KeyCode)
            {
                case Keys.Right when snakeDirection == Keys.Left && snakeBody.Count > 1:
                    snakeDirection = Keys.Left;
                    break;
                case Keys.Right:
                case Keys.Left when snakeDirection == Keys.Right && snakeBody.Count > 1:
                    snakeDirection = Keys.Right;
                    break;
                case Keys.Left:
                    snakeDirection = Keys.Left;
                    break;
                case Keys.Down when snakeDirection == Keys.Up && snakeBody.Count > 1:
                    snakeDirection = Keys.Up;
                    break;
                case Keys.Down:
                case Keys.Up when snakeDirection == Keys.Down && snakeBody.Count > 1:
                    snakeDirection = Keys.Down;
                    break;
                case Keys.Up:
                    snakeDirection = Keys.Up;
                    break;
            }
        }

        private void GameOver()
        {
            timer1.Stop();
            MessageBox.Show("GameOver");
            snakeBody.Clear();
            headLocation = new Point(0, 0);
            timer1.Interval = 250;
            snakeBody.Add(new Point(0, 0));
            snakeDirection = Keys.A;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
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

            for (int i = 1; i < snakeBody.Count; i++)
                if (snakeBody[0] == snakeBody[i])
                    GameOver();

            if (snakeBody[0] != food)
                snakeBody.RemoveAt(snakeBody.Count - 1);
            else
                GenerateFood();

            pictureBox1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            for (int x = 0; x < this.Width; x += SquareSize)
                e.Graphics.DrawLine(Pens.Black, x, 0, x, this.Height);
            for (int y = 0; y < this.Height; y += SquareSize)
                e.Graphics.DrawLine(Pens.Black, 0, y, this.Width, y);

            if (snakeBody[0].X * SquareSize >= pictureBox1.Width || snakeBody[0].X * SquareSize < 0 || snakeBody[0].Y * SquareSize < 0 ||
                snakeBody[0].Y * SquareSize >= pictureBox1.Height)
                GameOver();

            foreach (var Snake in snakeBody)
                e.Graphics.FillRectangle(Brushes.Green, Snake.X * SquareSize, Snake.Y * SquareSize, SquareSize, SquareSize);

            e.Graphics.FillRectangle(Brushes.Blue, food.X * SquareSize, food.Y * SquareSize, SquareSize, SquareSize);
        }
    }
}
