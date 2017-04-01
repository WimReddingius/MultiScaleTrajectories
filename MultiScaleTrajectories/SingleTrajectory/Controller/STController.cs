using System.IO;
using System.Windows.Forms;
using AlgorithmVisualization.Controller;
using MultiScaleTrajectories.SingleTrajectory.Algorithm;
using MultiScaleTrajectories.SingleTrajectory.Algorithm.ImaiIri;
using MultiScaleTrajectories.SingleTrajectory.View;
using MultiScaleTrajectories.SingleTrajectory.View.Edit;
using MultiScaleTrajectories.SingleTrajectory.View.Explore.Canvas;
using MultiScaleTrajectories.SingleTrajectory.View.Explore.Geo;

namespace MultiScaleTrajectories.SingleTrajectory.Controller
{
    class STController : AlgorithmController<STInput, STOutput>
    {
        public override string Name => "Single Trajectory Simplification";

        public STController()
        {
            CanImport = true;

            AddSimpleInputEditor(new STInputEditor(new TrajectoryEditorCanvas()));
            AddSimpleInputEditor(new STInputEditor(new TrajectoryEditorGeo()));

            AddRunExplorerType(typeof(LevelTrajectoryCanvasExplorer));
            AddRunExplorerType(typeof(LevelTrajectoryGeoExplorer));

            AddAlgorithmType(typeof(ImaiIriHierarchical));
            AddAlgorithmType(typeof(ImaiIriGreedy));
            AddAlgorithmType(typeof(ImaiIriNaive));
        }

        public override STInput ImportInput(string fileName, out bool customName)
        {
            customName = true;

            var dialog = new STMoveBankDialog(fileName);
            var result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                return new STInput(dialog.ChosenTrajectory)
                {
                    Name = Path.GetFileNameWithoutExtension(fileName) + "_" + dialog.ChosenTrajectoryName
                };
            }

            return null;
        }

    }
}
