namespace MultiScaleTrajectories.MultiScale.View.Algorithm
{
    partial class ImaiIriOptions
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
            this.shortcutFinderComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // shortcutFinderComboBox
            // 
            this.shortcutFinderComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.shortcutFinderComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.shortcutFinderComboBox.FormattingEnabled = true;
            this.shortcutFinderComboBox.Location = new System.Drawing.Point(0, 20);
            this.shortcutFinderComboBox.Name = "shortcutFinderComboBox";
            this.shortcutFinderComboBox.Size = new System.Drawing.Size(162, 21);
            this.shortcutFinderComboBox.TabIndex = 0;
            this.shortcutFinderComboBox.SelectedIndexChanged += new System.EventHandler(this.shortcutFinderComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Shortcut Finder";
            // 
            // ImaiIriOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.shortcutFinderComboBox);
            this.Name = "ImaiIriOptions";
            this.Size = new System.Drawing.Size(162, 54);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox shortcutFinderComboBox;
        private System.Windows.Forms.Label label1;
    }
}
