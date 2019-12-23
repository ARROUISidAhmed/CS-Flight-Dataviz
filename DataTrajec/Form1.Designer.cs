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
            // Trajectories
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1336, 839);
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
    }
}

