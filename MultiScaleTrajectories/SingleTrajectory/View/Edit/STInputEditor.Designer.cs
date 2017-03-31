namespace MultiScaleTrajectories.SingleTrajectory.View.Edit
{
    partial class STInputEditor
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
            this.STInputOptions1 = new MultiScaleTrajectories.SingleTrajectory.View.Edit.STInputOptions();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // STInputOptions1
            // 
            this.STInputOptions1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.STInputOptions1.Location = new System.Drawing.Point(0, 0);
            this.STInputOptions1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.STInputOptions1.Name = "STInputOptions1";
            this.STInputOptions1.Size = new System.Drawing.Size(224, 510);
            this.STInputOptions1.TabIndex = 0;
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
            this.splitContainer1.Panel1.Controls.Add(this.STInputOptions1);
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

        private STInputOptions STInputOptions1;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}
