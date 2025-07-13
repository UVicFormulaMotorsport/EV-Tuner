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
        private static Form1 _instance;
        public Settings currentSettings = new Settings(); // needs to change when connecting to CAN actually auto imports current settings
        
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

        private void loadSettings()
        {
            // needs to be updated every time new settings are added
            textBox6.Text = currentSettings.maxElectricalPower.ToString();
            textBox7.Text = currentSettings.maxMotorTorque.ToString();
            textBox8.Text = currentSettings.absoluteMaxAccumulatorCurrent.ToString();
            textBox11.Text = currentSettings.maxAccumulatorCurrent5s.ToString();
            textBox10.Text = currentSettings.absoluteMaxMotorRPM.ToString();
            textBox9.Text = currentSettings.regenRPMThreshold.ToString();
            textBox17.Text = currentSettings.minAPPSOffset.ToString();
            textBox16.Text = currentSettings.maxAPPSOffset.ToString();
            textBox15.Text = currentSettings.minAPPSValue.ToString();
            textBox14.Text = currentSettings.maxAPPSValue.ToString();
            textBox13.Text = currentSettings.minBPSValue.ToString();
            textBox12.Text = currentSettings.maxBPSValue.ToString();
            textBox19.Text = currentSettings.appsTOP.ToString();
            textBox21.Text = currentSettings.appsBOTTOM.ToString();
            textBox23.Text = currentSettings.appsPlausibilityCheckActiviationThreshold.ToString();
            textBox25.Text = currentSettings.bpsPlausibilityCheckActiviationThreshold.ToString();
            textBox27.Text = currentSettings.appsPlausibilityCheckRecoveryThreshold.ToString();
            textBox29.Text = currentSettings.bpsPlausibilityCheckRecoveryThreshold.ToString();
            textBox28.Text = currentSettings.numberDrivingModes.ToString();
            textBox26.Text = currentSettings.drivingLoopPeriod.ToString();
            textBox24.Text = currentSettings.regenSOCThreshold.ToString();
            textBox22.Text = currentSettings.someBoolFlags.ToString();
        }

        private void overwriteSettings()
        {
            string message1 = "Warning: This will overwrite saved data.";
            string caption1 = "Warning";
            MessageBoxButtons buttons1 = MessageBoxButtons.OKCancel;
            MessageBoxIcon icon1 = MessageBoxIcon.Exclamation;
            var result = MessageBox.Show(message1, caption1, buttons1, icon1);

            // needs to be updated every time new settings are added

            if (result == DialogResult.OK)
            {
                try
                {
                    currentSettings.maxElectricalPower = int.Parse(textBox6.Text);
                    currentSettings.maxMotorTorque = int.Parse(textBox7.Text);
                    currentSettings.absoluteMaxAccumulatorCurrent = int.Parse(textBox8.Text);
                    currentSettings.maxAccumulatorCurrent5s = int.Parse(textBox11.Text);
                    currentSettings.absoluteMaxMotorRPM = int.Parse(textBox10.Text);
                    currentSettings.regenRPMThreshold = int.Parse(textBox9.Text);
                    currentSettings.minAPPSOffset = int.Parse(textBox17.Text);
                    currentSettings.maxAPPSOffset = int.Parse(textBox16.Text);
                    currentSettings.minAPPSValue = int.Parse(textBox15.Text);
                    currentSettings.maxAPPSValue = int.Parse(textBox14.Text);
                    currentSettings.minBPSValue = int.Parse(textBox13.Text);
                    currentSettings.maxBPSValue = int.Parse(textBox12.Text);
                    currentSettings.appsTOP = int.Parse(textBox19.Text);
                    currentSettings.appsBOTTOM = int.Parse(textBox21.Text);
                    currentSettings.appsPlausibilityCheckActiviationThreshold = int.Parse(textBox23.Text);
                    currentSettings.bpsPlausibilityCheckActiviationThreshold = int.Parse(textBox25.Text);
                    currentSettings.appsPlausibilityCheckRecoveryThreshold = int.Parse(textBox27.Text);
                    currentSettings.bpsPlausibilityCheckRecoveryThreshold = int.Parse(textBox29.Text);
                    currentSettings.numberDrivingModes = int.Parse(textBox28.Text);
                    currentSettings.drivingLoopPeriod = int.Parse(textBox26.Text);
                    currentSettings.regenSOCThreshold = int.Parse(textBox24.Text);
                    currentSettings.someBoolFlags = int.Parse(textBox22.Text);
                }
                catch (Exception e)
                {
                    string message = "Make sure that all data fields are of the right type.";
                    string caption = "Error Detected in Input";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBoxIcon icon = MessageBoxIcon.Error;
                    MessageBox.Show(message, caption, buttons, icon);
                }
            }
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

            // Load Default Settings
            loadSettings();
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
            currentSettings = DataHandler.Import();
            loadSettings();
            Console.WriteLine("Data Imported");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DataHandler.Export(currentSettings);
            Console.WriteLine("Data Exported");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            overwriteSettings();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            loadSettings();
        }
    }
}
