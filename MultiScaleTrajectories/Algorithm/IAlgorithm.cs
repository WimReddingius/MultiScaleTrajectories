using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiScaleTrajectories.Algorithm
{
    interface IAlgorithm<Input, Output>
    {
        Output Compute(Input input);

        string ToString();

    }
}
