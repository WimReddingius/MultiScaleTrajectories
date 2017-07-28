using MultiScaleTrajectories.Simplification.ShortcutPathFinding.Algorithm;
using MultiScaleTrajectories.Simplification.ShortcutPathFinding.Algorithm.Graph;
using MultiScaleTrajectories.Simplification.ShortcutPathFinding.Algorithm.Intervals;
using MultiScaleTrajectories.Simplification.ShortcutPathFinding.View.Edit;
using MultiScaleTrajectories.Trajectory.Single;
using MultiScaleTrajectories.Trajectory.Single.View;

namespace MultiScaleTrajectories.Simplification.ShortcutPathFinding
{
    class SPFController : SingleTrajectoryAlgorithmController<SPFInput, SPFOutput>
    {
        public SPFController() : base("Shortcut Path Finding")
        {
            AddAlgorithm(() => new IntervalsBFS());
            AddAlgorithm(() => new IntervalsRangeQueries());
            AddAlgorithm(() => new ShortcutGraphBFS());
            AddAlgorithm(() => new ShortcutGraphDijkstra());

            AddSimpleInputEditor(new SPFInputEditor(new SingleTrajectoryEditorCanvas()));
            AddSimpleInputEditor(new SPFInputEditor(new SingleTrajectoryEditorGeo()));
        }
    }
}
