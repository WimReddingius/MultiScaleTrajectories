using System.Collections.Generic;
using System.Linq;
using AlgorithmVisualization.Algorithm;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.MultiScale.Algorithm
{
    class MSOutput : Output
    {
        [JsonProperty]
        private readonly Dictionary<int, Trajectory2D> Levels;

        public int NumLevels => Levels.Count;

        public MSOutput()
        {
            Levels = new Dictionary<int, Trajectory2D>();
        }

        protected override void RegisterStatistics()
        {
            base.RegisterStatistics();
            Statistics.Put("Points", () => Levels.Select(l => l.Value.Count).Sum());
        }

        public void SetTrajectoryAtLevel(int level, Trajectory2D trajectory)
        {
            Levels.Add(level, trajectory);
            Statistics.Put("Points @ level " + level, trajectory.Count);
        }

        public Trajectory2D GetTrajectoryAtLevel(int i)
        {
            return Levels[i];
        }

    }
}
