using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        const int SizeOfSquare = 20;
        private static int ofx = 0, ofy = 0;
        List<Point> SnakeBody = new List<Point>();
        private new Point food = new Point(120, 120);
        Keys SnakeDirection;
        Random random = new Random();

        public Form1()
        {
            SnakeBody.Add(new Point(0, 0));
            InitializeComponent();
        }
        private void GenerateFood()
        {
            timer1.Interval -= 5;
            food = new Point(random.Next(SizeOfSquare) * SizeOfSquare, random.Next(SizeOfSquare) * SizeOfSquare);
            foreach (var Snake in SnakeBody)
            {
                if (food == Snake)
                {
                    food = new Point(random.Next(SizeOfSquare) * SizeOfSquare, random.Next(SizeOfSquare) * SizeOfSquare);
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
                case Keys.Right when SnakeDirection == Keys.Right && SnakeBody.Count > 1:
                    SnakeDirection = Keys.Left;
                    break;
                case Keys.Right:
                case Keys.Left when SnakeDirection == Keys.Right && SnakeBody.Count > 1:
                    SnakeDirection = Keys.Right;
                    break;
                case Keys.Left:
                    SnakeDirection = Keys.Left;
                    break;
                case Keys.Down when SnakeDirection == Keys.Up && SnakeBody.Count > 1:
                    SnakeDirection = Keys.Up;
                    break;
                case Keys.Down:
                case Keys.Up when SnakeDirection == Keys.Down && SnakeBody.Count > 1:
                    SnakeDirection = Keys.Down;
                    break;
                case Keys.Up:
                    SnakeDirection = Keys.Up;
                    break;
            }
        }
        private void GameOver()
        {
            timer1.Stop();
            MessageBox.Show("GameOver");
            SnakeBody.Clear();
            timer1.Interval = 250;
            SnakeBody.Add(new Point(0, 0));
            SnakeDirection = Keys.A;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (SnakeDirection)
            {
                case Keys.Right:
                    ofx = 1;
                    ofy = 0;

                    break;
                case Keys.Left:
                    ofx = -1;
                    ofy = 0;

                    break;
                case Keys.Up:
                    ofx = 0;
                    ofy = -1;

                    break;
                case Keys.Down:
                    ofx = 0;
                    ofy = 1;

                    break;
            }
            SnakeBody.Insert(0, new Point(SnakeBody[0].X + (ofx * SizeOfSquare), SnakeBody[0].Y + (ofy * SizeOfSquare)));
            if (SnakeBody[0] != food)
                SnakeBody.RemoveAt(SnakeBody.Count - 1);
            else
                GenerateFood();
            pictureBox1.Refresh();
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            for (int x = 0; x < this.Width; x += SizeOfSquare)
                e.Graphics.DrawLine(Pens.Black, x, 0, x, this.Height);
            for (int y = 0; y < this.Height; y += SizeOfSquare)
                e.Graphics.DrawLine(Pens.Black, 0, y, this.Width, y);

            if (SnakeBody[0].X >= pictureBox1.Width || SnakeBody[0].X < 0 || SnakeBody[0].Y < 0 ||
                SnakeBody[0].Y >= pictureBox1.Height)
                GameOver();

            for (int i = 1; i < SnakeBody.Count; i++)
                if (SnakeBody[0] == SnakeBody[i])
                    GameOver();

            foreach (var Snake in SnakeBody)
                e.Graphics.FillRectangle(Brushes.Green, Snake.X, Snake.Y, SizeOfSquare, SizeOfSquare);

            e.Graphics.FillRectangle(Brushes.Blue, food.X, food.Y, SizeOfSquare, SizeOfSquare);

        }
    }
}
