using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiScaleTrajectories.algorithm.ST
{
    class STShortcutShortestPath : STAlgorithm
    {
        public STSolution Solve(Trajectory2D trajectory, List<double> epsilons)
        {
            STSolution solution = new STSolution();
            solution.setTrajectoryAtLevel(1, trajectory);
            solution.setTrajectoryAtLevel(2, new Trajectory2D());
            solution.setTrajectoryAtLevel(3, new Trajectory2D());
            return solution;
        }

        public override string ToString()
        {
            return "Shortcut Shortest Path";
        }
    }
}
