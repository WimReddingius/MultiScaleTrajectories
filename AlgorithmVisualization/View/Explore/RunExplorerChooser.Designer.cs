namespace AlgorithmVisualization.View.Explore
{
    partial class RunExplorerChooser<TIn, TOut>
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
            this.components = new System.ComponentModel.Container();
            this.runExplorerComboBox = new System.Windows.Forms.ComboBox();
            this.visualizationContainer = new System.Windows.Forms.Panel();
            this.runSelectionUnavailableLabel = new System.Windows.Forms.Label();
            this.splitContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.verticalSplitContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.horizontalSplitContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unsplitContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.splitButton = new System.Windows.Forms.Button();
            this.autoChooseRunsCheckBox = new System.Windows.Forms.CheckBox();
            this.chooseRunSelectionCheckBox = new System.Windows.Forms.CheckBox();
            this.visualizationContainer.SuspendLayout();
            this.splitContextMenu.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // runExplorerComboBox
            // 
            this.runExplorerComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.runExplorerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.runExplorerComboBox.FormattingEnabled = true;
            this.runExplorerComboBox.Location = new System.Drawing.Point(3, 4);
            this.runExplorerComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
            this.runExplorerComboBox.Name = "runExplorerComboBox";
            this.runExplorerComboBox.Size = new System.Drawing.Size(171, 21);
            this.runExplorerComboBox.TabIndex = 0;
            this.runExplorerComboBox.SelectedIndexChanged += new System.EventHandler(this.runExplorerComboBox_SelectedIndexChanged);
            // 
            // visualizationContainer
            // 
            this.visualizationContainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.visualizationContainer.Controls.Add(this.runSelectionUnavailableLabel);
            this.visualizationContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.visualizationContainer.Location = new System.Drawing.Point(0, 0);
            this.visualizationContainer.Name = "visualizationContainer";
            this.visualizationContainer.Size = new System.Drawing.Size(443, 424);
            this.visualizationContainer.TabIndex = 1;
            // 
            // runSelectionUnavailableLabel
            // 
            this.runSelectionUnavailableLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.runSelectionUnavailableLabel.Location = new System.Drawing.Point(0, 0);
            this.runSelectionUnavailableLabel.Name = "runSelectionUnavailableLabel";
            this.runSelectionUnavailableLabel.Size = new System.Drawing.Size(443, 424);
            this.runSelectionUnavailableLabel.TabIndex = 2;
            this.runSelectionUnavailableLabel.Text = "Run selection unsupported";
            this.runSelectionUnavailableLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitContextMenu
            // 
            this.splitContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.verticalSplitContextMenuItem,
            this.horizontalSplitContextMenuItem,
            this.unsplitContextMenuItem});
            this.splitContextMenu.Name = "splitContextMenu";
            this.splitContextMenu.Size = new System.Drawing.Size(139, 70);
            // 
            // verticalSplitContextMenuItem
            // 
            this.verticalSplitContextMenuItem.Name = "verticalSplitContextMenuItem";
            this.verticalSplitContextMenuItem.Size = new System.Drawing.Size(138, 22);
            this.verticalSplitContextMenuItem.Text = "Vertically";
            // 
            // horizontalSplitContextMenuItem
            // 
            this.horizontalSplitContextMenuItem.Name = "horizontalSplitContextMenuItem";
            this.horizontalSplitContextMenuItem.Size = new System.Drawing.Size(138, 22);
            this.horizontalSplitContextMenuItem.Text = "Horizontally";
            // 
            // unsplitContextMenuItem
            // 
            this.unsplitContextMenuItem.Name = "unsplitContextMenuItem";
            this.unsplitContextMenuItem.Size = new System.Drawing.Size(138, 22);
            this.unsplitContextMenuItem.Text = "Unsplit";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.runExplorerComboBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.splitButton, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.autoChooseRunsCheckBox, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.chooseRunSelectionCheckBox, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(443, 29);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // splitButton
            // 
            this.splitButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitButton.Location = new System.Drawing.Point(356, 3);
            this.splitButton.Name = "splitButton";
            this.splitButton.Size = new System.Drawing.Size(84, 23);
            this.splitButton.TabIndex = 4;
            this.splitButton.Text = "Split";
            this.splitButton.UseVisualStyleBackColor = true;
            this.splitButton.Click += new System.EventHandler(this.splitButton_Click);
            // 
            // autoChooseRunsCheckBox
            // 
            this.autoChooseRunsCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.autoChooseRunsCheckBox.AutoSize = true;
            this.autoChooseRunsCheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.autoChooseRunsCheckBox.Location = new System.Drawing.Point(268, 3);
            this.autoChooseRunsCheckBox.Name = "autoChooseRunsCheckBox";
            this.autoChooseRunsCheckBox.Size = new System.Drawing.Size(82, 23);
            this.autoChooseRunsCheckBox.TabIndex = 6;
            this.autoChooseRunsCheckBox.Text = "Auto";
            this.autoChooseRunsCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.autoChooseRunsCheckBox.UseVisualStyleBackColor = true;
            this.autoChooseRunsCheckBox.CheckedChanged += new System.EventHandler(this.autoChooseRunsCheckBox_CheckedChanged);
            // 
            // chooseRunSelectionCheckBox
            // 
            this.chooseRunSelectionCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this.chooseRunSelectionCheckBox.AutoSize = true;
            this.chooseRunSelectionCheckBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chooseRunSelectionCheckBox.Location = new System.Drawing.Point(180, 3);
            this.chooseRunSelectionCheckBox.Name = "chooseRunSelectionCheckBox";
            this.chooseRunSelectionCheckBox.Size = new System.Drawing.Size(82, 23);
            this.chooseRunSelectionCheckBox.TabIndex = 7;
            this.chooseRunSelectionCheckBox.Text = "Choose";
            this.chooseRunSelectionCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chooseRunSelectionCheckBox.UseVisualStyleBackColor = true;
            this.chooseRunSelectionCheckBox.CheckedChanged += new System.EventHandler(this.chooseRunSelectionCheckBox_CheckedChanged);
            // 
            // RunExplorerChooser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.visualizationContainer);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "RunExplorerChooser";
            this.Size = new System.Drawing.Size(443, 424);
            this.visualizationContainer.ResumeLayout(false);
            this.splitContextMenu.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox runExplorerComboBox;
        private System.Windows.Forms.Panel visualizationContainer;
        private System.Windows.Forms.Label runSelectionUnavailableLabel;
        private System.Windows.Forms.ContextMenuStrip splitContextMenu;
        public System.Windows.Forms.ToolStripMenuItem verticalSplitContextMenuItem;
        public System.Windows.Forms.ToolStripMenuItem horizontalSplitContextMenuItem;
        public System.Windows.Forms.ToolStripMenuItem unsplitContextMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        public System.Windows.Forms.Button splitButton;
        private System.Windows.Forms.CheckBox chooseRunSelectionCheckBox;
        private System.Windows.Forms.CheckBox autoChooseRunsCheckBox;
    }
}