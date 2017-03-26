namespace AlgorithmVisualization.View
{
    public partial class AlgorithmForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlgorithmForm));
            this.baseSplitContainer = new System.Windows.Forms.SplitContainer();
            this.configurationSplitContainer = new System.Windows.Forms.SplitContainer();
            this.algorithmProblemLabel = new System.Windows.Forms.Label();
            this.algorithmProblemComboBox = new System.Windows.Forms.ComboBox();
            this.saveInputDialog = new System.Windows.Forms.SaveFileDialog();
            this.openInputDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.baseSplitContainer)).BeginInit();
            this.baseSplitContainer.Panel2.SuspendLayout();
            this.baseSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.configurationSplitContainer)).BeginInit();
            this.configurationSplitContainer.Panel1.SuspendLayout();
            this.configurationSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // baseSplitContainer
            // 
            this.baseSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.baseSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.baseSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.baseSplitContainer.Name = "baseSplitContainer";
            // 
            // baseSplitContainer.Panel2
            // 
            this.baseSplitContainer.Panel2.Controls.Add(this.configurationSplitContainer);
            this.baseSplitContainer.Panel2.Padding = new System.Windows.Forms.Padding(1, 4, 2, 4);
            this.baseSplitContainer.Size = new System.Drawing.Size(1319, 710);
            this.baseSplitContainer.SplitterDistance = 940;
            this.baseSplitContainer.TabIndex = 0;
            // 
            // configurationSplitContainer
            // 
            this.configurationSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.configurationSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.configurationSplitContainer.IsSplitterFixed = true;
            this.configurationSplitContainer.Location = new System.Drawing.Point(1, 4);
            this.configurationSplitContainer.Name = "configurationSplitContainer";
            this.configurationSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // configurationSplitContainer.Panel1
            // 
            this.configurationSplitContainer.Panel1.Controls.Add(this.algorithmProblemLabel);
            this.configurationSplitContainer.Panel1.Controls.Add(this.algorithmProblemComboBox);
            this.configurationSplitContainer.Size = new System.Drawing.Size(372, 702);
            this.configurationSplitContainer.SplitterDistance = 58;
            this.configurationSplitContainer.TabIndex = 11;
            // 
            // algorithmProblemLabel
            // 
            this.algorithmProblemLabel.AutoSize = true;
            this.algorithmProblemLabel.Location = new System.Drawing.Point(-3, 9);
            this.algorithmProblemLabel.Name = "algorithmProblemLabel";
            this.algorithmProblemLabel.Size = new System.Drawing.Size(91, 13);
            this.algorithmProblemLabel.TabIndex = 10;
            this.algorithmProblemLabel.Text = "Algorithm Problem";
            // 
            // algorithmProblemComboBox
            // 
            this.algorithmProblemComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.algorithmProblemComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.algorithmProblemComboBox.Location = new System.Drawing.Point(0, 25);
            this.algorithmProblemComboBox.Name = "algorithmProblemComboBox";
            this.algorithmProblemComboBox.Size = new System.Drawing.Size(369, 21);
            this.algorithmProblemComboBox.TabIndex = 1;
            this.algorithmProblemComboBox.SelectedIndexChanged += new System.EventHandler(this.algorithmProblemComboBox_SelectedIndexChanged);
            // 
            // saveInputDialog
            // 
            this.saveInputDialog.DefaultExt = "json";
            this.saveInputDialog.Filter = "JSON(*.json)|*json";
            this.saveInputDialog.Title = "Save Configuration";
            // 
            // openInputDialog
            // 
            this.openInputDialog.DefaultExt = "json";
            this.openInputDialog.FileName = "openFileDialog1";
            this.openInputDialog.Filter = "JSON(*.json)|*json";
            this.openInputDialog.Title = "Open Configuration";
            // 
            // AlgorithmForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1319, 710);
            this.Controls.Add(this.baseSplitContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AlgorithmForm";
            this.Text = "Algorithm Form";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AlgorithmForm_FormClosed);
            this.baseSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.baseSplitContainer)).EndInit();
            this.baseSplitContainer.ResumeLayout(false);
            this.configurationSplitContainer.Panel1.ResumeLayout(false);
            this.configurationSplitContainer.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.configurationSplitContainer)).EndInit();
            this.configurationSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer baseSplitContainer;
        private System.Windows.Forms.ComboBox algorithmProblemComboBox;
        private System.Windows.Forms.Label algorithmProblemLabel;
        private System.Windows.Forms.SaveFileDialog saveInputDialog;
        private System.Windows.Forms.OpenFileDialog openInputDialog;
        private System.Windows.Forms.SplitContainer configurationSplitContainer;
    }
}

