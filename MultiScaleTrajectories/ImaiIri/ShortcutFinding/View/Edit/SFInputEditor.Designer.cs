namespace MultiScaleTrajectories.ImaiIri.ShortcutFinding.View.Edit
{
    partial class SFInputEditor
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
            this.epsilonTextBox = new System.Windows.Forms.TextBox();
            this.epsilonContainer = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.epsilonContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // epsilonTextBox
            // 
            this.epsilonTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.epsilonTextBox.Location = new System.Drawing.Point(3, 22);
            this.epsilonTextBox.Name = "epsilonTextBox";
            this.epsilonTextBox.Size = new System.Drawing.Size(67, 20);
            this.epsilonTextBox.TabIndex = 0;
            this.epsilonTextBox.TextChanged += new System.EventHandler(this.epsilonTextBox_TextChanged);
            // 
            // epsilonContainer
            // 
            this.epsilonContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.epsilonContainer.Controls.Add(this.label1);
            this.epsilonContainer.Controls.Add(this.epsilonTextBox);
            this.epsilonContainer.Location = new System.Drawing.Point(0, 342);
            this.epsilonContainer.Name = "epsilonContainer";
            this.epsilonContainer.Size = new System.Drawing.Size(73, 45);
            this.epsilonContainer.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Epsilon";
            // 
            // SFInputEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.epsilonContainer);
            this.Name = "SFInputEditor";
            this.Size = new System.Drawing.Size(467, 387);
            this.epsilonContainer.ResumeLayout(false);
            this.epsilonContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox epsilonTextBox;
        private System.Windows.Forms.Panel epsilonContainer;
        private System.Windows.Forms.Label label1;
    }
}
