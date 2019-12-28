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
            this.alphaTrack = new System.Windows.Forms.TrackBar();
            this.alphaGroup = new System.Windows.Forms.GroupBox();
            this.altitude = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.maxTrack = new System.Windows.Forms.TrackBar();
            this.minTrack = new System.Windows.Forms.TrackBar();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.rotateY = new System.Windows.Forms.Button();
            this.rotateX = new System.Windows.Forms.Button();
            this.resetText = new System.Windows.Forms.Label();
            this.resetButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.alphaTrack)).BeginInit();
            this.alphaGroup.SuspendLayout();
            this.altitude.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxTrack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minTrack)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // alphaTrack
            // 
            this.alphaTrack.Location = new System.Drawing.Point(6, 29);
            this.alphaTrack.Maximum = 100;
            this.alphaTrack.Name = "alphaTrack";
            this.alphaTrack.Size = new System.Drawing.Size(234, 56);
            this.alphaTrack.TabIndex = 0;
            this.alphaTrack.Value = 50;
            this.alphaTrack.ValueChanged += new System.EventHandler(this.AlphaTrack_ValueChanged);
            // 
            // alphaGroup
            // 
            this.alphaGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.alphaGroup.Controls.Add(this.alphaTrack);
            this.alphaGroup.Location = new System.Drawing.Point(3, 3);
            this.alphaGroup.Name = "alphaGroup";
            this.alphaGroup.Size = new System.Drawing.Size(265, 131);
            this.alphaGroup.TabIndex = 1;
            this.alphaGroup.TabStop = false;
            this.alphaGroup.Text = "Alpha";
            // 
            // altitude
            // 
            this.altitude.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.altitude.Controls.Add(this.label2);
            this.altitude.Controls.Add(this.label1);
            this.altitude.Controls.Add(this.maxTrack);
            this.altitude.Controls.Add(this.minTrack);
            this.altitude.Location = new System.Drawing.Point(3, 140);
            this.altitude.Name = "altitude";
            this.altitude.Size = new System.Drawing.Size(265, 207);
            this.altitude.TabIndex = 2;
            this.altitude.TabStop = false;
            this.altitude.Text = "Altitude";
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
            this.maxTrack.Maximum = 100;
            this.maxTrack.Minimum = -100;
            this.maxTrack.Name = "maxTrack";
            this.maxTrack.Size = new System.Drawing.Size(180, 56);
            this.maxTrack.TabIndex = 1;
            this.maxTrack.Value = 100;
            this.maxTrack.ValueChanged += new System.EventHandler(this.MaxTrack_ValueChanged);
            // 
            // minTrack
            // 
            this.minTrack.Location = new System.Drawing.Point(79, 37);
            this.minTrack.Maximum = 100;
            this.minTrack.Minimum = -100;
            this.minTrack.Name = "minTrack";
            this.minTrack.Size = new System.Drawing.Size(180, 56);
            this.minTrack.TabIndex = 0;
            this.minTrack.Value = -100;
            this.minTrack.ValueChanged += new System.EventHandler(this.MinTrack_ValueChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.alphaGroup);
            this.flowLayoutPanel1.Controls.Add(this.altitude);
            this.flowLayoutPanel1.Controls.Add(this.resetText);
            this.flowLayoutPanel1.Controls.Add(this.resetButton);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(1059, 2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(280, 542);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // rotateY
            // 
            this.rotateY.Location = new System.Drawing.Point(12, 352);
            this.rotateY.Name = "rotateY";
            this.rotateY.Size = new System.Drawing.Size(110, 24);
            this.rotateY.TabIndex = 5;
            this.rotateY.Text = "Rotate Y";
            this.rotateY.UseVisualStyleBackColor = true;
            this.rotateY.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button2_MouseDown);
            this.rotateY.MouseMove += new System.Windows.Forms.MouseEventHandler(this.button2_MouseMove);
            this.rotateY.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button2_MouseUp);
            // 
            // rotateX
            // 
            this.rotateX.Location = new System.Drawing.Point(625, 804);
            this.rotateX.Name = "rotateX";
            this.rotateX.Size = new System.Drawing.Size(110, 24);
            this.rotateX.TabIndex = 6;
            this.rotateX.Text = "Rotate X";
            this.rotateX.UseVisualStyleBackColor = true;
            this.rotateX.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rotateX_MouseDown);
            this.rotateX.MouseMove += new System.Windows.Forms.MouseEventHandler(this.rotateX_MouseMove);
            this.rotateX.MouseUp += new System.Windows.Forms.MouseEventHandler(this.rotateX_MouseUp);
            // 
            // resetText
            // 
            this.resetText.AutoSize = true;
            this.resetText.Location = new System.Drawing.Point(3, 350);
            this.resetText.Name = "resetText";
            this.resetText.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.resetText.Size = new System.Drawing.Size(144, 27);
            this.resetText.TabIndex = 3;
            this.resetText.Text = "Set rotation to default";
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(153, 353);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(75, 23);
            this.resetButton.TabIndex = 4;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // Trajectories
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1336, 839);
            this.Controls.Add(this.rotateX);
            this.Controls.Add(this.rotateY);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "Trajectories";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Trajectories_Load);
            this.Resize += new System.EventHandler(this.Trajectories_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.alphaTrack)).EndInit();
            this.alphaGroup.ResumeLayout(false);
            this.alphaGroup.PerformLayout();
            this.altitude.ResumeLayout(false);
            this.altitude.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxTrack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minTrack)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
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
        private System.Windows.Forms.Label resetText;
        private System.Windows.Forms.Button resetButton;
    }
}

