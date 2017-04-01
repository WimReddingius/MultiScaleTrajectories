namespace MultiScaleTrajectories.SingleTrajectory.View.Explore.Geo
{
    partial class LevelTrajectoryGMap
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.gMap = new MultiScaleTrajectories.View.TrajectoryGMap();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 190);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(343, 30);
            this.panel1.TabIndex = 2;
            // 
            // gMap
            // 
            this.gMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gMap.Location = new System.Drawing.Point(0, 0);
            this.gMap.Margin = new System.Windows.Forms.Padding(1);
            this.gMap.Name = "gMap";
            this.gMap.Size = new System.Drawing.Size(343, 220);
            this.gMap.TabIndex = 1;
            // 
            // STOutputGMapExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gMap);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "STOutputGMapExplorer";
            this.Size = new System.Drawing.Size(343, 220);
            this.ResumeLayout(false);

        }

        #endregion

        private MultiScaleTrajectories.View.TrajectoryGMap gMap;
        private System.Windows.Forms.Panel panel1;
    }
}
