using MultiScaleTrajectories.Trajectory.View;

namespace MultiScaleTrajectories.Simplification.MultiScale.View.Explore
{
    partial class LevelTrajectoryGeoAuto
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
            this.trajectoryGMap = new TrajectoryGeo();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.detailNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.detailNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // trajectoryGMap
            // 
            this.trajectoryGMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trajectoryGMap.Location = new System.Drawing.Point(0, 0);
            this.trajectoryGMap.Margin = new System.Windows.Forms.Padding(1);
            this.trajectoryGMap.Name = "trajectoryGMap";
            this.trajectoryGMap.Size = new System.Drawing.Size(445, 220);
            this.trajectoryGMap.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.detailNumericUpDown);
            this.panel1.Location = new System.Drawing.Point(0, 190);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(169, 30);
            this.panel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Detail";
            // 
            // detailNumericUpDown
            // 
            this.detailNumericUpDown.DecimalPlaces = 4;
            this.detailNumericUpDown.Increment = 0.01M;
            this.detailNumericUpDown.Location = new System.Drawing.Point(45, 5);
            this.detailNumericUpDown.Name = "detailNumericUpDown";
            this.detailNumericUpDown.Size = new System.Drawing.Size(120, 20);
            this.detailNumericUpDown.TabIndex = 0;
            this.detailNumericUpDown.Value = 200;
            this.detailNumericUpDown.Maximum = decimal.MaxValue;
            this.detailNumericUpDown.Minimum = 0.0001M;
            this.detailNumericUpDown.ValueChanged += new System.EventHandler(this.detailNumericUpDown_ValueChanged);
            // 
            // LevelTrajectoryGeoAuto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.trajectoryGMap);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "LevelTrajectoryGeoAuto";
            this.Size = new System.Drawing.Size(445, 220);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.detailNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TrajectoryGeo trajectoryGMap;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown detailNumericUpDown;
    }
}
