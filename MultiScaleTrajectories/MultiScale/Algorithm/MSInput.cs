using System.Collections.Generic;
using AlgorithmVisualization.Algorithm;
using MultiScaleTrajectories.Algorithm.Geometry;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.MultiScale.Algorithm
{
    sealed class MSInput : Input
    {
        public int NumLevels => Epsilons.Count;
        public Trajectory2D Trajectory;

        [JsonProperty]
        private List<double> Epsilons;
        

        [JsonConstructor]
        public MSInput(Trajectory2D Trajectory, List<double> Epsilons)
        {
            Load(Trajectory, Epsilons);
        }

        public MSInput(Trajectory2D trajectory)
        {
            Clear();
            Trajectory = trajectory;
        }

        public MSInput()
        {
            Clear();
        }

        protected override void InitStatistics()
        {
            Statistics.Put("Levels", () => Epsilons.Count);
            Statistics.Put("Points", () => Trajectory.Count);
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
            Load(new Trajectory2D(), new List<double> { 0.0 });
        }

        private void Load(Trajectory2D trajectory, List<double> epsilons)
        {
            Trajectory = trajectory;
            Epsilons = epsilons;
        }

    }
}
