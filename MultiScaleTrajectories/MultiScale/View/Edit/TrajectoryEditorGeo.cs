using AlgorithmVisualization.Controller.Edit;
using MultiScaleTrajectories.MultiScale.Algorithm;
using MultiScaleTrajectories.Trajectory;

namespace MultiScaleTrajectories.MultiScale.View.Edit
{
    partial class TrajectoryEditorGeo : TrajectoryGeo, IInputEditor<MSInput>
    {
        public TrajectoryEditorGeo()
        {
            InitializeComponent();

            Name = "Geo";
        }

        public void LoadInput(MSInput input)
        {
            LookAtTrajectory(input.Trajectory);
            DrawSingleTrajectory(input.Trajectory);
        }
    }
}
