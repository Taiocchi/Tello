using MjpegProcessor;
using demoTello.helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using tellocs;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Diagnostics;
using System.IO;


namespace Tello
{
    public partial class Form1 : Form
    {

        private tellocs.TelloCmd _tello;

        Thread threadStop;

        private MjpegDecoder mjpeg;

        private void mjpeg_FrameReady(object sender, FrameReadyEventArgs e)
        {
            Bitmap bmp;
            using (var ms = new MemoryStream(e.FrameBuffer))
            {
                bmp = new Bitmap(ms);
            }

            System.Drawing.Image newImg = (System.Drawing.Image)bmp.Clone();
            bmp.Dispose();

            newImg.RotateFlip(RotateFlipType.RotateNoneFlipX);

            System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(newImg);
            string drawString = "camera!";
            System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 12);
            System.Drawing.SolidBrush drawBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);

            float x = 560.0f;
            float y = 460.0f;
            gr.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Green), new System.Drawing.Rectangle(545, 465, 10, 10));

            gr.DrawString(drawString, drawFont, drawBrush, x, y);

            gr.DrawString(DateTime.Now.ToLocalTime().ToString(), drawFont, drawBrush, 12, y);

            pictureBox1.Image = newImg;

            drawFont.Dispose();
            drawBrush.Dispose();
            gr.Dispose();
        }

        private void mjpeg_Error(object sender, MjpegProcessor.ErrorEventArgs e)
        {
            MessageBox.Show(e.Message);
        }

        protected void StartMJPEGserverProcess()
        {
            //Per drone
            //DRONE: -- ffmpeg -i udp://192.168.10.1:11111  -video_size 640x480 -vcodec mjpeg -rtbufsize 1000M -f mpjpeg -r 10 -q 3 -

            //WEBCAM: var camera = "Logi C310 HD WebCam";
            //string arguments = $@"ffmpeg -f dshow -i video=""GENERAL WEBCAM"":audio=""Microfono (2 - GENERAL WEBCAM)"" -vcodec mjpeg -acodec aac -r 30 -s 640x480 -y output.mp4";

            string arguments = $@"-- ffmpeg -i udp://192.168.10.1:11111  -video_size 640x480 -vcodec mjpeg -rtbufsize 1000M -f mpjpeg -r 10 -q 3 -";

            ProcessStartInfo info = new ProcessStartInfo()
            {
                FileName = "MJPEGServer.exe",
                Arguments = arguments,
                UseShellExecute = true, // window
                LoadUserProfile = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                //RedirectStandardOutput = true,
            };

            Process.Start(info);
        }
        public Form1()
        {
            _tello = new TelloCmd();
            InitializeComponent();
        }
        
        private void Avvia_telecamera_Click(object sender, EventArgs e)
        {
            if ((sender as Button).Text == "Avvia camera") // AVVIA
            {
                _tello.ExecuteCommand("command");
                _tello.StartVideoStreaming();
                //StartMJPEGserverProcess(); // avvia lo streaming da ffmpeg
                mjpeg = new MjpegDecoder();
                mjpeg.FrameReady += mjpeg_FrameReady;
                mjpeg.Error += mjpeg_Error;
                mjpeg.ParseStream(new Uri("http://127.0.0.1:9000")); // start stream
                (sender as Button).Text = "STOP";
                pictureBox1.Visible = true;
            }
            else // FERMA
            
            {
                pictureBox1.Visible = false;
                pictureBox1.Image = null;
                _tello.StopVideoStreaming();

                mjpeg.FrameReady -= mjpeg_FrameReady;
                mjpeg.Error -= mjpeg_Error;
                mjpeg.StopStream();

                (sender as Button).Text = "Avvia camera";
            }
        }

        //Collegare tello
        private void button1_Click(object sender, EventArgs e)
        {
            if (_tello.Connected)
            {
                _tello.Disconnect();
                button1.Text = "CONNECT";
            }
            else
            {
                _tello.Connect();
                button1.Text = "DISCONNECT";
            }
        }

        //Disegna un quadrato di 20cm
        private void button2_Click(object sender, EventArgs e)
        {
            for(int i=0; i<4; i++)
            {
                _tello.Fly("forward", 20);
                _tello.Clockwise(90);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            threadStop = new Thread(Stop);
            threadStop.Start();
        }

        private void Stop()
        {
            _tello.Emergency();
        }

        private void buttonTakeOff_Click(object sender, EventArgs e)
        {
            _tello.Takeoff();
        }

        private void buttonLand_Click(object sender, EventArgs e)
        {
            _tello.Land();
        }

        private void buttonUP_Click(object sender, EventArgs e)
        {
            _tello.Up(Convert.ToInt32(numericUpDown_distance.Value));
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            _tello.Down(Convert.ToInt32(numericUpDown_distance.Value));
        }

        private void buttonSX_Click(object sender, EventArgs e)
        {
            _tello.Left(Convert.ToInt32(numericUpDown_distance.Value));
        }

        private void buttonDX_Click(object sender, EventArgs e)
        {
            _tello.Right(Convert.ToInt32(numericUpDown_distance.Value));
        }

        private void buttonForward_Click(object sender, EventArgs e)
        {
            _tello.Forward(Convert.ToInt32(numericUpDown_distance.Value));
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            _tello.Back(Convert.ToInt32(numericUpDown_distance.Value));
            
        }

        private void buttonRotate_Click(object sender, EventArgs e)
        {
            _tello.Clockwise(Convert.ToInt32(numericUpDownDegrees.Value));
        }

        private void buttonFlip_Click(object sender, EventArgs e)
        {
            _tello.Flip(comboBoxDirections.SelectedItem.ToString());
        }

        private void buttonBattery_Click(object sender, EventArgs e)
        {
            MessageBox.Show(_tello.Battery.ToString() + " %", "Battery");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show(_tello.Speed.ToString() + " cm/s", "Speed");
        }
    }
}
