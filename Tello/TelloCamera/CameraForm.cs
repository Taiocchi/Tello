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

namespace TelloCamera
{
    public partial class CameraForm : Form
    {
        private MjpegDecoder mjpeg;
        private bool isProcessingYOLO = false;
        int frameCountYolo;
        Timer timer = new Timer();
        int Xmin;
        int Xmax;
        private bool timerStarted = false;
        int[] array = new int[100];
        int i = 0;

        public CameraForm()
        {
            InitializeComponent();
            mjpeg = new MjpegDecoder();
            mjpeg.FrameReady += mjpeg_FrameReadyYOLO;
            mjpeg.Error += mjpeg_Error;
            mjpeg.ParseStream(new Uri("http://127.0.0.1:9000"));
            if (!timerStarted)
            {
                timer.Interval = 2000;
                timer.Tick += new EventHandler(Distance);
                timer.Start();
                timerStarted = true;
            }
        }
        private async void mjpeg_FrameReadyYOLO(object sender, FrameReadyEventArgs e)
        {
            if (isProcessingYOLO)
            {
                return; // Skip processing if the previous call is still running
            }

            isProcessingYOLO = true;

            try
            {
                using (MemoryStream ms = new MemoryStream(e.FrameBuffer))
                {
                    var detection = await RunDetect(e.FrameBuffer, new Bitmap(ms));

                    System.Drawing.Bitmap newImg = new System.Drawing.Bitmap(ms);
                    System.Drawing.Graphics gr = System.Drawing.Graphics.FromImage(newImg);
                    // Disegna FPS
                    System.Drawing.Font fpsFont = new System.Drawing.Font("Arial", 16);
                    System.Drawing.SolidBrush fpsBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Yellow);    
                     
                    Xmin = (int)detection.Xmin;
                    Xmax = (int)detection.Xmax;

                    if (detection != default(DetectionResult))
                    {
                        // Converti le coordinate in interi
                        int x = (int)detection.Xmin;
                        int y = (int)detection.Ymin;
                        int xmax = (int)detection.Xmax;
                        int ymax = (int)detection.Ymax;
                        float confidence = (float)Math.Round(detection.Confidence, 1);

                        // Disegna il rettangolo

                        using (Pen pen = new Pen(Color.FromArgb(255, 0, 255, 0), 1))
                        {
                            // Disegna linee individuali per replicare il comportamento esatto di OpenCV
                            gr.DrawLine(pen, x, y, xmax, y);       // Linea superiore
                            gr.DrawLine(pen, xmax, y, xmax, ymax); // Linea destra
                            gr.DrawLine(pen, xmax, ymax, x, ymax); // Linea inferiore
                            gr.DrawLine(pen, x, ymax, x, y);       // Linea sinistra
                        }
                        // Disegna il testo
                        using (Font font = new Font("Arial", 8))
                        using (SolidBrush brush = new SolidBrush(Color.FromArgb(255, 0, 0, 255)))
                        {
                            string text = detection.Name + " " + confidence;
                            gr.DrawString(text, font, brush, x + 5, y + 15);
                        }
                    }

                    // Salva l'immagine elaborata
                    pictureBox1.Image = null;
                    pictureBox1.Image = newImg;

                    fpsFont.Dispose();
                    fpsBrush.Dispose();
                    gr.Dispose();
                }

                isProcessingYOLO = false;

            }
            catch (Exception ex)
            {
                //this.Text = ex.Message;
                // esci dall'applicazione se c'è un errore
                //System.Windows.Forms.Application.Exit();
            }
        }

        private void Distance(object sender, EventArgs e)
        {    
            int distanza = Xmax - Xmin;
            i++;

            array[i-1] = distanza;

            if (i >= 2 && array[i - 2] >= array[i - 1])
                label1.Text = "Si sta avvicinando!!";
            else if(i >= 2 && array[i - 2] <= array[i - 1])
                label1.Text = "Si sta allontanando!!";
        }


        private static async Task<DetectionResult> RunDetect(byte[] frameB, Bitmap frameIn = null)
        {
            try
            {
                // URL per il servizio di object detection
                string url = "https://cv.sitai.duckdns.org/v1/object-detection/yolo";

                //https://powerful-man-distinctly.ngrok-free.app/v1/object-detection/yolo
                //https://cv.sitai.duckdns.org/v1/object-detection/yolo


                // Invia l'immagine al servizio di object detection
                string responseText = await SendImageForDetection(url, frameB);

                //File.WriteAllText("detection_response.json", responseText);

                List<DetectionResult> detections = JsonConvert.DeserializeObject<List<DetectionResult>>(responseText);

                // cerca tra le detection se siano presenti oggetti person e nel caso restituisce quello con la confidenza più alta
                var personDetection = detections.Where(d => d.Name == "person").OrderByDescending(d => d.Confidence).FirstOrDefault();
                return personDetection ?? null;

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
                    content.Add(new ByteArrayContent(imageBytes), "image", "image.jpg");

                    try
                    {
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

            [JsonProperty("class")]
            public int Class { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }
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
