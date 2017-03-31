namespace MultiScaleTrajectories.SingleTrajectory.View.Explore.Map
{
    partial class STOutputGMapExplorer
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gMap = new MultiScaleTrajectories.View.TrajectoryGMap();
            this.SuspendLayout();
            // 
            // gMap
            // 
            this.gMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gMap.Location = new System.Drawing.Point(0, 0);
            this.gMap.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.gMap.Name = "gMap";
            this.gMap.Size = new System.Drawing.Size(100, 97);
            this.gMap.TabIndex = 1;
            // 
            // STOutputGMapExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gMap);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "STOutputGMapExplorer";
            this.Size = new System.Drawing.Size(100, 97);
            this.ResumeLayout(false);

        }

        #endregion

        private MultiScaleTrajectories.View.TrajectoryGMap gMap;
    }
}
