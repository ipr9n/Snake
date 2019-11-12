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
        Random random = new Random();
        
        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 250;
            snakeGame.Restart();
            timer1.Start();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            snakeGame.TurnSnake(e.KeyCode);
        }

    /*    private void GameOver()
        {
            timer1.Stop();
            snakeBody.Clear();
            timer1.Interval = 250;
            snakeBody.Add(new Point(0, 0));
           snakeDirection = Keys.A;
            MessageBox.Show("GameOver");
        }*/

        private void timer1_Tick(object sender, EventArgs e)
        {
           snakeGame.Update(new Point(pictureBox1.Width, pictureBox1.Height));
           pictureBox1.Refresh();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            snakeGame.Draw(e.Graphics, new Point(pictureBox1.Width, pictureBox1.Height));
        }
    }

}
