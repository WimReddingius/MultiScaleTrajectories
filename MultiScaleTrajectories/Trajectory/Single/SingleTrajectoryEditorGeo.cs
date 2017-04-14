using AlgorithmVisualization.Controller.Edit;

namespace MultiScaleTrajectories.Trajectory.Single
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
