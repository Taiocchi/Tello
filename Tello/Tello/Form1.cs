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
        Thread threadTelecamera;

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
        public struct movimento
        {
            public string move;
            public int value;
        }

        movimento[] elemovimento = new movimento[100];


        private void Avvia_telecamera_Click(object sender, EventArgs e)
        {
            // Passiamo il riferimento al pulsante al thread
            threadTelecamera = new Thread(new ParameterizedThreadStart(telecamera));
            threadTelecamera.Start(sender);
        }

        private void telecamera(object param)
        {
            if (param is Button button)
            {
                if (button.InvokeRequired)
                {
                    button.Invoke(new Action(() => telecamera(button)));
                    return;
                }

                if (button.Text == "Avvia camera") // AVVIA
                {
                    // Esegui i comandi di _tello nel thread UI
                    this.Invoke(new Action(() =>
                    {
                        _tello.ExecuteCommand("command");
                        _tello.StartVideoStreaming();
                    }));

                    // Inizializza MJPEG nel thread UI
                    mjpeg = new MjpegDecoder();
                    mjpeg.FrameReady += mjpeg_FrameReady;
                    mjpeg.Error += mjpeg_Error;
                    mjpeg.ParseStream(new Uri("http://127.0.0.1:9000")); // Start stream

                    pictureBox1.Invoke(new Action(() =>
                    {
                        pictureBox1.Visible = true;
                        button.Text = "STOP";
                    }));
                }
                else // FERMA
                {
                    // Esegui lo stop nel thread UI
                    this.Invoke(new Action(() =>
                    {
                        _tello.StopVideoStreaming();
                    }));

                    mjpeg.FrameReady -= mjpeg_FrameReady;
                    mjpeg.Error -= mjpeg_Error;
                    mjpeg.StopStream();

                    pictureBox1.Invoke(new Action(() =>
                    {
                        pictureBox1.Visible = false;
                        pictureBox1.Image = null;
                        button.Text = "Avvia camera";
                    }));
                }
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
            for (int i = 0; i < 4; i++)
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





        private void button5_Click_1(object sender, EventArgs e)
        {
            bool visual = default;
            if (visual != true)
            {

                MessageBox.Show("Il drone esegue movimenti in un ciclo a righe alterne:\r\n\r\nAlza (Up)\r\nAbbassa (Down)\r\nAvanza (Forward)\r\nIndietro (Back)\r\nDestra (Right)\r\nSinistra (Left)\r\nDecollo (TakeOff)\r\nAtterraggio (Land)\r\nOgni movimento è associato al valore di un'azione da compiere, eccetto per takeOff e land, dove la riga seguente deve rimanere vuota per garantire la corretta sequenza operativa.\r\n\r\nLa corretta sequenza prevede che, tra i comandi di decollo e atterraggio, non vi siano movimenti. L'ordine e la logica dei comandi sono fondamentali per il funzionamento sicuro e preciso del drone.", "ATTENZIONE!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                visual = true;

            }
            OpenFileDialog openfiledialog;

            openfiledialog = new OpenFileDialog();
            openfiledialog.InitialDirectory = @"Risorse";
            openfiledialog.CheckPathExists = true;
            openfiledialog.CheckFileExists = true;
            openfiledialog.DefaultExt = @".txt";
            openfiledialog.FilterIndex = 1;




            openfiledialog.ShowDialog();
            if (string.IsNullOrEmpty(openfiledialog.FileName))
                return;
            textBox1.Text = openfiledialog.FileName;

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("selezionare prima un percorso");
                return;
            }
            int x = default;



            StreamReader percorso;
            percorso = new StreamReader(System.IO.Path.Combine(@"risorse", textBox1.Text));


            while (percorso.EndOfStream == false)
            {
                elemovimento[x].move = percorso.ReadLine();

                elemovimento[x].value = int.Parse(percorso.ReadLine());
                x++;

            }
            int y = default;

            if (elemovimento[0].move != "Takeoff")
            {
                MessageBox.Show("il primo comando deve essere quello di decollo");
                return;
            }
            while (y < x)
            {
                if (elemovimento[x].move == "Takeoff")
                {
                    _tello.Takeoff();
                }

                if (elemovimento[y].move == "Land")
                {
                    _tello.Land();
                }
                if (elemovimento[y].move == "Up")
                {
                    _tello.Up(elemovimento[y].value);
                }
                if (elemovimento[y].move == "Down")
                {
                    _tello.Down(elemovimento[y].value);
                }
                if (elemovimento[y].move == "Forward")
                {
                    _tello.Forward(elemovimento[y].value);
                }
                if (elemovimento[y].move == "Back")
                {
                    _tello.Back(elemovimento[y].value);
                }
                if (elemovimento[y].move == "Right")
                {
                    _tello.Right(elemovimento[y].value);
                }
                if (elemovimento[y].move == "Left")
                {
                    _tello.Left(elemovimento[y].value);
                }
                y++;
                if (y % 2 == 0)
                {
                    if (elemovimento[y].move != "Up" && elemovimento[y].move != "Down" && elemovimento[y].move != "Land" && elemovimento[y].move != "Takeoff" && elemovimento[y].move != "Left" && elemovimento[y].move != "Right" && elemovimento[y].move != "Back" && elemovimento[y].move != "Forward")
                    {
                        MessageBox.Show("uno o più dati non sono validi, ricontrollali");
                    }
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {



            // Avvia l'applicazione Blocco Note (Notepad)
            Process.Start("notepad.exe");

        }
    }
}
