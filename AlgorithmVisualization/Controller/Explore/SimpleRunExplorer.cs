using System;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Run;

namespace AlgorithmVisualization.Controller.Explore
{
    public class SimpleRunExplorer<TIn, TOut> : RunExplorer<TIn, TOut> where TIn : Input, new() where TOut : Output
    {
        private readonly IRunExplorer<TIn, TOut> explorer;


        public SimpleRunExplorer(IRunExplorer<TIn, TOut> explorer)
        {
            if (!(explorer is Control))
                throw new ArgumentOutOfRangeException(nameof(explorer), "Explorer does not inherit from Control");

            this.explorer = explorer;
            MinConsolidation = explorer.MinConsolidation;
            MaxConsolidation = explorer.MaxConsolidation;
            Priority = explorer.Priority;

            WrapControl((Control) explorer);
            Name = explorer.Name;
        }

        public override void Visualize(params AlgorithmRun<TIn, TOut>[] runs)
        {
            explorer.Visualize(runs);
        }

        public override void Destroy()
        {
            base.Destroy();
            explorer.Destroy();
        }

    }

}
