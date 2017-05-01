namespace MultiScaleTrajectories.Simplification.MultiScale.View.Algorithm
{
    partial class ShortcutOptions
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
            this.shortcutProviderComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.shortcutProviderOptions = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.shortestPathProviderComboBox = new System.Windows.Forms.ComboBox();
            this.shortestPathProviderOptions = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // shortcutProviderComboBox
            // 
            this.shortcutProviderComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.shortcutProviderComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.shortcutProviderComboBox.FormattingEnabled = true;
            this.shortcutProviderComboBox.Items.AddRange(new object[] {
            "Graph-based",
            "Region-based"});
            this.shortcutProviderComboBox.Location = new System.Drawing.Point(0, 20);
            this.shortcutProviderComboBox.Name = "shortcutProviderComboBox";
            this.shortcutProviderComboBox.Size = new System.Drawing.Size(166, 21);
            this.shortcutProviderComboBox.TabIndex = 0;
            this.shortcutProviderComboBox.SelectedIndexChanged += new System.EventHandler(this.shortcutProviderComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Shortcut Provider";
            // 
            // shortcutProviderOptions
            // 
            this.shortcutProviderOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.shortcutProviderOptions.Location = new System.Drawing.Point(0, 51);
            this.shortcutProviderOptions.Name = "shortcutProviderOptions";
            this.shortcutProviderOptions.Size = new System.Drawing.Size(166, 52);
            this.shortcutProviderOptions.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Shortest Path Provider";
            // 
            // shortestPathProviderComboBox
            // 
            this.shortestPathProviderComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.shortestPathProviderComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.shortestPathProviderComboBox.FormattingEnabled = true;
            this.shortestPathProviderComboBox.Items.AddRange(new object[] {
            "Graph-based",
            "Region-based"});
            this.shortestPathProviderComboBox.Location = new System.Drawing.Point(0, 126);
            this.shortestPathProviderComboBox.Name = "shortestPathProviderComboBox";
            this.shortestPathProviderComboBox.Size = new System.Drawing.Size(166, 21);
            this.shortestPathProviderComboBox.TabIndex = 5;
            this.shortestPathProviderComboBox.SelectedIndexChanged += new System.EventHandler(this.shortestPathProviderComboBox_SelectedIndexChanged);
            // 
            // shortestPathProviderOptions
            // 
            this.shortestPathProviderOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.shortestPathProviderOptions.Location = new System.Drawing.Point(0, 153);
            this.shortestPathProviderOptions.Name = "shortestPathProviderOptions";
            this.shortestPathProviderOptions.Size = new System.Drawing.Size(166, 70);
            this.shortestPathProviderOptions.TabIndex = 7;
            // 
            // ShortcutOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.shortestPathProviderOptions);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.shortestPathProviderComboBox);
            this.Controls.Add(this.shortcutProviderOptions);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.shortcutProviderComboBox);
            this.Name = "ShortcutOptions";
            this.Size = new System.Drawing.Size(166, 223);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox shortcutProviderComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel shortcutProviderOptions;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox shortestPathProviderComboBox;
        private System.Windows.Forms.Panel shortestPathProviderOptions;
    }
}
