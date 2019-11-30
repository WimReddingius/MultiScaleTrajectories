namespace MultiScaleTrajectories.Simplification.MultiScale.View.Edit
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
            this.Level = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Closeness = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.errorSamplerSplitPanel = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.errorSamplerComboBox = new System.Windows.Forms.ComboBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.openErrorsFileDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.levelTable)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorSamplerSplitPanel)).BeginInit();
            this.errorSamplerSplitPanel.Panel1.SuspendLayout();
            this.errorSamplerSplitPanel.SuspendLayout();
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
            this.addLevelButton.Size = new System.Drawing.Size(100, 37);
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
            this.removeLevelButton.Size = new System.Drawing.Size(98, 37);
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
            this.levelTable.Size = new System.Drawing.Size(202, 404);
            this.levelTable.TabIndex = 12;
            this.levelTable.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.levelTable_CellEndEdit);
            this.levelTable.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.levelTable_UserDeletedRow);
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
            dataGridViewCellStyle1.Format = "N10";
            dataGridViewCellStyle1.NullValue = null;
            this.Closeness.DefaultCellStyle = dataGridViewCellStyle1;
            this.Closeness.HeaderText = "Closeness";
            this.Closeness.MinimumWidth = 150;
            this.Closeness.Name = "Closeness";
            this.Closeness.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Closeness.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
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
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(208, 208);
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
            this.splitContainer2.Size = new System.Drawing.Size(202, 37);
            this.splitContainer2.SplitterDistance = 100;
            this.splitContainer2.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.errorSamplerSplitPanel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 46);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(202, 159);
            this.panel1.TabIndex = 1;
            // 
            // errorSamplerSplitPanel
            // 
            this.errorSamplerSplitPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.errorSamplerSplitPanel.Location = new System.Drawing.Point(0, 0);
            this.errorSamplerSplitPanel.Name = "errorSamplerSplitPanel";
            this.errorSamplerSplitPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // errorSamplerSplitPanel.Panel1
            // 
            this.errorSamplerSplitPanel.Panel1.Controls.Add(this.label1);
            this.errorSamplerSplitPanel.Panel1.Controls.Add(this.errorSamplerComboBox);
            this.errorSamplerSplitPanel.Panel1MinSize = 23;
            this.errorSamplerSplitPanel.Size = new System.Drawing.Size(202, 159);
            this.errorSamplerSplitPanel.SplitterDistance = 25;
            this.errorSamplerSplitPanel.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Sampler";
            // 
            // errorSamplerComboBox
            // 
            this.errorSamplerComboBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.errorSamplerComboBox.FormattingEnabled = true;
            this.errorSamplerComboBox.Location = new System.Drawing.Point(51, 0);
            this.errorSamplerComboBox.Name = "errorSamplerComboBox";
            this.errorSamplerComboBox.Size = new System.Drawing.Size(151, 21);
            this.errorSamplerComboBox.TabIndex = 19;
            this.errorSamplerComboBox.SelectedIndexChanged += new System.EventHandler(this.errorSamplerComboBox_SelectedIndexChanged);
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
            this.splitContainer1.Size = new System.Drawing.Size(208, 622);
            this.splitContainer1.SplitterDistance = 410;
            this.splitContainer1.TabIndex = 14;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.levelTable);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3);
            this.panel2.Size = new System.Drawing.Size(208, 410);
            this.panel2.TabIndex = 13;
            // 
            // openErrorsFileDialog
            // 
            this.openErrorsFileDialog.DefaultExt = "json";
            this.openErrorsFileDialog.Filter = "JSON Files|*.json";
            // 
            // EpsilonListEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "EpsilonListEditor";
            this.Size = new System.Drawing.Size(208, 622);
            ((System.ComponentModel.ISupportInitialize)(this.levelTable)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.errorSamplerSplitPanel.Panel1.ResumeLayout(false);
            this.errorSamplerSplitPanel.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorSamplerSplitPanel)).EndInit();
            this.errorSamplerSplitPanel.ResumeLayout(false);
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
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.OpenFileDialog openErrorsFileDialog;
        private System.Windows.Forms.DataGridViewTextBoxColumn Level;
        private System.Windows.Forms.DataGridViewTextBoxColumn Closeness;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox errorSamplerComboBox;
        private System.Windows.Forms.SplitContainer errorSamplerSplitPanel;
        private System.Windows.Forms.Label label1;
    }
}
