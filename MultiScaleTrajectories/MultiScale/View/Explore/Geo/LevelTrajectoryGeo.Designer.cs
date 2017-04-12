namespace MultiScaleTrajectories.MultiScale.View.Explore.Geo
{
    partial class LevelTrajectoryGeo
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
            this.trajectoryGMap = new MultiScaleTrajectories.Trajectory.TrajectoryGeo();
            this.SuspendLayout();
            // 
            // trajectoryGMap
            // 
            this.trajectoryGMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trajectoryGMap.Location = new System.Drawing.Point(0, 0);
            this.trajectoryGMap.Margin = new System.Windows.Forms.Padding(1);
            this.trajectoryGMap.Name = "trajectoryGMap";
            this.trajectoryGMap.Size = new System.Drawing.Size(343, 220);
            this.trajectoryGMap.TabIndex = 1;
            // 
            // LevelTrajectoryGMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.trajectoryGMap);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "LevelTrajectoryGMap";
            this.Size = new System.Drawing.Size(343, 220);
            this.ResumeLayout(false);

        }

        #endregion

        private MultiScaleTrajectories.Trajectory.TrajectoryGeo trajectoryGMap;
    }
}
