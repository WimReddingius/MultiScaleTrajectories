using AlgorithmVisualization.Controller.Edit;
using MultiScaleTrajectories.SingleTrajectory.Algorithm;
using MultiScaleTrajectories.View;

namespace MultiScaleTrajectories.SingleTrajectory.View.Edit
{
    partial class TrajectoryEditorGMap : TrajectoryGMap, IInputEditor<STInput>
    {
        public TrajectoryEditorGMap()
        {
            InitializeComponent();

            Name = "Map";
        }

        public void LoadInput(STInput input)
        {
            LookAtTrajectory(input.Trajectory);
            ShowSingleTrajectory(input.Trajectory);
        }
    }
}
