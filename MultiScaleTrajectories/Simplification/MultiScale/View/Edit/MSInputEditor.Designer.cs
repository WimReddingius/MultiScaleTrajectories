namespace MultiScaleTrajectories.Simplification.MultiScale.View.Edit
{
    partial class MSInputEditor
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
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // epsilonEditor
            // 
            this.epsilonEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.epsilonEditor.Location = new System.Drawing.Point(0, 0);
            this.epsilonEditor.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.epsilonEditor.Name = "epsilonEditor";
            this.epsilonEditor.Size = new System.Drawing.Size(224, 510);
            this.epsilonEditor.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.epsilonEditor);
            this.splitContainer1.Size = new System.Drawing.Size(871, 510);
            this.splitContainer1.SplitterDistance = 224;
            this.splitContainer1.TabIndex = 1;
            // 
            // STInputEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "STInputEditor";
            this.Size = new System.Drawing.Size(871, 510);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private EpsilonListEditor epsilonEditor;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}
