using MultiScaleTrajectories.Algorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiScaleTrajectories.Algorithm
{
    class AlgorithmRunner<IN, OUT>
    {

        public OUT Output;
        public IN Input;
        public Algorithm<IN, OUT> Algorithm;

        public AlgorithmRunner(IN input, OUT output)
        {
            Input = input;
            Output = output;
        }

        void Run()
        {
            Output = Algorithm.Compute(Input);
        }

    }
}
