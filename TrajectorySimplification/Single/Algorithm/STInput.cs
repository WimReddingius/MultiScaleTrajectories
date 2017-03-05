using System.Collections.Generic;
using AlgorithmVisualization.Algorithm;
using Newtonsoft.Json;
using TrajectorySimplification.Algorithm.Geometry;

namespace TrajectorySimplification.Single.Algorithm
{

    class STInput : Input
    {
        public Trajectory2D Trajectory;
        public List<double> Epsilons;

        public int NumLevels => Epsilons.Count;

        public STInput()
        {
            Clear();
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

        public override string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public override void Clear()
        {
            Trajectory = new Trajectory2D();
            Epsilons = new List<double> { double.PositiveInfinity };
        }

        public override void LoadSerialized(string serializedInput)
        {
            STInput input = JsonConvert.DeserializeObject<STInput>(serializedInput);
            Trajectory = input.Trajectory;
            Epsilons = input.Epsilons;
        }

    }
}
