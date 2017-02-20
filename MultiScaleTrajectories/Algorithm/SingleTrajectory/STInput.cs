using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiScaleTrajectories.Algorithm.SingleTrajectory
{
    class STInput
    {
        public Trajectory2D Trajectory;
        public List<double> Epsilons;

        public int NumLevels { get { return Epsilons.Count;  } }

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

        internal void SetEpsilon(int level, double epsilon)
        {
            Epsilons[level - 1] = epsilon;
        }

        internal void InsertLevel(int level, double epsilon)
        {
            Epsilons.Insert(level - 1, epsilon);
        }
    }
}
