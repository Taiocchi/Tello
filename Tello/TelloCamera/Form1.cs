using MjpegProcessor;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing.Imaging;
using Newtonsoft.Json;
using tellocs;

namespace TelloCamera
{
    public partial class Form1 : Form
    {
        string detectionUrl = "https://cv.sitai.duckdns.org/v1/object-detection/yolo";
        private MjpegDecoder mjpeg;
        private tellocs.TelloCmd _tello;

        public Form1()
        {
            InitializeComponent();
            _tello = new TelloCmd();
        }

        private void mjpeg_Error(object sender, MjpegProcessor.ErrorEventArgs e)
        {
            MessageBox.Show(e.Message);
        }
        private void mjpeg_FrameReady(object sender, FrameReadyEventArgs e)
        {
            Console.WriteLine("Frame ricevuto!");
            Bitmap bmp;
            using (var ms = new MemoryStream(e.FrameBuffer))
            {
                bmp = new Bitmap(ms);
            }

            // Invia il frame per l'elaborazione
            Task.Run(async () =>
            {
                Bitmap processedImage = await SendFrameForDetection(bmp);

                if (processedImage != null)
                {
                    pictureBox1.Invoke(new Action(() =>
                    {
                        pictureBox1.Image?.Dispose(); // Evita memory leak
                        pictureBox1.Image = new Bitmap(processedImage);
                    }));
                }

                bmp.Dispose(); // Libera la memoria del frame originale
            });
        }

        private async Task<Bitmap> SendFrameForDetection(Bitmap frame)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                frame.Save(ms, ImageFormat.Jpeg);
                byte[] imageBytes = ms.ToArray();

                using (HttpClient client = new HttpClient())
                using (MultipartFormDataContent content = new MultipartFormDataContent())
                {
                    content.Add(new ByteArrayContent(imageBytes), "image", "image.jpg");
                    HttpResponseMessage response = await client.PostAsync(detectionUrl, content);
                    response.EnsureSuccessStatusCode();
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    List<DetectionResult> detections = JsonConvert.DeserializeObject<List<DetectionResult>>(jsonResponse);

                    // Cloniamo il frame originale per modificarlo
                    Bitmap processedImage = new Bitmap(frame);
                    DrawDetections(processedImage, detections);

                    return processedImage;
                }
            }
        }

        private void DrawDetections(Bitmap image, List<DetectionResult> detections)
        {
            using (Graphics g = Graphics.FromImage(image))
            {
                foreach (var obj in detections)
                {
                    int x = (int)obj.Xmin;
                    int y = (int)obj.Ymin;
                    int xmax = (int)obj.Xmax;
                    int ymax = (int)obj.Ymax;
                    float confidence = (float)Math.Round(obj.Confidence, 1);

                    using (Pen pen = new Pen(Color.Lime, 2))
                    {
                        g.DrawRectangle(pen, x, y, xmax - x, ymax - y);
                    }

                    using (System.Drawing.Font font = new System.Drawing.Font("Arial", 8))
                    using (SolidBrush brush = new SolidBrush(Color.Blue))
                    {
                        string text = obj.Name + " " + confidence;
                        g.DrawString(text, font, brush, x + 5, y + 15);
                    }
                }
            }
        }


        private void Avvia_telecamera_Click(object sender, EventArgs e)
        {
            if ((sender as Button).Text == "Avvia camera") // AVVIA
            {
                try
                {
                    _tello.ExecuteCommand("command");
                    _tello.StartVideoStreaming();

                    // Aggiungi log per verificare che il flusso MJPEG stia iniziando
                    mjpeg = new MjpegDecoder();
                    mjpeg.FrameReady += mjpeg_FrameReady;
                    mjpeg.Error += mjpeg_Error;

                    // Controlla se il flusso MJPEG è attivo
                    Console.WriteLine("Provo a connettermi al flusso MJPEG...");
                    mjpeg.ParseStream(new Uri("http://127.0.0.1:9000")); // Start MJPEG stream
                    Console.WriteLine("Flusso MJPEG avviato.");

                    (sender as Button).Text = "STOP";
                    pictureBox1.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Errore durante l'avvio della telecamera: " + ex.Message);
                }
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


        public class DetectionResult
        {
            [JsonProperty("xmin")]
            public float Xmin { get; set; }
            [JsonProperty("ymin")]
            public float Ymin { get; set; }
            [JsonProperty("xmax")]
            public float Xmax { get; set; }
            [JsonProperty("ymax")]
            public float Ymax { get; set; }
            [JsonProperty("confidence")]
            public float Confidence { get; set; }
            [JsonProperty("name")]
            public string Name { get; set; }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

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
    }
}
