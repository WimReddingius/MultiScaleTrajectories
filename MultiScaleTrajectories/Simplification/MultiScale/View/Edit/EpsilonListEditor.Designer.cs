namespace MultiScaleTrajectories.MultiScale.View.Edit
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.addLevelButton = new System.Windows.Forms.Button();
            this.removeLevelButton = new System.Windows.Forms.Button();
            this.levelTable = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Level = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Closeness = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.levelTable)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // addLevelButton
            // 
            this.addLevelButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addLevelButton.Location = new System.Drawing.Point(3, 3);
            this.addLevelButton.Name = "addLevelButton";
            this.addLevelButton.Size = new System.Drawing.Size(95, 26);
            this.addLevelButton.TabIndex = 10;
            this.addLevelButton.Text = "Add";
            this.addLevelButton.UseVisualStyleBackColor = true;
            this.addLevelButton.Click += new System.EventHandler(this.addLevelButton_Click);
            // 
            // removeLevelButton
            // 
            this.removeLevelButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.removeLevelButton.Location = new System.Drawing.Point(104, 3);
            this.removeLevelButton.Name = "removeLevelButton";
            this.removeLevelButton.Size = new System.Drawing.Size(95, 26);
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
            this.levelTable.Location = new System.Drawing.Point(0, 0);
            this.levelTable.MultiSelect = false;
            this.levelTable.Name = "levelTable";
            this.levelTable.RowHeadersVisible = false;
            this.levelTable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.levelTable.Size = new System.Drawing.Size(202, 271);
            this.levelTable.TabIndex = 12;
            this.levelTable.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.levelTable_CellEndEdit);
            this.levelTable.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.levelTable_UserDeletedRow);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.addLevelButton, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.removeLevelButton, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(202, 32);
            this.tableLayoutPanel1.TabIndex = 13;
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
            dataGridViewCellStyle6.Format = "0.00##";
            dataGridViewCellStyle6.NullValue = null;
            this.Closeness.DefaultCellStyle = dataGridViewCellStyle6;
            this.Closeness.HeaderText = "Closeness";
            this.Closeness.MinimumWidth = 150;
            this.Closeness.Name = "Closeness";
            this.Closeness.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Closeness.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.levelTable);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer1.Size = new System.Drawing.Size(202, 307);
            this.splitContainer1.SplitterDistance = 271;
            this.splitContainer1.TabIndex = 14;
            // 
            // EpsilonListEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "EpsilonListEditor";
            this.Size = new System.Drawing.Size(202, 307);
            ((System.ComponentModel.ISupportInitialize)(this.levelTable)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button addLevelButton;
        private System.Windows.Forms.Button removeLevelButton;
        private System.Windows.Forms.DataGridView levelTable;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Level;
        private System.Windows.Forms.DataGridViewTextBoxColumn Closeness;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}
