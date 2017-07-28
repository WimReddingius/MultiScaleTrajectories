using MultiScaleTrajectories.Simplification.MultiScale.View.Edit;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.View
{
    partial class MSSInputEditor
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
            this.epsilonEditor = new EpsilonListEditor();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cumulativeCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // epsilonEditor
            // 
            this.epsilonEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.epsilonEditor.Location = new System.Drawing.Point(0, 43);
            this.epsilonEditor.Name = "epsilonEditor";
            this.epsilonEditor.Size = new System.Drawing.Size(221, 361);
            this.epsilonEditor.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cumulativeCheckBox);
            this.splitContainer1.Panel1.Controls.Add(this.epsilonEditor);
            this.splitContainer1.Size = new System.Drawing.Size(527, 404);
            this.splitContainer1.SplitterDistance = 224;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 1;
            // 
            // cumulativeCheckBox
            // 
            this.cumulativeCheckBox.AutoSize = true;
            this.cumulativeCheckBox.Location = new System.Drawing.Point(7, 10);
            this.cumulativeCheckBox.Name = "cumulativeCheckBox";
            this.cumulativeCheckBox.Size = new System.Drawing.Size(78, 17);
            this.cumulativeCheckBox.TabIndex = 1;
            this.cumulativeCheckBox.Text = "Cumulative";
            this.cumulativeCheckBox.UseVisualStyleBackColor = true;
            this.cumulativeCheckBox.CheckedChanged += new System.EventHandler(this.cumulativeCheckBox_CheckedChanged);
            // 
            // MSSInputEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "MSSInputEditor";
            this.Size = new System.Drawing.Size(527, 404);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private EpsilonListEditor epsilonEditor;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.CheckBox cumulativeCheckBox;
    }
}
