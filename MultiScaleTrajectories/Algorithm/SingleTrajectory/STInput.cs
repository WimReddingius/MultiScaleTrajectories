using System.Collections.Generic;

namespace MultiScaleTrajectories.Algorithm.SingleTrajectory
{

    public delegate void InputLoadedEventHandler();

    class STInput
    {
        public Trajectory2D Trajectory;
        public List<double> Epsilons;

        public event InputLoadedEventHandler Loaded;

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

        public void Load(Trajectory2D trajectory, List<double> epsilons)
        {
            Trajectory = trajectory;
            Epsilons = epsilons;
            Loaded?.Invoke();
        }

        public void Clear()
        {
            Load(new Trajectory2D(), new List<double>());
        }
    }
}
