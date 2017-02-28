namespace MultiScaleTrajectories.View.SingleTrajectory.Input
{
    partial class STInputOptions
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
            ((System.ComponentModel.ISupportInitialize)(this.levelTable)).BeginInit();
            this.SuspendLayout();
            // 
            // addLevelButton
            // 
            this.addLevelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addLevelButton.Location = new System.Drawing.Point(3, 172);
            this.addLevelButton.Name = "addLevelButton";
            this.addLevelButton.Size = new System.Drawing.Size(90, 23);
            this.addLevelButton.TabIndex = 10;
            this.addLevelButton.Text = "Add";
            this.addLevelButton.UseVisualStyleBackColor = true;
            this.addLevelButton.Click += new System.EventHandler(this.addLevelButton_Click);
            // 
            // removeLevelButton
            // 
            this.removeLevelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.removeLevelButton.Location = new System.Drawing.Point(103, 172);
            this.removeLevelButton.Name = "removeLevelButton";
            this.removeLevelButton.Size = new System.Drawing.Size(88, 23);
            this.removeLevelButton.TabIndex = 11;
            this.removeLevelButton.Text = "Remove";
            this.removeLevelButton.UseVisualStyleBackColor = true;
            this.removeLevelButton.Click += new System.EventHandler(this.removeLevelButton_Click);
            // 
            // levelTable
            // 
            this.levelTable.AllowUserToAddRows = false;
            this.levelTable.AllowUserToDeleteRows = false;
            this.levelTable.AllowUserToResizeColumns = false;
            this.levelTable.AllowUserToResizeRows = false;
            this.levelTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.levelTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.levelTable.BackgroundColor = System.Drawing.SystemColors.Control;
            this.levelTable.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.levelTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.levelTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Level,
            this.Closeness});
            this.levelTable.Location = new System.Drawing.Point(0, 0);
            this.levelTable.MultiSelect = false;
            this.levelTable.Name = "levelTable";
            this.levelTable.RowHeadersVisible = false;
            this.levelTable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.levelTable.Size = new System.Drawing.Size(194, 162);
            this.levelTable.TabIndex = 12;
            this.levelTable.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.levelTable_CellEndEdit);
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
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = null;
            this.Closeness.DefaultCellStyle = dataGridViewCellStyle1;
            this.Closeness.HeaderText = "Closeness";
            this.Closeness.MinimumWidth = 150;
            this.Closeness.Name = "Closeness";
            this.Closeness.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Closeness.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // STInputOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.levelTable);
            this.Controls.Add(this.removeLevelButton);
            this.Controls.Add(this.addLevelButton);
            this.Name = "STInputOptions";
            this.Size = new System.Drawing.Size(194, 198);
            ((System.ComponentModel.ISupportInitialize)(this.levelTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button addLevelButton;
        private System.Windows.Forms.Button removeLevelButton;
        private System.Windows.Forms.DataGridView levelTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn Level;
        private System.Windows.Forms.DataGridViewTextBoxColumn Closeness;
    }
}
