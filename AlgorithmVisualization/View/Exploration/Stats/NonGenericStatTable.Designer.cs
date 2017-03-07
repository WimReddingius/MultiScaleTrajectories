namespace AlgorithmVisualization.View.Exploration.Stats
{
    partial class NonGenericStatTable
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.outputStatsTable = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.inputStatsTable = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.runStatsTable = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outputStatsTable)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputStatsTable)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.runStatsTable)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(421, 452);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.outputStatsTable);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 303);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(415, 146);
            this.panel3.TabIndex = 4;
            // 
            // outputStatsTable
            // 
            this.outputStatsTable.AllowUserToAddRows = false;
            this.outputStatsTable.AllowUserToDeleteRows = false;
            this.outputStatsTable.AllowUserToResizeRows = false;
            this.outputStatsTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outputStatsTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.outputStatsTable.BackgroundColor = System.Drawing.SystemColors.Control;
            this.outputStatsTable.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.outputStatsTable.Location = new System.Drawing.Point(0, 22);
            this.outputStatsTable.Name = "outputStatsTable";
            this.outputStatsTable.ReadOnly = true;
            this.outputStatsTable.RowHeadersVisible = false;
            this.outputStatsTable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.outputStatsTable.Size = new System.Drawing.Size(415, 124);
            this.outputStatsTable.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Output Statistics";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.inputStatsTable);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 153);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(415, 144);
            this.panel2.TabIndex = 3;
            // 
            // inputStatsTable
            // 
            this.inputStatsTable.AllowUserToAddRows = false;
            this.inputStatsTable.AllowUserToDeleteRows = false;
            this.inputStatsTable.AllowUserToResizeRows = false;
            this.inputStatsTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inputStatsTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.inputStatsTable.BackgroundColor = System.Drawing.SystemColors.Control;
            this.inputStatsTable.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.inputStatsTable.Location = new System.Drawing.Point(0, 16);
            this.inputStatsTable.Name = "inputStatsTable";
            this.inputStatsTable.ReadOnly = true;
            this.inputStatsTable.RowHeadersVisible = false;
            this.inputStatsTable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.inputStatsTable.Size = new System.Drawing.Size(415, 128);
            this.inputStatsTable.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Input Statistics";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.runStatsTable);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(415, 144);
            this.panel1.TabIndex = 2;
            // 
            // runStatsTable
            // 
            this.runStatsTable.AllowUserToAddRows = false;
            this.runStatsTable.AllowUserToDeleteRows = false;
            this.runStatsTable.AllowUserToResizeRows = false;
            this.runStatsTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.runStatsTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.runStatsTable.BackgroundColor = System.Drawing.SystemColors.Control;
            this.runStatsTable.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.runStatsTable.Location = new System.Drawing.Point(0, 16);
            this.runStatsTable.Name = "runStatsTable";
            this.runStatsTable.ReadOnly = true;
            this.runStatsTable.RowHeadersVisible = false;
            this.runStatsTable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.runStatsTable.Size = new System.Drawing.Size(415, 128);
            this.runStatsTable.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Run Statistics";
            // 
            // NonGenericStatTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "NonGenericStatTable";
            this.Size = new System.Drawing.Size(421, 452);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outputStatsTable)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputStatsTable)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.runStatsTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView outputStatsTable;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView inputStatsTable;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView runStatsTable;
    }
}
