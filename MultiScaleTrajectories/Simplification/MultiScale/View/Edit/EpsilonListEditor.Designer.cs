﻿namespace MultiScaleTrajectories.Simplification.MultiScale.View.Edit
{
    partial class EpsilonListEditor
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.addLevelButton = new System.Windows.Forms.Button();
            this.removeLevelButton = new System.Windows.Forms.Button();
            this.levelTable = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.openErrorsButton = new System.Windows.Forms.Button();
            this.shortcutFindingProgressLabel = new System.Windows.Forms.Label();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.computeEpsilons = new System.Windows.Forms.Button();
            this.revalidateDistributionButton = new System.Windows.Forms.Button();
            this.numLevelsChooser = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lowerPercentageChooser = new System.Windows.Forms.NumericUpDown();
            this.upperPercentageChooser = new System.Windows.Forms.NumericUpDown();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.openErrorsFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.Level = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Closeness = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.levelTable)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLevelsChooser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lowerPercentageChooser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upperPercentageChooser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // addLevelButton
            // 
            this.addLevelButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addLevelButton.Location = new System.Drawing.Point(0, 0);
            this.addLevelButton.Name = "addLevelButton";
            this.addLevelButton.Size = new System.Drawing.Size(101, 34);
            this.addLevelButton.TabIndex = 10;
            this.addLevelButton.Text = "Add";
            this.addLevelButton.UseVisualStyleBackColor = true;
            this.addLevelButton.Click += new System.EventHandler(this.addLevelButton_Click);
            // 
            // removeLevelButton
            // 
            this.removeLevelButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.removeLevelButton.Location = new System.Drawing.Point(0, 0);
            this.removeLevelButton.Name = "removeLevelButton";
            this.removeLevelButton.Size = new System.Drawing.Size(97, 34);
            this.removeLevelButton.TabIndex = 11;
            this.removeLevelButton.Text = "Remove";
            this.removeLevelButton.UseVisualStyleBackColor = true;
            this.removeLevelButton.Click += new System.EventHandler(this.removeLevelButton_Click);
            // 
            // levelTable
            // 
            this.levelTable.AllowUserToAddRows = false;
            this.levelTable.AllowUserToResizeColumns = false;
            this.levelTable.AllowUserToResizeRows = false;
            this.levelTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.levelTable.BackgroundColor = System.Drawing.SystemColors.Control;
            this.levelTable.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.levelTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.levelTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Level,
            this.Closeness});
            this.levelTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.levelTable.Location = new System.Drawing.Point(3, 3);
            this.levelTable.MultiSelect = false;
            this.levelTable.Name = "levelTable";
            this.levelTable.RowHeadersVisible = false;
            this.levelTable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.levelTable.Size = new System.Drawing.Size(202, 180);
            this.levelTable.TabIndex = 12;
            this.levelTable.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.levelTable_CellEndEdit);
            this.levelTable.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.levelTable_UserDeletedRow);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.splitContainer2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(208, 206);
            this.tableLayoutPanel1.TabIndex = 13;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.addLevelButton);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.removeLevelButton);
            this.splitContainer2.Size = new System.Drawing.Size(202, 34);
            this.splitContainer2.SplitterDistance = 101;
            this.splitContainer2.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.openErrorsButton);
            this.panel1.Controls.Add(this.shortcutFindingProgressLabel);
            this.panel1.Controls.Add(this.splitContainer3);
            this.panel1.Controls.Add(this.numLevelsChooser);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lowerPercentageChooser);
            this.panel1.Controls.Add(this.upperPercentageChooser);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(202, 160);
            this.panel1.TabIndex = 1;
            // 
            // openErrorsButton
            // 
            this.openErrorsButton.Location = new System.Drawing.Point(119, 4);
            this.openErrorsButton.Name = "openErrorsButton";
            this.openErrorsButton.Size = new System.Drawing.Size(75, 23);
            this.openErrorsButton.TabIndex = 18;
            this.openErrorsButton.Text = "Open";
            this.openErrorsButton.UseVisualStyleBackColor = false;
            this.openErrorsButton.Click += new System.EventHandler(this.openErrorsButton_Click);
            // 
            // shortcutFindingProgressLabel
            // 
            this.shortcutFindingProgressLabel.AutoSize = true;
            this.shortcutFindingProgressLabel.Location = new System.Drawing.Point(4, 87);
            this.shortcutFindingProgressLabel.Name = "shortcutFindingProgressLabel";
            this.shortcutFindingProgressLabel.Size = new System.Drawing.Size(0, 13);
            this.shortcutFindingProgressLabel.TabIndex = 17;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitContainer3.IsSplitterFixed = true;
            this.splitContainer3.Location = new System.Drawing.Point(0, 126);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.computeEpsilons);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.revalidateDistributionButton);
            this.splitContainer3.Size = new System.Drawing.Size(202, 34);
            this.splitContainer3.SplitterDistance = 101;
            this.splitContainer3.TabIndex = 16;
            // 
            // computeEpsilons
            // 
            this.computeEpsilons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.computeEpsilons.Location = new System.Drawing.Point(0, 0);
            this.computeEpsilons.Name = "computeEpsilons";
            this.computeEpsilons.Size = new System.Drawing.Size(101, 34);
            this.computeEpsilons.TabIndex = 13;
            this.computeEpsilons.Text = "Compute";
            this.computeEpsilons.UseVisualStyleBackColor = true;
            this.computeEpsilons.Click += new System.EventHandler(this.computeErrorDistribution_Click);
            // 
            // revalidateDistributionButton
            // 
            this.revalidateDistributionButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.revalidateDistributionButton.Enabled = false;
            this.revalidateDistributionButton.Location = new System.Drawing.Point(0, 0);
            this.revalidateDistributionButton.Name = "revalidateDistributionButton";
            this.revalidateDistributionButton.Size = new System.Drawing.Size(97, 34);
            this.revalidateDistributionButton.TabIndex = 14;
            this.revalidateDistributionButton.Text = "Redistribute";
            this.revalidateDistributionButton.UseVisualStyleBackColor = true;
            this.revalidateDistributionButton.Click += new System.EventHandler(this.revalidateDistributionButton_Click);
            // 
            // numLevelsChooser
            // 
            this.numLevelsChooser.Location = new System.Drawing.Point(131, 59);
            this.numLevelsChooser.Name = "numLevelsChooser";
            this.numLevelsChooser.Size = new System.Drawing.Size(63, 20);
            this.numLevelsChooser.TabIndex = 15;
            this.numLevelsChooser.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Amount";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Distribution";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(105, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "To";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "From";
            // 
            // lowerPercentageChooser
            // 
            this.lowerPercentageChooser.DecimalPlaces = 1;
            this.lowerPercentageChooser.Location = new System.Drawing.Point(40, 33);
            this.lowerPercentageChooser.Name = "lowerPercentageChooser";
            this.lowerPercentageChooser.Size = new System.Drawing.Size(59, 20);
            this.lowerPercentageChooser.TabIndex = 2;
            // 
            // upperPercentageChooser
            // 
            this.upperPercentageChooser.DecimalPlaces = 1;
            this.upperPercentageChooser.Location = new System.Drawing.Point(131, 33);
            this.upperPercentageChooser.Name = "upperPercentageChooser";
            this.upperPercentageChooser.Size = new System.Drawing.Size(63, 20);
            this.upperPercentageChooser.TabIndex = 1;
            this.upperPercentageChooser.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Panel2MinSize = 140;
            this.splitContainer1.Size = new System.Drawing.Size(208, 396);
            this.splitContainer1.SplitterDistance = 186;
            this.splitContainer1.TabIndex = 14;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.levelTable);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3);
            this.panel2.Size = new System.Drawing.Size(208, 186);
            this.panel2.TabIndex = 13;
            // 
            // openErrorsFileDialog
            // 
            this.openErrorsFileDialog.DefaultExt = "json";
            this.openErrorsFileDialog.Filter = "JSON Files|*.json";
            // 
            // Level
            // 
            this.Level.HeaderText = "Level";
            this.Level.MinimumWidth = 50;
            this.Level.Name = "Level";
            this.Level.ReadOnly = true;
            this.Level.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Level.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Closeness
            // 
            dataGridViewCellStyle1.Format = "N6";
            dataGridViewCellStyle1.NullValue = null;
            this.Closeness.DefaultCellStyle = dataGridViewCellStyle1;
            this.Closeness.HeaderText = "Closeness";
            this.Closeness.MinimumWidth = 150;
            this.Closeness.Name = "Closeness";
            this.Closeness.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Closeness.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // EpsilonListEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "EpsilonListEditor";
            this.Size = new System.Drawing.Size(208, 396);
            ((System.ComponentModel.ISupportInitialize)(this.levelTable)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numLevelsChooser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lowerPercentageChooser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upperPercentageChooser)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button addLevelButton;
        private System.Windows.Forms.Button removeLevelButton;
        private System.Windows.Forms.DataGridView levelTable;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown lowerPercentageChooser;
        private System.Windows.Forms.NumericUpDown upperPercentageChooser;
        private System.Windows.Forms.NumericUpDown numLevelsChooser;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button computeEpsilons;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Button revalidateDistributionButton;
        private System.Windows.Forms.Label shortcutFindingProgressLabel;
        private System.Windows.Forms.Button openErrorsButton;
        private System.Windows.Forms.OpenFileDialog openErrorsFileDialog;
        private System.Windows.Forms.DataGridViewTextBoxColumn Level;
        private System.Windows.Forms.DataGridViewTextBoxColumn Closeness;
    }
}