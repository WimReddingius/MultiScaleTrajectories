namespace MultiScaleTrajectories.Simplification.MultiScale.View.Edit
{
    partial class ZoomAwareErrorSampler
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
            this.computeButton = new System.Windows.Forms.Button();
            this.zoomFactorLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.zoomFactorNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.detailPercentileNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numLevelsNumericUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.zoomFactorNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detailPercentileNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLevelsNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // computeButton
            // 
            this.computeButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.computeButton.Location = new System.Drawing.Point(0, 190);
            this.computeButton.Name = "computeButton";
            this.computeButton.Size = new System.Drawing.Size(177, 31);
            this.computeButton.TabIndex = 0;
            this.computeButton.Text = "Compute";
            this.computeButton.UseVisualStyleBackColor = true;
            this.computeButton.Click += new System.EventHandler(this.computeButton_Click);
            // 
            // zoomFactorLabel
            // 
            this.zoomFactorLabel.AutoSize = true;
            this.zoomFactorLabel.Location = new System.Drawing.Point(3, 14);
            this.zoomFactorLabel.Name = "zoomFactorLabel";
            this.zoomFactorLabel.Size = new System.Drawing.Size(67, 13);
            this.zoomFactorLabel.TabIndex = 1;
            this.zoomFactorLabel.Text = "Zoom Factor";
            this.zoomFactorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Detail percentile";
            // 
            // zoomFactorNumericUpDown
            // 
            this.zoomFactorNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.zoomFactorNumericUpDown.DecimalPlaces = 8;
            this.zoomFactorNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.zoomFactorNumericUpDown.Location = new System.Drawing.Point(92, 12);
            this.zoomFactorNumericUpDown.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.zoomFactorNumericUpDown.Name = "zoomFactorNumericUpDown";
            this.zoomFactorNumericUpDown.Size = new System.Drawing.Size(82, 20);
            this.zoomFactorNumericUpDown.TabIndex = 3;
            this.zoomFactorNumericUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // detailPercentileNumericUpDown
            // 
            this.detailPercentileNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.detailPercentileNumericUpDown.DecimalPlaces = 8;
            this.detailPercentileNumericUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            196608});
            this.detailPercentileNumericUpDown.Location = new System.Drawing.Point(92, 44);
            this.detailPercentileNumericUpDown.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.detailPercentileNumericUpDown.Name = "detailPercentileNumericUpDown";
            this.detailPercentileNumericUpDown.Size = new System.Drawing.Size(82, 20);
            this.detailPercentileNumericUpDown.TabIndex = 4;
            this.detailPercentileNumericUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            196608});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Levels";
            // 
            // numLevelsNumericUpDown
            // 
            this.numLevelsNumericUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numLevelsNumericUpDown.Location = new System.Drawing.Point(92, 81);
            this.numLevelsNumericUpDown.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numLevelsNumericUpDown.Name = "numLevelsNumericUpDown";
            this.numLevelsNumericUpDown.Size = new System.Drawing.Size(82, 20);
            this.numLevelsNumericUpDown.TabIndex = 6;
            this.numLevelsNumericUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            196608});
            // 
            // ZoomAwareErrorSampler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.numLevelsNumericUpDown);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.detailPercentileNumericUpDown);
            this.Controls.Add(this.zoomFactorNumericUpDown);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.zoomFactorLabel);
            this.Controls.Add(this.computeButton);
            this.Name = "ZoomAwareErrorSampler";
            this.Size = new System.Drawing.Size(177, 221);
            ((System.ComponentModel.ISupportInitialize)(this.zoomFactorNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.detailPercentileNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLevelsNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button computeButton;
        private System.Windows.Forms.Label zoomFactorLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown zoomFactorNumericUpDown;
        private System.Windows.Forms.NumericUpDown detailPercentileNumericUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numLevelsNumericUpDown;
    }
}
