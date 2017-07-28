using System.Collections.Generic;
using System.Linq;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using MultiScaleTrajectories.Simplification.MultiScale.Algorithm.ImaiIri.ShortcutProvision;
using MultiScaleTrajectories.Simplification.MultiScale.View.Algorithm;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.MultiScale.Algorithm.ImaiIri.Hierarchical
{
    class HierarchicalImaiIriBUCao : ImaiIriAlgorithm
    {

        [JsonConstructor]
        public HierarchicalImaiIriBUCao(ShortcutOptions shortcutOptions = null) : base("H - Imai Iri Bottom Up - Cao", shortcutOptions)
        {
        }

        public override void Compute(MSInput input, out MSOutput output)
        {
            output = new MSOutput();
            var trajectory = input.Trajectory;
            ShortcutProvider.Init(input, output, false);

            var onDemandShortcutProvider = (ShortcutsOnDemand) ShortcutProvider;
            var prevTrajectory = trajectory;
            for (var level = 1; level <= input.NumLevels; level++)
            {
                var epsilon = input.GetEpsilon(level);

                var shortcutAlgo = onDemandShortcutProvider.Algorithm;
                var shortcutInput = new MSSInput(prevTrajectory, new List<double> {epsilon});
                MSSOutput shortcutOutput;

                shortcutAlgo.Compute(shortcutInput, out shortcutOutput);
                var levelShortcuts = shortcutOutput.GetShortcuts(1);

                output.LogObject("Number of shortcuts found on level " + level, levelShortcuts.Count);

                var levelShortestPath = ShortestPathProvider.FindShortestPath(levelShortcuts, prevTrajectory.First(), prevTrajectory.Last()).Points;
                levelShortestPath.AddFirst(prevTrajectory.First());

                //O(n)
                var levelTrajectory = new Trajectory2D(levelShortestPath);

                //report level trajectory
                output.SetTrajectoryAtLevel(level, levelTrajectory);

                prevTrajectory = levelTrajectory;
            }
        }
        
    }
}
