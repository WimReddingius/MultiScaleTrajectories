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
            this.algorithmTypeLabel = new System.Windows.Forms.Label();
            this.algorithmTypeComboBox = new System.Windows.Forms.ComboBox();
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
            this.baseSplitContainer.Size = new System.Drawing.Size(1106, 648);
            this.baseSplitContainer.SplitterDistance = 797;
            this.baseSplitContainer.TabIndex = 0;
            // 
            // configurationSplitContainer
            // 
            this.configurationSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.configurationSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.configurationSplitContainer.IsSplitterFixed = true;
            this.configurationSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.configurationSplitContainer.Name = "configurationSplitContainer";
            this.configurationSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // configurationSplitContainer.Panel1
            // 
            this.configurationSplitContainer.Panel1.Controls.Add(this.algorithmTypeLabel);
            this.configurationSplitContainer.Panel1.Controls.Add(this.algorithmTypeComboBox);
            this.configurationSplitContainer.Size = new System.Drawing.Size(305, 648);
            this.configurationSplitContainer.SplitterDistance = 58;
            this.configurationSplitContainer.TabIndex = 11;
            // 
            // algorithmTypeLabel
            // 
            this.algorithmTypeLabel.AutoSize = true;
            this.algorithmTypeLabel.Location = new System.Drawing.Point(1, 9);
            this.algorithmTypeLabel.Name = "algorithmTypeLabel";
            this.algorithmTypeLabel.Size = new System.Drawing.Size(77, 13);
            this.algorithmTypeLabel.TabIndex = 10;
            this.algorithmTypeLabel.Text = "Algorithm Type";
            // 
            // algorithmTypeComboBox
            // 
            this.algorithmTypeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.algorithmTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.algorithmTypeComboBox.Location = new System.Drawing.Point(3, 25);
            this.algorithmTypeComboBox.Name = "algorithmTypeComboBox";
            this.algorithmTypeComboBox.Size = new System.Drawing.Size(299, 21);
            this.algorithmTypeComboBox.TabIndex = 1;
            this.algorithmTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.algorithmTypeComboBox_SelectedIndexChanged);
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
            this.ClientSize = new System.Drawing.Size(1106, 648);
            this.Controls.Add(this.baseSplitContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AlgorithmForm";
            this.Text = "Algorithm Form";
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
        private System.Windows.Forms.ComboBox algorithmTypeComboBox;
        private System.Windows.Forms.Label algorithmTypeLabel;
        private System.Windows.Forms.SaveFileDialog saveInputDialog;
        private System.Windows.Forms.OpenFileDialog openInputDialog;
        private System.Windows.Forms.SplitContainer configurationSplitContainer;
    }
}

