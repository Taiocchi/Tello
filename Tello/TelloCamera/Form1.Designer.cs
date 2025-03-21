namespace TelloCamera
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pictureBox1 = new PictureBox();
            Avvia_telecamera = new Button();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(90, 26);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(747, 480);
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // Avvia_telecamera
            // 
            Avvia_telecamera.Font = new Font("Microsoft Sans Serif", 24F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            Avvia_telecamera.Location = new Point(291, 544);
            Avvia_telecamera.Name = "Avvia_telecamera";
            Avvia_telecamera.Size = new Size(350, 59);
            Avvia_telecamera.TabIndex = 1;
            Avvia_telecamera.Text = "Avvia camera";
            Avvia_telecamera.UseVisualStyleBackColor = true;
            Avvia_telecamera.Click += Avvia_telecamera_Click;
            // 
            // button1
            // 
            button1.Location = new Point(137, 580);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 2;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(995, 652);
            Controls.Add(button1);
            Controls.Add(Avvia_telecamera);
            Controls.Add(pictureBox1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private Button Avvia_telecamera;
        private Button button1;
    }
}
