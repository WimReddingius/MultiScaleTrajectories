using AlgorithmVisualization.Controller.Edit;
using MultiScaleTrajectories.Trajectory.View;

namespace MultiScaleTrajectories.Trajectory.Single.View
{
    partial class SingleTrajectoryEditorGeo : TrajectoryGeo, IInputEditor<SingleTrajectoryInput>
    {
        public SingleTrajectoryEditorGeo()
        {
            InitializeComponent();

            Name = "Geo";
        }

        public void LoadInput(SingleTrajectoryInput input)
        {
            LookAtTrajectory(input.Trajectory);
            DrawSingleTrajectory(input.Trajectory);
        }
    }
}
