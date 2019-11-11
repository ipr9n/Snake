using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakePictureBox
{
    public partial class Form1 : Form
    {
        private PictureBox food;
        const int xh = 120, yh = 120, size = 20;
        public int SnakeLengths = 0;
        public string Move = "";
       // private List<PictureBox> trails = new List<PictureBox>();


            /* public void addTrail(Point last, int SnakeLength)
        {
            trails.Add(new PictureBox());
            this.Controls.Add(trails[SnakeLength]);
            trails[SnakeLength].Location = last;
            trails[SnakeLength].Size = new Size(size,size);
            trails[SnakeLength].BackColor = Color.Yellow;


        }*/
        public Form1()
        {
            food = new PictureBox();
            this.Controls.Add(food);
            food.BackColor = Color.Green;
            food.Size = new Size(size, size);
            food.Location = new Point(xh, yh);
            InitializeComponent();


        }


       

        private void Form1_Load(object sender, EventArgs e)
        {
            SnakeHead.Width = size;
            SnakeHead.Height = size;
            SnakeHead.Location = new Point(0, 0);
            SnakeHead.BackColor = Color.Blue;
            pictureBox1.Location = new Point(0,0);
            timer1.Interval = 250;
            timer1.Start();
            

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "Right")
                Move = "Right";
            if (e.KeyCode.ToString() == "Left")
                Move = "Left";
            if (e.KeyCode.ToString() == "Down")
                Move = "Down";
            if (e.KeyCode.ToString() == "Up")
                Move = "Up";
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
                    SnakeHead.Location = new Point(SnakeHead.Location.X + size, SnakeHead.Location.Y);
                    if (SnakeHead.Location.X == food.Location.X && SnakeHead.Location.Y == food.Location.Y)  // Проверка видит ли змея еду
                    {
                        GameOver();
                        //Generate food();
                        // Add trail();
                    }
                    if (SnakeHead.Location.X >= pictureBox1.Height - (size * 2)) // Смерть при врезании в стену
                        GameOver();
                    break;
                case "Left":
                    SnakeHead.Location = new Point(SnakeHead.Location.X - size, SnakeHead.Location.Y);
                    if (SnakeHead.Location.X == food.Location.X && SnakeHead.Location.Y == food.Location.Y)// Проверка видит ли змея еду
                    {
                        GameOver();
                        //Generate food();
                        
                    //     addTrail(new Point(SnakeHead.Location.X-size,SnakeHead.Location.Y),SnakeLengths);
                     //    SnakeLengths++;
                    }
                    if (SnakeHead.Location.X < 0) // Смерть при врезании в стену
                        GameOver();
                    break;
                case "Up":
                    SnakeHead.Location = new Point(SnakeHead.Location.X, SnakeHead.Location.Y-size);
                    if (SnakeHead.Location.X == food.Location.X && SnakeHead.Location.Y == food.Location.Y)// Проверка видит ли змея еду
                    {
                        GameOver();
                        //Generate food();
                        // Add trail();
                    }
                    if (SnakeHead.Location.Y < 0) // Смерть при врезании в стену
                        GameOver();
                    break;
                case "Down":
                    SnakeHead.Location = new Point(SnakeHead.Location.X, SnakeHead.Location.Y+size);
                    if (SnakeHead.Location.X == food.Location.X && SnakeHead.Location.Y == food.Location.Y)// Проверка видит ли змея еду
                    {
                        GameOver();
                        //Generate food();
                        // Add trail();
                    }
                    if (SnakeHead.Location.Y >= pictureBox1.Width-size) // Смерть при врезании в стену
                        GameOver();
                    break;
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
         {
             //                      Заливка поля                            ///

             for (int x = 0; x < this.Width; x += size)
                 e.Graphics.DrawLine(Pens.Black, x, 0, x, this.Height);
             for (int y = 0; y < this.Height; y += size)
                 e.Graphics.DrawLine(Pens.Black, 0, y, this.Width, y);
             ///                                                     /////////////


         }
    }
}
