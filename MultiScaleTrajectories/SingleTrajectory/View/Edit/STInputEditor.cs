using System;
using System.Windows.Forms;
using AlgorithmVisualization.Controller.Edit;
using AlgorithmVisualization.View.Util;
using MultiScaleTrajectories.SingleTrajectory.Algorithm;

namespace MultiScaleTrajectories.SingleTrajectory.View.Edit
{
    partial class STInputEditor : InputEditor<STInput>
    {
        private readonly IInputEditor<STInput> trajectoryEditor;

        private STInputEditor()
        {
            InitializeComponent();
        }

        public STInputEditor(InputEditor<STInput> trajectoryEditor) : this()
        {
            this.trajectoryEditor = trajectoryEditor;
            SetTrajectoryEditorControl(trajectoryEditor);
        }

        public STInputEditor(IInputEditor<STInput> trajectoryEditor) : this()
        {
            if (!(trajectoryEditor is Control))
                throw new ArgumentOutOfRangeException();

            this.trajectoryEditor = trajectoryEditor;

            SetTrajectoryEditorControl((Control) trajectoryEditor);
        }

        private void SetTrajectoryEditorControl(Control ctrl)
        {
            splitContainer1.Panel2.Fill(ctrl);
            Name = ctrl.Name;
        }

        public override void LoadInput(STInput input)
        {
            STInputOptions1.LoadInput(input);
            trajectoryEditor?.LoadInput(input);
        }

    }
}
