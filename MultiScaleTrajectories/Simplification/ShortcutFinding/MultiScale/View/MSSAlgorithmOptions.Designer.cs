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
            this.shortcutSetBuilderComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.shortcutSetFactoryComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Shortcut Set Builder";
            // 
            // shortcutSetBuilderComboBox
            // 
            this.shortcutSetBuilderComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.shortcutSetBuilderComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.shortcutSetBuilderComboBox.Items.AddRange(new object[] {
            "Graph-based",
            "Region-based"});
            this.shortcutSetBuilderComboBox.Location = new System.Drawing.Point(0, 20);
            this.shortcutSetBuilderComboBox.Name = "shortcutSetBuilderComboBox";
            this.shortcutSetBuilderComboBox.Size = new System.Drawing.Size(150, 21);
            this.shortcutSetBuilderComboBox.TabIndex = 2;
            this.shortcutSetBuilderComboBox.SelectedIndexChanged += new System.EventHandler(this.shortcutSetBuilderComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Shortcut Representation";
            // 
            // shortcutSetFactoryComboBox
            // 
            this.shortcutSetFactoryComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.shortcutSetFactoryComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.shortcutSetFactoryComboBox.Items.AddRange(new object[] {
            "Graph-based",
            "Region-based"});
            this.shortcutSetFactoryComboBox.Location = new System.Drawing.Point(0, 67);
            this.shortcutSetFactoryComboBox.Name = "shortcutSetFactoryComboBox";
            this.shortcutSetFactoryComboBox.Size = new System.Drawing.Size(150, 21);
            this.shortcutSetFactoryComboBox.TabIndex = 4;
            this.shortcutSetFactoryComboBox.SelectedIndexChanged += new System.EventHandler(this.shortcutSetFactoryComboBox_SelectedIndexChanged);
            // 
            // MSSAlgorithmOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.shortcutSetFactoryComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.shortcutSetBuilderComboBox);
            this.Name = "MSSAlgorithmOptions";
            this.Size = new System.Drawing.Size(150, 101);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox shortcutSetBuilderComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox shortcutSetFactoryComboBox;
    }
}
