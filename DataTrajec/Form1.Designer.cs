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
            ((System.ComponentModel.ISupportInitialize)(this.alphaTrack)).BeginInit();
            this.alphaGroup.SuspendLayout();
            this.altitude.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxTrack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minTrack)).BeginInit();
            this.SuspendLayout();
            // 
            // alphaTrack
            // 
            this.alphaTrack.Location = new System.Drawing.Point(6, 29);
            this.alphaTrack.Maximum = 100;
            this.alphaTrack.Name = "alphaTrack";
            this.alphaTrack.Size = new System.Drawing.Size(234, 56);
            this.alphaTrack.TabIndex = 0;
            this.alphaTrack.ValueChanged += new System.EventHandler(this.AlphaTrack_ValueChanged);
            // 
            // alphaGroup
            // 
            this.alphaGroup.Controls.Add(this.alphaTrack);
            this.alphaGroup.Location = new System.Drawing.Point(1059, 236);
            this.alphaGroup.Name = "alphaGroup";
            this.alphaGroup.Size = new System.Drawing.Size(265, 91);
            this.alphaGroup.TabIndex = 1;
            this.alphaGroup.TabStop = false;
            this.alphaGroup.Text = "Alpha";
            // 
            // altitude
            // 
            this.altitude.Controls.Add(this.label2);
            this.altitude.Controls.Add(this.label1);
            this.altitude.Controls.Add(this.maxTrack);
            this.altitude.Controls.Add(this.minTrack);
            this.altitude.Location = new System.Drawing.Point(1059, 352);
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
            // 
            // Trajectories
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1336, 839);
            this.Controls.Add(this.altitude);
            this.Controls.Add(this.alphaGroup);
            this.Name = "Trajectories";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Trajectories_Load);
            ((System.ComponentModel.ISupportInitialize)(this.alphaTrack)).EndInit();
            this.alphaGroup.ResumeLayout(false);
            this.alphaGroup.PerformLayout();
            this.altitude.ResumeLayout(false);
            this.altitude.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maxTrack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minTrack)).EndInit();
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
    }
}

