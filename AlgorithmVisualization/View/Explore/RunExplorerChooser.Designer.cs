namespace AlgorithmVisualization.View.Explore
{
    partial class RunExplorerChooser<TIn, TOut>
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
            this.runExplorerComboBox = new System.Windows.Forms.ComboBox();
            this.visualizationContainer = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // runExplorerComboBox
            // 
            this.runExplorerComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.runExplorerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.runExplorerComboBox.Location = new System.Drawing.Point(268, 0);
            this.runExplorerComboBox.Name = "runExplorerComboBox";
            this.runExplorerComboBox.Size = new System.Drawing.Size(175, 21);
            this.runExplorerComboBox.TabIndex = 0;
            this.runExplorerComboBox.SelectedIndexChanged += new System.EventHandler(this.runExplorerComboBox_SelectedIndexChanged);
            // 
            // visualizationContainer
            // 
            this.visualizationContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.visualizationContainer.Location = new System.Drawing.Point(0, 0);
            this.visualizationContainer.Name = "visualizationContainer";
            this.visualizationContainer.Size = new System.Drawing.Size(443, 424);
            this.visualizationContainer.TabIndex = 1;
            // 
            // RunExplorerChooser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.visualizationContainer);
            this.Controls.Add(this.runExplorerComboBox);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "RunExplorerChooser";
            this.Size = new System.Drawing.Size(443, 424);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox runExplorerComboBox;
        private System.Windows.Forms.Panel visualizationContainer;
    }
}
