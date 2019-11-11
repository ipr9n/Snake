using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        const int size = 20;
        private static int ofx = 0, ofy = 0;
        List<Point> SnakeBody = new List<Point>();
        private new Point food = new Point(120, 120);

        public string Move = "";

        Random loc = new Random();




        public Form1()
        {
            SnakeBody.Add(new Point(0, 0));
            InitializeComponent();


        }

        private void GenerateFood()
        {
            timer1.Interval -= 5;
            food = new Point(loc.Next(size) * size, loc.Next(size) * size);
            for (int i = 0; i < SnakeBody.Count; ++i)
            {
                if (food == SnakeBody[i])
                {
                    food = new Point(loc.Next(size) * size, loc.Next(size) * size);
                    break;
                }
            }
        }



        private void Form1_Load(object sender, EventArgs e)
        {

            timer1.Interval = 250;
            timer1.Start();


        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Right")
            {
                if (Move == "Left" && SnakeBody.Count > 1)
                    Move = "Left";
                else
                    Move = "Right";
            }

            if (e.KeyCode.ToString() == "Left")
            {
                if (Move == "Right" && SnakeBody.Count > 1)
                    Move = "Right";
                else
                    Move = "Left";
            }

            if (e.KeyCode.ToString() == "Down")
            {
                if (Move == "Up" && SnakeBody.Count > 1)
                    Move = "Up";
                else
                    Move = "Down";
            }

            if (e.KeyCode.ToString() == "Up")
            {
                if (Move == "Down" && SnakeBody.Count > 1)
                    Move = "Down";
                else
                    Move = "Up";
            }

        }

        private void GameOver()
        {
            timer1.Stop();
            MessageBox.Show("GameOver");
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (Move)
            {
                case "Right":
                    ofx = 1;
                    ofy = 0;

                    break;
                case "Left":
                    ofx = -1;
                    ofy = 0;

                    break;
                case "Up":
                    ofx = 0;
                    ofy = -1;

                    break;
                case "Down":
                    ofx = 0;
                    ofy = 1;

                    break;
            }
            pictureBox1.Refresh();
        }


        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

            for (int x = 0; x < this.Width; x += size)
                e.Graphics.DrawLine(Pens.Black, x, 0, x, this.Height);
            for (int y = 0; y < this.Height; y += size)
                e.Graphics.DrawLine(Pens.Black, 0, y, this.Width, y);


            SnakeBody.Insert(0, new Point(SnakeBody[0].X + (ofx * size), SnakeBody[0].Y + (ofy * size)));
            if (SnakeBody[0] != food)
                SnakeBody.RemoveAt(SnakeBody.Count - 1);
            else
                GenerateFood();

            if (SnakeBody[0].X >= pictureBox1.Width || SnakeBody[0].X < 0 || SnakeBody[0].Y < 0 ||
                SnakeBody[0].Y >= pictureBox1.Height) GameOver();

            for (int i = 1; i < SnakeBody.Count; ++i)
                if (SnakeBody[0] == SnakeBody[i]) GameOver();

            for (int i = 0; i < SnakeBody.Count; ++i)
                e.Graphics.FillRectangle(Brushes.Green, SnakeBody[i].X, SnakeBody[i].Y, size, size);

            e.Graphics.FillRectangle(Brushes.Blue, food.X, food.Y, size, size);




        }
    }
}
