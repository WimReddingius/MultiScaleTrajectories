using AlgorithmVisualization.View.Util.Components;

namespace AlgorithmVisualization.View.Explore.Components.Stats
{
    partial class StatOverview<TIn, TOut>
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
            this.resizableTableLayoutPanel1 = new AlgorithmVisualization.View.Util.Components.ResizableTableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.inputStatsTable = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.runStatsTable = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.algorithmStatsTable = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.outputStatsTable = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.resizableTableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputStatsTable)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.runStatsTable)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.algorithmStatsTable)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outputStatsTable)).BeginInit();
            this.SuspendLayout();
            // 
            // resizableTableLayoutPanel1
            // 
            this.resizableTableLayoutPanel1.ColumnCount = 1;
            this.resizableTableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.resizableTableLayoutPanel1.Controls.Add(this.panel4, 0, 3);
            this.resizableTableLayoutPanel1.Controls.Add(this.panel3, 0, 2);
            this.resizableTableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.resizableTableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.resizableTableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resizableTableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.resizableTableLayoutPanel1.Name = "resizableTableLayoutPanel1";
            this.resizableTableLayoutPanel1.RowCount = 4;
            this.resizableTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.resizableTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.resizableTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.resizableTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.resizableTableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.resizableTableLayoutPanel1.Size = new System.Drawing.Size(433, 482);
            this.resizableTableLayoutPanel1.SplitterSize = 6;
            this.resizableTableLayoutPanel1.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.inputStatsTable);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 243);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(427, 114);
            this.panel3.TabIndex = 4;
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
            this.inputStatsTable.Location = new System.Drawing.Point(0, 22);
            this.inputStatsTable.Name = "inputStatsTable";
            this.inputStatsTable.ReadOnly = true;
            this.inputStatsTable.RowHeadersVisible = false;
            this.inputStatsTable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.inputStatsTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.inputStatsTable.Size = new System.Drawing.Size(427, 92);
            this.inputStatsTable.TabIndex = 13;
            this.inputStatsTable.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.statsTable_DataBindingComplete);
            this.inputStatsTable.Leave += new System.EventHandler(this.statsTable_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Input Statistics";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.runStatsTable);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(427, 114);
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
            this.runStatsTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.runStatsTable.Size = new System.Drawing.Size(427, 98);
            this.runStatsTable.TabIndex = 13;
            this.runStatsTable.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.statsTable_DataBindingComplete);
            this.runStatsTable.Leave += new System.EventHandler(this.statsTable_Leave);
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
            // panel2
            // 
            this.panel2.Controls.Add(this.algorithmStatsTable);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 123);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(427, 114);
            this.panel2.TabIndex = 3;
            // 
            // algorithmStatsTable
            // 
            this.algorithmStatsTable.AllowUserToAddRows = false;
            this.algorithmStatsTable.AllowUserToDeleteRows = false;
            this.algorithmStatsTable.AllowUserToResizeRows = false;
            this.algorithmStatsTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.algorithmStatsTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.algorithmStatsTable.BackgroundColor = System.Drawing.SystemColors.Control;
            this.algorithmStatsTable.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.algorithmStatsTable.Location = new System.Drawing.Point(0, 16);
            this.algorithmStatsTable.Name = "algorithmStatsTable";
            this.algorithmStatsTable.ReadOnly = true;
            this.algorithmStatsTable.RowHeadersVisible = false;
            this.algorithmStatsTable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.algorithmStatsTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.algorithmStatsTable.Size = new System.Drawing.Size(427, 98);
            this.algorithmStatsTable.TabIndex = 13;
            this.algorithmStatsTable.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.statsTable_DataBindingComplete);
            this.algorithmStatsTable.Leave += new System.EventHandler(this.statsTable_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Algorithm Statistics";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.outputStatsTable);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 363);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(427, 116);
            this.panel4.TabIndex = 5;
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
            this.outputStatsTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.outputStatsTable.Size = new System.Drawing.Size(427, 94);
            this.outputStatsTable.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Output Statistics";
            // 
            // Statistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.Controls.Add(this.resizableTableLayoutPanel1);
            this.Name = "Statistics";
            this.Size = new System.Drawing.Size(433, 482);
            this.resizableTableLayoutPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputStatsTable)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.runStatsTable)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.algorithmStatsTable)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outputStatsTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView runStatsTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView algorithmStatsTable;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView inputStatsTable;
        private System.Windows.Forms.Label label3;
        private ResizableTableLayoutPanel resizableTableLayoutPanel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DataGridView outputStatsTable;
        private System.Windows.Forms.Label label4;
    }
}
