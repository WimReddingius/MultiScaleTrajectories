using System.ComponentModel;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Controller.Edit;
using AlgorithmVisualization.View.Util;

namespace AlgorithmVisualization.View.Edit
{
    public partial class InputEditorChooser<TIn> : UserControl where TIn : Input, new()
    {
        public TIn Input;
        private InputEditor<TIn> CurrentInputEditor => (InputEditor<TIn>)inputEditorComboBox.SelectedItem;

        public InputEditorChooser(BindingList<InputEditor<TIn>> inputEditors)
        {
            InitializeComponent();
            inputEditorComboBox.BringToFront();

            inputEditorComboBox.DataSource = inputEditors;
            inputEditorComboBox.Format += (s, e) => { e.Value = ((InputEditor<TIn>)e.Value).Name; };

            inputEditorComboBox_SelectedIndexChanged(null, null);
        }

        private void inputEditorComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (CurrentInputEditor != null)
            {
                inputEditorContainer.Fill(CurrentInputEditor);
                LoadInput(Input);
            }
        }

        public void LoadInput(TIn input)
        {
            Input = input;
            if (Input != null)
            {
                CurrentInputEditor?.LoadInput(Input);
            }
        }

    }
}
