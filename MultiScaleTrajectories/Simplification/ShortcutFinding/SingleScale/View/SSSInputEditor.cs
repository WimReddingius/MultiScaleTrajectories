using System;
using System.Globalization;
using System.Windows.Forms;
using AlgorithmVisualization.Controller.Edit;
using AlgorithmVisualization.View.Util;
using MultiScaleTrajectories.Simplification.ShortcutFinding.SingleScale.Algorithm;
using MultiScaleTrajectories.Trajectory.Single;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.SingleScale.View
{
    partial class SSSInputEditor : UserControl, IInputEditor<SSSInput>
    {
        private readonly InputEditor<SingleTrajectoryInput> trajectoryEditor;
        private SSSInput input;

        public SSSInputEditor(IInputEditor<SingleTrajectoryInput> editor)
        {
            InitializeComponent();

            trajectoryEditor = new SimpleInputEditor<SingleTrajectoryInput>(editor);

            this.Fill(trajectoryEditor, false);
            epsilonContainer.BringToFront();

            Name = trajectoryEditor.Name;
        }

        public void LoadInput(SSSInput input)
        {
            this.input = input;
            epsilonTextBox.Text = input.Epsilon.ToString(CultureInfo.InvariantCulture);
            trajectoryEditor.LoadInput(input);
        }

        private void epsilonTextBox_TextChanged(object sender, EventArgs e)
        {
            if (epsilonTextBox.Text != "")
            {
                try
                {
                    var epsilon = double.Parse(epsilonTextBox.Text, CultureInfo.InvariantCulture);
                    input.Epsilon = epsilon;
                }
                catch (Exception ex)
                {
                    FormsUtil.ShowErrorMessage(ex.ToString());
                    epsilonTextBox.Text = "";
                }
            }
        }
    }
}
