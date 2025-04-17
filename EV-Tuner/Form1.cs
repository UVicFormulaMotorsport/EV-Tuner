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

        public void changeStatus(String ID, String message)
        {
            textBox2.Text = ID;
            textBox1.Text = message;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CanHandler.Initialize();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PcanChannel channel = PcanChannel.Usb01;
            PcanStatus result = Api.Initialize(channel, Bitrate.Pcan250);
            if (result != PcanStatus.OK)
            {
                // An error occurred
                Api.GetErrorText(result, out var errorText);
                Console.WriteLine(errorText);
            }
            else
            {
                // A success message on connection is shown.
                Console.WriteLine($"The hardware represented by the handle {channel} was successfully initialized.");
                PcanMessage msg = new PcanMessage()
                {
                    ID = 0x520,
                    DLC = 1,
                    MsgType = MessageType.Standard,
                    Data = new byte[] { 0x01 }
                };

                result = Api.Write(channel, msg);
                System.Threading.Thread.Sleep(1);
                if (result != PcanStatus.OK)
                {
                    // An error occurred
                    Api.GetErrorText(result, out var errorText);
                    Console.WriteLine(errorText);
                    Console.WriteLine($"Application terminated.");
                    return;
                }
                else
                {
                    Console.WriteLine($"Message was successfully sent.");
                }
                result = Api.Read(channel, out msg);
                if (result == PcanStatus.OK)
                {
                    // Process the received message
                    //
                    if (true)
                    {
                        Console.WriteLine(msg.ToString());
                        string msgText = $"Data: ";
                        for (int i = 0; i < msg.Length; i++)
                        {
                            msgText += $"{msg.Data[i]} ";
                        }
                        string idText = $"ID: ";
                        if ((msg.MsgType & MessageType.Extended) == MessageType.Extended)
                        {
                            idText += $"{msg.ID:X8}";
                        }
                        else
                        {
                            idText += $"{msg.ID:X4}";
                        }
                        Form1.Instance.changeStatus(idText, msgText);
                        ProcessMessage(msg);
                    }
                }
                //draft starts here
                if($"{msg.Data[5]} " == $"0" && $"{msg.Data[6]} " == $"1" && $"{msg.Data[7]} " == $"0")
                { 
                    Console.WriteLine($"VCU Firmware Accpetable"); 
                }
                // ends here
                result = Api.Uninitialize(channel);
                if (result != PcanStatus.OK)
                {
                    // An error occurred
                    Api.GetErrorText(result, out var errorText);
                    Console.WriteLine(errorText);
                }
                else
                {
                    Console.WriteLine($"The hardware represented by the handle {channel} was successfully finalized.");
                }
            }
        }
    }
}
