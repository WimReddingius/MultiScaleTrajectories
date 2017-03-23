using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using AlgorithmVisualization.Algorithm;
using MultiScaleTrajectories.Algorithm.Geometry;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.SingleTrajectory.Algorithm
{
    class STOutput : Output
    {
        [JsonProperty]
        private readonly Dictionary<int, Trajectory2D> Levels;

        public int NumLevels => Levels.Count;

        public STOutput()
        {
            Levels = new Dictionary<int, Trajectory2D>();
        }

        protected override void InitStatistics()
        {
            Statistics.Put("Points", () =>
            {
                if (Levels.Count == 0)
                    return 0;

                return Levels.Select(l => l.Value.Count).Aggregate((t1, t2) => t1 + t2);
            });
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
