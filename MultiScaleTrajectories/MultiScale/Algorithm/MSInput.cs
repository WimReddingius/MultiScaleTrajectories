using System.Collections.Generic;
using MultiScaleTrajectories.Algorithm.Geometry;
using MultiScaleTrajectories.Trajectory.Single;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.MultiScale.Algorithm
{
    sealed class MSInput : SingleTrajectoryInput
    {
        public int NumLevels => Epsilons.Count;

        [JsonProperty]
        private List<double> Epsilons;
        

        [JsonConstructor]
        public MSInput(Trajectory2D Trajectory, List<double> Epsilons) : base(Trajectory)
        {
            this.Epsilons = Epsilons;
        }

        public MSInput()
        {
            Clear();
        }

        protected override void RegisterStatistics()
        {
            base.RegisterStatistics();
            Statistics.Put("Levels", () => Epsilons.Count);
        }

        public void AppendLevel(double epsilon)
        {
            Epsilons.Add(epsilon);
        }

        public void RemoveLevel(int level)
        {
            Epsilons.RemoveAt(level - 1);
        }

        public double GetEpsilon(int level)
        {
            return Epsilons[level - 1];
        }

        public void SetEpsilon(int level, double epsilon)
        {
            Epsilons[level - 1] = epsilon;
        }

        public void InsertLevel(int level, double epsilon)
        {
            Epsilons.Insert(level - 1, epsilon);
        }

        public override void Clear()
        {
            base.Clear();
            ClearEpsilons();
        }

        private void ClearEpsilons()
        {
            Epsilons = new List<double> { 0.0 };
        }

    }
}
