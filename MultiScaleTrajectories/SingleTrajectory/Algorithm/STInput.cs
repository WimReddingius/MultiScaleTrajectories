using System.Collections.Generic;
using AlgorithmVisualization.Algorithm;
using MultiScaleTrajectories.Algorithm.Geometry;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.SingleTrajectory.Algorithm
{

    class STInput : Input
    {
        public int NumLevels => Epsilons.Count;
        public Trajectory2D Trajectory;

        [JsonProperty]
        private List<double> Epsilons;
        

        [JsonConstructor]
        public STInput(Trajectory2D Trajectory, List<double> Epsilons, string DisplayName) : base(DisplayName)
        {
            Load(Trajectory, Epsilons);
        }

        public STInput(Trajectory2D trajectory)
        {
            Clear();
            Trajectory = trajectory;
        }

        public STInput()
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

        public sealed override void Clear()
        {
            Load(new Trajectory2D(), new List<double> { 0.0 }); //double.PositiveInfinity });
        }
        private void Load(Trajectory2D trajectory, List<double> epsilons)
        {
            Trajectory = trajectory;
            Epsilons = epsilons;
        }

    }
}
