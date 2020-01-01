namespace DataTrajec
{
    partial class Trajectories
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.alphaTrack = new System.Windows.Forms.TrackBar();
            this.alphaGroup = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.fovyTrack = new System.Windows.Forms.TrackBar();
            this.altitude = new System.Windows.Forms.GroupBox();
            this.colormaxbutton = new System.Windows.Forms.Button();
            this.colorminbutton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.maxTrack = new System.Windows.Forms.TrackBar();
            this.minTrack = new System.Windows.Forms.TrackBar();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.resetBox = new System.Windows.Forms.GroupBox();
            this.defaultMaxColor = new System.Windows.Forms.Button();
            this.defaultMinColor = new System.Windows.Forms.Button();
            this.resetColor = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.resetText = new System.Windows.Forms.Label();
            this.resetButton = new System.Windows.Forms.Button();
            this.rotateY = new System.Windows.Forms.Button();
            this.rotateX = new System.Windows.Forms.Button();
            this.resetAnimationTimer = new System.Windows.Forms.Timer(this.components);
            this.minColorDialog = new System.Windows.Forms.ColorDialog();
            this.maxColorDialog = new System.Windows.Forms.ColorDialog();
            this.defaultMaxColorDialog = new System.Windows.Forms.ColorDialog();
            this.defaultMinClorDialog = new System.Windows.Forms.ColorDialog();
            this.particleButton = new System.Windows.Forms.RadioButton();
            this.lineButton = new System.Windows.Forms.RadioButton();
            this.particleAnimationTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.alphaTrack)).BeginInit();
            this.alphaGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fovyTrack)).BeginInit();
            this.altitude.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxTrack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minTrack)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.resetBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // alphaTrack
            // 
            this.alphaTrack.Location = new System.Drawing.Point(89, 19);
            this.alphaTrack.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.alphaTrack.Maximum = 100;
            this.alphaTrack.Name = "alphaTrack";
            this.alphaTrack.Size = new System.Drawing.Size(161, 56);
            this.alphaTrack.TabIndex = 0;
            this.alphaTrack.TickFrequency = 0;
            this.alphaTrack.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.alphaTrack.Value = 50;
            this.alphaTrack.ValueChanged += new System.EventHandler(this.AlphaTrack_ValueChanged);
            // 
            // alphaGroup
            // 
            this.alphaGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.alphaGroup.Controls.Add(this.lineButton);
            this.alphaGroup.Controls.Add(this.particleButton);
            this.alphaGroup.Controls.Add(this.label5);
            this.alphaGroup.Controls.Add(this.label4);
            this.alphaGroup.Controls.Add(this.fovyTrack);
            this.alphaGroup.Controls.Add(this.alphaTrack);
            this.alphaGroup.Location = new System.Drawing.Point(3, 2);
            this.alphaGroup.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.alphaGroup.Name = "alphaGroup";
            this.alphaGroup.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.alphaGroup.Size = new System.Drawing.Size(265, 188);
            this.alphaGroup.TabIndex = 1;
            this.alphaGroup.TabStop = false;
            this.alphaGroup.Text = "Visual";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 79);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 17);
            this.label5.TabIndex = 3;
            this.label5.Text = "Fovy";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "Alpha";
            // 
            // fovyTrack
            // 
            this.fovyTrack.Location = new System.Drawing.Point(89, 69);
            this.fovyTrack.Minimum = 1;
            this.fovyTrack.Name = "fovyTrack";
            this.fovyTrack.Size = new System.Drawing.Size(151, 56);
            this.fovyTrack.TabIndex = 1;
            this.fovyTrack.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.fovyTrack.Value = 1;
            this.fovyTrack.ValueChanged += new System.EventHandler(this.FovyTrack_ValueChanged);
            // 
            // altitude
            // 
            this.altitude.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.altitude.Controls.Add(this.colormaxbutton);
            this.altitude.Controls.Add(this.colorminbutton);
            this.altitude.Controls.Add(this.label2);
            this.altitude.Controls.Add(this.label1);
            this.altitude.Controls.Add(this.maxTrack);
            this.altitude.Controls.Add(this.minTrack);
            this.altitude.Location = new System.Drawing.Point(3, 194);
            this.altitude.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.altitude.Name = "altitude";
            this.altitude.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.altitude.Size = new System.Drawing.Size(265, 207);
            this.altitude.TabIndex = 2;
            this.altitude.TabStop = false;
            this.altitude.Text = "Filtering";
            // 
            // colormaxbutton
            // 
            this.colormaxbutton.BackColor = System.Drawing.Color.Blue;
            this.colormaxbutton.Location = new System.Drawing.Point(49, 145);
            this.colormaxbutton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.colormaxbutton.Name = "colormaxbutton";
            this.colormaxbutton.Size = new System.Drawing.Size(24, 23);
            this.colormaxbutton.TabIndex = 9;
            this.colormaxbutton.UseVisualStyleBackColor = false;
            this.colormaxbutton.Click += new System.EventHandler(this.Colormaxbutton_Click);
            // 
            // colorminbutton
            // 
            this.colorminbutton.BackColor = System.Drawing.Color.Green;
            this.colorminbutton.Location = new System.Drawing.Point(48, 37);
            this.colorminbutton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.colorminbutton.Name = "colorminbutton";
            this.colorminbutton.Size = new System.Drawing.Size(24, 23);
            this.colorminbutton.TabIndex = 8;
            this.colorminbutton.UseVisualStyleBackColor = false;
            this.colorminbutton.Click += new System.EventHandler(this.Colorminbutton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Min";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 145);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Max";
            // 
            // maxTrack
            // 
            this.maxTrack.Location = new System.Drawing.Point(79, 145);
            this.maxTrack.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.maxTrack.Maximum = 100;
            this.maxTrack.Minimum = -100;
            this.maxTrack.Name = "maxTrack";
            this.maxTrack.Size = new System.Drawing.Size(180, 56);
            this.maxTrack.TabIndex = 1;
            this.maxTrack.TickFrequency = 0;
            this.maxTrack.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.maxTrack.Value = 100;
            this.maxTrack.ValueChanged += new System.EventHandler(this.MaxTrack_ValueChanged);
            // 
            // minTrack
            // 
            this.minTrack.Location = new System.Drawing.Point(79, 37);
            this.minTrack.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.minTrack.Maximum = 100;
            this.minTrack.Minimum = -100;
            this.minTrack.Name = "minTrack";
            this.minTrack.Size = new System.Drawing.Size(180, 56);
            this.minTrack.TabIndex = 0;
            this.minTrack.TickFrequency = 0;
            this.minTrack.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.minTrack.Value = -100;
            this.minTrack.ValueChanged += new System.EventHandler(this.MinTrack_ValueChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.alphaGroup);
            this.flowLayoutPanel1.Controls.Add(this.altitude);
            this.flowLayoutPanel1.Controls.Add(this.resetBox);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(1059, 2);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(280, 849);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // resetBox
            // 
            this.resetBox.Controls.Add(this.defaultMaxColor);
            this.resetBox.Controls.Add(this.defaultMinColor);
            this.resetBox.Controls.Add(this.resetColor);
            this.resetBox.Controls.Add(this.label3);
            this.resetBox.Controls.Add(this.resetText);
            this.resetBox.Controls.Add(this.resetButton);
            this.resetBox.Location = new System.Drawing.Point(3, 405);
            this.resetBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.resetBox.Name = "resetBox";
            this.resetBox.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.resetBox.Size = new System.Drawing.Size(265, 130);
            this.resetBox.TabIndex = 6;
            this.resetBox.TabStop = false;
            this.resetBox.Text = "Default values";
            // 
            // defaultMaxColor
            // 
            this.defaultMaxColor.BackColor = System.Drawing.Color.Blue;
            this.defaultMaxColor.Location = new System.Drawing.Point(216, 101);
            this.defaultMaxColor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.defaultMaxColor.Name = "defaultMaxColor";
            this.defaultMaxColor.Size = new System.Drawing.Size(24, 23);
            this.defaultMaxColor.TabIndex = 8;
            this.defaultMaxColor.UseVisualStyleBackColor = false;
            this.defaultMaxColor.Click += new System.EventHandler(this.DefaultMaxColor_Click);
            // 
            // defaultMinColor
            // 
            this.defaultMinColor.BackColor = System.Drawing.Color.Green;
            this.defaultMinColor.Location = new System.Drawing.Point(187, 101);
            this.defaultMinColor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.defaultMinColor.Name = "defaultMinColor";
            this.defaultMinColor.Size = new System.Drawing.Size(24, 23);
            this.defaultMinColor.TabIndex = 7;
            this.defaultMinColor.UseVisualStyleBackColor = false;
            this.defaultMinColor.Click += new System.EventHandler(this.DefaultMinColor_Click);
            // 
            // resetColor
            // 
            this.resetColor.Location = new System.Drawing.Point(173, 75);
            this.resetColor.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.resetColor.Name = "resetColor";
            this.resetColor.Size = new System.Drawing.Size(75, 23);
            this.resetColor.TabIndex = 6;
            this.resetColor.Text = "Reset";
            this.resetColor.UseVisualStyleBackColor = true;
            this.resetColor.Click += new System.EventHandler(this.ResetColor_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 70);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.label3.Size = new System.Drawing.Size(86, 27);
            this.label3.TabIndex = 5;
            this.label3.Text = "Reset Color ";
            // 
            // resetText
            // 
            this.resetText.AutoSize = true;
            this.resetText.Location = new System.Drawing.Point(5, 26);
            this.resetText.Name = "resetText";
            this.resetText.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.resetText.Size = new System.Drawing.Size(102, 27);
            this.resetText.TabIndex = 3;
            this.resetText.Text = "Reset position ";
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(175, 34);
            this.resetButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(75, 23);
            this.resetButton.TabIndex = 4;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // rotateY
            // 
            this.rotateY.BackColor = System.Drawing.Color.Black;
            this.rotateY.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.rotateY.FlatAppearance.BorderSize = 0;
            this.rotateY.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rotateY.ForeColor = System.Drawing.Color.White;
            this.rotateY.Location = new System.Drawing.Point(12, 352);
            this.rotateY.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rotateY.Name = "rotateY";
            this.rotateY.Size = new System.Drawing.Size(109, 25);
            this.rotateY.TabIndex = 5;
            this.rotateY.Text = "Rotate->Y";
            this.rotateY.UseVisualStyleBackColor = false;
            this.rotateY.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rotateY_MouseDown);
            this.rotateY.MouseMove += new System.Windows.Forms.MouseEventHandler(this.rotateY_MouseMove);
            this.rotateY.MouseUp += new System.Windows.Forms.MouseEventHandler(this.rotateY_MouseUp);
            // 
            // rotateX
            // 
            this.rotateX.BackColor = System.Drawing.Color.Black;
            this.rotateX.FlatAppearance.BorderSize = 0;
            this.rotateX.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rotateX.ForeColor = System.Drawing.Color.White;
            this.rotateX.Location = new System.Drawing.Point(625, 804);
            this.rotateX.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rotateX.Name = "rotateX";
            this.rotateX.Size = new System.Drawing.Size(109, 25);
            this.rotateX.TabIndex = 6;
            this.rotateX.Text = "Rotate->X";
            this.rotateX.UseVisualStyleBackColor = false;
            this.rotateX.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rotateX_MouseDown);
            this.rotateX.MouseMove += new System.Windows.Forms.MouseEventHandler(this.rotateX_MouseMove);
            this.rotateX.MouseUp += new System.Windows.Forms.MouseEventHandler(this.rotateX_MouseUp);
            // 
            // resetAnimationTimer
            // 
            this.resetAnimationTimer.Interval = 20;
            this.resetAnimationTimer.Tick += new System.EventHandler(this.ResetAnimationTimer_Tick);
            // 
            // particleButton
            // 
            this.particleButton.AutoSize = true;
            this.particleButton.Location = new System.Drawing.Point(138, 131);
            this.particleButton.Name = "particleButton";
            this.particleButton.Size = new System.Drawing.Size(83, 21);
            this.particleButton.TabIndex = 5;
            this.particleButton.Text = "Particles";
            this.particleButton.UseVisualStyleBackColor = true;
            this.particleButton.Click += new System.EventHandler(this.ParticleButton_Click);
            // 
            // lineButton
            // 
            this.lineButton.AutoSize = true;
            this.lineButton.Checked = true;
            this.lineButton.Location = new System.Drawing.Point(9, 131);
            this.lineButton.Name = "lineButton";
            this.lineButton.Size = new System.Drawing.Size(63, 21);
            this.lineButton.TabIndex = 6;
            this.lineButton.TabStop = true;
            this.lineButton.Text = "Lines";
            this.lineButton.UseVisualStyleBackColor = true;
            this.lineButton.Click += new System.EventHandler(this.LineButton_Click);
            // 
            // particleAnimationTimer
            // 
            this.particleAnimationTimer.Interval = 20;
            this.particleAnimationTimer.Tick += new System.EventHandler(this.ParticleAnimationTimer_Tick);
            // 
            // Trajectories
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1336, 839);
            this.Controls.Add(this.rotateX);
            this.Controls.Add(this.rotateY);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Trajectories";
            this.Text = "DataViz- ARROUI Sid Ahmed - BOURAS Abdelhadi - BENZERGA Amine";
            this.Load += new System.EventHandler(this.Trajectories_Load);
            this.Resize += new System.EventHandler(this.Trajectories_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.alphaTrack)).EndInit();
            this.alphaGroup.ResumeLayout(false);
            this.alphaGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fovyTrack)).EndInit();
            this.altitude.ResumeLayout(false);
            this.altitude.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxTrack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minTrack)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.resetBox.ResumeLayout(false);
            this.resetBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TrackBar alphaTrack;
        private System.Windows.Forms.GroupBox alphaGroup;
        private System.Windows.Forms.GroupBox altitude;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar maxTrack;
        private System.Windows.Forms.TrackBar minTrack;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button rotateY;
        private System.Windows.Forms.Button rotateX;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Timer resetAnimationTimer;
        private System.Windows.Forms.ColorDialog minColorDialog;
        private System.Windows.Forms.GroupBox resetBox;
        private System.Windows.Forms.ColorDialog maxColorDialog;
        private System.Windows.Forms.Button resetColor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label resetText;
        private System.Windows.Forms.Button defaultMaxColor;
        private System.Windows.Forms.Button defaultMinColor;
        private System.Windows.Forms.Button colormaxbutton;
        private System.Windows.Forms.Button colorminbutton;
        private System.Windows.Forms.ColorDialog defaultMaxColorDialog;
        private System.Windows.Forms.ColorDialog defaultMinClorDialog;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar fovyTrack;
        private System.Windows.Forms.RadioButton lineButton;
        private System.Windows.Forms.RadioButton particleButton;
        private System.Windows.Forms.Timer particleAnimationTimer;
    }
}

