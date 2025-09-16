using MjpegProcessor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Reflection.Emit;

namespace TelloCamera
{
    public partial class CameraForm : Form
    {
        private MjpegDecoder mjpeg;
        private DateTime _lastDetectionTime = DateTime.MinValue;

        public CameraForm()
        {
            InitializeComponent();
            mjpeg = new MjpegDecoder();
            mjpeg.FrameReady += mjpeg_FrameReady;
            mjpeg.Error += mjpeg_Error;
            mjpeg.ParseStream(new Uri("http://127.0.0.1:9000"));
        }

        private async void mjpeg_FrameReady(object sender, FrameReadyEventArgs e)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream(e.FrameBuffer))
                {
                    using (Bitmap bmp = new Bitmap(ms))
                    {
                        // Analizza solo se sono passati almeno 333ms
                        if ((DateTime.Now - _lastDetectionTime).TotalMilliseconds >= 999)
                        {
                            _lastDetectionTime = DateTime.Now;

                            string nome = await RunDetect(e.FrameBuffer, bmp);
                            if (nome != null)
                                label1.Text = nome;
                        }

                        System.Drawing.Bitmap newImg = new System.Drawing.Bitmap(ms);
                        pictureBox1.Image = newImg;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Errore: " + ex.Message);
            }
        }


        private static async Task<string> RunDetect(byte[] frameB, Bitmap frameIn = null)
        {
            try
            {
                // URL per il servizio di object detection
                string url = "http://localhost:5001/classify";

                //https://powerful-man-distinctly.ngrok-free.app/v1/object-detection/yolo
                //https://cv.sitai.duckdns.org/v1/object-detection/yolo

                // Invia l'immagine al servizio di object detection
                string responseText = await SendImageForDetection(url, frameB);

                PredictionResult detections = JsonConvert.DeserializeObject<PredictionResult>(responseText);

                string class_name = detections.Label;

                //await Task.Delay(1000); // Pausa per 1 secondo (100

                return class_name;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore durante il rilevamento: {ex.Message}");
            }

            return null;
        }

        public static async Task<string> SendImageForDetection(string url, byte[] imageBytes)
        {
            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(2); // Timeout più lungo

                using (MultipartFormDataContent content = new MultipartFormDataContent())
                {
                    // Aggiungi l'immagine al contenuto della richiesta
                    var fileContent = new ByteArrayContent(imageBytes);
                    fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
                    content.Add(fileContent, "file", "image.jpg");


                    try
                    {
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        // Invia la richiesta POST
                        HttpResponseMessage response = await client.PostAsync(url, content);

                        // Verifica che la richiesta sia stata completata con successo
                        response.EnsureSuccessStatusCode();

                        // Leggi e restituisci la risposta
                        return await response.Content.ReadAsStringAsync();
                    }
                    catch (HttpRequestException ex)
                    {
                        Console.WriteLine($"Errore HTTP: {ex.Message}");
                        throw;
                    }
                }
            }
        }
        public class PredictionResult
        {
            [JsonProperty("label")]
            public string Label { get; set; }

            [JsonProperty("confidence")]
            public double Confidence { get; set; }
        }

        private void mjpeg_Error(object sender, MjpegProcessor.ErrorEventArgs e)
        {
            MessageBox.Show(e.Message);
        }

        private void CameraForm_Load(object sender, EventArgs e)
        {

        }
    }
}

/*private void mjpeg_FrameReady(object sender, FrameReadyEventArgs e)
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
*/