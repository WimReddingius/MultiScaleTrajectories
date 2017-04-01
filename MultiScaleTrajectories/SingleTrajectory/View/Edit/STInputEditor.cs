using System.Windows.Forms;
using AlgorithmVisualization.Controller.Edit;
using AlgorithmVisualization.View.Util;
using MultiScaleTrajectories.SingleTrajectory.Algorithm;

namespace MultiScaleTrajectories.SingleTrajectory.View.Edit
{
    partial class STInputEditor : UserControl, IInputEditor<STInput>
    {
        private readonly InputEditor<STInput> trajectoryEditor;

        public STInputEditor(object editor)
        {
            InitializeComponent();

            trajectoryEditor = InputEditor<STInput>.CreateSimple(editor);

            SetTrajectoryEditorControl(trajectoryEditor);
        }

        private void SetTrajectoryEditorControl(Control ctrl)
        {
            splitContainer1.Panel2.Fill(ctrl);
            Name = ctrl.Name;
        }

        public void LoadInput(STInput input)
        {
            STInputOptions1.LoadInput(input);
            trajectoryEditor?.LoadInput(input);
        }
    }
}
