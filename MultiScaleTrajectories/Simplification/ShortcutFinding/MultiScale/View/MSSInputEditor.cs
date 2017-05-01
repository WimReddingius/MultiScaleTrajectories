using System.Windows.Forms;
using AlgorithmVisualization.Controller.Edit;
using AlgorithmVisualization.View.Util;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm;
using MultiScaleTrajectories.Trajectory.Single;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.View
{
    partial class MSSInputEditor : UserControl, IInputEditor<MSSInput>
    {
        private readonly InputEditor<SingleTrajectoryInput> trajectoryEditor;
        private MSSInput input;

        public MSSInputEditor(IInputEditor<SingleTrajectoryInput> editor)
        {
            InitializeComponent();

            trajectoryEditor = new SimpleInputEditor<SingleTrajectoryInput>(editor);

            splitContainer1.Panel2.Fill(trajectoryEditor);
            Name = trajectoryEditor.Name;
        }

        public void LoadInput(MSSInput input)
        {
            this.input = input;
            epsilonEditor.LoadInput(input);
            trajectoryEditor.LoadInput(input);

            cumulativeCheckBox.CheckState = input.Cumulative ? CheckState.Checked : CheckState.Unchecked;
        }

        private void cumulativeCheckBox_CheckedChanged(object sender, System.EventArgs e)
        {
            input.Cumulative = cumulativeCheckBox.CheckState == CheckState.Checked;
        }
    }
}
