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
        string cameraExePath = @"..\..\..\TelloCamera\TelloCamera\bin\Debug\net8.0-windows\TelloCamera.exe"; //Modificare in base al percorso del form Camera

        private MjpegDecoder mjpeg;


        public Form1()
        {
            _tello = new TelloCmd();
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(MainForm_FormClosing);
        }
        public struct movimento
        {
            public string move;
            public int value;
        }

        movimento[] elemovimento = new movimento[100];


        private Process cameraProcess;

        private void Avvia_telecamera_Click(object sender, EventArgs e)
        {
            _tello.ExecuteCommand("command");
            _tello.StartVideoStreaming();


            /*if (cameraProcess == null || cameraProcess.HasExited)
            {
                if (File.Exists(cameraExePath))
                {
                    cameraProcess = Process.Start(cameraExePath);
                    Avvia_telecamera.Text = "STOP";
                }
                else
                {
                    MessageBox.Show("Errore: CameraForm.exe non trovato!");
                }
            }
            else
            {
                cameraProcess.CloseMainWindow();
                cameraProcess.Dispose();
                cameraProcess = null;
                Avvia_telecamera.Text = "Avvia camera";
            }*/
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (cameraProcess != null && !cameraProcess.HasExited)
            {
                try
                {
                    cameraProcess.Kill();
                    cameraProcess.WaitForExit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Errore durante la chiusura del processo: " + ex.Message);
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