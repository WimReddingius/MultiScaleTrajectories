namespace AlgorithmVisualization.View.Edit
{
    partial class InputEditorChooser<TIn>
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
            this.inputEditorComboBox = new System.Windows.Forms.ComboBox();
            this.inputEditorContainer = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // inputEditorComboBox
            // 
            this.inputEditorComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.inputEditorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.inputEditorComboBox.FormattingEnabled = true;
            this.inputEditorComboBox.Location = new System.Drawing.Point(42, 0);
            this.inputEditorComboBox.Name = "inputEditorComboBox";
            this.inputEditorComboBox.Size = new System.Drawing.Size(143, 21);
            this.inputEditorComboBox.TabIndex = 0;
            this.inputEditorComboBox.SelectedIndexChanged += new System.EventHandler(this.inputEditorComboBox_SelectedIndexChanged);
            // 
            // inputEditorContainer
            // 
            this.inputEditorContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputEditorContainer.Location = new System.Drawing.Point(0, 0);
            this.inputEditorContainer.Margin = new System.Windows.Forms.Padding(2);
            this.inputEditorContainer.Name = "inputEditorContainer";
            this.inputEditorContainer.Size = new System.Drawing.Size(184, 126);
            this.inputEditorContainer.TabIndex = 1;
            // 
            // InputEditorChooser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.inputEditorContainer);
            this.Controls.Add(this.inputEditorComboBox);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "InputEditorChooser";
            this.Size = new System.Drawing.Size(184, 126);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox inputEditorComboBox;
        private System.Windows.Forms.Panel inputEditorContainer;
    }
}
