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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.levelTable = new System.Windows.Forms.DataGridView();
            this.Level = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Closeness = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.editButton = new System.Windows.Forms.Button();
            this.solveButton = new System.Windows.Forms.Button();
            this.removeLevelButton = new System.Windows.Forms.Button();
            this.addLevelButton = new System.Windows.Forms.Button();
            this.algorithmLabel = new System.Windows.Forms.Label();
            this.algorithmComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.levelTable)).BeginInit();
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
            this.splitContainer1.Panel2.Controls.Add(this.levelTable);
            this.splitContainer1.Panel2.Controls.Add(this.editButton);
            this.splitContainer1.Panel2.Controls.Add(this.solveButton);
            this.splitContainer1.Panel2.Controls.Add(this.removeLevelButton);
            this.splitContainer1.Panel2.Controls.Add(this.addLevelButton);
            this.splitContainer1.Panel2.Controls.Add(this.algorithmLabel);
            this.splitContainer1.Panel2.Controls.Add(this.algorithmComboBox);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.button2);
            this.splitContainer1.Panel2.Controls.Add(this.saveButton);
            this.splitContainer1.Size = new System.Drawing.Size(1016, 573);
            this.splitContainer1.SplitterDistance = 787;
            this.splitContainer1.TabIndex = 0;
            // 
            // levelTable
            // 
            this.levelTable.AllowUserToAddRows = false;
            this.levelTable.AllowUserToResizeColumns = false;
            this.levelTable.AllowUserToResizeRows = false;
            this.levelTable.BackgroundColor = System.Drawing.SystemColors.Control;
            this.levelTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.levelTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Level,
            this.Closeness});
            this.levelTable.Location = new System.Drawing.Point(13, 148);
            this.levelTable.MultiSelect = false;
            this.levelTable.Name = "levelTable";
            this.levelTable.RowHeadersVisible = false;
            this.levelTable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.levelTable.Size = new System.Drawing.Size(199, 159);
            this.levelTable.TabIndex = 9;
            this.levelTable.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.levelTable_CellEndEdit);
            this.levelTable.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.levelTable_UserDeletedRow);
            // 
            // Level
            // 
            this.Level.HeaderText = "Level";
            this.Level.Name = "Level";
            this.Level.ReadOnly = true;
            this.Level.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Level.Width = 50;
            // 
            // Closeness
            // 
            this.Closeness.HeaderText = "Closeness";
            this.Closeness.Name = "Closeness";
            this.Closeness.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Closeness.Width = 150;
            // 
            // editButton
            // 
            this.editButton.Location = new System.Drawing.Point(123, 538);
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size(88, 23);
            this.editButton.TabIndex = 8;
            this.editButton.Text = "Edit";
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler(this.editButton_Click);
            // 
            // solveButton
            // 
            this.solveButton.Location = new System.Drawing.Point(14, 538);
            this.solveButton.Name = "solveButton";
            this.solveButton.Size = new System.Drawing.Size(90, 23);
            this.solveButton.TabIndex = 7;
            this.solveButton.Text = "Solve";
            this.solveButton.UseVisualStyleBackColor = true;
            this.solveButton.Click += new System.EventHandler(this.solveButton_Click);
            // 
            // removeLevelButton
            // 
            this.removeLevelButton.Location = new System.Drawing.Point(123, 325);
            this.removeLevelButton.Name = "removeLevelButton";
            this.removeLevelButton.Size = new System.Drawing.Size(88, 23);
            this.removeLevelButton.TabIndex = 6;
            this.removeLevelButton.Text = "Remove";
            this.removeLevelButton.UseVisualStyleBackColor = true;
            this.removeLevelButton.Click += new System.EventHandler(this.removeLevelButton_Click);
            // 
            // addLevelButton
            // 
            this.addLevelButton.Location = new System.Drawing.Point(13, 325);
            this.addLevelButton.Name = "addLevelButton";
            this.addLevelButton.Size = new System.Drawing.Size(90, 23);
            this.addLevelButton.TabIndex = 5;
            this.addLevelButton.Text = "Add";
            this.addLevelButton.UseVisualStyleBackColor = true;
            this.addLevelButton.Click += new System.EventHandler(this.addLevelButton_Click);
            // 
            // algorithmLabel
            // 
            this.algorithmLabel.AutoSize = true;
            this.algorithmLabel.Location = new System.Drawing.Point(14, 88);
            this.algorithmLabel.Name = "algorithmLabel";
            this.algorithmLabel.Size = new System.Drawing.Size(50, 13);
            this.algorithmLabel.TabIndex = 3;
            this.algorithmLabel.Text = "Algorithm";
            // 
            // algorithmComboBox
            // 
            this.algorithmComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.algorithmComboBox.FormattingEnabled = true;
            this.algorithmComboBox.Location = new System.Drawing.Point(14, 107);
            this.algorithmComboBox.Name = "algorithmComboBox";
            this.algorithmComboBox.Size = new System.Drawing.Size(198, 21);
            this.algorithmComboBox.TabIndex = 2;
            this.algorithmComboBox.SelectedIndexChanged += new System.EventHandler(this.algorithmComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(14, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 2);
            this.label1.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(14, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(90, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Open";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(121, 12);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(90, 23);
            this.saveButton.TabIndex = 0;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1016, 573);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainForm";
            this.Text = "Multi-Scale Trajectory Simplification Algorithms Framework";
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.levelTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button removeLevelButton;
        private System.Windows.Forms.Button addLevelButton;
        private System.Windows.Forms.Label algorithmLabel;
        private System.Windows.Forms.ComboBox algorithmComboBox;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button solveButton;
        private System.Windows.Forms.DataGridView levelTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn Level;
        private System.Windows.Forms.DataGridViewTextBoxColumn Closeness;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button saveButton;
    }
}

