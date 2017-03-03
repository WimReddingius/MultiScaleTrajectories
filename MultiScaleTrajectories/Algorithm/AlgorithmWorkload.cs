using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MultiScaleTrajectories.Algorithm
{
    class AlgorithmWorkload<TIn, TOut> where TIn : Input, new() where TOut : Output, new()
    {

        public BindingList<AlgorithmRun<TIn, TOut>> Runs;
        public bool HasStarted;
        

        public AlgorithmWorkload()
        {
            Runs = new BindingList<AlgorithmRun<TIn, TOut>>();
        }

        public AlgorithmRun<TIn, TOut> CreateRun(Algorithm<TIn, TOut> algorithm, TIn input)
        {
            var run = new AlgorithmRun<TIn, TOut>(algorithm, input);
            Runs.Add(run);
            return run;
        }

        public void Run()
        {
            foreach (AlgorithmRun<TIn, TOut> run in Runs)
            {
                run.Run();
            }
            HasStarted = true;
        }

        public void Reset()
        {
            foreach (AlgorithmRun<TIn, TOut> run in Runs)
            {
                run.Reset();
            }
            HasStarted = false;
        }

    }
}
