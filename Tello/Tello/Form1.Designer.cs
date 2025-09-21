namespace Tello
{
    partial class Form1
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.buttonTakeOff = new System.Windows.Forms.Button();
            this.buttonLand = new System.Windows.Forms.Button();
            this.labelHeight = new System.Windows.Forms.Label();
            this.numericUpDown_distance = new System.Windows.Forms.NumericUpDown();
            this.buttonUP = new System.Windows.Forms.Button();
            this.buttonDown = new System.Windows.Forms.Button();
            this.buttonDX = new System.Windows.Forms.Button();
            this.buttonSX = new System.Windows.Forms.Button();
            this.buttonForward = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.numericUpDownDegrees = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxDirections = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonRotate = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.buttonFlip = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.buttonBattery = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.button6 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.Avvia_telecamera = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_distance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDegrees)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(7, 30);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 38);
            this.button1.TabIndex = 0;
            this.button1.Text = "CONNECT";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(7, 64);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(84, 29);
            this.button2.TabIndex = 1;
            this.button2.Text = "QUADRATO";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Red;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(617, 12);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(270, 91);
            this.button3.TabIndex = 2;
            this.button3.Text = "STOP";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // buttonTakeOff
            // 
            this.buttonTakeOff.Location = new System.Drawing.Point(8, 19);
            this.buttonTakeOff.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonTakeOff.Name = "buttonTakeOff";
            this.buttonTakeOff.Size = new System.Drawing.Size(84, 29);
            this.buttonTakeOff.TabIndex = 3;
            this.buttonTakeOff.Text = "DECOLLA";
            this.buttonTakeOff.UseVisualStyleBackColor = true;
            this.buttonTakeOff.Click += new System.EventHandler(this.buttonTakeOff_Click);
            // 
            // buttonLand
            // 
            this.buttonLand.Location = new System.Drawing.Point(8, 64);
            this.buttonLand.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonLand.Name = "buttonLand";
            this.buttonLand.Size = new System.Drawing.Size(84, 29);
            this.buttonLand.TabIndex = 4;
            this.buttonLand.Text = "ATTERRA";
            this.buttonLand.UseVisualStyleBackColor = true;
            this.buttonLand.Click += new System.EventHandler(this.buttonLand_Click);
            // 
            // labelHeight
            // 
            this.labelHeight.AutoSize = true;
            this.labelHeight.Location = new System.Drawing.Point(175, 21);
            this.labelHeight.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelHeight.Name = "labelHeight";
            this.labelHeight.Size = new System.Drawing.Size(144, 13);
            this.labelHeight.TabIndex = 5;
            this.labelHeight.Text = "Spostamento drone (cm)";
            // 
            // numericUpDown_distance
            // 
            this.numericUpDown_distance.Location = new System.Drawing.Point(179, 37);
            this.numericUpDown_distance.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.numericUpDown_distance.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDown_distance.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numericUpDown_distance.Name = "numericUpDown_distance";
            this.numericUpDown_distance.Size = new System.Drawing.Size(139, 20);
            this.numericUpDown_distance.TabIndex = 6;
            this.numericUpDown_distance.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // buttonUP
            // 
            this.buttonUP.Image = ((System.Drawing.Image)(resources.GetObject("buttonUP.Image")));
            this.buttonUP.Location = new System.Drawing.Point(7, 19);
            this.buttonUP.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonUP.Name = "buttonUP";
            this.buttonUP.Size = new System.Drawing.Size(84, 29);
            this.buttonUP.TabIndex = 7;
            this.buttonUP.UseVisualStyleBackColor = true;
            this.buttonUP.Click += new System.EventHandler(this.buttonUP_Click);
            // 
            // buttonDown
            // 
            this.buttonDown.Image = ((System.Drawing.Image)(resources.GetObject("buttonDown.Image")));
            this.buttonDown.Location = new System.Drawing.Point(7, 64);
            this.buttonDown.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonDown.Name = "buttonDown";
            this.buttonDown.Size = new System.Drawing.Size(84, 29);
            this.buttonDown.TabIndex = 8;
            this.buttonDown.UseVisualStyleBackColor = true;
            this.buttonDown.Click += new System.EventHandler(this.buttonDown_Click);
            // 
            // buttonDX
            // 
            this.buttonDX.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonDX.BackgroundImage")));
            this.buttonDX.Location = new System.Drawing.Point(472, 260);
            this.buttonDX.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonDX.Name = "buttonDX";
            this.buttonDX.Size = new System.Drawing.Size(57, 57);
            this.buttonDX.TabIndex = 9;
            this.buttonDX.UseVisualStyleBackColor = true;
            this.buttonDX.Click += new System.EventHandler(this.buttonDX_Click);
            // 
            // buttonSX
            // 
            this.buttonSX.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonSX.BackgroundImage")));
            this.buttonSX.Location = new System.Drawing.Point(28, 260);
            this.buttonSX.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonSX.Name = "buttonSX";
            this.buttonSX.Size = new System.Drawing.Size(57, 57);
            this.buttonSX.TabIndex = 10;
            this.buttonSX.UseVisualStyleBackColor = true;
            this.buttonSX.Click += new System.EventHandler(this.buttonSX_Click);
            // 
            // buttonForward
            // 
            this.buttonForward.Image = ((System.Drawing.Image)(resources.GetObject("buttonForward.Image")));
            this.buttonForward.Location = new System.Drawing.Point(257, 76);
            this.buttonForward.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonForward.Name = "buttonForward";
            this.buttonForward.Size = new System.Drawing.Size(57, 57);
            this.buttonForward.TabIndex = 11;
            this.buttonForward.UseVisualStyleBackColor = true;
            this.buttonForward.Click += new System.EventHandler(this.buttonForward_Click);
            // 
            // buttonBack
            // 
            this.buttonBack.Image = ((System.Drawing.Image)(resources.GetObject("buttonBack.Image")));
            this.buttonBack.Location = new System.Drawing.Point(257, 420);
            this.buttonBack.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(57, 57);
            this.buttonBack.TabIndex = 12;
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // numericUpDownDegrees
            // 
            this.numericUpDownDegrees.Location = new System.Drawing.Point(13, 44);
            this.numericUpDownDegrees.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.numericUpDownDegrees.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numericUpDownDegrees.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownDegrees.Name = "numericUpDownDegrees";
            this.numericUpDownDegrees.Size = new System.Drawing.Size(139, 20);
            this.numericUpDownDegrees.TabIndex = 14;
            this.numericUpDownDegrees.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Gradi (°)";
            // 
            // comboBoxDirections
            // 
            this.comboBoxDirections.FormattingEnabled = true;
            this.comboBoxDirections.Items.AddRange(new object[] {
            "l",
            "r",
            "f",
            "b"});
            this.comboBoxDirections.Location = new System.Drawing.Point(179, 75);
            this.comboBoxDirections.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.comboBoxDirections.Name = "comboBoxDirections";
            this.comboBoxDirections.Size = new System.Drawing.Size(140, 21);
            this.comboBoxDirections.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(177, 59);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Direzione";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonTakeOff);
            this.groupBox1.Controls.Add(this.buttonLand);
            this.groupBox1.Location = new System.Drawing.Point(569, 337);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Size = new System.Drawing.Size(105, 112);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Volo";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonUP);
            this.groupBox2.Controls.Add(this.buttonDown);
            this.groupBox2.Location = new System.Drawing.Point(691, 337);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Size = new System.Drawing.Size(105, 112);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Altezza";
            // 
            // buttonRotate
            // 
            this.buttonRotate.Location = new System.Drawing.Point(13, 70);
            this.buttonRotate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonRotate.Name = "buttonRotate";
            this.buttonRotate.Size = new System.Drawing.Size(139, 29);
            this.buttonRotate.TabIndex = 20;
            this.buttonRotate.Text = "RUOTA";
            this.buttonRotate.UseVisualStyleBackColor = true;
            this.buttonRotate.Click += new System.EventHandler(this.buttonRotate_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.comboBoxDirections);
            this.groupBox3.Controls.Add(this.labelHeight);
            this.groupBox3.Controls.Add(this.buttonRotate);
            this.groupBox3.Controls.Add(this.numericUpDown_distance);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.numericUpDownDegrees);
            this.groupBox3.Location = new System.Drawing.Point(569, 471);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox3.Size = new System.Drawing.Size(359, 112);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Setup Movimenti";
            // 
            // groupBox4
            // 
            this.groupBox4.Location = new System.Drawing.Point(713, 353);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox4.Size = new System.Drawing.Size(0, 0);
            this.groupBox4.TabIndex = 22;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Setup Movimenti";
            // 
            // buttonFlip
            // 
            this.buttonFlip.Location = new System.Drawing.Point(7, 19);
            this.buttonFlip.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonFlip.Name = "buttonFlip";
            this.buttonFlip.Size = new System.Drawing.Size(84, 29);
            this.buttonFlip.TabIndex = 23;
            this.buttonFlip.Text = "FLIP";
            this.buttonFlip.UseVisualStyleBackColor = true;
            this.buttonFlip.Click += new System.EventHandler(this.buttonFlip_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.button2);
            this.groupBox5.Controls.Add(this.buttonFlip);
            this.groupBox5.Location = new System.Drawing.Point(814, 337);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox5.Size = new System.Drawing.Size(105, 112);
            this.groupBox5.TabIndex = 24;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Avanzate";
            // 
            // buttonBattery
            // 
            this.buttonBattery.Image = ((System.Drawing.Image)(resources.GetObject("buttonBattery.Image")));
            this.buttonBattery.Location = new System.Drawing.Point(231, 30);
            this.buttonBattery.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonBattery.Name = "buttonBattery";
            this.buttonBattery.Size = new System.Drawing.Size(105, 38);
            this.buttonBattery.TabIndex = 25;
            this.buttonBattery.UseVisualStyleBackColor = true;
            this.buttonBattery.Click += new System.EventHandler(this.buttonBattery_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(28, 40);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(501, 498);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 29;
            this.pictureBox2.TabStop = false;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.buttonBattery);
            this.groupBox6.Controls.Add(this.button1);
            this.groupBox6.Location = new System.Drawing.Point(569, 199);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(350, 100);
            this.groupBox6.TabIndex = 30;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Stato Drone";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(768, 109);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(128, 43);
            this.button6.TabIndex = 37;
            this.button6.Text = "esegui";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click_1);
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(625, 158);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 36;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(617, 109);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(128, 43);
            this.button5.TabIndex = 35;
            this.button5.Text = "carica percorso";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click_1);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(547, 109);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(46, 43);
            this.button7.TabIndex = 38;
            this.button7.Text = "crea";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // Avvia_telecamera
            // 
            this.Avvia_telecamera.Location = new System.Drawing.Point(181, 571);
            this.Avvia_telecamera.Name = "Avvia_telecamera";
            this.Avvia_telecamera.Size = new System.Drawing.Size(191, 55);
            this.Avvia_telecamera.TabIndex = 39;
            this.Avvia_telecamera.Text = "Avvia Camera";
            this.Avvia_telecamera.UseVisualStyleBackColor = true;
            this.Avvia_telecamera.Click += new System.EventHandler(this.Avvia_telecamera_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(961, 663);
            this.Controls.Add(this.Avvia_telecamera);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.buttonForward);
            this.Controls.Add(this.buttonSX);
            this.Controls.Add(this.buttonDX);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.pictureBox2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Form1";
            this.Text = "TELLO";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_distance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDegrees)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button buttonTakeOff;
        private System.Windows.Forms.Button buttonLand;
        private System.Windows.Forms.Label labelHeight;
        private System.Windows.Forms.NumericUpDown numericUpDown_distance;
        private System.Windows.Forms.Button buttonUP;
        private System.Windows.Forms.Button buttonDown;
        private System.Windows.Forms.Button buttonDX;
        private System.Windows.Forms.Button buttonSX;
        private System.Windows.Forms.Button buttonForward;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.NumericUpDown numericUpDownDegrees;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxDirections;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonRotate;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button buttonFlip;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button buttonBattery;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button Avvia_telecamera;
    }
}

