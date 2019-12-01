namespace MultiScaleTrajectories.Trajectory.View
{
    partial class MoveBankTrajectoryChooser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MoveBankTrajectoryChooser));
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.trajectoryGMap = new MultiScaleTrajectories.Trajectory.View.TrajectoryGeo();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.trajectoryTable = new System.Windows.Forms.DataGridView();
            this.trajectoryIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trajectoryPointsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.downSampleToChooser = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.toPointChooser = new System.Windows.Forms.NumericUpDown();
            this.fromPointChooser = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trajectoryTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.downSampleToChooser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toPointChooser)).BeginInit();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(654, 521);
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
            this.cancelButton.Location = new System.Drawing.Point(735, 521);
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
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(822, 507);
            this.splitContainer1.SplitterDistance = 528;
            this.splitContainer1.TabIndex = 2;
            // 
            // trajectoryGMap
            // 
            this.trajectoryGMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trajectoryGMap.Location = new System.Drawing.Point(0, 0);
            this.trajectoryGMap.Margin = new System.Windows.Forms.Padding(2);
            this.trajectoryGMap.Name = "trajectoryGMap";
            this.trajectoryGMap.Size = new System.Drawing.Size(528, 507);
            this.trajectoryGMap.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.trajectoryTable);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.label9);
            this.splitContainer2.Panel2.Controls.Add(this.label8);
            this.splitContainer2.Panel2.Controls.Add(this.downSampleToChooser);
            this.splitContainer2.Panel2.Controls.Add(this.label4);
            this.splitContainer2.Panel2.Controls.Add(this.toPointChooser);
            this.splitContainer2.Panel2.Controls.Add(this.label10);
            this.splitContainer2.Panel2.Controls.Add(this.fromPointChooser);
            this.splitContainer2.Panel2.Controls.Add(this.label3);
            this.splitContainer2.Size = new System.Drawing.Size(290, 507);
            this.splitContainer2.SplitterDistance = 379;
            this.splitContainer2.TabIndex = 5;
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
            this.trajectoryTable.Size = new System.Drawing.Size(290, 379);
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
            // label9
            // 
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label9.Location = new System.Drawing.Point(90, 84);
            this.label9.MaximumSize = new System.Drawing.Size(0, 2);
            this.label9.MinimumSize = new System.Drawing.Size(0, 2);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(0, 2);
            this.label9.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.Location = new System.Drawing.Point(70, 13);
            this.label8.MaximumSize = new System.Drawing.Size(0, 2);
            this.label8.MinimumSize = new System.Drawing.Size(0, 2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(0, 2);
            this.label8.TabIndex = 14;
            // 
            // downSampleToChooser
            // 
            this.downSampleToChooser.Location = new System.Drawing.Point(86, 34);
            this.downSampleToChooser.Name = "downSampleToChooser";
            this.downSampleToChooser.Size = new System.Drawing.Size(72, 20);
            this.downSampleToChooser.TabIndex = 11;
            this.downSampleToChooser.Minimum = 2;
            this.downSampleToChooser.ValueChanged += new System.EventHandler(this.downSampleToChooser_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Downsample";
            // 
            // toPointChooser
            // 
            this.toPointChooser.Location = new System.Drawing.Point(206, 13);
            this.toPointChooser.Name = "toPointChooser";
            this.fromPointChooser.Minimum = 2;
            this.toPointChooser.Size = new System.Drawing.Size(72, 20);
            this.toPointChooser.TabIndex = 9;
            this.toPointChooser.ValueChanged += new System.EventHandler(this.toPointChooser_ValueChanged);
            // 
            // fromPointChooser
            // 
            this.fromPointChooser.Location = new System.Drawing.Point(86, 13);
            this.fromPointChooser.Name = "fromPointChooser";
            this.fromPointChooser.Minimum = 1;
            this.fromPointChooser.Size = new System.Drawing.Size(72, 20);
            this.fromPointChooser.TabIndex = 7;
            this.fromPointChooser.ValueChanged += new System.EventHandler(this.fromPointChooser_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "From";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(165, 13);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(33, 13);
            this.label10.TabIndex = 8;
            this.label10.Text = "To";
            // 
            // MoveBankTrajectoryChooser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 556);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MoveBankTrajectoryChooser";
            this.Text = "Choose Trajectory";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trajectoryTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.downSampleToChooser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fromPointChooser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toPointChooser)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private TrajectoryGeo trajectoryGMap;
        private System.Windows.Forms.DataGridView trajectoryTable;
        private System.Windows.Forms.DataGridViewTextBoxColumn trajectoryIdColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn trajectoryPointsColumn;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.NumericUpDown toPointChooser;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown downSampleToChooser;
        private System.Windows.Forms.NumericUpDown fromPointChooser;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
    }
}