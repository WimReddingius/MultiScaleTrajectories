using MultiScaleTrajectories.Simplification.MultiScale.View.Edit;

namespace MultiScaleTrajectories.Simplification.ShortcutPathFinding.View.Edit
{
    partial class SPFInputEditor
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.shortcutFindingProgressLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.customRangeCheckbox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.startToEndCheckbox = new System.Windows.Forms.CheckBox();
            this.targetIndexUpdown = new System.Windows.Forms.NumericUpDown();
            this.sourceIndexUpDown = new System.Windows.Forms.NumericUpDown();
            this.computeShortcutsButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.errorUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.targetIndexUpdown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sourceIndexUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.shortcutFindingProgressLabel);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.customRangeCheckbox);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.startToEndCheckbox);
            this.splitContainer1.Panel1.Controls.Add(this.targetIndexUpdown);
            this.splitContainer1.Panel1.Controls.Add(this.sourceIndexUpDown);
            this.splitContainer1.Panel1.Controls.Add(this.computeShortcutsButton);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.errorUpDown);
            this.splitContainer1.Size = new System.Drawing.Size(581, 331);
            this.splitContainer1.SplitterDistance = 228;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 1;
            // 
            // shortcutFindingProgressLabel
            // 
            this.shortcutFindingProgressLabel.AutoSize = true;
            this.shortcutFindingProgressLabel.Location = new System.Drawing.Point(7, 69);
            this.shortcutFindingProgressLabel.Name = "shortcutFindingProgressLabel";
            this.shortcutFindingProgressLabel.Size = new System.Drawing.Size(0, 13);
            this.shortcutFindingProgressLabel.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(117, 168);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "to";
            // 
            // customRangeCheckbox
            // 
            this.customRangeCheckbox.AutoSize = true;
            this.customRangeCheckbox.Location = new System.Drawing.Point(7, 146);
            this.customRangeCheckbox.Name = "customRangeCheckbox";
            this.customRangeCheckbox.Size = new System.Drawing.Size(61, 17);
            this.customRangeCheckbox.TabIndex = 7;
            this.customRangeCheckbox.Text = "Custom";
            this.customRangeCheckbox.UseVisualStyleBackColor = true;
            this.customRangeCheckbox.Click += new System.EventHandler(this.customRangeCheckbox_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 166);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "From";
            // 
            // startToEndCheckbox
            // 
            this.startToEndCheckbox.AutoSize = true;
            this.startToEndCheckbox.Checked = true;
            this.startToEndCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.startToEndCheckbox.Location = new System.Drawing.Point(7, 123);
            this.startToEndCheckbox.Name = "startToEndCheckbox";
            this.startToEndCheckbox.Size = new System.Drawing.Size(81, 17);
            this.startToEndCheckbox.TabIndex = 5;
            this.startToEndCheckbox.Text = "Start to end";
            this.startToEndCheckbox.UseVisualStyleBackColor = true;
            this.startToEndCheckbox.Click += new System.EventHandler(this.startToEndCheckbox_Click);
            // 
            // targetIndexUpdown
            // 
            this.targetIndexUpdown.Location = new System.Drawing.Point(139, 166);
            this.targetIndexUpdown.Name = "targetIndexUpdown";
            this.targetIndexUpdown.Size = new System.Drawing.Size(79, 20);
            this.targetIndexUpdown.TabIndex = 4;
            this.targetIndexUpdown.ValueChanged += new System.EventHandler(this.targetIndexUpdown_ValueChanged);
            // 
            // sourceIndexUpDown
            // 
            this.sourceIndexUpDown.Location = new System.Drawing.Point(40, 166);
            this.sourceIndexUpDown.Name = "sourceIndexUpDown";
            this.sourceIndexUpDown.Size = new System.Drawing.Size(71, 20);
            this.sourceIndexUpDown.TabIndex = 3;
            this.sourceIndexUpDown.ValueChanged += new System.EventHandler(this.sourceIndexUpDown_ValueChanged);
            // 
            // computeShortcutsButton
            // 
            this.computeShortcutsButton.Location = new System.Drawing.Point(6, 36);
            this.computeShortcutsButton.Name = "computeShortcutsButton";
            this.computeShortcutsButton.Size = new System.Drawing.Size(211, 23);
            this.computeShortcutsButton.TabIndex = 2;
            this.computeShortcutsButton.Text = "Compute Shortcuts";
            this.computeShortcutsButton.UseVisualStyleBackColor = true;
            this.computeShortcutsButton.Click += new System.EventHandler(this.computeShortcutsButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Error";
            // 
            // errorUpDown
            // 
            this.errorUpDown.DecimalPlaces = 4;
            this.errorUpDown.Location = new System.Drawing.Point(97, 8);
            this.errorUpDown.Name = "errorUpDown";
            this.errorUpDown.Size = new System.Drawing.Size(120, 20);
            this.errorUpDown.TabIndex = 0;
            // 
            // SPFInputEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "SPFInputEditor";
            this.Size = new System.Drawing.Size(581, 331);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.targetIndexUpdown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sourceIndexUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.NumericUpDown errorUpDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox customRangeCheckbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox startToEndCheckbox;
        private System.Windows.Forms.NumericUpDown targetIndexUpdown;
        private System.Windows.Forms.NumericUpDown sourceIndexUpDown;
        private System.Windows.Forms.Button computeShortcutsButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label shortcutFindingProgressLabel;
    }
}
