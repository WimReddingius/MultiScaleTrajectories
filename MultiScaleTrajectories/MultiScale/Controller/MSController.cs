using System.IO;
using System.Windows.Forms;
using AlgorithmVisualization.Controller;
using MultiScaleTrajectories.MultiScale.Algorithm;
using MultiScaleTrajectories.MultiScale.Algorithm.ImaiIri;
using MultiScaleTrajectories.MultiScale.View.Edit;
using MultiScaleTrajectories.MultiScale.View.Explore.Canvas;
using MultiScaleTrajectories.MultiScale.View.Explore.Geo;
using MultiScaleTrajectories.Trajectory;

namespace MultiScaleTrajectories.MultiScale.Controller
{
    class MSController : AlgorithmController<MSInput, MSOutput>
    {
        public override string Name => "Single Trajectory Multi-Scale Simplification";

        public MSController()
        {
            CanImport = true;

            AddSimpleInputEditor(new MSInputEditor(new TrajectoryEditorCanvas()));
            AddSimpleInputEditor(new MSInputEditor(new TrajectoryEditorGeo()));

            AddRunExplorerType(typeof(LevelTrajectoryCanvasExplorer));
            AddRunExplorerType(typeof(LevelTrajectoryGeoExplorer));

            AddAlgorithmType(typeof(ImaiIriHierarchical));
            AddAlgorithmType(typeof(ImaiIriGreedy));
            AddAlgorithmType(typeof(ImaiIriNaive));
        }

        public override MSInput ImportInput(string fileName, out bool customName)
        {
            customName = true;

            var dialog = new MoveBankTrajectoryChooser(fileName);
            var result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                return new MSInput(dialog.ChosenTrajectory)
                {
                    Name = Path.GetFileNameWithoutExtension(fileName) + "_" + dialog.ChosenTrajectoryName
                };
            }

            return null;
        }

    }
}
