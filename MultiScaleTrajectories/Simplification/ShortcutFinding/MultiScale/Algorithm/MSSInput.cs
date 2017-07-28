using System.Collections.Generic;
using System.Linq;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using MultiScaleTrajectories.Simplification.MultiScale.Algorithm;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm
{
    class MSSInput : MSInput
    {
        public bool Cumulative;
        public HashSet<TPoint2D> PrunedPoints;
        public LinkedList<TPoint2D> SearchIntervals;

        [JsonConstructor]
        public MSSInput(Trajectory2D Trajectory, List<double> Epsilons) : base(Trajectory, Epsilons)
        {
            this.Epsilons = Epsilons;

            PrunedPoints = new HashSet<TPoint2D>();
            SearchIntervals = null;
            Cumulative = false;
        }

        public MSSInput()
        {
        }    

        protected override void RegisterStatistics()
        {
            base.RegisterStatistics();
            Statistics.Put("Cumulative", () => Cumulative);
        }

        public override void Clear()
        {
            base.Clear();
            Cumulative = false;
            PrunedPoints = new HashSet<TPoint2D>();
        }
    }
}
