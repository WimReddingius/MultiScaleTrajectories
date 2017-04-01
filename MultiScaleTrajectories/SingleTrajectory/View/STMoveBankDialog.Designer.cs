namespace MultiScaleTrajectories.SingleTrajectory.View
{
    partial class STMoveBankDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(STMoveBankDialog));
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.trajectoryTable = new System.Windows.Forms.DataGridView();
            this.trajectoryIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trajectoryPointsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trajectoryGMap = new MultiScaleTrajectories.View.TrajectoryGMap();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trajectoryTable)).BeginInit();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(599, 498);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(680, 498);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.trajectoryGMap);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.trajectoryTable);
            this.splitContainer1.Size = new System.Drawing.Size(767, 484);
            this.splitContainer1.SplitterDistance = 551;
            this.splitContainer1.TabIndex = 2;
            // 
            // trajectoryTable
            // 
            this.trajectoryTable.AllowUserToAddRows = false;
            this.trajectoryTable.AllowUserToDeleteRows = false;
            this.trajectoryTable.AllowUserToResizeRows = false;
            this.trajectoryTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.trajectoryTable.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.trajectoryTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.trajectoryTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.trajectoryIdColumn,
            this.trajectoryPointsColumn});
            this.trajectoryTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trajectoryTable.Location = new System.Drawing.Point(0, 0);
            this.trajectoryTable.MultiSelect = false;
            this.trajectoryTable.Name = "trajectoryTable";
            this.trajectoryTable.ReadOnly = true;
            this.trajectoryTable.RowHeadersVisible = false;
            this.trajectoryTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.trajectoryTable.Size = new System.Drawing.Size(212, 484);
            this.trajectoryTable.TabIndex = 0;
            this.trajectoryTable.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // trajectoryIdColumn
            // 
            this.trajectoryIdColumn.HeaderText = "Id";
            this.trajectoryIdColumn.Name = "trajectoryIdColumn";
            this.trajectoryIdColumn.ReadOnly = true;
            // 
            // trajectoryPointsColumn
            // 
            this.trajectoryPointsColumn.HeaderText = "Points";
            this.trajectoryPointsColumn.Name = "trajectoryPointsColumn";
            this.trajectoryPointsColumn.ReadOnly = true;
            // 
            // trajectoryGMap
            // 
            this.trajectoryGMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trajectoryGMap.Location = new System.Drawing.Point(0, 0);
            this.trajectoryGMap.Margin = new System.Windows.Forms.Padding(2);
            this.trajectoryGMap.Name = "trajectoryGMap";
            this.trajectoryGMap.Size = new System.Drawing.Size(551, 484);
            this.trajectoryGMap.TabIndex = 0;
            // 
            // STMoveBankDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 533);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "STMoveBankDialog";
            this.Text = "Choose Trajectory";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trajectoryTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private MultiScaleTrajectories.View.TrajectoryGMap trajectoryGMap;
        private System.Windows.Forms.DataGridView trajectoryTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn trajectoryIdColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn trajectoryPointsColumn;
    }
}