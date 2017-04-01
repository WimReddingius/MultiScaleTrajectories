using System.Windows.Forms;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Algorithm.Run;
using AlgorithmVisualization.Util.Factory;

namespace AlgorithmVisualization.Controller.Explore
{
    public class RunExplorerWrapper<TIn, TOut, TExplore> : RunExplorer<TIn, TOut>
        where TIn : Input, new() 
        where TOut : Output, new()
        where TExplore : Control, IRunExplorer<TIn, TOut>
    {
        private readonly TExplore explorer;

        public RunExplorerWrapper() : this(new object[] {})
        {
        }

        public RunExplorerWrapper(params object[] args) : this(new Factory<TExplore>().Create(args))
        {
        }

        public RunExplorerWrapper(TExplore explorer)
        {
            this.explorer = explorer;
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
