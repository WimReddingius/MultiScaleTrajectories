using System;
using System.Linq;
using AlgorithmVisualization.Algorithm;
using MultiScaleTrajectories.Algorithm;
using MultiScaleTrajectories.Algorithm.ImaiIri;

namespace MultiScaleTrajectories.SingleTrajectory.Algorithm.NaiveImaiIri
{
    class NaiveImaiIri : Algorithm<STInput, STOutput>
    {
        public override string Name => "Imai Iri - Naive";

        public override void Compute(STInput input, STOutput output)
        {
            var trajectory = input.Trajectory;

            //n^3 (inefficient)
            var shortcuts = ShortcutFinder.FindAllShortcuts(trajectory);

            //k * (n^2 + n^2)
            for (var level = 1; level <= input.NumLevels; level++)
            {
                var epsilon = input.GetEpsilon(level);
                var shortcutGraph = new ShortcutGraph(trajectory);

                //n^2 shortcut selection
                shortcuts
                    .FindAll(s => s.MaxDistance <= epsilon)
                    .ForEach(s => shortcutGraph.AddShortcut(s));

                var shortestPath = shortcutGraph.GetShortestPath(shortcutGraph.FirstNode, shortcutGraph.LastNode);
                output.SetTrajectoryAtLevel(level, shortcutGraph.GetTrajectory(shortestPath));
            }
        }
        
    }
}
