using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Snake
{

    public partial class Form1 : Form
    {
        private SnakeGame snakeGame = new SnakeGame();

        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 100;
            snakeGame.Restart();
            timer1.Start();
            snakeGame.Defeat += GameOver;
            snakeGame.squareSize = 20;
            snakeGame.squareCount = 20;
            snakeGame.width = snakeGame.squareSize * snakeGame.squareCount;
            snakeGame.height = snakeGame.squareSize * snakeGame.squareCount;
            pictureBox1.Width = snakeGame.squareSize * snakeGame.squareCount + snakeGame.squareSize;
            pictureBox1.Height = snakeGame.squareSize * snakeGame.squareCount + snakeGame.squareSize * 2;
            this.Size = pictureBox1.Size;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            snakeGame.TurnSnake(e.KeyCode);
        }

        public void GameOver()
        {
            timer1.Stop();
            timer1.Interval = 250;
            MessageBox.Show("GameOver");
            timer1.Start();
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
