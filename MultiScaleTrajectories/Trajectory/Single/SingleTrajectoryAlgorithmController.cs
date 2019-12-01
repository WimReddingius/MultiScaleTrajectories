using System.IO;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Controller;
using MultiScaleTrajectories.Trajectory.View;

namespace MultiScaleTrajectories.Trajectory.Single
{
    abstract class SingleTrajectoryAlgorithmController<TIn, TOut> : AlgorithmController<TIn, TOut> 
        where TIn : SingleTrajectoryInput, new() where TOut : Output
    {
        protected SingleTrajectoryAlgorithmController(string name) : base(name)
        {
            CanImport = true;
        }

        public override TIn ImportInput(string fileName, out bool customName)
        {
            customName = true;

            var dialog = new MoveBankTrajectoryChooser(fileName);
            var result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                return new TIn {
                    Trajectory = dialog.ChosenTrajectory,
                    Name = Path.GetFileNameWithoutExtension(fileName) + " - " + dialog.ChosenTrajectoryName
                };
            }

            return null;
        }
    }
}
