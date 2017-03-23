namespace MultiScaleTrajectories.SingleTrajectory.View.Algorithm
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
            this.SuspendLayout();
            // 
            // shortcutFinderComboBox
            // 
            this.shortcutFinderComboBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.shortcutFinderComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.shortcutFinderComboBox.FormattingEnabled = true;
            this.shortcutFinderComboBox.Location = new System.Drawing.Point(0, 0);
            this.shortcutFinderComboBox.Name = "shortcutFinderComboBox";
            this.shortcutFinderComboBox.Size = new System.Drawing.Size(150, 21);
            this.shortcutFinderComboBox.TabIndex = 0;
            this.shortcutFinderComboBox.SelectedIndexChanged += new System.EventHandler(this.shortcutFinderComboBox_SelectedIndexChanged);
            // 
            // ImaiIriOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.shortcutFinderComboBox);
            this.Name = "ImaiIriOptions";
            this.Size = new System.Drawing.Size(150, 43);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox shortcutFinderComboBox;
    }
}
