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
            this.RotationX = new System.Windows.Forms.Label();
            this.RotationY = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.alphaTrack)).BeginInit();
            this.SuspendLayout();
            // 
            // alphaTrack
            // 
            this.alphaTrack.Location = new System.Drawing.Point(1077, 12);
            this.alphaTrack.Maximum = 100;
            this.alphaTrack.Name = "alphaTrack";
            this.alphaTrack.Size = new System.Drawing.Size(234, 56);
            this.alphaTrack.TabIndex = 0;
            this.alphaTrack.ValueChanged += new System.EventHandler(this.AlphaTrack_ValueChanged);
            // 
            // RotationX
            // 
            this.RotationX.AutoSize = true;
            this.RotationX.Location = new System.Drawing.Point(12, 419);
            this.RotationX.Name = "RotationX";
            this.RotationX.Size = new System.Drawing.Size(70, 17);
            this.RotationX.TabIndex = 1;
            this.RotationX.Text = "RotationX";
            // 
            // RotationY
            // 
            this.RotationY.AutoSize = true;
            this.RotationY.Location = new System.Drawing.Point(619, 813);
            this.RotationY.Name = "RotationY";
            this.RotationY.Size = new System.Drawing.Size(70, 17);
            this.RotationY.TabIndex = 2;
            this.RotationY.Text = "RotationY";
            // 
            // Trajectories
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1336, 839);
            this.Controls.Add(this.RotationY);
            this.Controls.Add(this.RotationX);
            this.Controls.Add(this.alphaTrack);
            this.Name = "Trajectories";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Trajectories_Load);
            ((System.ComponentModel.ISupportInitialize)(this.alphaTrack)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar alphaTrack;
        private System.Windows.Forms.Label RotationX;
        private System.Windows.Forms.Label RotationY;
    }
}

