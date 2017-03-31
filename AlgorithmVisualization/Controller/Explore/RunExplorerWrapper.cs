using System.Windows.Forms;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Run;

namespace AlgorithmVisualization.Controller.Explore
{
    public class RunExplorerWrapper<TIn, TOut, TExplore> : RunExplorer<TIn, TOut>
        where TIn : Input, new() where TOut : Output, new()
        where TExplore : Control, IRunExplorer<TIn, TOut>, new()
    {
        private readonly TExplore explorer;

        public RunExplorerWrapper()
        {
            explorer = new TExplore();
            MinConsolidation = explorer.MinConsolidation;
            MaxConsolidation = explorer.MaxConsolidation;
            Priority = explorer.Priority;

            WrapControl(explorer);
            Name = explorer.Name;
        }

        public override void Visualize(params AlgorithmRun<TIn, TOut>[] runs)
        {
            explorer.Visualize(runs);
        }

        public override void Destroy()
        {
            explorer.Destroy();
        }

    }

}
