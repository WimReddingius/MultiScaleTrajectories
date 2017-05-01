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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.controllerFactoryComboBox = new System.Windows.Forms.ComboBox();
            this.addControllerButton = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.controllerComboBox = new System.Windows.Forms.ComboBox();
            this.removeControllerButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.saveControllerButton = new System.Windows.Forms.Button();
            this.openControllerButton = new System.Windows.Forms.Button();
            this.algorithmProblemLabel = new System.Windows.Forms.Label();
            this.saveControllerDialog = new System.Windows.Forms.SaveFileDialog();
            this.openControllerDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.baseSplitContainer)).BeginInit();
            this.baseSplitContainer.Panel2.SuspendLayout();
            this.baseSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.configurationSplitContainer)).BeginInit();
            this.configurationSplitContainer.Panel1.SuspendLayout();
            this.configurationSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
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
            this.configurationSplitContainer.Panel1.Controls.Add(this.splitContainer1);
            this.configurationSplitContainer.Panel1.Controls.Add(this.splitContainer2);
            this.configurationSplitContainer.Panel1.Controls.Add(this.tableLayoutPanel1);
            this.configurationSplitContainer.Panel1.Controls.Add(this.algorithmProblemLabel);
            this.configurationSplitContainer.Size = new System.Drawing.Size(372, 702);
            this.configurationSplitContainer.SplitterDistance = 130;
            this.configurationSplitContainer.TabIndex = 11;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(3, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.controllerFactoryComboBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.addControllerButton);
            this.splitContainer1.Size = new System.Drawing.Size(366, 22);
            this.splitContainer1.SplitterDistance = 250;
            this.splitContainer1.TabIndex = 12;
            // 
            // controllerFactoryComboBox
            // 
            this.controllerFactoryComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controllerFactoryComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.controllerFactoryComboBox.FormattingEnabled = true;
            this.controllerFactoryComboBox.Location = new System.Drawing.Point(0, 0);
            this.controllerFactoryComboBox.Name = "controllerFactoryComboBox";
            this.controllerFactoryComboBox.Size = new System.Drawing.Size(250, 21);
            this.controllerFactoryComboBox.TabIndex = 0;
            // 
            // addControllerButton
            // 
            this.addControllerButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addControllerButton.Location = new System.Drawing.Point(0, 0);
            this.addControllerButton.Name = "addControllerButton";
            this.addControllerButton.Size = new System.Drawing.Size(112, 22);
            this.addControllerButton.TabIndex = 2;
            this.addControllerButton.Text = "Add";
            this.addControllerButton.UseVisualStyleBackColor = true;
            this.addControllerButton.Click += new System.EventHandler(this.addControllerButton_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(3, 53);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.controllerComboBox);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.removeControllerButton);
            this.splitContainer2.Size = new System.Drawing.Size(366, 22);
            this.splitContainer2.SplitterDistance = 250;
            this.splitContainer2.TabIndex = 11;
            // 
            // controllerComboBox
            // 
            this.controllerComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controllerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.controllerComboBox.Location = new System.Drawing.Point(0, 0);
            this.controllerComboBox.Name = "controllerComboBox";
            this.controllerComboBox.Size = new System.Drawing.Size(250, 21);
            this.controllerComboBox.TabIndex = 1;
            this.controllerComboBox.SelectedIndexChanged += new System.EventHandler(this.controllerComboBox_SelectedIndexChanged);
            // 
            // removeControllerButton
            // 
            this.removeControllerButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.removeControllerButton.Location = new System.Drawing.Point(0, 0);
            this.removeControllerButton.Name = "removeControllerButton";
            this.removeControllerButton.Size = new System.Drawing.Size(112, 22);
            this.removeControllerButton.TabIndex = 2;
            this.removeControllerButton.Text = "Remove";
            this.removeControllerButton.UseVisualStyleBackColor = true;
            this.removeControllerButton.Click += new System.EventHandler(this.removeControllerButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.saveControllerButton, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.openControllerButton, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 83);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(372, 35);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // saveControllerButton
            // 
            this.saveControllerButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saveControllerButton.Location = new System.Drawing.Point(189, 3);
            this.saveControllerButton.Name = "saveControllerButton";
            this.saveControllerButton.Size = new System.Drawing.Size(180, 29);
            this.saveControllerButton.TabIndex = 13;
            this.saveControllerButton.Text = "Save";
            this.saveControllerButton.UseVisualStyleBackColor = true;
            this.saveControllerButton.Click += new System.EventHandler(this.saveControllerButton_Click);
            // 
            // openControllerButton
            // 
            this.openControllerButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.openControllerButton.Location = new System.Drawing.Point(3, 3);
            this.openControllerButton.Name = "openControllerButton";
            this.openControllerButton.Size = new System.Drawing.Size(180, 29);
            this.openControllerButton.TabIndex = 12;
            this.openControllerButton.Text = "Open";
            this.openControllerButton.UseVisualStyleBackColor = true;
            this.openControllerButton.Click += new System.EventHandler(this.openControllerButton_Click);
            // 
            // algorithmProblemLabel
            // 
            this.algorithmProblemLabel.AutoSize = true;
            this.algorithmProblemLabel.Location = new System.Drawing.Point(2, 8);
            this.algorithmProblemLabel.Name = "algorithmProblemLabel";
            this.algorithmProblemLabel.Size = new System.Drawing.Size(82, 13);
            this.algorithmProblemLabel.TabIndex = 10;
            this.algorithmProblemLabel.Text = "Algorithm Types";
            // 
            // saveControllerDialog
            // 
            this.saveControllerDialog.DefaultExt = "config";
            this.saveControllerDialog.Filter = "Config Files|*.config";
            this.saveControllerDialog.Title = "Save Problem Configuration";
            // 
            // openControllerDialog
            // 
            this.openControllerDialog.DefaultExt = "config";
            this.openControllerDialog.Filter = "Config Files|*.config";
            this.openControllerDialog.Title = "Open Problem Configuration";
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
            this.Load += new System.EventHandler(this.AlgorithmForm_Load);
            this.baseSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.baseSplitContainer)).EndInit();
            this.baseSplitContainer.ResumeLayout(false);
            this.configurationSplitContainer.Panel1.ResumeLayout(false);
            this.configurationSplitContainer.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.configurationSplitContainer)).EndInit();
            this.configurationSplitContainer.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer baseSplitContainer;
        private System.Windows.Forms.ComboBox controllerComboBox;
        private System.Windows.Forms.Label algorithmProblemLabel;
        private System.Windows.Forms.SaveFileDialog saveControllerDialog;
        private System.Windows.Forms.OpenFileDialog openControllerDialog;
        private System.Windows.Forms.SplitContainer configurationSplitContainer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button saveControllerButton;
        private System.Windows.Forms.Button openControllerButton;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button removeControllerButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ComboBox controllerFactoryComboBox;
        private System.Windows.Forms.Button addControllerButton;
    }
}

