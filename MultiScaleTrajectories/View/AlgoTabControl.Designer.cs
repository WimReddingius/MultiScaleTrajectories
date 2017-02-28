using MultiScaleTrajectories.Algorithm;

namespace MultiScaleTrajectories.View
{
    partial class AlgoTabControl<TIn, TOut> where TIn : Input, new() where TOut : Algorithm.Output, new()
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.runTabPage = new System.Windows.Forms.TabPage();
            this.computeButton = new System.Windows.Forms.Button();
            this.algorithmLabel = new System.Windows.Forms.Label();
            this.algorithmComboBox = new System.Windows.Forms.ComboBox();
            this.inputTabPage = new System.Windows.Forms.TabPage();
            this.inputOptionsPanel = new System.Windows.Forms.Panel();
            this.openInputButton = new System.Windows.Forms.Button();
            this.clearInputButton = new System.Windows.Forms.Button();
            this.saveInputButton = new System.Windows.Forms.Button();
            this.outputTabPage = new System.Windows.Forms.TabPage();
            this.outputViewOptionsPanel = new System.Windows.Forms.Panel();
            this.outputControllerComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.openInputDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveInputDialog = new System.Windows.Forms.SaveFileDialog();
            this.tabControl.SuspendLayout();
            this.runTabPage.SuspendLayout();
            this.inputTabPage.SuspendLayout();
            this.outputTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.runTabPage);
            this.tabControl.Controls.Add(this.inputTabPage);
            this.tabControl.Controls.Add(this.outputTabPage);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(248, 376);
            this.tabControl.TabIndex = 5;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // runTabPage
            // 
            this.runTabPage.Controls.Add(this.computeButton);
            this.runTabPage.Controls.Add(this.algorithmLabel);
            this.runTabPage.Controls.Add(this.algorithmComboBox);
            this.runTabPage.Location = new System.Drawing.Point(4, 22);
            this.runTabPage.Name = "runTabPage";
            this.runTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.runTabPage.Size = new System.Drawing.Size(240, 350);
            this.runTabPage.TabIndex = 2;
            this.runTabPage.Text = "Run";
            this.runTabPage.UseVisualStyleBackColor = true;
            // 
            // computeButton
            // 
            this.computeButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.computeButton.Location = new System.Drawing.Point(4, 321);
            this.computeButton.Name = "computeButton";
            this.computeButton.Size = new System.Drawing.Size(230, 23);
            this.computeButton.TabIndex = 6;
            this.computeButton.Text = "Compute";
            this.computeButton.UseVisualStyleBackColor = true;
            this.computeButton.Click += new System.EventHandler(this.computeButton_Click);
            // 
            // algorithmLabel
            // 
            this.algorithmLabel.AutoSize = true;
            this.algorithmLabel.Location = new System.Drawing.Point(3, 6);
            this.algorithmLabel.Name = "algorithmLabel";
            this.algorithmLabel.Size = new System.Drawing.Size(50, 13);
            this.algorithmLabel.TabIndex = 5;
            this.algorithmLabel.Text = "Algorithm";
            // 
            // algorithmComboBox
            // 
            this.algorithmComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.algorithmComboBox.DisplayMember = "ToStringdsf";
            this.algorithmComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.algorithmComboBox.Location = new System.Drawing.Point(3, 25);
            this.algorithmComboBox.Name = "algorithmComboBox";
            this.algorithmComboBox.Size = new System.Drawing.Size(233, 21);
            this.algorithmComboBox.TabIndex = 4;
            this.algorithmComboBox.SelectedIndexChanged += new System.EventHandler(this.algorithmComboBox_SelectedIndexChanged);
            // 
            // inputTabPage
            // 
            this.inputTabPage.Controls.Add(this.inputOptionsPanel);
            this.inputTabPage.Controls.Add(this.openInputButton);
            this.inputTabPage.Controls.Add(this.clearInputButton);
            this.inputTabPage.Controls.Add(this.saveInputButton);
            this.inputTabPage.Location = new System.Drawing.Point(4, 22);
            this.inputTabPage.Name = "inputTabPage";
            this.inputTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.inputTabPage.Size = new System.Drawing.Size(240, 350);
            this.inputTabPage.TabIndex = 0;
            this.inputTabPage.Text = "Input";
            this.inputTabPage.UseVisualStyleBackColor = true;
            // 
            // inputOptionsPanel
            // 
            this.inputOptionsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inputOptionsPanel.Location = new System.Drawing.Point(3, 35);
            this.inputOptionsPanel.Name = "inputOptionsPanel";
            this.inputOptionsPanel.Size = new System.Drawing.Size(234, 315);
            this.inputOptionsPanel.TabIndex = 14;
            // 
            // openInputButton
            // 
            this.openInputButton.Location = new System.Drawing.Point(3, 6);
            this.openInputButton.Name = "openInputButton";
            this.openInputButton.Size = new System.Drawing.Size(67, 23);
            this.openInputButton.TabIndex = 1;
            this.openInputButton.Text = "Open";
            this.openInputButton.UseVisualStyleBackColor = true;
            this.openInputButton.Click += new System.EventHandler(this.openInputButton_Click);
            // 
            // clearInputButton
            // 
            this.clearInputButton.Location = new System.Drawing.Point(79, 6);
            this.clearInputButton.Name = "clearInputButton";
            this.clearInputButton.Size = new System.Drawing.Size(73, 23);
            this.clearInputButton.TabIndex = 13;
            this.clearInputButton.Text = "Clear";
            this.clearInputButton.UseVisualStyleBackColor = true;
            this.clearInputButton.Click += new System.EventHandler(this.clearInputButton_Click);
            // 
            // saveInputButton
            // 
            this.saveInputButton.Location = new System.Drawing.Point(161, 6);
            this.saveInputButton.Name = "saveInputButton";
            this.saveInputButton.Size = new System.Drawing.Size(61, 23);
            this.saveInputButton.TabIndex = 0;
            this.saveInputButton.Text = "Save";
            this.saveInputButton.UseVisualStyleBackColor = true;
            this.saveInputButton.Click += new System.EventHandler(this.saveInputButton_Click);
            // 
            // outputTabPage
            // 
            this.outputTabPage.Controls.Add(this.outputViewOptionsPanel);
            this.outputTabPage.Controls.Add(this.outputControllerComboBox);
            this.outputTabPage.Controls.Add(this.label2);
            this.outputTabPage.Location = new System.Drawing.Point(4, 22);
            this.outputTabPage.Name = "outputTabPage";
            this.outputTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.outputTabPage.Size = new System.Drawing.Size(240, 350);
            this.outputTabPage.TabIndex = 1;
            this.outputTabPage.Text = "Output";
            this.outputTabPage.UseVisualStyleBackColor = true;
            // 
            // outputViewOptionsPanel
            // 
            this.outputViewOptionsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outputViewOptionsPanel.Location = new System.Drawing.Point(6, 51);
            this.outputViewOptionsPanel.Name = "outputViewOptionsPanel";
            this.outputViewOptionsPanel.Size = new System.Drawing.Size(226, 340);
            this.outputViewOptionsPanel.TabIndex = 16;
            // 
            // outputControllerComboBox
            // 
            this.outputControllerComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outputControllerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.outputControllerComboBox.Location = new System.Drawing.Point(6, 24);
            this.outputControllerComboBox.Name = "outputControllerComboBox";
            this.outputControllerComboBox.Size = new System.Drawing.Size(226, 21);
            this.outputControllerComboBox.TabIndex = 3;
            this.outputControllerComboBox.SelectedIndexChanged += new System.EventHandler(this.outputControllerComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "View";
            // 
            // openInputDialog
            // 
            this.openInputDialog.DefaultExt = "json";
            this.openInputDialog.FileName = "openFileDialog1";
            this.openInputDialog.Filter = "JSON(*.json)|*json";
            this.openInputDialog.Title = "Open Configuration";
            // 
            // saveInputDialog
            // 
            this.saveInputDialog.DefaultExt = "json";
            this.saveInputDialog.Filter = "JSON(*.json)|*json";
            this.saveInputDialog.Title = "Save Configuration";
            // 
            // AlgoTabControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Name = "AlgoTabControl";
            this.Size = new System.Drawing.Size(248, 376);
            this.tabControl.ResumeLayout(false);
            this.runTabPage.ResumeLayout(false);
            this.runTabPage.PerformLayout();
            this.inputTabPage.ResumeLayout(false);
            this.outputTabPage.ResumeLayout(false);
            this.outputTabPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage inputTabPage;
        private System.Windows.Forms.Panel inputOptionsPanel;
        private System.Windows.Forms.Button openInputButton;
        private System.Windows.Forms.Button clearInputButton;
        private System.Windows.Forms.Button saveInputButton;
        private System.Windows.Forms.TabPage outputTabPage;
        private System.Windows.Forms.Panel outputViewOptionsPanel;
        private System.Windows.Forms.ComboBox outputControllerComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage runTabPage;
        private System.Windows.Forms.Label algorithmLabel;
        private System.Windows.Forms.ComboBox algorithmComboBox;
        private System.Windows.Forms.Button computeButton;
        private System.Windows.Forms.OpenFileDialog openInputDialog;
        private System.Windows.Forms.SaveFileDialog saveInputDialog;
    }
}
