using MultiScaleTrajectories.Algorithm;

namespace MultiScaleTrajectories.View
{
    partial class AlgoConfig<TIn, TOut> where TIn : Input, new() where TOut : Algorithm.Output, new()
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.runTabPage = new System.Windows.Forms.TabPage();
            this.removeWorkloadRunButton = new System.Windows.Forms.Button();
            this.resetWorkloadButton = new System.Windows.Forms.Button();
            this.addWorkloadRunButton = new System.Windows.Forms.Button();
            this.workloadTable = new System.Windows.Forms.DataGridView();
            this.workloadTableRunColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.workloadTableInputColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.workloadTableAlgoColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.computeWorkloadButton = new System.Windows.Forms.Button();
            this.inputTabPage = new System.Windows.Forms.TabPage();
            this.inputOptionsPanel = new System.Windows.Forms.Panel();
            this.inputSplitContainer = new System.Windows.Forms.SplitContainer();
            this.inputComboBox = new System.Windows.Forms.ComboBox();
            this.removeInputButton = new System.Windows.Forms.Button();
            this.addInputButton = new System.Windows.Forms.Button();
            this.openInputButton = new System.Windows.Forms.Button();
            this.clearInputButton = new System.Windows.Forms.Button();
            this.saveInputButton = new System.Windows.Forms.Button();
            this.outputTabPage = new System.Windows.Forms.TabPage();
            this.outputSplitContainer = new System.Windows.Forms.SplitContainer();
            this.removeOutputRunButton = new System.Windows.Forms.Button();
            this.addOutputButton = new System.Windows.Forms.Button();
            this.outputTable = new System.Windows.Forms.DataGridView();
            this.outputTableRunColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.outputTableInputColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.outputTableAlgoColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.explorationControllerComboBox = new System.Windows.Forms.ComboBox();
            this.outputViewOptionsPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.openInputDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveInputDialog = new System.Windows.Forms.SaveFileDialog();
            this.tabControl.SuspendLayout();
            this.runTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.workloadTable)).BeginInit();
            this.inputTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputSplitContainer)).BeginInit();
            this.inputSplitContainer.Panel1.SuspendLayout();
            this.inputSplitContainer.Panel2.SuspendLayout();
            this.inputSplitContainer.SuspendLayout();
            this.outputTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outputSplitContainer)).BeginInit();
            this.outputSplitContainer.Panel1.SuspendLayout();
            this.outputSplitContainer.Panel2.SuspendLayout();
            this.outputSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outputTable)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.runTabPage);
            this.tabControl.Controls.Add(this.inputTabPage);
            this.tabControl.Controls.Add(this.outputTabPage);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(270, 523);
            this.tabControl.TabIndex = 5;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // runTabPage
            // 
            this.runTabPage.Controls.Add(this.removeWorkloadRunButton);
            this.runTabPage.Controls.Add(this.resetWorkloadButton);
            this.runTabPage.Controls.Add(this.addWorkloadRunButton);
            this.runTabPage.Controls.Add(this.workloadTable);
            this.runTabPage.Controls.Add(this.computeWorkloadButton);
            this.runTabPage.Location = new System.Drawing.Point(4, 22);
            this.runTabPage.Name = "runTabPage";
            this.runTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.runTabPage.Size = new System.Drawing.Size(262, 497);
            this.runTabPage.TabIndex = 2;
            this.runTabPage.Text = "Workload";
            this.runTabPage.UseVisualStyleBackColor = true;
            // 
            // removeWorkloadRunButton
            // 
            this.removeWorkloadRunButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.removeWorkloadRunButton.Location = new System.Drawing.Point(135, 442);
            this.removeWorkloadRunButton.Name = "removeWorkloadRunButton";
            this.removeWorkloadRunButton.Size = new System.Drawing.Size(126, 23);
            this.removeWorkloadRunButton.TabIndex = 22;
            this.removeWorkloadRunButton.Text = "Remove";
            this.removeWorkloadRunButton.UseVisualStyleBackColor = true;
            this.removeWorkloadRunButton.Click += new System.EventHandler(this.removeWorkloadRunButton_Click);
            // 
            // resetWorkloadButton
            // 
            this.resetWorkloadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.resetWorkloadButton.Enabled = false;
            this.resetWorkloadButton.Location = new System.Drawing.Point(135, 471);
            this.resetWorkloadButton.Name = "resetWorkloadButton";
            this.resetWorkloadButton.Size = new System.Drawing.Size(124, 23);
            this.resetWorkloadButton.TabIndex = 21;
            this.resetWorkloadButton.Text = "Reset";
            this.resetWorkloadButton.UseVisualStyleBackColor = true;
            this.resetWorkloadButton.Click += new System.EventHandler(this.resetWorkloadButton_Click);
            // 
            // addWorkloadRunButton
            // 
            this.addWorkloadRunButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addWorkloadRunButton.Location = new System.Drawing.Point(3, 442);
            this.addWorkloadRunButton.Name = "addWorkloadRunButton";
            this.addWorkloadRunButton.Size = new System.Drawing.Size(126, 23);
            this.addWorkloadRunButton.TabIndex = 19;
            this.addWorkloadRunButton.Text = "Add";
            this.addWorkloadRunButton.UseVisualStyleBackColor = true;
            this.addWorkloadRunButton.Click += new System.EventHandler(this.addWorkloadRunButton_Click);
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
            this.workloadTable.MultiSelect = false;
            this.workloadTable.Name = "workloadTable";
            this.workloadTable.RowHeadersVisible = false;
            this.workloadTable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.workloadTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.workloadTable.Size = new System.Drawing.Size(256, 433);
            this.workloadTable.TabIndex = 18;
            this.workloadTable.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.workloadTable_CellEndEdit);
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
            dataGridViewCellStyle2.NullValue = null;
            this.workloadTableInputColumn.DefaultCellStyle = dataGridViewCellStyle2;
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
            // computeWorkloadButton
            // 
            this.computeWorkloadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.computeWorkloadButton.Location = new System.Drawing.Point(3, 471);
            this.computeWorkloadButton.Name = "computeWorkloadButton";
            this.computeWorkloadButton.Size = new System.Drawing.Size(126, 23);
            this.computeWorkloadButton.TabIndex = 6;
            this.computeWorkloadButton.Text = "Compute";
            this.computeWorkloadButton.UseVisualStyleBackColor = true;
            this.computeWorkloadButton.Click += new System.EventHandler(this.computeWorkloadButton_Click);
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
            this.inputOptionsPanel.Location = new System.Drawing.Point(3, 115);
            this.inputOptionsPanel.Name = "inputOptionsPanel";
            this.inputOptionsPanel.Size = new System.Drawing.Size(256, 379);
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
            this.inputSplitContainer.Panel1.Controls.Add(this.inputComboBox);
            this.inputSplitContainer.Panel1.Controls.Add(this.removeInputButton);
            this.inputSplitContainer.Panel1.Controls.Add(this.addInputButton);
            // 
            // inputSplitContainer.Panel2
            // 
            this.inputSplitContainer.Panel2.Controls.Add(this.openInputButton);
            this.inputSplitContainer.Panel2.Controls.Add(this.clearInputButton);
            this.inputSplitContainer.Panel2.Controls.Add(this.saveInputButton);
            this.inputSplitContainer.Size = new System.Drawing.Size(256, 491);
            this.inputSplitContainer.SplitterDistance = 67;
            this.inputSplitContainer.TabIndex = 18;
            // 
            // inputComboBox
            // 
            this.inputComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inputComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.inputComboBox.Location = new System.Drawing.Point(0, 3);
            this.inputComboBox.Name = "inputComboBox";
            this.inputComboBox.Size = new System.Drawing.Size(256, 21);
            this.inputComboBox.TabIndex = 15;
            this.inputComboBox.SelectedIndexChanged += new System.EventHandler(this.inputComboBox_SelectedIndexChanged);
            // 
            // removeInputButton
            // 
            this.removeInputButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.removeInputButton.Location = new System.Drawing.Point(124, 30);
            this.removeInputButton.Name = "removeInputButton";
            this.removeInputButton.Size = new System.Drawing.Size(132, 23);
            this.removeInputButton.TabIndex = 17;
            this.removeInputButton.Text = "Remove";
            this.removeInputButton.UseVisualStyleBackColor = true;
            this.removeInputButton.Click += new System.EventHandler(this.removeInputButton_Click);
            // 
            // addInputButton
            // 
            this.addInputButton.Location = new System.Drawing.Point(0, 30);
            this.addInputButton.Name = "addInputButton";
            this.addInputButton.Size = new System.Drawing.Size(118, 23);
            this.addInputButton.TabIndex = 16;
            this.addInputButton.Text = "Add";
            this.addInputButton.UseVisualStyleBackColor = true;
            this.addInputButton.Click += new System.EventHandler(this.addInputButton_Click);
            // 
            // openInputButton
            // 
            this.openInputButton.Location = new System.Drawing.Point(0, 12);
            this.openInputButton.Name = "openInputButton";
            this.openInputButton.Size = new System.Drawing.Size(87, 23);
            this.openInputButton.TabIndex = 1;
            this.openInputButton.Text = "Open";
            this.openInputButton.UseVisualStyleBackColor = true;
            this.openInputButton.Click += new System.EventHandler(this.openInputButton_Click);
            // 
            // clearInputButton
            // 
            this.clearInputButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clearInputButton.Location = new System.Drawing.Point(93, 12);
            this.clearInputButton.Name = "clearInputButton";
            this.clearInputButton.Size = new System.Drawing.Size(78, 23);
            this.clearInputButton.TabIndex = 13;
            this.clearInputButton.Text = "Clear";
            this.clearInputButton.UseVisualStyleBackColor = true;
            this.clearInputButton.Click += new System.EventHandler(this.clearInputButton_Click);
            // 
            // saveInputButton
            // 
            this.saveInputButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.saveInputButton.Location = new System.Drawing.Point(177, 12);
            this.saveInputButton.Name = "saveInputButton";
            this.saveInputButton.Size = new System.Drawing.Size(79, 23);
            this.saveInputButton.TabIndex = 0;
            this.saveInputButton.Text = "Save";
            this.saveInputButton.UseVisualStyleBackColor = true;
            this.saveInputButton.Click += new System.EventHandler(this.saveInputButton_Click);
            // 
            // outputTabPage
            // 
            this.outputTabPage.Controls.Add(this.outputSplitContainer);
            this.outputTabPage.Location = new System.Drawing.Point(4, 22);
            this.outputTabPage.Name = "outputTabPage";
            this.outputTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.outputTabPage.Size = new System.Drawing.Size(262, 497);
            this.outputTabPage.TabIndex = 1;
            this.outputTabPage.Text = "Explore";
            this.outputTabPage.UseVisualStyleBackColor = true;
            // 
            // outputSplitContainer
            // 
            this.outputSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.outputSplitContainer.Location = new System.Drawing.Point(3, 3);
            this.outputSplitContainer.Name = "outputSplitContainer";
            this.outputSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // outputSplitContainer.Panel1
            // 
            this.outputSplitContainer.Panel1.Controls.Add(this.removeOutputRunButton);
            this.outputSplitContainer.Panel1.Controls.Add(this.addOutputButton);
            this.outputSplitContainer.Panel1.Controls.Add(this.outputTable);
            // 
            // outputSplitContainer.Panel2
            // 
            this.outputSplitContainer.Panel2.Controls.Add(this.explorationControllerComboBox);
            this.outputSplitContainer.Panel2.Controls.Add(this.outputViewOptionsPanel);
            this.outputSplitContainer.Panel2.Controls.Add(this.label2);
            this.outputSplitContainer.Size = new System.Drawing.Size(256, 491);
            this.outputSplitContainer.SplitterDistance = 186;
            this.outputSplitContainer.TabIndex = 18;
            // 
            // removeOutputRunButton
            // 
            this.removeOutputRunButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.removeOutputRunButton.Enabled = false;
            this.removeOutputRunButton.Location = new System.Drawing.Point(130, 160);
            this.removeOutputRunButton.Name = "removeOutputRunButton";
            this.removeOutputRunButton.Size = new System.Drawing.Size(126, 23);
            this.removeOutputRunButton.TabIndex = 23;
            this.removeOutputRunButton.Text = "Remove";
            this.removeOutputRunButton.UseVisualStyleBackColor = true;
            this.removeOutputRunButton.Click += new System.EventHandler(this.removeOutputRunButton_Click);
            // 
            // addOutputButton
            // 
            this.addOutputButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addOutputButton.Enabled = false;
            this.addOutputButton.Location = new System.Drawing.Point(0, 160);
            this.addOutputButton.Name = "addOutputButton";
            this.addOutputButton.Size = new System.Drawing.Size(123, 23);
            this.addOutputButton.TabIndex = 21;
            this.addOutputButton.Text = "Add";
            this.addOutputButton.UseVisualStyleBackColor = true;
            this.addOutputButton.Click += new System.EventHandler(this.addOutputButton_Click);
            // 
            // outputTable
            // 
            this.outputTable.AllowUserToAddRows = false;
            this.outputTable.AllowUserToResizeRows = false;
            this.outputTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.outputTable.BackgroundColor = System.Drawing.SystemColors.Control;
            this.outputTable.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.outputTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.outputTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.outputTableRunColumn,
            this.outputTableInputColumn,
            this.outputTableAlgoColumn});
            this.outputTable.Dock = System.Windows.Forms.DockStyle.Top;
            this.outputTable.Enabled = false;
            this.outputTable.Location = new System.Drawing.Point(0, 0);
            this.outputTable.MultiSelect = false;
            this.outputTable.Name = "outputTable";
            this.outputTable.RowHeadersVisible = false;
            this.outputTable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.outputTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.outputTable.Size = new System.Drawing.Size(256, 154);
            this.outputTable.TabIndex = 17;
            // 
            // outputTableRunColumn
            // 
            this.outputTableRunColumn.FillWeight = 50F;
            this.outputTableRunColumn.HeaderText = "Run";
            this.outputTableRunColumn.Name = "outputTableRunColumn";
            this.outputTableRunColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // outputTableInputColumn
            // 
            this.outputTableInputColumn.FillWeight = 50F;
            this.outputTableInputColumn.HeaderText = "Input";
            this.outputTableInputColumn.Name = "outputTableInputColumn";
            this.outputTableInputColumn.ReadOnly = true;
            // 
            // outputTableAlgoColumn
            // 
            this.outputTableAlgoColumn.HeaderText = "Algorithm";
            this.outputTableAlgoColumn.Name = "outputTableAlgoColumn";
            this.outputTableAlgoColumn.ReadOnly = true;
            this.outputTableAlgoColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // explorationControllerComboBox
            // 
            this.explorationControllerComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.explorationControllerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.explorationControllerComboBox.Enabled = false;
            this.explorationControllerComboBox.Location = new System.Drawing.Point(0, 26);
            this.explorationControllerComboBox.Name = "explorationControllerComboBox";
            this.explorationControllerComboBox.Size = new System.Drawing.Size(256, 21);
            this.explorationControllerComboBox.TabIndex = 3;
            this.explorationControllerComboBox.SelectedIndexChanged += new System.EventHandler(this.outputControllerComboBox_SelectedIndexChanged);
            // 
            // outputViewOptionsPanel
            // 
            this.outputViewOptionsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outputViewOptionsPanel.Location = new System.Drawing.Point(0, 53);
            this.outputViewOptionsPanel.Name = "outputViewOptionsPanel";
            this.outputViewOptionsPanel.Size = new System.Drawing.Size(256, 248);
            this.outputViewOptionsPanel.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Available Views";
            // 
            // openInputDialog
            // 
            this.openInputDialog.DefaultExt = "json";
            this.openInputDialog.FileName = "openFileDialog1";
            this.openInputDialog.Filter = "JSON(*.json)|*json";
            this.openInputDialog.Title = "Open Configuration";
            // 
            // saveInputDialog
            // 
            this.saveInputDialog.DefaultExt = "json";
            this.saveInputDialog.Filter = "JSON(*.json)|*json";
            this.saveInputDialog.Title = "Save Configuration";
            // 
            // AlgoConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Name = "AlgoConfig";
            this.Size = new System.Drawing.Size(270, 523);
            this.tabControl.ResumeLayout(false);
            this.runTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.workloadTable)).EndInit();
            this.inputTabPage.ResumeLayout(false);
            this.inputSplitContainer.Panel1.ResumeLayout(false);
            this.inputSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.inputSplitContainer)).EndInit();
            this.inputSplitContainer.ResumeLayout(false);
            this.outputTabPage.ResumeLayout(false);
            this.outputSplitContainer.Panel1.ResumeLayout(false);
            this.outputSplitContainer.Panel2.ResumeLayout(false);
            this.outputSplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outputSplitContainer)).EndInit();
            this.outputSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.outputTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage inputTabPage;
        private System.Windows.Forms.Panel inputOptionsPanel;
        private System.Windows.Forms.Button openInputButton;
        private System.Windows.Forms.Button clearInputButton;
        private System.Windows.Forms.Button saveInputButton;
        private System.Windows.Forms.TabPage outputTabPage;
        private System.Windows.Forms.Panel outputViewOptionsPanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage runTabPage;
        private System.Windows.Forms.Button computeWorkloadButton;
        private System.Windows.Forms.OpenFileDialog openInputDialog;
        private System.Windows.Forms.SaveFileDialog saveInputDialog;
        private System.Windows.Forms.ComboBox explorationControllerComboBox;
        private System.Windows.Forms.DataGridView outputTable;
        private System.Windows.Forms.DataGridView workloadTable;
        private System.Windows.Forms.Button addWorkloadRunButton;
        private System.Windows.Forms.SplitContainer outputSplitContainer;
        private System.Windows.Forms.Button addOutputButton;
        private System.Windows.Forms.ComboBox inputComboBox;
        private System.Windows.Forms.Button removeInputButton;
        private System.Windows.Forms.Button addInputButton;
        private System.Windows.Forms.SplitContainer inputSplitContainer;
        private System.Windows.Forms.Button resetWorkloadButton;
        private System.Windows.Forms.Button removeWorkloadRunButton;
        private System.Windows.Forms.Button removeOutputRunButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn workloadTableRunColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn workloadTableInputColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn workloadTableAlgoColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn outputTableRunColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn outputTableInputColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn outputTableAlgoColumn;
    }
}
