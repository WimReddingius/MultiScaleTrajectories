using AlgorithmVisualization.Algorithm;

namespace AlgorithmVisualization.View
{
    partial class AlgorithmViewConcrete<TIn, TOut> where TIn : Input, new() where TOut : Output, new()
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.openInputDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveInputDialog = new System.Windows.Forms.SaveFileDialog();
            this.inputTabPage = new System.Windows.Forms.TabPage();
            this.inputOptionsPanel = new System.Windows.Forms.Panel();
            this.inputSplitContainer = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.addInputButton = new System.Windows.Forms.Button();
            this.removeInputButton = new System.Windows.Forms.Button();
            this.inputComboBox = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.importInputButton = new System.Windows.Forms.Button();
            this.saveInputButton = new System.Windows.Forms.Button();
            this.openInputButton = new System.Windows.Forms.Button();
            this.clearInputButton = new System.Windows.Forms.Button();
            this.runTabPage = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.saveRunButton = new System.Windows.Forms.Button();
            this.openRunButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.addRunButton = new System.Windows.Forms.Button();
            this.resetWorkloadButton = new System.Windows.Forms.Button();
            this.computeWorkloadButton = new System.Windows.Forms.Button();
            this.removeRunButton = new System.Windows.Forms.Button();
            this.workloadTable = new System.Windows.Forms.DataGridView();
            this.workloadTableRunColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.workloadTableInputColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.workloadTableAlgoColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.workloadTableAmountColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.algorithmComboBox = new System.Windows.Forms.ComboBox();
            this.removeAlgorithmButton = new System.Windows.Forms.Button();
            this.algorithmOptionsPanel = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.algorithmFactoryComboBox = new System.Windows.Forms.ComboBox();
            this.addAlgorithmButton = new System.Windows.Forms.Button();
            this.openRunDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveRunDialog = new System.Windows.Forms.SaveFileDialog();
            this.importRunDialog = new System.Windows.Forms.OpenFileDialog();
            this.inputTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputSplitContainer)).BeginInit();
            this.inputSplitContainer.Panel1.SuspendLayout();
            this.inputSplitContainer.Panel2.SuspendLayout();
            this.inputSplitContainer.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.runTabPage.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.workloadTable)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openInputDialog
            // 
            this.openInputDialog.DefaultExt = "input";
            this.openInputDialog.FileName = "openFileDialog1";
            this.openInputDialog.Filter = "Input Files|*.input; *.csv";
            this.openInputDialog.Title = "Open Input";
            // 
            // saveInputDialog
            // 
            this.saveInputDialog.DefaultExt = "input";
            this.saveInputDialog.Filter = "Input Files(*.input)|*input";
            this.saveInputDialog.Title = "Save Input";
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
            this.removeInputButton.Enabled = false;
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
            this.tableLayoutPanel4.ColumnCount = 4;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.Controls.Add(this.importInputButton, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.saveInputButton, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.openInputButton, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.clearInputButton, 2, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(256, 32);
            this.tableLayoutPanel4.TabIndex = 14;
            // 
            // importInputButton
            // 
            this.importInputButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.importInputButton.Location = new System.Drawing.Point(195, 3);
            this.importInputButton.Name = "importInputButton";
            this.importInputButton.Size = new System.Drawing.Size(58, 26);
            this.importInputButton.TabIndex = 14;
            this.importInputButton.Text = "Import";
            this.importInputButton.UseVisualStyleBackColor = true;
            this.importInputButton.Click += new System.EventHandler(this.importInputButton_Click);
            // 
            // saveInputButton
            // 
            this.saveInputButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saveInputButton.Enabled = false;
            this.saveInputButton.Location = new System.Drawing.Point(67, 3);
            this.saveInputButton.Name = "saveInputButton";
            this.saveInputButton.Size = new System.Drawing.Size(58, 26);
            this.saveInputButton.TabIndex = 0;
            this.saveInputButton.Text = "Save";
            this.saveInputButton.UseVisualStyleBackColor = true;
            this.saveInputButton.Click += new System.EventHandler(this.saveInputButton_Click);
            // 
            // openInputButton
            // 
            this.openInputButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.openInputButton.Location = new System.Drawing.Point(3, 3);
            this.openInputButton.Name = "openInputButton";
            this.openInputButton.Size = new System.Drawing.Size(58, 26);
            this.openInputButton.TabIndex = 1;
            this.openInputButton.Text = "Open";
            this.openInputButton.UseVisualStyleBackColor = true;
            this.openInputButton.Click += new System.EventHandler(this.openInputButton_Click);
            // 
            // clearInputButton
            // 
            this.clearInputButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clearInputButton.Enabled = false;
            this.clearInputButton.Location = new System.Drawing.Point(131, 3);
            this.clearInputButton.Name = "clearInputButton";
            this.clearInputButton.Size = new System.Drawing.Size(58, 26);
            this.clearInputButton.TabIndex = 13;
            this.clearInputButton.Text = "Clear";
            this.clearInputButton.UseVisualStyleBackColor = true;
            this.clearInputButton.Click += new System.EventHandler(this.clearInputButton_Click);
            // 
            // runTabPage
            // 
            this.runTabPage.Controls.Add(this.tableLayoutPanel3);
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
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel3.Controls.Add(this.saveRunButton, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.openRunButton, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(256, 34);
            this.tableLayoutPanel3.TabIndex = 24;
            // 
            // saveRunButton
            // 
            this.saveRunButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saveRunButton.Location = new System.Drawing.Point(131, 3);
            this.saveRunButton.Name = "saveRunButton";
            this.saveRunButton.Size = new System.Drawing.Size(122, 28);
            this.saveRunButton.TabIndex = 2;
            this.saveRunButton.Text = "Save";
            this.saveRunButton.UseVisualStyleBackColor = true;
            this.saveRunButton.Click += new System.EventHandler(this.saveRunButton_Click);
            // 
            // openRunButton
            // 
            this.openRunButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.openRunButton.Location = new System.Drawing.Point(3, 3);
            this.openRunButton.Name = "openRunButton";
            this.openRunButton.Size = new System.Drawing.Size(122, 28);
            this.openRunButton.TabIndex = 0;
            this.openRunButton.Text = "Open";
            this.openRunButton.UseVisualStyleBackColor = true;
            this.openRunButton.Click += new System.EventHandler(this.openRunButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.addRunButton, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.resetWorkloadButton, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.computeWorkloadButton, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.removeRunButton, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 428);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(256, 66);
            this.tableLayoutPanel1.TabIndex = 23;
            // 
            // addRunButton
            // 
            this.addRunButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addRunButton.Location = new System.Drawing.Point(0, 3);
            this.addRunButton.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.addRunButton.Name = "addRunButton";
            this.addRunButton.Size = new System.Drawing.Size(125, 27);
            this.addRunButton.TabIndex = 19;
            this.addRunButton.Text = "Add";
            this.addRunButton.UseVisualStyleBackColor = true;
            this.addRunButton.Click += new System.EventHandler(this.addRunButton_Click);
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
            // removeRunButton
            // 
            this.removeRunButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.removeRunButton.Enabled = false;
            this.removeRunButton.Location = new System.Drawing.Point(131, 3);
            this.removeRunButton.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.removeRunButton.Name = "removeRunButton";
            this.removeRunButton.Size = new System.Drawing.Size(125, 27);
            this.removeRunButton.TabIndex = 22;
            this.removeRunButton.Text = "Remove";
            this.removeRunButton.UseVisualStyleBackColor = true;
            this.removeRunButton.Click += new System.EventHandler(this.removeRunButton_Click);
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
            this.workloadTableAlgoColumn,
            this.workloadTableAmountColumn});
            this.workloadTable.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.workloadTable.Location = new System.Drawing.Point(3, 43);
            this.workloadTable.Name = "workloadTable";
            this.workloadTable.RowHeadersVisible = false;
            this.workloadTable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.workloadTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.workloadTable.Size = new System.Drawing.Size(256, 379);
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
            dataGridViewCellStyle1.NullValue = null;
            this.workloadTableInputColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.workloadTableInputColumn.FillWeight = 76F;
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
            // workloadTableAmountColumn
            // 
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = null;
            this.workloadTableAmountColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.workloadTableAmountColumn.FillWeight = 50F;
            this.workloadTableAmountColumn.HeaderText = "Amount";
            this.workloadTableAmountColumn.Name = "workloadTableAmountColumn";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.runTabPage);
            this.tabControl.Controls.Add(this.inputTabPage);
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(270, 523);
            this.tabControl.TabIndex = 5;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer2);
            this.tabPage1.Controls.Add(this.algorithmOptionsPanel);
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(262, 497);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "Algorithm";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(1, 31);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.algorithmComboBox);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.removeAlgorithmButton);
            this.splitContainer2.Size = new System.Drawing.Size(261, 22);
            this.splitContainer2.SplitterDistance = 180;
            this.splitContainer2.TabIndex = 4;
            // 
            // algorithmComboBox
            // 
            this.algorithmComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.algorithmComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.algorithmComboBox.FormattingEnabled = true;
            this.algorithmComboBox.Location = new System.Drawing.Point(0, 0);
            this.algorithmComboBox.Name = "algorithmComboBox";
            this.algorithmComboBox.Size = new System.Drawing.Size(180, 21);
            this.algorithmComboBox.TabIndex = 1;
            this.algorithmComboBox.SelectedIndexChanged += new System.EventHandler(this.algorithmComboBox_SelectedIndexChanged);
            // 
            // removeAlgorithmButton
            // 
            this.removeAlgorithmButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.removeAlgorithmButton.Enabled = false;
            this.removeAlgorithmButton.Location = new System.Drawing.Point(0, 0);
            this.removeAlgorithmButton.Name = "removeAlgorithmButton";
            this.removeAlgorithmButton.Size = new System.Drawing.Size(77, 22);
            this.removeAlgorithmButton.TabIndex = 2;
            this.removeAlgorithmButton.Text = "Remove";
            this.removeAlgorithmButton.UseVisualStyleBackColor = true;
            this.removeAlgorithmButton.Click += new System.EventHandler(this.removeAlgorithmButton_Click);
            // 
            // algorithmOptionsPanel
            // 
            this.algorithmOptionsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.algorithmOptionsPanel.Location = new System.Drawing.Point(1, 59);
            this.algorithmOptionsPanel.Name = "algorithmOptionsPanel";
            this.algorithmOptionsPanel.Size = new System.Drawing.Size(261, 438);
            this.algorithmOptionsPanel.TabIndex = 4;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(1, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.algorithmFactoryComboBox);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.addAlgorithmButton);
            this.splitContainer1.Size = new System.Drawing.Size(261, 22);
            this.splitContainer1.SplitterDistance = 180;
            this.splitContainer1.TabIndex = 3;
            // 
            // algorithmFactoryComboBox
            // 
            this.algorithmFactoryComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.algorithmFactoryComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.algorithmFactoryComboBox.FormattingEnabled = true;
            this.algorithmFactoryComboBox.Location = new System.Drawing.Point(0, 0);
            this.algorithmFactoryComboBox.Name = "algorithmFactoryComboBox";
            this.algorithmFactoryComboBox.Size = new System.Drawing.Size(180, 21);
            this.algorithmFactoryComboBox.TabIndex = 0;
            // 
            // addAlgorithmButton
            // 
            this.addAlgorithmButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addAlgorithmButton.Location = new System.Drawing.Point(0, 0);
            this.addAlgorithmButton.Name = "addAlgorithmButton";
            this.addAlgorithmButton.Size = new System.Drawing.Size(77, 22);
            this.addAlgorithmButton.TabIndex = 2;
            this.addAlgorithmButton.Text = "Add";
            this.addAlgorithmButton.UseVisualStyleBackColor = true;
            this.addAlgorithmButton.Click += new System.EventHandler(this.addAlgorithmButton_Click);
            // 
            // openRunDialog
            // 
            this.openRunDialog.DefaultExt = "run";
            this.openRunDialog.Filter = "Run Files(*.run)|*run";
            this.openRunDialog.Title = "Open Run";
            // 
            // saveRunDialog
            // 
            this.saveRunDialog.DefaultExt = "run";
            this.saveRunDialog.Filter = "Run Files(*.run)|*run";
            this.saveRunDialog.Title = "Save Run";
            // 
            // importRunDialog
            // 
            this.importRunDialog.Title = "Import Run";
            // 
            // AlgorithmViewConcrete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Name = "AlgorithmViewConcrete";
            this.Size = new System.Drawing.Size(270, 523);
            this.inputTabPage.ResumeLayout(false);
            this.inputSplitContainer.Panel1.ResumeLayout(false);
            this.inputSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.inputSplitContainer)).EndInit();
            this.inputSplitContainer.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.runTabPage.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.workloadTable)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openInputDialog;
        private System.Windows.Forms.SaveFileDialog saveInputDialog;
        private System.Windows.Forms.TabPage inputTabPage;
        private System.Windows.Forms.Panel inputOptionsPanel;
        private System.Windows.Forms.SplitContainer inputSplitContainer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button addInputButton;
        private System.Windows.Forms.Button removeInputButton;
        private System.Windows.Forms.ComboBox inputComboBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button saveInputButton;
        private System.Windows.Forms.Button clearInputButton;
        private System.Windows.Forms.Button openInputButton;
        private System.Windows.Forms.TabPage runTabPage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button addRunButton;
        private System.Windows.Forms.Button resetWorkloadButton;
        private System.Windows.Forms.Button computeWorkloadButton;
        private System.Windows.Forms.Button removeRunButton;
        private System.Windows.Forms.DataGridView workloadTable;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.DataGridViewTextBoxColumn workloadTableRunColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn workloadTableInputColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn workloadTableAlgoColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn workloadTableAmountColumn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button openRunButton;
        private System.Windows.Forms.OpenFileDialog openRunDialog;
        private System.Windows.Forms.SaveFileDialog saveRunDialog;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ComboBox algorithmFactoryComboBox;
        private System.Windows.Forms.ComboBox algorithmComboBox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button addAlgorithmButton;
        private System.Windows.Forms.Panel algorithmOptionsPanel;
        private System.Windows.Forms.Button saveRunButton;
        private System.Windows.Forms.OpenFileDialog importRunDialog;
        private System.Windows.Forms.Button importInputButton;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button removeAlgorithmButton;
    }
}
