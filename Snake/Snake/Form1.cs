using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        private const int SquareSize = 20;
        private const int SquareCount = 20;
        private SnakeGame snakeGame = new SnakeGame(SquareSize, SquareCount);

        public Form1()
        {
            InitializeComponent();

            timer1.Interval = 200;
            snakeGame.Restart();
            snakeGame.Defeat += GameOver;
            pictureBox1.Width = SquareSize * SquareCount + SquareSize;
            pictureBox1.Height = SquareSize * SquareCount + SquareSize * 2;
            this.Size = pictureBox1.Size;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            snakeGame.TurnSnake(e.KeyCode);
            if (!timer1.Enabled)
                timer1.Start();
        }

        public void GameOver()
        {
            timer1.Stop();
            timer1.Interval = 200;
            MessageBox.Show("GameOver");
            snakeGame.Restart();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            snakeGame.Update();
            pictureBox1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            snakeGame.Draw(e.Graphics);
        }
    }
}
