namespace MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.View
{
    partial class MSSAlgorithmOptions
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
            this.label1 = new System.Windows.Forms.Label();
            this.shortcutSetFactoriesComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Shortcut Representation";
            // 
            // shortcutSetFactoriesComboBox
            // 
            this.shortcutSetFactoriesComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.shortcutSetFactoriesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.shortcutSetFactoriesComboBox.FormattingEnabled = true;
            this.shortcutSetFactoriesComboBox.Items.AddRange(new object[] {
            "Graph-based",
            "Region-based"});
            this.shortcutSetFactoriesComboBox.Location = new System.Drawing.Point(0, 20);
            this.shortcutSetFactoriesComboBox.Name = "shortcutSetFactoriesComboBox";
            this.shortcutSetFactoriesComboBox.Size = new System.Drawing.Size(150, 21);
            this.shortcutSetFactoriesComboBox.TabIndex = 2;
            this.shortcutSetFactoriesComboBox.SelectedIndexChanged += new System.EventHandler(this.shortcutSetFactoriesComboBox_SelectedIndexChanged);
            // 
            // ShortcutFinderOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.shortcutSetFactoriesComboBox);
            this.Name = "MSShortcutFinderOptions";
            this.Size = new System.Drawing.Size(150, 49);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox shortcutSetFactoriesComboBox;
    }
}
