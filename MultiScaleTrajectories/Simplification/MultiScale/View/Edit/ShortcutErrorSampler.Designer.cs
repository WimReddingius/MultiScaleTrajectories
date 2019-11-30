namespace MultiScaleTrajectories.Simplification.MultiScale.View.Edit
{
    partial class ShortcutErrorSampler
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
            this.openErrorsButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lowerPercentageChooser = new System.Windows.Forms.NumericUpDown();
            this.upperPercentageChooser = new System.Windows.Forms.NumericUpDown();
            this.computeErrorDistributionButton = new System.Windows.Forms.Button();
            this.redistributeErrorDistributionButton = new System.Windows.Forms.Button();
            this.shortcutFindingProgressLabel = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.openErrorsFileDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.lowerPercentageChooser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upperPercentageChooser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openErrorsButton
            // 
            this.openErrorsButton.Location = new System.Drawing.Point(79, 3);
            this.openErrorsButton.Name = "openErrorsButton";
            this.openErrorsButton.Size = new System.Drawing.Size(75, 23);
            this.openErrorsButton.TabIndex = 28;
            this.openErrorsButton.Text = "Open";
            this.openErrorsButton.UseVisualStyleBackColor = false;
            this.openErrorsButton.Click += new System.EventHandler(this.openErrorsButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Distribution";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "To";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "From";
            // 
            // lowerPercentageChooser
            // 
            this.lowerPercentageChooser.DecimalPlaces = 1;
            this.lowerPercentageChooser.Location = new System.Drawing.Point(79, 34);
            this.lowerPercentageChooser.Name = "lowerPercentageChooser";
            this.lowerPercentageChooser.Size = new System.Drawing.Size(59, 20);
            this.lowerPercentageChooser.TabIndex = 20;
            // 
            // upperPercentageChooser
            // 
            this.upperPercentageChooser.DecimalPlaces = 1;
            this.upperPercentageChooser.Location = new System.Drawing.Point(79, 62);
            this.upperPercentageChooser.Name = "upperPercentageChooser";
            this.upperPercentageChooser.Size = new System.Drawing.Size(63, 20);
            this.upperPercentageChooser.TabIndex = 19;
            this.upperPercentageChooser.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // computeErrorDistributionButton
            // 
            this.computeErrorDistributionButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.computeErrorDistributionButton.Location = new System.Drawing.Point(0, 0);
            this.computeErrorDistributionButton.Name = "computeErrorDistributionButton";
            this.computeErrorDistributionButton.Size = new System.Drawing.Size(92, 40);
            this.computeErrorDistributionButton.TabIndex = 29;
            this.computeErrorDistributionButton.Text = "Compute";
            this.computeErrorDistributionButton.UseVisualStyleBackColor = true;
            this.computeErrorDistributionButton.Click += new System.EventHandler(this.computeErrorDistribution_Click);
            // 
            // redistributeErrorDistributionButton
            // 
            this.redistributeErrorDistributionButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.redistributeErrorDistributionButton.Location = new System.Drawing.Point(0, 0);
            this.redistributeErrorDistributionButton.Name = "redistributeErrorDistributionButton";
            this.redistributeErrorDistributionButton.Size = new System.Drawing.Size(97, 40);
            this.redistributeErrorDistributionButton.TabIndex = 30;
            this.redistributeErrorDistributionButton.Text = "Redistribute";
            this.redistributeErrorDistributionButton.UseVisualStyleBackColor = true;
            this.redistributeErrorDistributionButton.Click += new System.EventHandler(this.revalidateDistributionButton_Click);
            // 
            // shortcutFindingProgressLabel
            // 
            this.shortcutFindingProgressLabel.AutoSize = true;
            this.shortcutFindingProgressLabel.Location = new System.Drawing.Point(6, 92);
            this.shortcutFindingProgressLabel.Name = "shortcutFindingProgressLabel";
            this.shortcutFindingProgressLabel.Size = new System.Drawing.Size(0, 13);
            this.shortcutFindingProgressLabel.TabIndex = 31;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 125);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.computeErrorDistributionButton);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.redistributeErrorDistributionButton);
            this.splitContainer1.Size = new System.Drawing.Size(193, 40);
            this.splitContainer1.SplitterDistance = 92;
            this.splitContainer1.TabIndex = 32;
            // 
            // openErrorsFileDialog
            // 
            this.openErrorsFileDialog.DefaultExt = "json";
            this.openErrorsFileDialog.Filter = "JSON Files|*.json";
            // 
            // ShortcutErrorSampler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.shortcutFindingProgressLabel);
            this.Controls.Add(this.openErrorsButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lowerPercentageChooser);
            this.Controls.Add(this.upperPercentageChooser);
            this.Name = "ShortcutErrorSampler";
            this.Size = new System.Drawing.Size(193, 165);
            ((System.ComponentModel.ISupportInitialize)(this.lowerPercentageChooser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upperPercentageChooser)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button openErrorsButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown lowerPercentageChooser;
        private System.Windows.Forms.NumericUpDown upperPercentageChooser;
        private System.Windows.Forms.Button computeErrorDistributionButton;
        private System.Windows.Forms.Button redistributeErrorDistributionButton;
        private System.Windows.Forms.Label shortcutFindingProgressLabel;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.OpenFileDialog openErrorsFileDialog;
    }
}
