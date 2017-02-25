using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiScaleTrajectories.Algorithm
{
    interface Algorithm<IN, OUT>
    {
        OUT Compute(IN input);

        string ToString();

    }
}
