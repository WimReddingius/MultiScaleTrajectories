using AlgorithmVisualization.Algorithm;

namespace AlgorithmVisualization.View
{
    partial class AlgorithmView<TIn, TOut> where TIn : Input, new() where TOut : Output, new()
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.runTabPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.addWorkloadRunButton = new System.Windows.Forms.Button();
            this.resetWorkloadButton = new System.Windows.Forms.Button();
            this.computeWorkloadButton = new System.Windows.Forms.Button();
            this.removeWorkloadRunButton = new System.Windows.Forms.Button();
            this.workloadTable = new System.Windows.Forms.DataGridView();
            this.workloadTableRunColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.workloadTableInputColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.workloadTableAlgoColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.inputTabPage = new System.Windows.Forms.TabPage();
            this.inputOptionsPanel = new System.Windows.Forms.Panel();
            this.inputSplitContainer = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.addInputButton = new System.Windows.Forms.Button();
            this.removeInputButton = new System.Windows.Forms.Button();
            this.inputComboBox = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.saveInputButton = new System.Windows.Forms.Button();
            this.clearInputButton = new System.Windows.Forms.Button();
            this.openInputButton = new System.Windows.Forms.Button();
            this.exploreTabPage = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.unsplitButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.splitHorizontallyButton = new System.Windows.Forms.Button();
            this.SplitVerticallyButton = new System.Windows.Forms.Button();
            this.openInputDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveInputDialog = new System.Windows.Forms.SaveFileDialog();
            this.tabControl.SuspendLayout();
            this.runTabPage.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.workloadTable)).BeginInit();
            this.inputTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputSplitContainer)).BeginInit();
            this.inputSplitContainer.Panel1.SuspendLayout();
            this.inputSplitContainer.Panel2.SuspendLayout();
            this.inputSplitContainer.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.exploreTabPage.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.runTabPage);
            this.tabControl.Controls.Add(this.inputTabPage);
            this.tabControl.Controls.Add(this.exploreTabPage);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(270, 523);
            this.tabControl.TabIndex = 5;
            // 
            // runTabPage
            // 
            this.runTabPage.Controls.Add(this.tableLayoutPanel1);
            this.runTabPage.Controls.Add(this.workloadTable);
            this.runTabPage.Location = new System.Drawing.Point(4, 22);
            this.runTabPage.Name = "runTabPage";
            this.runTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.runTabPage.Size = new System.Drawing.Size(262, 497);
            this.runTabPage.TabIndex = 2;
            this.runTabPage.Text = "Workload";
            this.runTabPage.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.addWorkloadRunButton, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.resetWorkloadButton, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.computeWorkloadButton, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.removeWorkloadRunButton, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 428);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(256, 66);
            this.tableLayoutPanel1.TabIndex = 23;
            // 
            // addWorkloadRunButton
            // 
            this.addWorkloadRunButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addWorkloadRunButton.Location = new System.Drawing.Point(0, 3);
            this.addWorkloadRunButton.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.addWorkloadRunButton.Name = "addWorkloadRunButton";
            this.addWorkloadRunButton.Size = new System.Drawing.Size(125, 27);
            this.addWorkloadRunButton.TabIndex = 19;
            this.addWorkloadRunButton.Text = "Add";
            this.addWorkloadRunButton.UseVisualStyleBackColor = true;
            this.addWorkloadRunButton.Click += new System.EventHandler(this.addWorkloadRunButton_Click);
            // 
            // resetWorkloadButton
            // 
            this.resetWorkloadButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resetWorkloadButton.Enabled = false;
            this.resetWorkloadButton.Location = new System.Drawing.Point(131, 36);
            this.resetWorkloadButton.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.resetWorkloadButton.Name = "resetWorkloadButton";
            this.resetWorkloadButton.Size = new System.Drawing.Size(125, 27);
            this.resetWorkloadButton.TabIndex = 21;
            this.resetWorkloadButton.Text = "Reset";
            this.resetWorkloadButton.UseVisualStyleBackColor = true;
            this.resetWorkloadButton.Click += new System.EventHandler(this.resetWorkloadButton_Click);
            // 
            // computeWorkloadButton
            // 
            this.computeWorkloadButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.computeWorkloadButton.Enabled = false;
            this.computeWorkloadButton.Location = new System.Drawing.Point(0, 36);
            this.computeWorkloadButton.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.computeWorkloadButton.Name = "computeWorkloadButton";
            this.computeWorkloadButton.Size = new System.Drawing.Size(125, 27);
            this.computeWorkloadButton.TabIndex = 6;
            this.computeWorkloadButton.Text = "Compute";
            this.computeWorkloadButton.UseVisualStyleBackColor = true;
            this.computeWorkloadButton.Click += new System.EventHandler(this.computeWorkloadButton_Click);
            // 
            // removeWorkloadRunButton
            // 
            this.removeWorkloadRunButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.removeWorkloadRunButton.Location = new System.Drawing.Point(131, 3);
            this.removeWorkloadRunButton.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.removeWorkloadRunButton.Name = "removeWorkloadRunButton";
            this.removeWorkloadRunButton.Size = new System.Drawing.Size(125, 27);
            this.removeWorkloadRunButton.TabIndex = 22;
            this.removeWorkloadRunButton.Text = "Remove";
            this.removeWorkloadRunButton.UseVisualStyleBackColor = true;
            this.removeWorkloadRunButton.Click += new System.EventHandler(this.removeWorkloadRunButton_Click);
            // 
            // workloadTable
            // 
            this.workloadTable.AllowUserToAddRows = false;
            this.workloadTable.AllowUserToResizeRows = false;
            this.workloadTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.workloadTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.workloadTable.BackgroundColor = System.Drawing.SystemColors.Control;
            this.workloadTable.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.workloadTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.workloadTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.workloadTableRunColumn,
            this.workloadTableInputColumn,
            this.workloadTableAlgoColumn});
            this.workloadTable.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.workloadTable.Location = new System.Drawing.Point(3, 3);
            this.workloadTable.Name = "workloadTable";
            this.workloadTable.RowHeadersVisible = false;
            this.workloadTable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.workloadTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.workloadTable.Size = new System.Drawing.Size(256, 419);
            this.workloadTable.TabIndex = 18;
            this.workloadTable.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.workloadTable_CellEndEdit);
            this.workloadTable.SelectionChanged += new System.EventHandler(this.workloadTable_SelectionChanged);
            // 
            // workloadTableRunColumn
            // 
            this.workloadTableRunColumn.FillWeight = 50F;
            this.workloadTableRunColumn.HeaderText = "Run";
            this.workloadTableRunColumn.Name = "workloadTableRunColumn";
            this.workloadTableRunColumn.ReadOnly = true;
            this.workloadTableRunColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.workloadTableRunColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // workloadTableInputColumn
            // 
            this.workloadTableInputColumn.AutoComplete = false;
            dataGridViewCellStyle3.NullValue = null;
            this.workloadTableInputColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.workloadTableInputColumn.FillWeight = 50F;
            this.workloadTableInputColumn.HeaderText = "Input";
            this.workloadTableInputColumn.Name = "workloadTableInputColumn";
            this.workloadTableInputColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.workloadTableInputColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // workloadTableAlgoColumn
            // 
            this.workloadTableAlgoColumn.AutoComplete = false;
            this.workloadTableAlgoColumn.HeaderText = "Algorithm";
            this.workloadTableAlgoColumn.Name = "workloadTableAlgoColumn";
            this.workloadTableAlgoColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.workloadTableAlgoColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // inputTabPage
            // 
            this.inputTabPage.Controls.Add(this.inputOptionsPanel);
            this.inputTabPage.Controls.Add(this.inputSplitContainer);
            this.inputTabPage.Location = new System.Drawing.Point(4, 22);
            this.inputTabPage.Name = "inputTabPage";
            this.inputTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.inputTabPage.Size = new System.Drawing.Size(262, 497);
            this.inputTabPage.TabIndex = 0;
            this.inputTabPage.Text = "Input";
            this.inputTabPage.UseVisualStyleBackColor = true;
            // 
            // inputOptionsPanel
            // 
            this.inputOptionsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inputOptionsPanel.Location = new System.Drawing.Point(6, 112);
            this.inputOptionsPanel.Name = "inputOptionsPanel";
            this.inputOptionsPanel.Size = new System.Drawing.Size(250, 382);
            this.inputOptionsPanel.TabIndex = 14;
            // 
            // inputSplitContainer
            // 
            this.inputSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.inputSplitContainer.IsSplitterFixed = true;
            this.inputSplitContainer.Location = new System.Drawing.Point(3, 3);
            this.inputSplitContainer.Name = "inputSplitContainer";
            this.inputSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // inputSplitContainer.Panel1
            // 
            this.inputSplitContainer.Panel1.Controls.Add(this.tableLayoutPanel2);
            this.inputSplitContainer.Panel1.Controls.Add(this.inputComboBox);
            // 
            // inputSplitContainer.Panel2
            // 
            this.inputSplitContainer.Panel2.Controls.Add(this.tableLayoutPanel4);
            this.inputSplitContainer.Size = new System.Drawing.Size(256, 491);
            this.inputSplitContainer.SplitterDistance = 62;
            this.inputSplitContainer.TabIndex = 18;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.addInputButton, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.removeInputButton, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 31);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(256, 31);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // addInputButton
            // 
            this.addInputButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addInputButton.Location = new System.Drawing.Point(3, 3);
            this.addInputButton.Name = "addInputButton";
            this.addInputButton.Size = new System.Drawing.Size(122, 25);
            this.addInputButton.TabIndex = 16;
            this.addInputButton.Text = "Add";
            this.addInputButton.UseVisualStyleBackColor = true;
            this.addInputButton.Click += new System.EventHandler(this.addInputButton_Click);
            // 
            // removeInputButton
            // 
            this.removeInputButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.removeInputButton.Location = new System.Drawing.Point(131, 3);
            this.removeInputButton.Name = "removeInputButton";
            this.removeInputButton.Size = new System.Drawing.Size(122, 25);
            this.removeInputButton.TabIndex = 17;
            this.removeInputButton.Text = "Remove";
            this.removeInputButton.UseVisualStyleBackColor = true;
            this.removeInputButton.Click += new System.EventHandler(this.removeInputButton_Click);
            // 
            // inputComboBox
            // 
            this.inputComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inputComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.inputComboBox.Location = new System.Drawing.Point(3, 3);
            this.inputComboBox.Name = "inputComboBox";
            this.inputComboBox.Size = new System.Drawing.Size(250, 21);
            this.inputComboBox.TabIndex = 15;
            this.inputComboBox.SelectedIndexChanged += new System.EventHandler(this.inputComboBox_SelectedIndexChanged);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel4.Controls.Add(this.saveInputButton, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.clearInputButton, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.openInputButton, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(256, 32);
            this.tableLayoutPanel4.TabIndex = 14;
            // 
            // saveInputButton
            // 
            this.saveInputButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saveInputButton.Location = new System.Drawing.Point(173, 3);
            this.saveInputButton.Name = "saveInputButton";
            this.saveInputButton.Size = new System.Drawing.Size(80, 26);
            this.saveInputButton.TabIndex = 0;
            this.saveInputButton.Text = "Save";
            this.saveInputButton.UseVisualStyleBackColor = true;
            this.saveInputButton.Click += new System.EventHandler(this.saveInputButton_Click);
            // 
            // clearInputButton
            // 
            this.clearInputButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clearInputButton.Location = new System.Drawing.Point(88, 3);
            this.clearInputButton.Name = "clearInputButton";
            this.clearInputButton.Size = new System.Drawing.Size(79, 26);
            this.clearInputButton.TabIndex = 13;
            this.clearInputButton.Text = "Clear";
            this.clearInputButton.UseVisualStyleBackColor = true;
            this.clearInputButton.Click += new System.EventHandler(this.clearInputButton_Click);
            // 
            // openInputButton
            // 
            this.openInputButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.openInputButton.Location = new System.Drawing.Point(3, 3);
            this.openInputButton.Name = "openInputButton";
            this.openInputButton.Size = new System.Drawing.Size(79, 26);
            this.openInputButton.TabIndex = 1;
            this.openInputButton.Text = "Open";
            this.openInputButton.UseVisualStyleBackColor = true;
            this.openInputButton.Click += new System.EventHandler(this.openInputButton_Click);
            // 
            // exploreTabPage
            // 
            this.exploreTabPage.Controls.Add(this.label1);
            this.exploreTabPage.Controls.Add(this.tableLayoutPanel5);
            this.exploreTabPage.Controls.Add(this.tableLayoutPanel3);
            this.exploreTabPage.Location = new System.Drawing.Point(4, 22);
            this.exploreTabPage.Name = "exploreTabPage";
            this.exploreTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.exploreTabPage.Size = new System.Drawing.Size(262, 497);
            this.exploreTabPage.TabIndex = 3;
            this.exploreTabPage.Text = "Explore";
            this.exploreTabPage.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Splitting";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.unsplitButton, 0, 0);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(1, 73);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(261, 39);
            this.tableLayoutPanel5.TabIndex = 3;
            // 
            // unsplitButton
            // 
            this.unsplitButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.unsplitButton.Location = new System.Drawing.Point(3, 3);
            this.unsplitButton.Name = "unsplitButton";
            this.unsplitButton.Size = new System.Drawing.Size(255, 33);
            this.unsplitButton.TabIndex = 0;
            this.unsplitButton.Text = "UnSplit";
            this.unsplitButton.UseVisualStyleBackColor = true;
            this.unsplitButton.Click += new System.EventHandler(this.unsplitButton_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.splitHorizontallyButton, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.SplitVerticallyButton, 1, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 28);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(261, 39);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // splitHorizontallyButton
            // 
            this.splitHorizontallyButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitHorizontallyButton.Location = new System.Drawing.Point(3, 3);
            this.splitHorizontallyButton.Name = "splitHorizontallyButton";
            this.splitHorizontallyButton.Size = new System.Drawing.Size(124, 33);
            this.splitHorizontallyButton.TabIndex = 0;
            this.splitHorizontallyButton.Text = "Horizontal";
            this.splitHorizontallyButton.UseVisualStyleBackColor = true;
            this.splitHorizontallyButton.Click += new System.EventHandler(this.splitHorizontallyButton_Click);
            // 
            // SplitVerticallyButton
            // 
            this.SplitVerticallyButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitVerticallyButton.Location = new System.Drawing.Point(133, 3);
            this.SplitVerticallyButton.Name = "SplitVerticallyButton";
            this.SplitVerticallyButton.Size = new System.Drawing.Size(125, 33);
            this.SplitVerticallyButton.TabIndex = 1;
            this.SplitVerticallyButton.Text = "Vertical";
            this.SplitVerticallyButton.UseVisualStyleBackColor = true;
            this.SplitVerticallyButton.Click += new System.EventHandler(this.SplitVerticallyButton_Click);
            // 
            // openInputDialog
            // 
            this.openInputDialog.DefaultExt = "json";
            this.openInputDialog.FileName = "openFileDialog1";
            this.openInputDialog.Filter = "Input Files|*.json; *.csv";
            this.openInputDialog.Title = "Open Configuration";
            // 
            // saveInputDialog
            // 
            this.saveInputDialog.DefaultExt = "json";
            this.saveInputDialog.Filter = "JSON(*.json)|*json";
            this.saveInputDialog.Title = "Save Configuration";
            // 
            // AlgorithmView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Name = "AlgorithmView";
            this.Size = new System.Drawing.Size(270, 523);
            this.tabControl.ResumeLayout(false);
            this.runTabPage.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.workloadTable)).EndInit();
            this.inputTabPage.ResumeLayout(false);
            this.inputSplitContainer.Panel1.ResumeLayout(false);
            this.inputSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.inputSplitContainer)).EndInit();
            this.inputSplitContainer.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.exploreTabPage.ResumeLayout(false);
            this.exploreTabPage.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage inputTabPage;
        private System.Windows.Forms.Panel inputOptionsPanel;
        private System.Windows.Forms.Button openInputButton;
        private System.Windows.Forms.Button clearInputButton;
        private System.Windows.Forms.Button saveInputButton;
        private System.Windows.Forms.TabPage runTabPage;
        private System.Windows.Forms.Button computeWorkloadButton;
        private System.Windows.Forms.OpenFileDialog openInputDialog;
        private System.Windows.Forms.SaveFileDialog saveInputDialog;
        private System.Windows.Forms.DataGridView workloadTable;
        private System.Windows.Forms.Button addWorkloadRunButton;
        private System.Windows.Forms.ComboBox inputComboBox;
        private System.Windows.Forms.Button removeInputButton;
        private System.Windows.Forms.Button addInputButton;
        private System.Windows.Forms.SplitContainer inputSplitContainer;
        private System.Windows.Forms.Button resetWorkloadButton;
        private System.Windows.Forms.Button removeWorkloadRunButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn workloadTableRunColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn workloadTableInputColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn workloadTableAlgoColumn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TabPage exploreTabPage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Button unsplitButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button splitHorizontallyButton;
        private System.Windows.Forms.Button SplitVerticallyButton;
    }
}
