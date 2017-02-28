using System.Collections.Generic;
using MultiScaleTrajectories.Algorithm.Geometry;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Algorithm.SingleTrajectory
{

    class STInput : Input
    {
        public Trajectory2D Trajectory;
        public List<double> Epsilons;

        public int NumLevels => Epsilons.Count;

        public STInput()
        {
            LoadFresh();
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

        public void Load(Trajectory2D trajectory, List<double> epsilons)
        {
            Trajectory = trajectory;
            Epsilons = epsilons;
        }

        public void LoadFresh()
        {
            List<double> epsilons = new List<double>();
            epsilons.Add(double.PositiveInfinity);
            Load(new Trajectory2D(), epsilons);
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static STInput DeSerialize(string serializedInput)
        {
            return JsonConvert.DeserializeObject<STInput>(serializedInput);
        }

    }
}
