using System.Collections.Generic;
using System.IO;
using AlgorithmVisualization.Algorithm;
using MultiScaleTrajectories.Algorithm.Geometry;
using MultiScaleTrajectories.Util;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.SingleTrajectory.Algorithm
{

    class STInput : Input
    {
        public Trajectory2D Trajectory;
        public List<double> Epsilons;

        public int NumLevels => Epsilons.Count;

        public STInput()
        {
            Trajectory = new Trajectory2D();
            Epsilons = new List<double>();
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
            Load(new Trajectory2D(), new List<double> { double.PositiveInfinity });
        }

        public override string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public override void LoadSerialized(string fileName)
        {
            if (MoveBank.IsMoveBankFile(fileName))
            {
                Clear();
                Trajectory = MoveBank.ReadSingleTrajectory(fileName);
            }
            else
            {
                string serializedInput = File.ReadAllText(fileName);
                STInput input = JsonConvert.DeserializeObject<STInput>(serializedInput);
                Load(input.Trajectory, input.Epsilons);
            }
        }

        private void Load(Trajectory2D trajectory, List<double> epsilons)
        {
            Trajectory = trajectory;
            Epsilons = epsilons;
            Statistics["Levels"] = () => Epsilons.Count;
            Statistics["Points"] = () => Trajectory.Count;
        }

    }
}
