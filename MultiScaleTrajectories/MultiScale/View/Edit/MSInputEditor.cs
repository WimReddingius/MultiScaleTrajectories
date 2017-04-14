using System.Windows.Forms;
using AlgorithmVisualization.Controller.Edit;
using AlgorithmVisualization.View.Util;
using MultiScaleTrajectories.MultiScale.Algorithm;
using MultiScaleTrajectories.Trajectory.Single;

namespace MultiScaleTrajectories.MultiScale.View.Edit
{
    partial class MSInputEditor : UserControl, IInputEditor<MSInput>
    {
        private readonly InputEditor<SingleTrajectoryInput> trajectoryEditor;

        public MSInputEditor(object editor)
        {
            InitializeComponent();

            trajectoryEditor = InputEditor<SingleTrajectoryInput>.CreateSimple(editor);

            splitContainer1.Panel2.Fill(trajectoryEditor);
            Name = trajectoryEditor.Name;
        }

        public void LoadInput(MSInput input)
        {
            epsilonEditor.LoadInput(input);
            trajectoryEditor.LoadInput(input);
        }
    }
}
