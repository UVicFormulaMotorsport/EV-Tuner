using Peak.Can.Basic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EV_Tuner
{
    public partial class Form1 : Form
    {
        Timer rotationTimer;
        int x = -106;
        int y = 221;
        bool reversing = false;
        private static Form1 _instance;

        public Form1()
        {
            InitializeComponent();
            _instance = this;
        }

        public static Form1 Instance => _instance;

        void rotationTimer_Tick(object sender, EventArgs e)
        {
            Image flipImage = motorImage.Image;
            flipImage.RotateFlip(RotateFlipType.Rotate90FlipXY);
            motorImage.Image = flipImage;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(8, 7, 8);
            tabPage1.BackColor = Color.FromArgb(8, 7, 8);
            tabPage2.BackColor = Color.FromArgb(8, 7, 8);
            tabPage3.BackColor = Color.FromArgb(8, 7, 8);
            tabPage4.BackColor = Color.FromArgb(8, 7, 8);
            tabPage5.BackColor = Color.FromArgb(8, 7, 8);
            tabPage6.BackColor = Color.FromArgb(8, 7, 8);
            pictureBox2.BackColor = Color.Transparent;

            rotationTimer = new Timer();
            rotationTimer.Interval = 1;    //you can change it to handle smoothness
            rotationTimer.Tick += rotationTimer_Tick;

            //create pictutrebox events
            rotationTimer.Start();

            //Settings.Testing();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CanHandler.ReadExample();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CanHandler.SendMessage();
        }

        public void changeStatus(String message)
        {
            textBox1.Text = message;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CanHandler.Initialize();
        }
    }
}
