using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiScaleTrajectories.algorithm.SingleTrajectory
{
    interface STAlgorithm
    {

        STSolution Solve(Trajectory2D trajectory, List<double> epsilons);

        string ToString();

    }
}
