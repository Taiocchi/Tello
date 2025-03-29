using MjpegProcessor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Diagnostics;
using System.IO;

namespace TelloCamera
{
    public partial class CameraForm : Form
    {
        private MjpegDecoder mjpeg;
        public CameraForm()
        {
            InitializeComponent();
            mjpeg = new MjpegDecoder();
            mjpeg.FrameReady += mjpeg_FrameReady;
            mjpeg.Error += mjpeg_Error;
            mjpeg.ParseStream(new Uri("http://127.0.0.1:9000"));
        }
        private void mjpeg_FrameReady(object sender, FrameReadyEventArgs e)
        {
            using (var ms = new MemoryStream(e.FrameBuffer))
            {
                pictureBox1.Image = new Bitmap(ms);
            }
        }

        private void mjpeg_Error(object sender, MjpegProcessor.ErrorEventArgs e)
        {
            MessageBox.Show(e.Message);
        }
    }
}
