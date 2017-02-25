using OpenTK;

namespace MultiScaleTrajectories
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.viewTypeComboBox = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.inputTabPage = new System.Windows.Forms.TabPage();
            this.inputPanel = new System.Windows.Forms.Panel();
            this.openInputButton = new System.Windows.Forms.Button();
            this.clearInputButton = new System.Windows.Forms.Button();
            this.saveInputButton = new System.Windows.Forms.Button();
            this.viewTabPage = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.algorithmTypeComboBox = new System.Windows.Forms.ComboBox();
            this.algorithmTypeLabel = new System.Windows.Forms.Label();
            this.algorithmLabel = new System.Windows.Forms.Label();
            this.algorithmComboBox = new System.Windows.Forms.ComboBox();
            this.saveInputDialog = new System.Windows.Forms.SaveFileDialog();
            this.openInputDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.inputTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.viewTypeComboBox);
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.algorithmTypeComboBox);
            this.splitContainer1.Panel2.Controls.Add(this.algorithmTypeLabel);
            this.splitContainer1.Panel2.Controls.Add(this.algorithmLabel);
            this.splitContainer1.Panel2.Controls.Add(this.algorithmComboBox);
            this.splitContainer1.Size = new System.Drawing.Size(988, 573);
            this.splitContainer1.SplitterDistance = 729;
            this.splitContainer1.TabIndex = 0;
            // 
            // viewTypeComboBox
            // 
            this.viewTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.viewTypeComboBox.Location = new System.Drawing.Point(10, 137);
            this.viewTypeComboBox.Name = "viewTypeComboBox";
            this.viewTypeComboBox.Size = new System.Drawing.Size(233, 21);
            this.viewTypeComboBox.TabIndex = 3;
            this.viewTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.viewTypeComboBox_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.inputTabPage);
            this.tabControl1.Controls.Add(this.viewTabPage);
            this.tabControl1.Location = new System.Drawing.Point(10, 175);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(233, 386);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 4;
            // 
            // inputTabPage
            // 
            this.inputTabPage.Controls.Add(this.inputPanel);
            this.inputTabPage.Controls.Add(this.openInputButton);
            this.inputTabPage.Controls.Add(this.clearInputButton);
            this.inputTabPage.Controls.Add(this.saveInputButton);
            this.inputTabPage.Location = new System.Drawing.Point(4, 22);
            this.inputTabPage.Name = "inputTabPage";
            this.inputTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.inputTabPage.Size = new System.Drawing.Size(225, 360);
            this.inputTabPage.TabIndex = 0;
            this.inputTabPage.Text = "Input";
            this.inputTabPage.UseVisualStyleBackColor = true;
            // 
            // inputPanel
            // 
            this.inputPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.inputPanel.Location = new System.Drawing.Point(3, 35);
            this.inputPanel.Name = "inputPanel";
            this.inputPanel.Size = new System.Drawing.Size(219, 322);
            this.inputPanel.TabIndex = 14;
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
            // viewTabPage
            // 
            this.viewTabPage.Location = new System.Drawing.Point(4, 22);
            this.viewTabPage.Name = "viewTabPage";
            this.viewTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.viewTabPage.Size = new System.Drawing.Size(225, 360);
            this.viewTabPage.TabIndex = 1;
            this.viewTabPage.Text = "View";
            this.viewTabPage.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "View Type";
            // 
            // algorithmTypeComboBox
            // 
            this.algorithmTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.algorithmTypeComboBox.Location = new System.Drawing.Point(10, 30);
            this.algorithmTypeComboBox.Name = "algorithmTypeComboBox";
            this.algorithmTypeComboBox.Size = new System.Drawing.Size(233, 21);
            this.algorithmTypeComboBox.TabIndex = 1;
            this.algorithmTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.algorithmTypeComboBox_SelectedIndexChanged);
            // 
            // algorithmTypeLabel
            // 
            this.algorithmTypeLabel.AutoSize = true;
            this.algorithmTypeLabel.Location = new System.Drawing.Point(10, 14);
            this.algorithmTypeLabel.Name = "algorithmTypeLabel";
            this.algorithmTypeLabel.Size = new System.Drawing.Size(77, 13);
            this.algorithmTypeLabel.TabIndex = 10;
            this.algorithmTypeLabel.Text = "Algorithm Type";
            // 
            // algorithmLabel
            // 
            this.algorithmLabel.AutoSize = true;
            this.algorithmLabel.Location = new System.Drawing.Point(10, 65);
            this.algorithmLabel.Name = "algorithmLabel";
            this.algorithmLabel.Size = new System.Drawing.Size(50, 13);
            this.algorithmLabel.TabIndex = 3;
            this.algorithmLabel.Text = "Algorithm";
            this.algorithmLabel.Click += new System.EventHandler(this.algorithmLabel_Click);
            // 
            // algorithmComboBox
            // 
            this.algorithmComboBox.DisplayMember = "ToStringdsf";
            this.algorithmComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.algorithmComboBox.Location = new System.Drawing.Point(10, 84);
            this.algorithmComboBox.Name = "algorithmComboBox";
            this.algorithmComboBox.Size = new System.Drawing.Size(233, 21);
            this.algorithmComboBox.TabIndex = 2;
            this.algorithmComboBox.SelectedIndexChanged += new System.EventHandler(this.algorithmComboBox_SelectedIndexChanged);
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(988, 573);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Multi-Scale Trajectory Simplification Algorithms Framework";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.inputTabPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label algorithmLabel;
        private System.Windows.Forms.ComboBox algorithmComboBox;
        private System.Windows.Forms.Button openInputButton;
        private System.Windows.Forms.Button saveInputButton;
        private System.Windows.Forms.ComboBox algorithmTypeComboBox;
        private System.Windows.Forms.Label algorithmTypeLabel;
        private System.Windows.Forms.SaveFileDialog saveInputDialog;
        private System.Windows.Forms.OpenFileDialog openInputDialog;
        private System.Windows.Forms.Button clearInputButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage inputTabPage;
        private System.Windows.Forms.TabPage viewTabPage;
        private System.Windows.Forms.Panel inputPanel;
        private System.Windows.Forms.ComboBox viewTypeComboBox;
    }
}

